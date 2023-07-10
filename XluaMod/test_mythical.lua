--引用SDK包
require "SDK/SDK"

--创建ResManager事件回调，这个事件发生在游戏资源加载后
OnResManagerInstall = function(resManager)
    local mythicalData = ResManagerTool:GetMythical(resManager)
    for key, mythical in pairs(mythicalData) do
        if mythical.Localization_Name.InputText == "咏叹宣叙" then
            mythical.Localization_Des.InputText = "测试，" + mythical.Localization_Des.InputText
        end
    end
end

--注册lua事件回调
XluaTool:RegCallback("OnResManagerInstall", "修改咏叹宣叙描述", OnResManagerInstall)
