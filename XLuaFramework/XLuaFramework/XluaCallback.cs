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
using HarmonyLib;
using System.Reflection;
using System.Runtime.CompilerServices;

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
        static Dictionary<string, List<string>> reg_events = new Dictionary<string, List<string>>();
        public static void RegCallback(string key, string description, LuaFunction luaFunction)
        {
            if (!callbacks.ContainsKey(key))
            {
                callbacks[key] = new LuaCallback() { func = luaFunction.Call, description = description };
            }
            else
            {
                callbacks[key].description += description + "\n\n";
                callbacks[key].func += luaFunction.Call;
            }
            Plugin.Log.LogMessage(string.Format("reg {0} {1} {2}", key, description, luaFunction.ToString()));
        }
        public static void RunCallback(string key, object[] args)
        {
            LuaCallback callback = null;
            if (callbacks.TryGetValue(key, out callback) && callback != null)
            {
                callback.func(args);
                //Plugin.Log.LogMessage(callback.description);
            }
        }
        public static bool RegEvent(MethodInfo methodInfo, string event_name, bool is_before)
        {
            if (methodInfo == null)
            {
                return false;
            }
            if (callbacks.ContainsKey(event_name))
            {
                Plugin.Log.LogError("事件名称重复");
                return false;
            }
            if (!reg_events.ContainsKey(methodInfo.FullDescription()))
            {
                reg_events[methodInfo.FullDescription()] = new List<string>();
            }
            reg_events[methodInfo.FullDescription()].Add(event_name);
            Plugin.Log.LogMessage(string.Format("注册事件:{0} {1} {2}", methodInfo.FullDescription(), event_name, is_before));
            if (is_before)
            {
                Plugin.harmony.Patch(methodInfo, prefix: new HarmonyMethod(typeof(XluaCallback).GetMethod("Xlua_Callback_Prefix")));
            }
            else
            {
                Plugin.harmony.Patch(methodInfo, postfix: new HarmonyMethod(typeof(XluaCallback).GetMethod("Xlua_Callback_Postfix")));
            }
            return true;
        }
        public static bool RegEvent(string type_name, string method_name, string event_name, bool is_before)
        {
            Type type = Type.GetType(type_name);
            if (type == null)
            {
                Plugin.Log.LogError("未发现类型:" + type_name);
                return false;
            }
            MethodInfo methodInfo = AccessTools.Method(type, method_name);
            if (methodInfo == null)
            {
                Plugin.Log.LogError("未发现方法:" + method_name);
                return false;
            }
            return RegEvent(methodInfo, event_name, is_before);
        }
        public static bool RegEvent(string type_name, string method_name, string[] args_type, string event_name, bool is_before)
        {
            Type type = Type.GetType(type_name);
            if (type == null)
            {
                Plugin.Log.LogError("未发现类型:" + type_name);
                return false;
            }
            List<Type> args_type_list = new List<Type>();
            foreach (string arg_type_name in args_type)
            {
                Type arg_type = Type.GetType(arg_type_name);
                if (arg_type == null)
                {
                    Plugin.Log.LogError("未发现类型:" + arg_type_name);
                    return false;
                }
                args_type_list.Add(arg_type);
            }
            MethodInfo methodInfo = AccessTools.Method(type, method_name, args_type_list.ToArray());
            if (methodInfo == null)
            {
                Plugin.Log.LogError("未发现方法:" + method_name);
                return false;
            }
            return RegEvent(methodInfo, event_name, is_before);
        }
        public static void Xlua_Callback_Prefix(object __instance, MethodBase __originalMethod)
        {
            List<string> events;
            if (reg_events.TryGetValue(__originalMethod.FullDescription(), out events))
            {
                foreach (string e in events)
                {
                    RunCallback(e, new object[] { __instance });
                }
            }
        }
        public static void Xlua_Callback_Postfix(object __instance, MethodBase __originalMethod)
        {
            List<string> events;
            Plugin.Log.LogMessage(__originalMethod.FullDescription());
            if (reg_events.TryGetValue(__originalMethod.FullDescription(), out events))
            {
                foreach (string e in events)
                {
                    RunCallback(e, new object[] { __instance });
                }
            }
        }
    }
}
