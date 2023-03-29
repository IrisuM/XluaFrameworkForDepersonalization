require "SDK/SDK"
--[[
以下是测试时使用的监控类型
MOD.InterludeData
InterludeEvent.SetInterludeTxtBg
InterludeEvent.JumpToTarChooseOption
InterludeEvent.DiceCheckOption
InterludeEvent.ShowCenterDialogOption
InterludeEvent.SaveRecordOption
InterludeEvent.SanCheckOption
]]
local module_key = "伪存区序章_姐妹日记"
local module_name = "姐妹日记"
local module_des = "测试模组"
local CreateText = function(k,text) LocallizationTool:CreateText(k,text) end

--创建ResManager事件回调，这个事件发生在游戏资源加载后
OnResManagerInstall = function(resManager)
    --创建一个文字模组，参数key尽可能保证唯一性
    local interlude = InterludeDataTool:Create(module_key)

    --设置模组名称
    InterludeDataTool:SetName(interlude, module_name)

    --设置模组描述s
    InterludeDataTool:SetDes(interlude, module_des)

    --设置启动BGM，如果有

    --添加剧本内容

    local start_branch_key = "开始"
    --创建分支
    local start_branch = InterludeDataTool:CreateInterludeChoose(start_branch_key)
    start_branch.Segments = {
        {
            NarratorData = {
                Localization_Des = CreateText(module_key,"第一句话测试")
            }
        }
    }
    start_branch.InterludeOptions = {

    }
    InterludeDataTool:AddInterludeChoose(interlude, start_branch)

    ResManagerTool:GetAllInterlude(resManager):Add(interlude)
end

--注册lua事件回调
XluaTool:RegCallback("OnResManagerInstall", module_key, OnResManagerInstall)
