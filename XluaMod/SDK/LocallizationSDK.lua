--用于生成本地化文字的SDK工具包
LocallizationTool = LocallizationTool or {}

function LocallizationTool:Create()
    local locallization = CS.LocalizationKeyData()
    return locallization
end

function LocallizationTool:SetText(locallization,localkey, chinese_text, other)
    other = other or {}
    locallization = locallization or LocallizationTool:Create()
    if chinese_text == nil or chinese_text == "" then
        for k,v in pairs(other) do
            chinese_text = v
            break
        end
        chinese_text = chinese_text or ""
    end
    locallization.TarKey = CS.MOD_CustomLocalizationManager.Instance.LocalizationData:SetDataToFileAndGetKey(
        chinese_text, CS.GameHelper.ToPinYin(localkey), nil, "中文", locallization:GetKeyDataFolderModel())
    for key, text in pairs(other) do
        CS.MOD_CustomLocalizationManager.Instance.LocalizationData:SetDataToFileAndGetKey(
            text, CS.GameHelper.ToPinYin(localkey), locallization.TarKey, key, locallization:GetKeyDataFolderModel())
    end
    locallization:FillInputText()
end

function LocallizationTool:CreateText(key, chinese_text,other)
    local text=LocallizationTool:Create()
    LocallizationTool:SetText(text, key, chinese_text, other)
    return text
end