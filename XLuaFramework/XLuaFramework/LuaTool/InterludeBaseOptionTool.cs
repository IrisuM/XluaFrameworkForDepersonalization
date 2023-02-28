using InterludeEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XLua;

namespace XLuaFramework.LuaTool
{
    public class InterludeBaseOptionTool : InterludeBaseOption
    {
        public string callback_event = "";
        public Dictionary<string, object> CustomDatas { get; set; }
        public override Task Run(InterludeControl interludeCtrl, HeroRoleData roleData = null)
        {
            XluaCallback.RunCallback(callback_event, new object[] { interludeCtrl, roleData, CustomDatas });
            return base.Run(interludeCtrl, roleData);
        }
        public InterludeBaseOptionTool(string key, Dictionary<string, object> customDatas) 
        {
            callback_event= key;
            CustomDatas = customDatas;
        }
    }
}
