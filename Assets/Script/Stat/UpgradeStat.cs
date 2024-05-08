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
    StageManager stage;
    public enum LevelUpStat
    {
        HP_UP,
        REGEN_UP,
        BLOOD_UP,
        DAMAGE_UP,
        MELEEDM_UP,
        RANGEDM_UP,
        ELEMENTALDM_UP,
        ATKSPEED_UP,
        CRITICAL_UP,
        ENGINE_UP,
        RANGE_UP,
        ARMOR_UP,
        EVASION_UP,
        LUCK_UP,
        HARVEST_UP,
        SPEED_UP,
    }

    void Awake()
    {
        stage = StageManager.instance;
    }

    void OnEnable() //생성시 티어를 정한다 (현재 1티어만 존재)
    {
        UpgradeTierSetting();
    }
    private void UpgradeTierSetting()
    {
        UpgradeStatInfoTable.Data import = GameManager.instance.gameDataBase.upgradeStatInfoTable.table[(int)upgradeType];
        int tier1 = 1;
        int tier2 = 0;
        int tier3 = 0;
        int tier4 = 0;

        float[] chanceLise = { tier1, tier2, tier3, tier4 };
        int index = StageManager.instance.Judgment(chanceLise);
        tier = index;

        name.text = import.upgradeName;
        effect.text = "<color=#4CFF52>+" + import.tierEffect[tier] + "</color> " + import.upgradeEffect;
    }

    public void StatUpgrade()
    {
        UpgradeStatInfoTable import = GameManager.instance.gameDataBase.upgradeStatInfoTable;

        if (LevelUpManager.instance.isSetting == false)
        {
            switch (upgradeType)
            {
                case LevelUpStat.HP_UP:
                    stage.playerInfo.maxHealth_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    stage.curHp += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.REGEN_UP:
                    stage.playerInfo.regeneration_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.BLOOD_UP:
                    stage.playerInfo.bloodSucking_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.DAMAGE_UP:
                    stage.playerInfo.persentDamage_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.MELEEDM_UP:
                    stage.playerInfo.meleeDamage_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.RANGEDM_UP:
                    stage.playerInfo.rangeDamage_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.ELEMENTALDM_UP:
                    stage.playerInfo.elementalDamage_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.ATKSPEED_UP:
                    stage.playerInfo.attackSpeed_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.CRITICAL_UP:
                    stage.playerInfo.criticalChance_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.ENGINE_UP:
                    stage.playerInfo.engine_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.RANGE_UP:
                    stage.playerInfo.range_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.ARMOR_UP:
                    stage.playerInfo.armor_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.EVASION_UP:
                    stage.playerInfo.evasion_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.LUCK_UP:
                    stage.playerInfo.lucky_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.HARVEST_UP:
                    stage.playerInfo.harvest_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.SPEED_UP:
                    stage.playerInfo.speed_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
            }

            stage.playerInfo.StatCalculate();

            stage.levelUpChance--;

            if (stage.levelUpChance <= 0)
            {
                //여기서 전리품 메뉴로
                if (stage.lootChance > 0)
                {
                    stage.LootMenuOpen();
                }
                else
                {
                    stage.ShopOpen();
                }
                stage.levelUpUI.SetActive(false);
            }
            else
            {
                LevelUpManager.instance.NextSelect();
            }
        }
    }
}
