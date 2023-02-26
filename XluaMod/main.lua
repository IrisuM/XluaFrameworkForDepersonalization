--引用SDK包
require "SDK/SDK"

--创建ResManager事件回调，这个事件发生在游戏资源加载后
OnResManagerInstall = function(resManager)
    --加载一个模板，在这个模板上替换文件
    rolemodel = CS.UnityEngine.Object.Instantiate(CS.UnityEngine.Resources.Load("Charator/Prefabs/Model_Custom_000"))
    rolemodel:SetActive(false) --隐藏物体，避免地图上莫名其妙多个模型
    rolemodel.name = "Model_Custom_huaji" --设置名称，非必要，但是防止冲突
    --创建动画配置，直接按照这个配置修改
    local animation = {
        idle = {
            --动作名称
            IntervalTime = 0.1,
            IsLoop = true,
            SpriteDatas = {
                [1] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite( --加载图片，分别是路径，大小，图片中心，图片像素倍率，后两项可照抄
                        "huajidating/1",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [2] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/2",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [3] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/3",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [4] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/4",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                }
            }
        },
        walk = {
            IntervalTime = 0.1,
            IsLoop = true,
            SpriteDatas = {
                [1] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/1_l",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [2] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/2_l",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [3] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/3_l",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [4] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/4_l",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                }
            }
        },
        run = {
            IntervalTime = 0.1,
            IsLoop = true,
            SpriteDatas = {
                [1] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/1_l",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [2] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/2_l",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [3] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/3_l",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                },
                [4] = {
                    ShowCount = 1,
                    Sprite = ResourcesTool:LoadSprite(
                        "huajidating/4_l",
                        CS.UnityEngine.Rect(0, 0, 56.0, 56.0),
                        {x = 0.5, y = 0},
                        64
                    )
                }
            }
        }
    }
    --应用动画配置到已有模板
    RoleModelTool:SetAnimation(rolemodel:GetComponent("RoleModel"), animation)
    --将该模型添加到资源缓存中
    ResourcesTool:AddPoolObject("Charator/Prefabs/Model_Custom_huaji", rolemodel)

    --为模型设置头像
    RoleModelTool:SetHeadIcons(
        rolemodel:GetComponent("RoleModel"),
        {
            {
                Icon = ResourcesTool:LoadSpriteAuto("huajidating/2"),
                Type = 0
            },
            {
                Icon = ResourcesTool:LoadSpriteAuto("huajidating/2"),
                Type = 1
            },
            {
                Icon = ResourcesTool:LoadSpriteAuto("huajidating/2"),
                Type = 2
            },
            {
                Icon = ResourcesTool:LoadSpriteAuto("huajidating/2"),
                Type = 3
            },
            {
                Icon = ResourcesTool:LoadSpriteAuto("huajidating/2"),
                Type = 4
            }
        }
    )

    --添加人物模型选择
    ResManagerTool:GetPeripheral(resManager).Models:Add(
        {
            HeadIcon = "huajidating/2",
            Res = "Model_Custom_huaji"
        }
    )
end

--注册lua事件回调
XluaTool:RegCallback("OnResManagerInstall", "创建滑稽mod测试", OnResManagerInstall)

--[[
OnShowActionTip = function(entity)
    if (Mengluu:StringStartWith(CS.BattleHelper.MapHero.Model.name, "Model_Custom_huaji") == false) then
        return
    end
    Mengluu:Print(entity.name..""..entity:ToString())
    if (entity.Model ~= nil) then
        local animation = RoleModelTool:GetAnimation(CS.BattleHelper.MapHero.Model)
        RoleModelTool:SetAnimation(entity.Model, animation)
        entity.gameObject:SetActive(false)
        entity.gameObject:SetActive(true)
    end
end

--注册显示交互提示事件
XluaTool:RegEvent(CS.MOD_Entity_BaseUnit, "ShowActionTip", "变成滑稽", true)
XluaTool:RegCallback("变成滑稽", "创建滑稽mod测试", OnShowActionTip)
--]]
On_MOD_Entity_Npc_Update = function(entity)
    if
        (CS.UnityEngine.Vector3.Distance(entity.transform.position, CS.BattleHelper.MapHero.transform.position) < 0.35 and
            Mengluu:StringStartWith(CS.BattleHelper.MapHero.Model.name, "Model_Custom_huaji"))
     then
        if (entity.Model ~= nil) then
            local animation = RoleModelTool:GetAnimation(CS.BattleHelper.MapHero.Model)
            RoleModelTool:SetAnimation(entity.Model, animation)
            entity.gameObject:SetActive(false)
            entity.gameObject:SetActive(true)
        end
    end
end
XluaTool:RegEvent(CS.MOD_Entity_Npc, "Update", "变成滑稽", true)
XluaTool:RegCallback("变成滑稽", "创建滑稽mod测试", On_MOD_Entity_Npc_Update)
