require "SDK/XluaSDK"
RoleModelTool = RoleModelTool or {}

--返回人物动画图片数组
function RoleModelTool:GetSpriteDatas(spriteDatas)
    local result = {}
    for k, v in pairs(spriteDatas) do
        table.insert(
            result,
            {
                ShowCount = v.ShowCount,
                Sprite = v.Sprite
            }
        )
    end
    return result
end

--根据传入参数设置
function RoleModelTool:SetSpriteDatas(spriteDatas, new_spriteDatas)
    spriteDatas:Clear()
    for key, value in ipairs(new_spriteDatas) do
        local data = CS.SpriteConfigData()
        data.Sprite = value.Sprite
        data.ShowCount = value.ShowCount or 1
        data.ActiveChild = false
        spriteDatas:Add(data)
    end
end

--返回人物动画数据
function RoleModelTool:GetAnimation(role_model)
    local result = {}
    for k, v in pairs(role_model.SpriteAnim.AnimationList) do
        result[v.Key] = {
            IntervalTime = v.IntervalTime,
            IsLoop = v.IsLoop,
            IsUseOldMode = v.IsUseOldMode,
            SpriteDatas = RoleModelTool:GetSpriteDatas(v.SpriteDatas)
        }
    end
    return result
end

--设置动画类型
function RoleModelTool:SetAnimation(role_model, new_animation)
    role_model.SpriteAnim.AnimationList:Clear()
    for key, value in pairs(new_animation) do
        local data = CS.SpriteAnimationData()
        data.Key = key
        data.IntervalTime = value.IntervalTime or 0.1
        data.IsLoop = value.IsLoop or true
        data.IsUseOldMode = value.IsUseOldMode or false
        data.SpriteDatas = XluaTool:CreateList(CS.SpriteConfigData)
        RoleModelTool:SetSpriteDatas(data.SpriteDatas, value.SpriteDatas or {})
        role_model.SpriteAnim.AnimationList:Add(data)
    end
end

--返回头像数据
function RoleModelTool:GetHeadIcons(role_model)
    local result = {}
    for k, v in pairs(role_model.HeadIcons) do
        result[v.Key] = {
            Icon = v.Icon,
            Type = v.Type
        }
    end
    return result
end

--设置头像数据
function RoleModelTool:SetHeadIcons(role_model, new_headicons)
    role_model.HeadIcons:Clear()
    for key, value in ipairs(new_headicons) do
        local data = CS.HeadIconData()
        data.Icon = value.Icon
        data.Type = value.Type or 0
        role_model.HeadIcons:Add(data)
    end
    return result
end

--加载适用于人物动画的图片格式
function RoleModelTool:LoadSpriteForRoleModel(path)
    return ResourcesTool:LoadSprite(path, CS.UnityEngine.Rect(0, 0, 48.0, 64.0), { x = 0.5, y = 0 }, 64)
end
