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
using XLuaFramework.LuaTool;
using static XLuaFramework.PluginConst;

namespace XLuaFramework
{
    [BepInPlugin(PluginConfig.PLUGIN_GUID, PluginConfig.PLUGIN_NAME, PluginConfig.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource Log;
        public static Plugin s_Instance;
        public static Harmony harmony;

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

            harmony = new Harmony(PluginConfig.PLUGIN_GUID);
            harmony.PatchAll(typeof(HookInterface.UnityEngine_Resources_Patch));
            harmony.PatchAll(typeof(HookInterface.ResManager_Patch));
        }
    }
}