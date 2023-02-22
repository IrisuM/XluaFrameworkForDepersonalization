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
            foreach (var mod in XluaModManager.lua_envs)
            {
                if (mod.Value.callback.OnResourceLoad != null)
                {
                    try
                    {
                        __result = mod.Value.callback.OnResourceLoad.Call(__result, path, systemTypeInstance.Name).First() as Object;
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
            }

        }
    }
}
