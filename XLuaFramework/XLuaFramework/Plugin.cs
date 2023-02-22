using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using UnityEngine;
using XLua;
using static XLuaFramework.PluginConst;

namespace XLuaFramework
{
    [BepInPlugin(PluginConfig.PLUGIN_GUID, PluginConfig.PLUGIN_NAME, PluginConfig.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        internal static Plugin s_Instance;

        private ConfigEntry<bool> is_merge_luaenv_config;
        private ConfigEntry<bool> is_enable_console_config;
        public bool is_merge_luaenv { get { return is_merge_luaenv_config.Value; } }
        public bool is_enable_console { get { return is_enable_console_config.Value; } }
        private void Awake()
        {
            // Plugin startup logic
            Log = Logger;
            s_Instance = this;
            Logger.LogInfo($"Plugin {PluginConfig.PLUGIN_GUID} is loaded!");
            Logger.LogInfo(PluginConfig.WELCOME_WORLD);

            is_merge_luaenv_config = Config.Bind(PluginConfig.CONFIG_SECTION, PluginConfig.CONFIG_IS_MERGE_LUAENV, false, new ConfigDescription(PluginConfig.CONFIG_IS_MERGE_LUAENV_DESCRIPTION));
            is_enable_console_config = Config.Bind(PluginConfig.CONFIG_SECTION, PluginConfig.CONFIG_IS_ENABLE_CONSOLE, false, new ConfigDescription(PluginConfig.CONFIG_IS_ENABLE_CONSOLE_DESCRIPTION));

            Harmony harmony = new Harmony(PluginConfig.PLUGIN_GUID);
            harmony.PatchAll(typeof(HookInterface.RoleModel_Patch));

            Log.LogMessage("开始加载XluaMod");
            //逐个加载lua mod
            foreach (string path in XluaHelper.GetModDir())
            {
                Log.LogMessage("加载:" + path);
                try
                {
                    XluaModManager.CreateXluaMod(path, path);
                }
                catch (Exception e)
                {
                    Log.LogError(e.Message);
                    while (e.InnerException != null)
                    {
                        e = e.InnerException;
                        Log.LogError(e.Message);
                    }
                }
            }
        }
    }
}