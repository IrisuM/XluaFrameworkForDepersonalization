using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using XLua;
using static Reporter;
using static VoicePackageData;
using static XLua.LuaEnv;
using static XLuaFramework.PluginConst;

namespace XLuaFramework
{
    public class XluaMod : LuaEnv
    {
        string modRoot = "";
        public LuaFunction OnRoleModelInstall = null;
        private byte[] FixLoadPath(ref string filepath)
        {
            string path = Path.Combine(modRoot, filepath + ".lua");
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            return null;
        }
        public XluaMod(string root)
        {
            try
            {
                modRoot = root;
                CustomLoader loader = FixLoadPath;
                AddLoader(loader);
                string main_path = Path.Combine(modRoot, PluginPathConfig.XluaModMain + ".lua");
                if (File.Exists(main_path))
                {
                    DoString(string.Format("require '{0}'", PluginPathConfig.XluaModMain));
                }
                else
                {
                    Plugin.Log.LogMessage("未发现主lua文件:" + main_path);
                }
                OnRoleModelInstall = Global.Get<LuaFunction>(PluginXluaCallbackConfig.OnRoleModelInstall);
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
        public XluaMod() : this(XluaHelper.GetLocalDir()) { }
    }
    public static class XluaModManager
    {
        static XluaMod gluaEnv = new XluaMod();
        static Dictionary<string, XluaMod> luaenvs = new Dictionary<string, XluaMod>();
        public static Dictionary<string, XluaMod> lua_envs { get { return luaenvs; } }
        static XluaModManager()
        {
            lua_envs["local"] = gluaEnv;
        }
        public static XluaMod CreateXluaMod(string env_name, string root_path)
        {
            if (!Plugin.s_Instance.is_merge_luaenv)
            {
                if (!luaenvs.ContainsKey(env_name))
                {
                    luaenvs[env_name] = new XluaMod(root_path);
                }
                return luaenvs[env_name];
            }
            else
            {
                return gluaEnv;
            }
        }
    }
}
