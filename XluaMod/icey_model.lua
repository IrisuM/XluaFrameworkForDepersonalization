--引用SDK包
require "SDK/SDK"

--创建ResManager事件回调，这个事件发生在游戏资源加载后
OnResManagerInstall = function(resManager)
    --加载一个模板，在这个模板上替换文件
    rolemodel = CS.UnityEngine.Object.Instantiate(CS.UnityEngine.Resources.Load("Charator/Prefabs/Model_Custom_000"))
    rolemodel.name = "Model_Custom_icey" --设置名称，非必要，但是防止冲突
    --创建动画配置，直接按照这个配置修改
    local animation = {
        idle = {
            --动作名称
            IntervalTime = 0.1,
            IsLoop = true,
            SpriteDatas = {
                [1] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel( --加载图片，分别是路径，大小，图片中心，图片像素倍率，后两项可照抄
                        "huajidating/1"
                    )
                },
                [2] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/2"
                    )
                },
                [3] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/3"
                    )
                },
                [4] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/4"
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
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/1_l"
                    )
                },
                [2] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/2_l"
                    )
                },
                [3] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/3_l"
                    )
                },
                [4] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/4_l"
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
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/1_l"
                    )
                },
                [2] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/2_l"
                    )
                },
                [3] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/3_l"
                    )
                },
                [4] = {
                    ShowCount = 1,
                    Sprite = RoleModelTool:LoadSpriteForRoleModel(
                        "huajidating/4_l"
                    )
                }
            }
        }
    }
    --应用动画配置到已有模板
    RoleModelTool:SetAnimation(rolemodel:GetComponent("RoleModel"), animation)
    --将该模型添加到资源缓存中
    ResourcesTool:AddPoolObject("Charator/Prefabs/Model_Custom_icey", rolemodel)

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
            HeadIcon = "huajidating/1",
            Res = "Model_Custom_icey"
        }
    )
end

--注册lua事件回调
XluaTool:RegCallback("OnResManagerInstall", "艾希MOD", OnResManagerInstall)