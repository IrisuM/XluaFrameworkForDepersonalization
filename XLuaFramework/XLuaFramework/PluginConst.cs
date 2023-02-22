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
                "欢迎关注 安堂いなり(安堂稻荷) https://space.bilibili.com/392505232" +
                "\n" +
                "是擅长各类恐怖游戏的可爱机械狐狐" +
                "\n" +
                "《人格解体》直播视频：https://www.bilibili.com/video/BV1SD4y177ne" +
                "\n\n\n";
            public const string CONFIG_SECTION = "Xlua执行框架";
            public const string CONFIG_IS_MERGE_LUAENV = "是否合并执行环境";
            public const string CONFIG_IS_MERGE_LUAENV_DESCRIPTION = "合并执行环境会减少开销，但是可能会因为mod编码不规范造成代码冲突";
            public const string CONFIG_IS_ENABLE_CONSOLE = "是否开启控制台";
            public const string CONFIG_IS_ENABLE_CONSOLE_DESCRIPTION = "开启控制台后可以显示lua输出，并执行简单的lua指令";
        }
        
        public static class PluginPathConfig
        {
            public const string FrameworkPath = "XluaMod";
            public const string XluaModMain = "main";
        }

        public static class PluginXluaCallbackConfig
        {
            public const string OnResourceLoad = "OnResourceLoad";
        }
    }
}
