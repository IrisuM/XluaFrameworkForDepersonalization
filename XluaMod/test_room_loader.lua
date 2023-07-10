--引用SDK包
require "SDK/SDK"

--加载模组列表后执行
OnFreshModule = function()
    
end

XluaTool:RegEvent(CS.HallWorld, "FreshModule", "测试房间_加载模组列表事件", false)
XluaTool:RegCallback("测试房间_加载模组列表事件", "当游戏刷新模组列表后发生", OnFreshModule)