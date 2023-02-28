using InterludeEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLuaFramework.LuaTool
{
    public class BaseCheckTool : BaseCheck
    {
        public string callback_event = "";
        public Dictionary<string, object> CustomDatas { get; set; }
        public override bool IsReach(InterludeControl interludeCtrl, HeroRoleData roleData = null)
        {
            XluaCallback.RunCallback(callback_event, new object[] { interludeCtrl, roleData, CustomDatas });
            return base.IsReach(interludeCtrl, roleData);
        }
        public BaseCheckTool(string key, Dictionary<string, object> customDatas)
        {
            callback_event = key;
            CustomDatas = customDatas;
        }
    }
}
