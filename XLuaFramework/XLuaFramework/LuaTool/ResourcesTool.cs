using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace XLuaFramework.LuaTool
{
    internal static class ResourcesTool
    {
        static GameObject res_root = new GameObject("res root");
        static ResourcesTool()
        {
            Object.DontDestroyOnLoad(res_root);
            res_root.SetActive(false);
        }
        static Dictionary<string, Object> ResPool = new Dictionary<string, Object>();
        public static Object GetPoolObject(string path)
        {
            Object result = null;
            result = ResPool.TryGetValue(path, out result) ? result : null;
            return result;
        }
        public static void AddPoolObject(string path,Object res)
        {
            Object.DontDestroyOnLoad(res);
            if((res as GameObject) != null)
            {
                (res as GameObject).transform.parent = res_root.transform;
            }
            ResPool[path] = res;
        }
    }
}
