using Mono.Cecil.Cil;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil.Rocks;

namespace XLuaFrameworkPatcher
{
    public static class Patcher
    {        // List of assemblies to patch
        public static IEnumerable<string> TargetDLLs => GetDLLs();

        // Patches the assemblies
        //定制化修改，随便写写
        //把加载资源全部改为缓存
        public static void Patch(ref AssemblyDefinition assembly)
        {
            switch (assembly.Name.Name)
            {
                case "Assembly-CSharp":
                    {
                        foreach (TypeDefinition type in assembly.MainModule.Types)
                        {
                            if (type.Name == "GamePoolManager")
                            {
                                foreach (MethodDefinition method in type.GetMethods())
                                {
                                    if (method.Name == "Alloc")
                                    {
                                        for (int i = 0; i + 2 < method.Body.Instructions.Count; i++)
                                        {
                                            if (method.Body.Instructions[i].OpCode == OpCodes.Ldloca_S &&
                                                method.Body.Instructions[i + 1].OpCode == OpCodes.Ldarg_3 &&
                                                method.Body.Instructions[i + 2].OpCode == OpCodes.Stfld && 
                                                (method.Body.Instructions[i + 2].Operand as FieldReference).Name == "isUseRes")
                                            {
                                                method.Body.Instructions[i + 1].OpCode = OpCodes.Ldc_I4_0;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public static IEnumerable<string> GetDLLs()
        {
            yield return "Assembly-CSharp.dll";
        }
    }
}
