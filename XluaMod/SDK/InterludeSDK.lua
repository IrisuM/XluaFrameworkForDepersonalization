--用于生成文字模组的SDK工具包
require "SDK/LocallizationSDK"

InterludeDataTool = InterludeDataTool or {}

function InterludeDataTool:Create(ModuleKey)
    local InterludeData = CS.MOD.InterludeData()
    InterludeData.ModuleKey = ModuleKey
    InterludeData.IsLock = false
    return InterludeData
end

function InterludeDataTool:SetName(interlude, module_name)
    LocallizationTool:SetText(interlude.Localization_ModuleName,interlude.ModuleKey, module_name)
end

function InterludeDataTool:SetDes(interlude, module_des)
    LocallizationTool:SetText(interlude.StartInterludeInfo,interlude.ModuleKey, module_des)
end

function InterludeDataTool:CreateInterludeChoose(InterludeChooseKey)
    local InterludeChoose = CS.MOD.InterludeChooseData()
    InterludeChoose.ChooseKey = InterludeChooseKey
    return InterludeChoose
end

function InterludeDataTool:AddInterludeChoose(interlude, InterludeChoose)
    interlude.Config.All:Add(InterludeChoose)
end