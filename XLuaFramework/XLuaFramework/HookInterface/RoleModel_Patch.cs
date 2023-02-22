using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLuaFramework.HookInterface
{
    internal class RoleModel_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(RoleModel), "Install")]
        static void RoleModel_Install_Postfix(RoleModel __instance)
        {
            foreach(var mod in XluaModManager.lua_envs)
            {
                if (mod.Value.OnRoleModelInstall != null)
                {
                    mod.Value.OnRoleModelInstall.Call(new object[] { __instance });
                }
            }
        }
    }
}
