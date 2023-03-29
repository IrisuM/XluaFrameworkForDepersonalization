--用于生成完整模组的工具包
require "SDK/XluaSDK"
ModuleTool = ModuleTool or {}

function ModuleTool:CreateModuleConfig(config)
    local module_config = CS.ModuleConfig()
    module_config.IsGuide = config.IsGuide or false
    module_config.IsLock = config.IsLock or false
    module_config.Stages = config.Stages or module_config.Stages
    module_config.Map = config.Map or module_config.Map
    module_config.Shop = config.Shop or module_config.Shop
    module_config.PersonDialog = config.PersonDialog or module_config.PersonDialog
    module_config.EditionKey = config.EditionKey or 0
    module_config.IntroduceConfig = config.IntroduceConfig or module_config.IntroduceConfig
    module_config.ShowOrder = config.ShowOrder or 0
    module_config.ScenePath = config.ScenePath or nil
    module_config.ModuleKey = config.ModuleKey or "测试房间"
end
