using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStat : MonoBehaviour
{
    public LevelUpStat upgradeType;
    public int tier;

    public Text name;
    public Text effect;
    public Text num;
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
        int tier1 = 1;
        int tier2 = 0;
        int tier3 = 0;
        int tier4 = 0;

        float[] chanceLise = { tier1, tier2, tier3, tier4 };
        int index = GameManager.instance.Judgment(chanceLise);
        tier = index;
    }

    void Update()
    {
        name.text = upgrade.upgradeName[(int)upgradeType];
        effect.text = "      " + upgrade.upgradeEffect[(int)upgradeType];

        if (upgradeType == LevelUpStat.HP_UP)
        {
            num.text = "+" + upgrade.heart[tier];
        }
        else if (upgradeType == LevelUpStat.REGEN_UP)
        {
            num.text = "+" + upgrade.lungs[tier];
        }
        else if (upgradeType == LevelUpStat.BLOOD_UP)
        {
            num.text = "+" + upgrade.teeth[tier];
        }
        else if (upgradeType == LevelUpStat.DAMAGE_UP)
        {
            num.text = "+" + upgrade.triceps[tier];
        }
        else if (upgradeType == LevelUpStat.MELEEDM_UP)
        {
            num.text = "+" + upgrade.forearms[tier];
        }
        else if (upgradeType == LevelUpStat.RANGEDM_UP)
        {
            num.text = "+" + upgrade.shoulders[tier];
        }
        else if (upgradeType == LevelUpStat.ATKSPEED_UP)
        {
            num.text = "+" + upgrade.reflexes[tier];
        }
        else if (upgradeType == LevelUpStat.CRITICAL_UP)
        {
            num.text = "+" + upgrade.fingers[tier];
        }
        else if (upgradeType == LevelUpStat.ENGINE_UP)
        {
            num.text = "+" + upgrade.skull[tier];
        }
        else if (upgradeType == LevelUpStat.RANGE_UP)
        {
            num.text = "+" + upgrade.eyes[tier];
        }
        else if (upgradeType == LevelUpStat.ARMOR_UP)
        {
            num.text = "+" + upgrade.chest[tier];
        }
        else if (upgradeType == LevelUpStat.EVASION_UP)
        {
            num.text = "+" + upgrade.back[tier];
        }
        else if (upgradeType == LevelUpStat.SPEED_UP)
        {
            num.text = "+" + upgrade.legs[tier];
        }
    }
    public void StatUpgrade()
    {
        if(LevelUpManager.instance.isSetting == true)
        {
            return;
        }


        if(upgradeType == LevelUpStat.HP_UP)
        {
            game.playerInfo.maxHealth += float.Parse(num.text);
            game.curHp += float.Parse(num.text); ;
        }
        else if(upgradeType == LevelUpStat.REGEN_UP)
        {
            game.playerInfo.regeneration += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.BLOOD_UP)
        {
            game.playerInfo.bloodSucking += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.DAMAGE_UP)
        {
            game.playerInfo.persentDamage += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.MELEEDM_UP)
        {
            game.playerInfo.meleeDamage += float.Parse(num.text);
        }
        else if (upgradeType == LevelUpStat.RANGEDM_UP)
        {
            game.playerInfo.rangeDamage += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.ATKSPEED_UP)
        {
            game.playerInfo.attackSpeed += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.CRITICAL_UP)
        {
            game.playerInfo.criticalChance += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.ENGINE_UP)
        {
            game.playerInfo.engine += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.RANGE_UP)
        {
            game.playerInfo.range += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.ARMOR_UP)
        {
            game.playerInfo.armor += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.EVASION_UP)
        {
            game.playerInfo.evasion += float.Parse(num.text); 
        }
        else if (upgradeType == LevelUpStat.SPEED_UP)
        {
            game.playerInfo.speed += float.Parse(num.text); 
        }
        else
        {
            Debug.Log("스탯 설정 필요");
        }

        game.levelUpChance--;
        
        if(game.levelUpChance <= 0)
        {
            game.levelUpUI.SetActive(false);
            //여기서 전리품 메뉴로
            game.nextWave();
        }
        else
        {
            LevelUpManager.instance.NextSelect();
        }
    }
}
