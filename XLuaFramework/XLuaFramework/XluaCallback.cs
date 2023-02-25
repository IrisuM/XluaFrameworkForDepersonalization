using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx.Workshop;
using static XLuaFramework.PluginConst;
using System.IO;
using System.Collections;
using Object = UnityEngine.Object;
using XLua;

namespace XLuaFramework
{
    internal static class XluaCallback
    {
        class LuaCallback
        {
            public LuaCallbackDelegate func;
            public string description = "";
        }
        public delegate object[] LuaCallbackDelegate(params object[] args);
        static Dictionary<string, LuaCallback> callbacks = new Dictionary<string, LuaCallback>();
        public static void RegCallback(string key, string description, LuaFunction luaFunction)
        {
            if (!callbacks.ContainsKey(key))
            {
                callbacks[key] = new LuaCallback() { func = (args) => { Plugin.Log.LogMessage(key); return null; } };
            }
            callbacks[key].description += description + "\n\n";
            callbacks[key].func += luaFunction.Call;
            Plugin.Log.LogMessage(string.Format("reg {0} {1} {2}", key, description, luaFunction.ToString()));
        }
        public static void RunCallback(string key, object[] args)
        {
            LuaCallback callback = null;
            if (callbacks.TryGetValue(key, out callback) && callback != null)
            {
                callback.func(args);
                Plugin.Log.LogMessage(callback.description);
            }
        }
    }
}
