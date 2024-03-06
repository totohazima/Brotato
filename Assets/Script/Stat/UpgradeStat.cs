using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStat : MonoBehaviour, ICustomUpdateMono
{
    public LevelUpStat upgradeType;
    public int tier;

    public Text name;
    public Text effect;
    GameManager game;
    UpgradeStatImporter upgrade;
    public enum LevelUpStat
    {
        HP_UP,
        REGEN_UP,
        BLOOD_UP,
        DAMAGE_UP,
        MELEEDM_UP,
        RANGEDM_UP,
        ATKSPEED_UP,
        CRITICAL_UP,
        ENGINE_UP,
        RANGE_UP,
        ARMOR_UP,
        EVASION_UP,
        SPEED_UP,
    }

    void Awake()
    {
        game = GameManager.instance;
        upgrade = UpgradeStatImporter.instance;
    }

    void OnEnable() //생성시 티어를 정한다 (현재 1티어만 존재)
    {
        CustomUpdateManager.customUpdates.Add(this);
        int tier1 = 1;
        int tier2 = 0;
        int tier3 = 0;
        int tier4 = 0;

        float[] chanceLise = { tier1, tier2, tier3, tier4 };
        int index = GameManager.instance.Judgment(chanceLise);
        tier = index;
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        name.text = upgrade.upgradeName[(int)upgradeType];

        if (upgradeType == LevelUpStat.HP_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.heart[tier] + "</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.REGEN_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.lungs[tier] + "%</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.BLOOD_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.teeth[tier] + "%</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.DAMAGE_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.triceps[tier] + "%</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.MELEEDM_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.forearms[tier] + "</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.RANGEDM_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.shoulders[tier] + "</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.ATKSPEED_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.reflexes[tier] + "%</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.CRITICAL_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.fingers[tier] + "%</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.ENGINE_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.skull[tier] + "</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.RANGE_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.eyes[tier] + "</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.ARMOR_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.chest[tier] + "%</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.EVASION_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.back[tier] + "%</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
        else if (upgradeType == LevelUpStat.SPEED_UP)
        {
            effect.text = "<color=#4CFF52>+" + upgrade.legs[tier] + "%</color> " + upgrade.upgradeEffect[(int)upgradeType];
        }
    }
    public void StatUpgrade()
    {
        if(LevelUpManager.instance.isSetting == true)
        {
            return;
        }

        switch(upgradeType)
        {
            case LevelUpStat.HP_UP:
                game.playerInfo.maxHealth += upgrade.heart[tier];
                game.curHp += upgrade.heart[tier];
                break;
            case LevelUpStat.REGEN_UP:
                game.playerInfo.regeneration += upgrade.lungs[tier];
                break;
            case LevelUpStat.BLOOD_UP:
                game.playerInfo.bloodSucking += upgrade.teeth[tier];
                break;
            case LevelUpStat.DAMAGE_UP:
                game.playerInfo.persentDamage += upgrade.triceps[tier];
                break;
            case LevelUpStat.MELEEDM_UP:
                game.playerInfo.meleeDamage += upgrade.forearms[tier];
                break;
            case LevelUpStat.RANGEDM_UP:
                game.playerInfo.rangeDamage += upgrade.shoulders[tier];
                break;
            case LevelUpStat.ATKSPEED_UP:
                game.playerInfo.attackSpeed += upgrade.reflexes[tier];
                break;
            case LevelUpStat.CRITICAL_UP:
                game.playerInfo.criticalChance += upgrade.fingers[tier];
                break;
            case LevelUpStat.ENGINE_UP:
                game.playerInfo.engine += upgrade.skull[tier];
                break;
            case LevelUpStat.RANGE_UP:
                game.playerInfo.range += upgrade.eyes[tier];
                break;
            case LevelUpStat.ARMOR_UP:
                game.playerInfo.armor += upgrade.chest[tier];
                break;
            case LevelUpStat.EVASION_UP:
                game.playerInfo.evasion += upgrade.back[tier];
                break;
            case LevelUpStat.SPEED_UP:
                game.playerInfo.speed += upgrade.legs[tier];
                break;
            default:
                Debug.Log("upgradeType 미 설정");
                break;
        }


        game.levelUpChance--;
        
        if(game.levelUpChance <= 0)
        {
            game.levelUpUI.SetActive(false);
            //여기서 전리품 메뉴로
            game.ShopOpen();
        }
        else
        {
            LevelUpManager.instance.NextSelect();
        }
    }
}
