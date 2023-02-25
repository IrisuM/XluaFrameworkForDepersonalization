using BepInEx.Workshop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class XluaMod
    {
        string modRoot = "";
        public LuaEnv luaEnv;
        private byte[] FixLoadPath(ref string filepath)
        {
            string path = Path.Combine(modRoot, filepath + ".lua");
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            return null;
        }
        public XluaMod(string root, LuaEnv luaEnv)
        {
            try
            {
                modRoot = root;
                CustomLoader loader = FixLoadPath;
                luaEnv.AddLoader(loader);
                string main_path = Path.Combine(modRoot, PluginPathConfig.XluaModMain + ".lua");
                if (File.Exists(main_path))
                {
                    luaEnv.DoString(string.Format("require '{0}'", PluginPathConfig.XluaModMain));
                }
                else
                {
                    Plugin.Log.LogMessage("未发现主lua文件:" + main_path);
                }
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

            this.luaEnv = luaEnv;
        }
        public XluaMod(LuaEnv luaEnv) : this(XluaModManager.GetLocalDir(), luaEnv) { }
        public XluaMod(string path) : this(path, new LuaEnv()) { }
        public XluaMod() : this(XluaModManager.GetLocalDir()) { }
    }
    public static class XluaModManager
    {
        public static bool isInit { get; private set; } = false;
        static LuaEnv gluaEnv = new LuaEnv();
        static Dictionary<string, XluaMod> luaenvs = new Dictionary<string, XluaMod>();
        public static Dictionary<string, XluaMod> lua_envs { get { return luaenvs; } }
        static XluaModManager()
        {
            luaenvs["local"] = new XluaMod(gluaEnv);
        }
        //创建MOD对象
        public static XluaMod CreateXluaMod(string env_name, string root_path)
        {
            XluaMod mod = null;
            if (!luaenvs.TryGetValue(env_name, out mod) || mod == null)
            {
                if (!Plugin.s_Instance.is_merge_luaenv)
                {
                    luaenvs[env_name] = new XluaMod(root_path);
                }
                else
                {
                    luaenvs[env_name] = new XluaMod(root_path, gluaEnv);
                }
            }
            isInit = true;
            return mod;
        }
        //获取本地MOD路径
        public static string GetLocalDir()
        {
            return Path.Combine(Application.streamingAssetsPath, PluginPathConfig.FrameworkPath);
        }
        //获取MOD路径
        public static string[] GetModDir()
        {
            List<string> mods = new List<string>();
            foreach (ModInfo info in WorkshopLoader.Inst.ModMag.ModList)
            {
                mods.Add(Path.Combine(info.ModDir, PluginPathConfig.FrameworkPath));
            }
            return mods.ToArray();
        }
    }
    public class XluaModComponent : MonoBehaviour
    {
        public static void CreateXluaModComponent()
        {
            GameObject XluaModManagerObj = new GameObject(PluginXluaCallbackConfig.XluaComponentName);
            XluaModManagerObj.AddComponent<XluaModComponent>();
            if (!XluaModManager.isInit)
            {
                Plugin.Log.LogMessage("开始加载XluaMod");
                //逐个加载lua mod
                foreach (string path in XluaModManager.GetModDir())
                {
                    Plugin.Log.LogMessage("加载:" + path);
                    try
                    {
                        XluaModManager.CreateXluaMod(path, path);
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
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        void FixedUpdate()
        {
            XluaCallback.RunCallback(PluginXluaCallbackConfig.OnFixedUpdate, new object[] { });
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        void Update()
        {
            foreach(var e in XluaModManager.lua_envs)
            {
                e.Value.luaEnv.Tick();
            }

            XluaCallback.RunCallback(PluginXluaCallbackConfig.OnUpdate, new object[] { });
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        void LateUpdate()
        {
            XluaCallback.RunCallback(PluginXluaCallbackConfig.OnLateUpdate, new object[] { });
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        void OnGUI()
        {
            XluaCallback.RunCallback(PluginXluaCallbackConfig.OnOnGUI, new object[] { });
        }
    }
}
