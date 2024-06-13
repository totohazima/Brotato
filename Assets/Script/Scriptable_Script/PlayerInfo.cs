using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "GameDataBase/playerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("PlayerState")]
    public bool isDie; //플레이어 사망
    public float doNotSpawnRange;
    #region BasicStats
    [FoldoutGroup("BasicStats")] public float maxHealth;    //최대 체력
    [FoldoutGroup("BasicStats")] public float playerHealth; //현재 체력
    [FoldoutGroup("BasicStats")] public float regeneration; //체력 재생
    [FoldoutGroup("BasicStats")] public float bloodSucking; //흡혈
    [FoldoutGroup("BasicStats")] public float persentDamage;//퍼센트 대미지
    [FoldoutGroup("BasicStats")] public float meleeDamage;  //근거리 대미지
    [FoldoutGroup("BasicStats")] public float rangeDamage;  //원거리 대미지
    [FoldoutGroup("BasicStats")] public float elementalDamage;//원소 대미지
    [FoldoutGroup("BasicStats")] public float attackSpeed;  //공격 속도
    [FoldoutGroup("BasicStats")] public float criticalChance;//치명타 확률
    [FoldoutGroup("BasicStats")] public float engine;       //엔지니어
    [FoldoutGroup("BasicStats")] public float range;        //범위(사거리)
    [FoldoutGroup("BasicStats")] public float armor;        //방어력
    [FoldoutGroup("BasicStats")] public float evasion;      //회피
    [FoldoutGroup("BasicStats")] public float accuracy;     //명중률
    [FoldoutGroup("BasicStats")] public float lucky;        //행운
    [FoldoutGroup("BasicStats")] public float harvest;      //수확
    [FoldoutGroup("BasicStats")] public float speed;        //이동속도
    #endregion
    #region DetailStats
    [FoldoutGroup("DetailStats")] public float consumableHeal; //소모품 회복량
    [FoldoutGroup("DetailStats")] public float meterialHeal;   //재료 획득 시 회복 확률
    [FoldoutGroup("DetailStats")] public float expGain;        //경험치 추가 획득량
    [FoldoutGroup("DetailStats")] public float magnetRange;    //자석 범위
    [FoldoutGroup("DetailStats")] public float priceSale;      //상점 가격 감소량
    [FoldoutGroup("DetailStats")] public float explosiveDamage;//폭발 대미지
    [FoldoutGroup("DetailStats")] public float explosiveSize;  //폭발 범위
    [FoldoutGroup("DetailStats")] public int chain;            //연쇄
    [FoldoutGroup("DetailStats")] public int penetrate;        //관통
    [FoldoutGroup("DetailStats")] public float penetrateDamage;//관통 대미지
    [FoldoutGroup("DetailStats")] public float bossDamage;     //보스 대미지
    [FoldoutGroup("DetailStats")] public float knockBack;      //넉백
    [FoldoutGroup("DetailStats")] public float doubleMeterial; //재료 두배 획득 확률
    [FoldoutGroup("DetailStats")] public float lootInMeterial; //상자 습득 시 재료 획득량
    [FoldoutGroup("DetailStats")] public float freeReroll;     //무료 리롤
    [FoldoutGroup("DetailStats")] public float tree;           //나무
    [FoldoutGroup("DetailStats")] public float enemyAmount;    //적 수
    [FoldoutGroup("DetailStats")] public float enemySpeed;     //적 속도
    [FoldoutGroup("DetailStats")] public float instantMagnet;  //재료 즉시 획득
    [FoldoutGroup("DetailStats")] public Stat.ItemTag[] characterItemTags;
    #endregion
    #region UpgradeRiseStats
    [FoldoutGroup("UpgradeRiseStats")] public float maxHealth_Upgrade;    //최대 체력
    [FoldoutGroup("UpgradeRiseStats")] public float playerHealth_Upgrade; //현재 체력
    [FoldoutGroup("UpgradeRiseStats")] public float regeneration_Upgrade; //체력 재생
    [FoldoutGroup("UpgradeRiseStats")] public float bloodSucking_Upgrade; //흡혈
    [FoldoutGroup("UpgradeRiseStats")] public float persentDamage_Upgrade;//퍼센트 대미지
    [FoldoutGroup("UpgradeRiseStats")] public float meleeDamage_Upgrade;  //근거리 대미지
    [FoldoutGroup("UpgradeRiseStats")] public float rangeDamage_Upgrade;  //원거리 대미지
    [FoldoutGroup("UpgradeRiseStats")] public float elementalDamage_Upgrade;//원소 대미지
    [FoldoutGroup("UpgradeRiseStats")] public float attackSpeed_Upgrade;  //공격 속도
    [FoldoutGroup("UpgradeRiseStats")] public float criticalChance_Upgrade;//치명타 확률
    [FoldoutGroup("UpgradeRiseStats")] public float engine_Upgrade;       //엔지니어
    [FoldoutGroup("UpgradeRiseStats")] public float range_Upgrade;        //범위(사거리)
    [FoldoutGroup("UpgradeRiseStats")] public float armor_Upgrade;        //방어력
    [FoldoutGroup("UpgradeRiseStats")] public float evasion_Upgrade;      //회피
    [FoldoutGroup("UpgradeRiseStats")] public float accuracy_Upgrade;     //명중률
    [FoldoutGroup("UpgradeRiseStats")] public float lucky_Upgrade;        //행운
    [FoldoutGroup("UpgradeRiseStats")] public float harvest_Upgrade;      //수확
    [FoldoutGroup("UpgradeRiseStats")] public float speed_Upgrade;        //이동속도
    #endregion
    [Header("Level")]
    public int playerLevel;
    public int levelUpChance; //웨이브 종료 후 레벨 업 할 횟수
    public int lootChance; //상자깡 찬스
    public float curExp;  //현재 경험치
    public float maxExp;  //최대 경험치
    [HideInInspector] public float overExp; //레벨업 후 남은 경험치
    [Header("Money")]
    public int money; //돈
    public int interest; //이자
    [Header("#Weapon_Info")]
    [HideInInspector]public Vector2 engineerBuildingPos;
    [FoldoutGroup("WeaponSett")] public int unArmed_Set;
    [FoldoutGroup("WeaponSett")] public int tool_Set;
    [FoldoutGroup("WeaponSett")] public int gun_Set;
    [FoldoutGroup("WeaponSett")] public int explosive_Set;
    [FoldoutGroup("WeaponSett")] public int precision_Set;
    [FoldoutGroup("WeaponSett")] public int native_Set;
    [FoldoutGroup("WeaponSett")] public int elemental_Set;
    [Header("#Item_Info")]
    public ItemGroup_Scriptable itemGroup_Scriptable;
    public Item invenItem;
    #region ActiveItems
    [FoldoutGroup("ActiveItems")] public bool isUglyTooth; //못생긴 이빨 구매 시 적 타격 시마다 스피드 -10% (3회 중첩)
    [FoldoutGroup("ActiveItems")] public bool isLumberJack; //럼버 잭 셔츠 구매 시 나무가 한 방에 파괴됨
    [FoldoutGroup("ActiveItems")] public bool isWeirdGhost; // 이상한 유령 구매 시 true가 되며 웨이브 시작 시 체력이 1이됨 
    [FoldoutGroup("ActiveItems")] public int minesCount; //지뢰 아이템 갯수
    [FoldoutGroup("ActiveItems")] public int turretCount; //터렛 아이템 갯수
    [FoldoutGroup("ActiveItems")] public int snakeCount; //뱀 아이템 갯수 (하나 당 화상 적용 시 전염되는 몬스터 수 +1)
    [FoldoutGroup("ActiveItems")] public bool isScaredSausage;
    [FoldoutGroup("ActiveItems")] public float scaredSausageChance; //겁먹은 소시지의 화상 확률 (하나당 25%)
    [FoldoutGroup("ActiveItems")] public float scaredSausageDamage; //겁먹은 소시지의 틱 당 화상 대미지 (하나당 1)
    [FoldoutGroup("ActiveItems")] public int scaredSausageDamageCount; //겁먹은 소시지의 틱 횟수 
    #endregion
    /// <summary>
    /// 플레이어 피격 시 대미지 계산
    /// </summary>
    /// <param name="damage">적이 나한테 입힌 대미지</param>
    public void HitCalculate(float damage)
    {
        float damages = Mathf.Round(damage);
        float hit, dodge;
        if (GameManager.instance.player_Info.evasion >= 60)
        {
            dodge = 60;
            hit = 100 - dodge;
        }
        else
        {
            dodge = GameManager.instance.player_Info.evasion;
            hit = 100 - dodge;
        }
        float[] chance = { hit, dodge };
        int index = Judgment(chance);
        if (index == 0)
        {
            //방어력이 0 초과
            if (GameManager.instance.player_Info.armor > 0)
            {
                float enduce = 1 / (1 + (GameManager.instance.player_Info.armor / 15));
                enduce = 1 - enduce;
                damages -= damages * enduce;
                GameManager.instance.playerInfo.playerHealth -= damages;
            }
            //방어력이 0 미만
            else if (GameManager.instance.player_Info.armor < 0)
            {
                float armor = Mathf.Abs(GameManager.instance.player_Info.armor);
                float enduce = 1 / (1 + (armor / 15));
                enduce = 1 + (1 - enduce);
                damages = (damages * enduce);
                GameManager.instance.playerInfo.playerHealth -= damages;
            }
            else
            {
                GameManager.instance.playerInfo.playerHealth -= damages;
            }
        }
        else
        {
            string dodgeText = "<color=#4CFF52>회피</color>";
            Transform text = DamageTextManager.instance.TextCreate(0, dodgeText).transform;
            text.position = GameManager.instance.player_Info.transform.position;
        }
    }
    public void ItemObtain(Item.ItemType itemType)
    {
        bool isGet = false;
        ItemScrip getItem = null;
        int index = 0;
        for (int i = 0; i < itemGroup_Scriptable.items.Length; i++)
        {
            if (itemGroup_Scriptable.items[i].itemCode == itemType)
            {
                index = i;
                getItem = itemGroup_Scriptable.items[i];
            }
        }
        if (getItem == null)
        {
            Debug.Log(itemType.ToString() + " 찾을 수 없음");
            return;
        }

        Item checkItem = null;

        for (int i = 0; i < GameManager.instance.player_Info.itemInventory.Count; i++)
        {
            Item Item = GameManager.instance.player_Info.itemInventory[i];
            if (getItem.itemCode == Item.itemType)
            {
                checkItem = Item;
                isGet = true;
            }
        }

        if (isGet == false)
        {
            ItemScrip item = GameManager.instance.playerInfo.itemGroup_Scriptable.items[index];
            GameObject objItem = Instantiate(invenItem.gameObject);
            objItem.transform.SetParent(ItemManager.instance.transform);
            Item invenItems = objItem.GetComponent<Item>();
            invenItems.Init(item);
            invenItems.curCount++;
            GameManager.instance.player_Info.itemInventory.Add(invenItems);

            GameManager.instance.player_Info.StatCalculate();
        }
        else if (isGet == true)
        {
            checkItem.curCount++;
            GameManager.instance.player_Info.StatCalculate();
        }

    }
   
    /// <summary>
    /// 시트에서 캐릭터 별 스탯 읽어오기
    /// </summary>
    public void StatImport(Player.Character index)
    {
        PlayerStatInfoTable.Data import = null;
        for (int i = 0; i < GameManager.instance.gameDataBase.playerStatInfoTable.table.Length; i++)
        {
            if(GameManager.instance.gameDataBase.playerStatInfoTable.table[i].playerCode == index)
            {
                import = GameManager.instance.gameDataBase.playerStatInfoTable.table[i];
            }
        }
        
        maxHealth = import.health;
        regeneration = import.hpRegen;
        bloodSucking = import.bloodSucking;
        persentDamage = import.persentDamage;
        meleeDamage = import.meleeDamage;
        rangeDamage = import.rangeDamage;
        elementalDamage = import.elementalDamage;
        attackSpeed = import.attackSpeed;
        criticalChance = import.criticalChance;
        engine = import.engine;
        range = import.range;
        armor = import.armor;
        evasion = import.evasion;
        accuracy = import.accuracy;
        lucky = import.lucky;
        harvest = import.harvest;
        speed = import.speed;

        consumableHeal = import.consumableHeal;
        meterialHeal = import.meterialHeal;
        expGain = import.expGain;
        magnetRange = import.magnetRange;
        priceSale = import.priceSale;
        explosiveDamage = import.explosiveDamage;
        explosiveSize = import.explosiveSize;
        chain = import.chain;
        penetrate = import.penetrate;
        penetrateDamage = import.penetrateDamage;
        bossDamage = import.bossDamage;
        knockBack = import.knockBack;
        doubleMeterial = import.doubleMeterial;
        lootInMeterial = import.lootInMeterial;
        freeReroll = import.freeReroll;
        tree = import.tree;
        enemyAmount = import.enemyAmount;
        enemySpeed = import.enemySpeed;
        instantMagnet = import.instantMagnet;

        characterItemTags = new Stat.ItemTag[import.itemTags.Length];
        for (int i = 0; i < import.itemTags.Length; i++)
        {
            characterItemTags[i] = import.itemTags[i];
        }
        WeaponSetSearch();
        StatCalculate();
    }

    /// <summary>
    /// 무기 세트, 아이템, 업그레이드로 올라가는 스탯 계산
    /// </summary>
    public void StatCalculate()
    {
        if (GameManager.instance.player_Info != null)
            ItemSearch();

        Player_Action info = GameManager.instance.player_Info;
        //아이템 스탯 계산
        for (int i = 0; i < info.itemInventory.Count; i++)
        {
            for (int k = 0; k < info.itemInventory[i].curCount; k++)
            {
                for (int j = 0; j < info.itemInventory[i].riseCount; j++)
                {
                    switch (info.itemInventory[i].riseStat[j])
                    {
                        case Stat.PlayerStat.MAXHEALTH:
                            if (GameManager.instance.character == Player.Character.RANGER) //레인저 체력 증가량 -25%
                                maxHealth += info.itemInventory[i].riseStats[j] * 0.75f;
                            else
                                maxHealth += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.REGENERATION:
                            if (GameManager.instance.character == Player.Character.BULL) //황소 재생 증가량 +50%
                                regeneration += info.itemInventory[i].riseStats[j] * 1.5f;
                            else
                                regeneration += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.BLOOD_SUCKING:
                            bloodSucking += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PERSENT_DAMAGE:
                            if (GameManager.instance.character == Player.Character.ENGINEER) //엔지니어 대미지 증가량 -50%
                                persentDamage += info.itemInventory[i].riseStats[j] * 0.5f;
                            else
                                persentDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.MELEE_DAMAGE:
                            meleeDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE_DAMAGE:
                            if (GameManager.instance.character == Player.Character.RANGER) //레인저 원거리 대미지 증가량 +50%
                                rangeDamage += info.itemInventory[i].riseStats[j] * 1.5f;
                            else
                                rangeDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ELEMENTAL_DAMAGE:
                            elementalDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ATTACK_SPEED:
                            attackSpeed += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CRITICAL_CHANCE:
                            criticalChance += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ENGINE:
                            if (GameManager.instance.character == Player.Character.ENGINEER) //엔지니어 엔지니어링 증가량 +25%
                                engine += info.itemInventory[i].riseStats[j] * 1.25f;
                            else
                                engine += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE:
                            range += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ARMOR:
                            armor += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EVASION:
                            evasion += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ACCURACY:
                            accuracy += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.LUCKY:
                            lucky += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.HARVEST:
                            harvest += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.SPEED:
                            speed += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CONSUMABLE_HEAL:
                            consumableHeal += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.METERIAL_HEAL:
                            meterialHeal += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXP_GAIN:
                            expGain += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.MAGNET_RANGE:
                            magnetRange += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PRICE_SALE:
                            priceSale += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_DAMAGE:
                            explosiveDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_SIZE:
                            explosiveSize += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CHAIN:
                            chain += (int)info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRATE:
                            penetrate += (int)info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRTE_DAMAGE:
                            penetrateDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.BOSS_DAMAGE:
                            bossDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.KNOCK_BACK:
                            knockBack += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.DOUBLE_METERIAL:
                            doubleMeterial += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.LOOT_IN_METERIAL:
                            lootInMeterial += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.FREE_REROLL:
                            freeReroll += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.TREE:
                            tree += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ENEMY_AMOUNT:
                            enemyAmount += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ENEMY_SPEED:
                            enemySpeed += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.INSTNAT_MAGNET:
                            instantMagnet += info.itemInventory[i].riseStats[j];
                            break;
                    }
                }
                for (int j = 0; j < info.itemInventory[i].decreaseCount; j++)
                {
                    switch (info.itemInventory[i].decreaseStat[j])
                    {
                        case Stat.PlayerStat.MAXHEALTH:
                            maxHealth -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.REGENERATION:
                            regeneration -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.BLOOD_SUCKING:
                            bloodSucking -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PERSENT_DAMAGE:
                            persentDamage -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.MELEE_DAMAGE:
                            meleeDamage -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE_DAMAGE:
                            rangeDamage -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ELEMENTAL_DAMAGE:
                            elementalDamage -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ATTACK_SPEED:
                            attackSpeed -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.CRITICAL_CHANCE:
                            criticalChance -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ENGINE:
                            engine -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE:
                            range -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ARMOR:
                            armor -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EVASION:
                            evasion -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ACCURACY:
                            accuracy -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.LUCKY:
                            lucky -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.HARVEST:
                            harvest -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.SPEED:
                            speed -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.CONSUMABLE_HEAL:
                            consumableHeal -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.METERIAL_HEAL:
                            meterialHeal -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXP_GAIN:
                            expGain -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.MAGNET_RANGE:
                            magnetRange -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PRICE_SALE:
                            priceSale -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_DAMAGE:
                            explosiveDamage -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_SIZE:
                            explosiveSize -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.CHAIN:
                            chain -= (int)info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRATE:
                            penetrate -= (int)info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRTE_DAMAGE:
                            penetrateDamage -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.BOSS_DAMAGE:
                            bossDamage -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.KNOCK_BACK:
                            knockBack -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.DOUBLE_METERIAL:
                            doubleMeterial -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.LOOT_IN_METERIAL:
                            lootInMeterial -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.FREE_REROLL:
                            freeReroll -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.TREE:
                            tree -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ENEMY_AMOUNT:
                            enemyAmount -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ENEMY_SPEED:
                            enemySpeed -= info.itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.INSTNAT_MAGNET:
                            instantMagnet -= info.itemInventory[i].decreaseStats[j];
                            break;
                    }
                }
            }
        }

        //무기 세트 스탯 계산
        SettCalculate();
        void SettCalculate()
        {
            GameManager game = GameManager.instance;
            float riseStat = 0;
            //비무장 = 회피
            if (game.playerInfo.unArmed_Set == 2)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.unArmed_Set == 3)
            {
                riseStat = 6;
            }
            else if (game.playerInfo.unArmed_Set == 4)
            {
                riseStat = 9;
            }
            else if (game.playerInfo.unArmed_Set == 5)
            {
                riseStat = 12;
            }
            else if (game.playerInfo.unArmed_Set >= 6)
            {
                riseStat = 15;
            }
            evasion += riseStat;
            riseStat = 0;
            //도구 = 엔지니어링
            if (game.playerInfo.tool_Set == 2)
            {
                riseStat = 1;
            }
            else if (game.playerInfo.tool_Set == 3)
            {
                riseStat = 2;
            }
            else if (game.playerInfo.tool_Set == 4)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.tool_Set == 5)
            {
                riseStat = 4;
            }
            else if (game.playerInfo.tool_Set >= 6)
            {
                riseStat = 5;
            }
            if (GameManager.instance.character == Player.Character.ENGINEER) //엔지니어 엔지니어링 증가량 +25%
                engine += riseStat * 1.25f;
            else
                engine += riseStat;
            riseStat = 0;
            //총 = 범위
            if (game.playerInfo.gun_Set == 2)
            {
                riseStat = 10;
            }
            else if (game.playerInfo.gun_Set == 3)
            {
                riseStat = 20;
            }
            else if (game.playerInfo.gun_Set == 4)
            {
                riseStat = 30;
            }
            else if (game.playerInfo.gun_Set == 5)
            {
                riseStat = 40;
            }
            else if (game.playerInfo.gun_Set >= 6)
            {
                riseStat = 50;
            }
            range += riseStat;
            riseStat = 0;
            //폭발물 = 폭발 크기
            if (game.playerInfo.explosive_Set == 2)
            {
                riseStat = 5;
            }
            else if (game.playerInfo.explosive_Set == 3)
            {
                riseStat = 10;
            }
            else if (game.playerInfo.explosive_Set == 4)
            {
                riseStat = 15;
            }
            else if (game.playerInfo.explosive_Set == 5)
            {
                riseStat = 20;
            }
            else if (game.playerInfo.explosive_Set >= 6)
            {
                riseStat = 25;
            }
            explosiveSize += riseStat;
            riseStat = 0;
            //정확 = 치명타율
            if (game.playerInfo.precision_Set == 2)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.precision_Set == 3)
            {
                riseStat = 6;
            }
            else if (game.playerInfo.precision_Set == 4)
            {
                riseStat = 9;

            }
            else if (game.playerInfo.precision_Set == 5)
            {
                riseStat = 12;
            }
            else if (game.playerInfo.precision_Set >= 6)
            {
                riseStat = 15;
            }
            criticalChance += riseStat;
            riseStat = 0;
            //원시 = 체력
            if (game.playerInfo.native_Set == 2)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.native_Set == 3)
            {
                riseStat = 6;
            }
            else if (game.playerInfo.native_Set == 4)
            {
                riseStat = 9;
            }
            else if (game.playerInfo.native_Set == 5)
            {
                riseStat = 12;
            }
            else if (game.playerInfo.native_Set >= 6)
            {
                riseStat = 15;
            }
            if (GameManager.instance.character == Player.Character.RANGER) //레인저 최대 체력 증가량 -25%
                maxHealth += riseStat * 0.75f;
            else
                maxHealth += riseStat;
            riseStat = 0;
            //원소 = 원소 대미지
            if (game.playerInfo.elemental_Set == 2)
            {
                riseStat = 1;
            }
            else if (game.playerInfo.elemental_Set == 3)
            {
                riseStat = 2;
            }
            else if (game.playerInfo.elemental_Set == 4)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.elemental_Set == 5)
            {
                riseStat = 4;
            }
            else if (game.playerInfo.elemental_Set >= 6)
            {
                riseStat = 5;
            }
            elementalDamage += riseStat;
            riseStat = 0;
        }

        harvest *= 1 + (GameManager.instance.harvestVariance_Amount / 100);
        harvest = Mathf.Round(harvest);

        if (GameManager.instance.character == Player.Character.MULTITASKER)
        {
            persentDamage -= 5 * GameManager.instance.player_Info.weapons.Count;
        }
        if (GameManager.instance.character == Player.Character.GLADIATOR)
        {
            bool isSame = false;
            List<Weapon.Weapons> sortWeapon = new List<Weapon.Weapons>();
            for (int i = 0; i < GameManager.instance.player_Info.weapons.Count; i++)
            {
                Weapon_Action myWeapon = GameManager.instance.player_Info.weapons[i].GetComponent<Weapon_Action>();

                for (int j = 0; j < sortWeapon.Count; j++)
                {
                    if (myWeapon.index == sortWeapon[j])
                    {
                        isSame = true;
                    }
                }
                //같은 무기가 없을 경우
                if (isSame == false)
                {
                    sortWeapon.Add(myWeapon.index);
                }
            }

            attackSpeed += 20 * sortWeapon.Count;
        }
    }

    /// <summary>
    /// 아이템 계산
    /// </summary>
    private int ghostCount = 0; //이상한 유령을 획득했는지 체크하기 위한 변수
    public void ItemSearch()
    {
        isUglyTooth = false;
        isLumberJack = false;
        isWeirdGhost = false;
        minesCount = 0;
        turretCount = 0;
        snakeCount = 0;
        isScaredSausage = false;
        scaredSausageChance = 0;
        scaredSausageDamage = 0;
        scaredSausageDamageCount = 0;

        List<Item> item = GameManager.instance.player_Info.itemInventory;

        for (int i = 0; i < item.Count; i++)
        {
            switch (item[i].itemType)
            {
                case Item.ItemType.UGLY_TOOTH:
                    isUglyTooth = true;
                    break;
                case Item.ItemType.LUMBERJACK_SHIRT:
                    isLumberJack = true;
                    break;
                case Item.ItemType.WEIRD_GHOST:
                    if (ghostCount != item[i].curCount)
                    {
                        isWeirdGhost = true;
                        ghostCount = item[i].curCount;
                    }
                    break;
                case Item.ItemType.LAND_MINES:
                    minesCount = item[i].curCount;
                    break;
                case Item.ItemType.TURRET:
                    turretCount = item[i].curCount;
                    break;
                case Item.ItemType.SNAKE:
                    snakeCount = item[i].curCount;
                    break;
                case Item.ItemType.SCARED_SAUSAGE:
                    isScaredSausage = true;
                    scaredSausageChance = item[i].curCount * 25f;
                    scaredSausageDamage = item[i].curCount * 1;
                    scaredSausageDamageCount = item[i].curCount * 3;
                    break;
            }

        }
    }

    /// <summary>
    /// 무기 세트 계산
    /// </summary>
    public void WeaponSetSearch()
    {
        unArmed_Set = 0;
        tool_Set = 0;
        gun_Set = 0;
        explosive_Set = 0;
        precision_Set = 0;
        native_Set = 0;
        elemental_Set = 0;

        if (GameManager.instance.player_Info != null)
        {
            Weapon_Action[] weapon = new Weapon_Action[GameManager.instance.player_Info.weapons.Count];
            for (int i = 0; i < weapon.Length; i++)
            {
                weapon[i] = GameManager.instance.player_Info.weapons[i].GetComponent<Weapon_Action>();
            }

            for (int i = 0; i < weapon.Length; i++)
            {
                for (int j = 0; j < weapon[i].setTypes.Length; j++)
                {
                    switch (weapon[i].setTypes[j])
                    {
                        case Weapon.SettType.UNARMED:
                            if (unArmed_Set < 6)
                                unArmed_Set++;
                            break;
                        case Weapon.SettType.TOOL:
                            if (tool_Set < 6)
                                tool_Set++;
                            break;
                        case Weapon.SettType.GUN:
                            if (gun_Set < 6)
                                gun_Set++;
                            break;
                        case Weapon.SettType.EXPLOSIVE:
                            if (explosive_Set < 6)
                                explosive_Set++;
                            break;
                        case Weapon.SettType.PRECISION:
                            if (precision_Set < 6)
                                precision_Set++;
                            break;
                        case Weapon.SettType.NATIVE:
                            if (native_Set < 6)
                                native_Set++;
                            break;
                        case Weapon.SettType.ELEMENTALS:
                            if (elemental_Set < 6)
                                elemental_Set++;
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// PlayerInfo를 초기화하는 함수
    /// </summary>
    public void Reset_PlayerInfo()
    {
        isDie = false;
        playerLevel = 0;
        levelUpChance = 0;
        lootChance = 0;
        curExp = 0;
        maxExp = 0;
        money = 0;
        interest = 0;
    }
    public void EngineerTurretPosSetting()
    {
        while (true)
        {
            float randomX = UnityEngine.Random.Range(StageManager.instance.xMin, StageManager.instance.xMax);
            float randomY = UnityEngine.Random.Range(StageManager.instance.yMin, StageManager.instance.yMax);

            Vector3 playerPos = GameManager.instance.playerTrans.position;
            Vector3 point = new Vector3(randomX, randomY);

            float distance = Vector3.Distance(playerPos, point);
            if (distance < 25)
            {
                engineerBuildingPos = point;
                break;
            }
            InfiniteLoopDetector.Run();
        }
    }
    
    private int Judgment(float[] rando)
    {
        int count = rando.Length;
        float max = 0;
        for (int i = 0; i < count; i++)
            max += rando[i];

        float range = UnityEngine.Random.Range(0f, (float)max);
        //0.1, 0.2, 30, 40
        double chance = 0;
        for (int i = 0; i < count; i++)
        {
            chance += rando[i];
            if (range > chance)
                continue;

            return i;
        }

        return -1;
    }


}
