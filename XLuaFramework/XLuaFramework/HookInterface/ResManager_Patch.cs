using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace XLuaFramework.HookInterface
{
    internal class ResManager_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ResManager), "Install")]
        static void ResManager_Install_Postfix(ResManager __instance, Task __result)
        {
            try
            {
                XluaCallback.RunCallback(PluginConst.PluginXluaCallbackConfig.OnResManagerInstall, new object[] { __instance });
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

        //这里用来加载Xlua管理组件
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ResManager), "Install")]
        static void ResManager_Install_Prefix()
        {
            XluaModComponent.CreateXluaModComponent();
        }
    }
}
