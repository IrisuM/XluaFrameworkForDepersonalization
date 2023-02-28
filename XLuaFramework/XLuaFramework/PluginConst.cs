using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLuaFramework
{
    public static class PluginConst
    {
        //插件配置
        public static class PluginConfig
        {
            public const string PLUGIN_GUID = "irisu.fyumi.xluaframework";
            public const string PLUGIN_NAME = "XluaFramework";
            public const string PLUGIN_VERSION = "1.0.0";
            public const string WELCOME_WORLD = "\n\n\n" +
                "欢迎关注 扇宝 https://space.bilibili.com/698438232" +
                "\n" +
                "大貔貅，大貔貅" +
                "\n" +
                "我有一个大貔貅" +
                "\n\n\n";
            public const string CONFIG_SECTION = "Xlua执行框架";
            public const string CONFIG_IS_MERGE_LUAENV = "是否合并执行环境";
            public const string CONFIG_IS_MERGE_LUAENV_DESCRIPTION = "合并执行环境会减少开销，但是可能会因为mod编码不规范造成代码冲突";
            public const string CONFIG_IS_ENABLE_CONSOLE = "是否开启控制台";
            public const string CONFIG_IS_ENABLE_CONSOLE_DESCRIPTION = "开启控制台后可以显示lua输出，并执行简单的lua指令";
        }
        
        public static class PluginPathConfig
        {
            public const string FrameworkPath = "plugins/XluaMod";
            public const string XluaModMain = "main";
        }

        public static class PluginXluaCallbackConfig
        {
            public const string XluaComponentName = "XluaModManager";
            public const string OnResourceLoad = "OnResourceLoad";
            public const string OnResManagerInstall = "OnResManagerInstall";
            public const string OnUpdate = "OnUpdate";
            public const string OnLateUpdate = "OnLateUpdate";
            public const string OnFixedUpdate = "OnFixedUpdate";
            public const string OnOnGUI = "OnOnGUI";
        }
    }
}
