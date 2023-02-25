using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace XLuaFramework.HookInterface
{
    internal class UnityEngine_Resources_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Resources), "Load", new Type[] { typeof(string), typeof(Type) })]
        static void UnityEngine_Resources_Load_Postfix(ref Object __result, string path, Type systemTypeInstance)
        {
            try
            {
                XluaCallback.RunCallback(PluginConst.PluginXluaCallbackConfig.OnResourceLoad, new object[] { __result, path, systemTypeInstance.Name });
            }
            catch (Exception e)
            {
                Plugin.Log.LogError(e.Message);
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    Plugin.Log.LogError(e.Message);
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Resources), "Load", new Type[] { typeof(string), typeof(Type) })]
        static bool UnityEngine_Resources_Load_Prefix(ref Object __result, string path)
        {
            Object result = LuaTool.ResourcesTool.GetPoolObject(path);
            if (result != null)
            {
                __result = result;
                return false;
            }
            return true;
        }
    }
}
