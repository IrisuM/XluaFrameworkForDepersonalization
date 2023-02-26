using Mono.Cecil.Cil;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil.Rocks;
using System.Reflection;
using System.IO;

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
            
        }

        public static IEnumerable<string> GetDLLs()
        {
            yield return "Assembly-CSharp.dll";
        }
    }
}
