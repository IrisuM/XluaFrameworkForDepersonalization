using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx.Workshop;
using static XLuaFramework.PluginConst;
using System.IO;

namespace XLuaFramework
{
    internal static class XluaHelper
    {
        public static string GetLocalDir()
        {
            return Path.Combine(Application.streamingAssetsPath, PluginPathConfig.FrameworkPath);
        }

        public static string[] GetModDir() 
        {
            List<string> mods = new List<string>();
            foreach(ModInfo info in WorkshopLoader.Inst.ModMag.ModList)
            {
                mods.Add(Path.Combine(info.ModDir, PluginPathConfig.FrameworkPath));
            }
            return mods.ToArray();
        }
    }
}
