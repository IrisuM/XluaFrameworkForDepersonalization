ResManagerTool = ResManagerTool or {}

function ResManagerTool:GetAchievement(resManager)
    return resManager.Achievements
end

function ResManagerTool:GetAllInterlude(resManager)
    return resManager.AllInterlude
end

function ResManagerTool:GetBattleRandomEvents(resManager)
    return resManager.BattleRandomEvents
end

function ResManagerTool:GetBattleSceneRandomEventPool(resManager)
    return resManager.BattleSceneRandomEventPool
end

function ResManagerTool:GetBuff(resManager)
    return resManager.BuffFactory.Buffs
end

function ResManagerTool:GetCareer(resManager)
    return resManager.CareerFactory.Careers
end

function ResManagerTool:GetCharacterBattleSkillData(resManager)
    return resManager.CharacterBattleSkillData
end

function ResManagerTool:GetChara(resManager)
    return resManager.CharaFactory.Charactors
end

function ResManagerTool:GetCommonBattleEffectShowList(resManager)
    return resManager.CommonBattleEffectShowList
end

function ResManagerTool:GetCommonLocalization(resManager)
    return resManager.CommonLocalizationFactory.Datas
end

function ResManagerTool:GetDamageAttrInteractionList(resManager)
    return resManager.DamageAttrInteractionList
end

function ResManagerTool:GetDescriptionTemplates(resManager)
    return resManager.DescriptionTemplates
end

function ResManagerTool:GetGlobal(resManager)
    return resManager.GlobalFactory.Data
end

function ResManagerTool:GetHelpSystemRes(resManager)
    return resManager.HelpSystemResFactory.ProcessDatas
end

function ResManagerTool:GetInterludeStudy(resManager)
    return resManager.InterludeStudyFactory.Datas
end

function ResManagerTool:GetInterludeVacation(resManager)
    return resManager.InterludeVacationFactory.Datas
end

function ResManagerTool:GetItem(resManager)
    return resManager.ItemFactory.Items
end

function ResManagerTool:GetItemProcessRes(resManager)
    return resManager.ItemProcessResFactory.ProcessDatas
end

function ResManagerTool:GetMagicSkillData(resManager)
    return resManager.MagicSkillData
end

function ResManagerTool:GetMagicUnlockList(resManager)
    return resManager.MagicUnlockList
end

function ResManagerTool:GetMonsterBattleSkillData(resManager)
    return resManager.MonsterBattleSkillData
end

function ResManagerTool:GetMythical(resManager)
    return resManager.MythicalFactory.MythicalItems
end

function ResManagerTool:GetPeripheral(resManager)
    return resManager.PeripheralFactory.Data
end

function ResManagerTool:GetSpeakConfig(resManager)
    return resManager.SpeakConfigFactory.Data
end

function ResManagerTool:GetTrait(resManager)
    return resManager.TraitFactory.Traits
end