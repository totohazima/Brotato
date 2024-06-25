using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStat : MonoBehaviour
{
    public LevelUpStat upgradeType;
    public int tier;
    public Text name;
    public Text effectText;
    public Image backGroundImage;
    public Outline outline;
    GameManager game;
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
        game = GameManager.instance;
        stage = StageManager.instance;
    }

    void OnEnable() //생성시 티어를 정한다 (현재 1티어만 존재)s
    {
        UpgradeTierSetting();
    }
    private void UpgradeTierSetting()
    {
        UpgradePercentageInfoTable.Data[] percentData = new UpgradePercentageInfoTable.Data[GameManager.instance.gameDataBase.upgradePercentageInfoTable.table.Length];
        for (int i = 0; i < percentData.Length; i++)
        {
            percentData[i] = GameManager.instance.gameDataBase.upgradePercentageInfoTable.table[i];
        }
        int level = game.playerInfo.playerLevel + 1 - (game.playerInfo.levelUpChance);
        float totalChance = 100;
        float tier1 = 0;
        float tier2 = 0;
        float tier3 = 0;
        float tier4 = 0;

        if (level == 1) { tier1 = 100; }
        else if (level == 5) { tier2 = 100; }
        else if (level == 10 || level == 15 || level == 20) { tier3 = 100; }
        else if (level > 24 && level % 5 == 0) { tier4 = 100; }
        else
        {
            for (int i = percentData.Length - 1; i >= 0; i--)
            {
                UpgradePercentageInfoTable.Data data = percentData[i];
                if (level >= data.minLevel)
                {
                    float chance = (data.chancePerLevel * (level - data.minLevel) + data.baseChance) * (1 + (stage.playerInfo.lucky / 100));
                    if (chance > data.maxChance)
                    {
                        chance = data.maxChance;
                    }

                    if (chance > totalChance)
                    {
                        chance = totalChance;
                        if(totalChance <= 0)
                        {
                            totalChance -= chance;
                        }
                    }
                    else
                    {
                        totalChance -= chance;
                    }

                    switch (i)
                    {
                        case 3:
                            tier4 = chance;
                            break;
                        case 2:
                            tier3 = chance;
                            break;
                        case 1:
                            tier2 = chance;
                            break;
                        case 0:
                            tier1 = chance;
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 3:
                            tier4 = 0;
                            break;
                        case 2:
                            tier3 = 0;
                            break;
                        case 1:
                            tier2 = 0;
                            break;
                        case 0:
                            tier1 = 0;
                            break;
                    }
                }
            }
        }
        float[] chanceLise = { tier1, tier2, tier3, tier4 };
        int index = StageManager.instance.Judgment(chanceLise);
        tier = index;

        UpgradeStatInfoTable.Data import = GameManager.instance.gameDataBase.upgradeStatInfoTable.table[(int)upgradeType];
        name.text = import.upgradeName;
        effectText.text = "<color=#4CFF52>+" + import.tierEffect[tier] + "</color> " + import.upgradeEffect;
        ImageRender(tier);

        ///확률 보여주기용
        for (int i = 0; i < GameManager.instance.upgradeChance.Length; i++)
        {
            switch(i)
            {
                case 0:
                    GameManager.instance.upgradeChance[i] = tier1;
                    break;
                case 1:
                    GameManager.instance.upgradeChance[i] = tier2;
                    break;
                case 2:
                    GameManager.instance.upgradeChance[i] = tier3;
                    break;
                case 3:
                    GameManager.instance.upgradeChance[i] = tier4;
                    break;
            }
        }
        ///
    }
    private void ImageRender(int tier)
    {
        switch(tier)
        {
            case 0:
                backGroundImage.color = Color.black;
                outline.effectColor = Color.black;
                name.color = Color.white;
                break;
            case 1:
                backGroundImage.color = new Color(5 / 255f, 25 / 255f, 40 / 255f);
                outline.effectColor = new Color(21 / 255f, 178 / 255f, 232 / 255f);
                name.color = new Color(21 / 255f, 178 / 255f, 232 / 255f);
                break;
            case 2:
                backGroundImage.color = new Color(20 / 255f, 10 / 255f, 45 / 255f);
                outline.effectColor = new Color(204 / 255f, 0 / 255f, 255 / 255f);
                name.color = new Color(204 / 255f, 0 / 255f, 255 / 255f);
                break;
            case 3:
                backGroundImage.color = new Color(45 / 255f, 10 / 255f, 10 / 255f);
                outline.effectColor = new Color(250 / 255f, 7 / 255f, 11 / 255f);
                name.color = new Color(250 / 255f, 7 / 255f, 11 / 255f);
                break;
        }
    }
    public void StatUpgrade()
    {
        UpgradeStatInfoTable import = GameManager.instance.gameDataBase.upgradeStatInfoTable;

        if (LevelUpManager.instance.isSetting == false)
        {
            switch (upgradeType)
            {
                case LevelUpStat.HP_UP:
                    if (game.character == Player.Character.RANGER) //레인저 체력 증가량 -25%
                    {
                        game.playerAct.maxHealth_Origin += import.table[(int)upgradeType].tierEffect[tier] * 0.75f;
                        game.playerInfo.playerHealth += game.playerAct.maxHealth;
                    }
                    else
                    {
                        game.playerAct.maxHealth_Origin += import.table[(int)upgradeType].tierEffect[tier];
                        game.playerInfo.playerHealth += game.playerAct.maxHealth;
                    }
                    break;
                case LevelUpStat.REGEN_UP:
                    if (game.character == Player.Character.BULL) //황소 재생 증가량 +50%
                    {
                        game.playerAct.regeneration_Origin += import.table[(int)upgradeType].tierEffect[tier] * 1.5f;
                    }
                    else
                    {
                        game.playerAct.regeneration_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    }
                    break;
                case LevelUpStat.BLOOD_UP:
                    game.playerAct.bloodSucking_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.DAMAGE_UP:
                    if (game.character == Player.Character.ENGINEER) //엔지니어 대미지 증가량 -50%
                    {
                        game.playerAct.persentDamage_Origin += import.table[(int)upgradeType].tierEffect[tier] * 0.5f;
                    }
                    else
                    {
                        game.playerAct.persentDamage_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    }
                    break;
                case LevelUpStat.MELEEDM_UP:
                    game.playerAct.meleeDamage_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.RANGEDM_UP:
                    if (game.character == Player.Character.ENGINEER) //레인저 원거리 대미지 증가량 +50%
                    {
                        game.playerAct.rangeDamage_Origin += import.table[(int)upgradeType].tierEffect[tier] * 1.5f;
                    }
                    else
                    {
                        game.playerAct.rangeDamage_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    }
                    break;
                case LevelUpStat.ELEMENTALDM_UP:
                    game.playerAct.elementalDamage_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.ATKSPEED_UP:
                    game.playerAct.attackSpeed_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.CRITICAL_UP:
                    game.playerAct.criticalChance_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.ENGINE_UP:
                    if (game.character == Player.Character.ENGINEER) //엔지니어 엔지니어링 증가량 +25%
                    {
                        game.playerAct.engine_Origin += import.table[(int)upgradeType].tierEffect[tier]* 1.25f;
                    }
                    else
                    {
                        game.playerAct.engine_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    }
                    break;
                case LevelUpStat.RANGE_UP:
                    game.playerAct.range_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.ARMOR_UP:
                    game.playerAct.armor_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.EVASION_UP:
                    game.playerAct.evasion_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.LUCK_UP:
                    game.playerAct.lucky_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.HARVEST_UP:
                    game.playerAct.harvest_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
                case LevelUpStat.SPEED_UP:
                    game.playerAct.speed_Origin += import.table[(int)upgradeType].tierEffect[tier];
                    break;
            }

            game.playerAct.StatCalculate();

            game.playerInfo.levelUpChance--;

            if (game.playerInfo.levelUpChance <= 0)
            {
                stage.StatUI_Off();
                //여기서 전리품 메뉴로
                if (GameManager.instance.playerInfo.lootChance > 0)
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
