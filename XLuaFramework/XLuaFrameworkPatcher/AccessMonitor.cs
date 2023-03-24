using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using ParameterAttributes = Mono.Cecil.ParameterAttributes;

namespace XLuaFrameworkPatcher
{
    public class AccessMonitor
    {
        private readonly AssemblyDefinition _assembly;
        private readonly TypeDefinition targetType;
        private readonly Action<string> _callback;

        public static MethodDefinition GetMethodDefinitionFromMethodInfo(MethodInfo methodInfo, AssemblyDefinition assemblyDefinition)
        {
            foreach (var typeDefinition in assemblyDefinition.MainModule.Types)
            {
                if (typeDefinition.FullName == methodInfo.DeclaringType.FullName)
                {
                    foreach (var methodDefinition in typeDefinition.Methods)
                    {
                        if (methodDefinition.Name == methodInfo.Name &&
                            methodDefinition.Parameters.Count == methodInfo.GetParameters().Length &&
                            methodDefinition.IsStatic == methodInfo.IsStatic)
                        {
                            return methodDefinition;
                        }
                    }
                }
            }

            return null;
        }
        public AccessMonitor(AssemblyDefinition assembly,string classDefinition, Action<string> callback)
        {
            _assembly = assembly;
            targetType = _assembly.MainModule.GetType(classDefinition);
            _callback = callback;
        }

        public void Monitor()
        {
            if (targetType == null)
            {
                _callback("未找到目标类型");
                return;
            }

            MethodReference monitorMethod = _assembly.MainModule.ImportReference(typeof(AccessMonitor).GetMethod("PrintInfo"));

            foreach (var method in targetType.Methods)
            {
                if (method.IsConstructor || method.IsAbstract || method.IsGetter || method.IsSetter)
                {
                    continue;
                }

                MonitorMethod(method, monitorMethod);
            }

            foreach (var property in targetType.Properties)
            {
                if (property.GetMethod != null)
                {
                    MonitorMethod(property.GetMethod, monitorMethod);
                }

                if (property.SetMethod != null)
                {
                    MonitorMethod(property.SetMethod, monitorMethod);
                }
            }

            foreach (var field in targetType.Fields)
            {
                MonitorField(field, monitorMethod);
            }
        }
        
        public static void PrintInfo(string memberinfo)
        {
            Console.WriteLine(GetCallerInfo(new StackTrace().GetFrame(1).GetMethod()) + " 被监控对象: " + memberinfo);
        }

        public static string GetCallerInfo(MethodBase method)
        {
            var sb = new StringBuilder();
            sb.Append("CS.");
            if (method.DeclaringType.Namespace != ""&& method.DeclaringType.Namespace !=null)
            {
                sb.Append(method.DeclaringType.Namespace);
                sb.Append(".");
            }
            sb.Append(method.DeclaringType.Name);
            sb.Append(".");
            sb.Append(method.Name);

            var parameters = method.GetParameters();
            sb.Append(" {");

            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.Append("CS.");
                if (parameters[i].ParameterType.Namespace != "")
                {
                    sb.Append(method.DeclaringType.Namespace);
                    sb.Append(".");
                }
                sb.Append(parameters[i].ParameterType.Name);
            }
            sb.Append("}");

            return sb.ToString();
        }

        public static void ReplaceShortBranchInstructions(MethodDefinition method)
        {
            var ilProcessor = method.Body.GetILProcessor();

            for (int i = 0; i < method.Body.Instructions.Count; i++)
            {
                var instruction = method.Body.Instructions[i];

                OpCode longOpCode = GetLongBranchOpCode(instruction.OpCode);

                if (longOpCode != OpCodes.Nop)
                {
                    Instruction newInstruction = ilProcessor.Create(longOpCode, (Instruction)instruction.Operand);
                    ilProcessor.Replace(instruction, newInstruction);
                }
            }
        }

        public static OpCode GetLongBranchOpCode(OpCode shortOpCode)
        {
            switch (shortOpCode.Code)
            {
                case Code.Br_S:
                    return OpCodes.Br;
                case Code.Brfalse_S:
                    return OpCodes.Brfalse;
                case Code.Brtrue_S:
                    return OpCodes.Brtrue;
                case Code.Beq_S:
                    return OpCodes.Beq;
                case Code.Bge_S:
                    return OpCodes.Bge;
                case Code.Bge_Un_S:
                    return OpCodes.Bge_Un;
                case Code.Bgt_S:
                    return OpCodes.Bgt;
                case Code.Bgt_Un_S:
                    return OpCodes.Bgt_Un;
                case Code.Ble_S:
                    return OpCodes.Ble;
                case Code.Ble_Un_S:
                    return OpCodes.Ble_Un;
                case Code.Blt_S:
                    return OpCodes.Blt;
                case Code.Blt_Un_S:
                    return OpCodes.Blt_Un;
                case Code.Bne_Un_S:
                    return OpCodes.Bne_Un;
                default:
                    return OpCodes.Nop;
            }
        }

        private void MonitorMethod(MethodDefinition method, MethodReference monitorMethod)
        {
            // 排除对 MonitorAccess 方法本身的监控
            if (method == monitorMethod)
            {
                return;
            }

            var ilProcessor = method.Body.GetILProcessor();
            var instr = ilProcessor.Create(OpCodes.Nop);

            var methodRef = _assembly.MainModule.ImportReference(monitorMethod);
            ilProcessor.InsertBefore(method.Body.Instructions.First(), instr);
            ilProcessor.InsertBefore(instr, ilProcessor.Create(OpCodes.Ldstr, method.FullName));
            ilProcessor.InsertBefore(instr, ilProcessor.Create(OpCodes.Call, methodRef));

            ReplaceShortBranchInstructions(method);
        }

        private void MonitorField(FieldDefinition field, MethodReference monitorMethod)
        {
            foreach (var targetType in _assembly.MainModule.GetTypes())
            {
                foreach (var method in targetType.Methods)
                {
                    if (method.IsConstructor || method.IsAbstract || method.IsGetter || method.IsSetter ||!method.HasBody)
                    {
                        continue;
                    }
                    bool has_changed = false;
                    var ilProcessor = method.Body.GetILProcessor();
                    var fieldRef = _assembly.MainModule.ImportReference(field);
                    for (Instruction instruction = ilProcessor.Body.Instructions.First(); instruction != ilProcessor.Body.Instructions.Last(); instruction=instruction.Next)
                    {
                        if ((instruction.OpCode == OpCodes.Ldfld || instruction.OpCode == OpCodes.Stfld) && instruction.Operand == fieldRef)
                        {
                            has_changed = true;
                            var instr = ilProcessor.Create(OpCodes.Nop);
                            ilProcessor.InsertBefore(instruction, instr);
                            ilProcessor.InsertBefore(instr, ilProcessor.Create(OpCodes.Ldstr, field.FullName));
                            ilProcessor.InsertBefore(instr, ilProcessor.Create(OpCodes.Call, _assembly.MainModule.ImportReference(monitorMethod)));
                        }
                    }
                    if (has_changed)
                    {
                        ReplaceShortBranchInstructions(method);
                    }
                    
                }
            }
        }
    }
}