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
    internal class WorkshopHelper_EndLoadConfigData_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WorkshopHelper), "EndLoadConfigData")]
        static void WorkshopHelper_EndLoadConfigData_Postfix()
        {
            try
            {
                XluaCallback.RunCallback(PluginConst.PluginXluaCallbackConfig.OnResManagerInstall, new object[] {  ResManager.Instance });
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
        [HarmonyPatch(typeof(WorkshopHelper), "EndLoadConfigData")]
        static void WorkshopHelper_EndLoadConfigData_Prefix()
        {
            XluaModComponent.CreateXluaModComponent();
        }
    }
}
