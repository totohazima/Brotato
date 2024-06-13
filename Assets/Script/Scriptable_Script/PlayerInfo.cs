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
    public bool isDie; //�÷��̾� ���
    public float doNotSpawnRange;
    #region BasicStats
    [FoldoutGroup("BasicStats")] public float maxHealth;    //�ִ� ü��
    [FoldoutGroup("BasicStats")] public float playerHealth; //���� ü��
    [FoldoutGroup("BasicStats")] public float regeneration; //ü�� ���
    [FoldoutGroup("BasicStats")] public float bloodSucking; //����
    [FoldoutGroup("BasicStats")] public float persentDamage;//�ۼ�Ʈ �����
    [FoldoutGroup("BasicStats")] public float meleeDamage;  //�ٰŸ� �����
    [FoldoutGroup("BasicStats")] public float rangeDamage;  //���Ÿ� �����
    [FoldoutGroup("BasicStats")] public float elementalDamage;//���� �����
    [FoldoutGroup("BasicStats")] public float attackSpeed;  //���� �ӵ�
    [FoldoutGroup("BasicStats")] public float criticalChance;//ġ��Ÿ Ȯ��
    [FoldoutGroup("BasicStats")] public float engine;       //�����Ͼ�
    [FoldoutGroup("BasicStats")] public float range;        //����(��Ÿ�)
    [FoldoutGroup("BasicStats")] public float armor;        //����
    [FoldoutGroup("BasicStats")] public float evasion;      //ȸ��
    [FoldoutGroup("BasicStats")] public float accuracy;     //���߷�
    [FoldoutGroup("BasicStats")] public float lucky;        //���
    [FoldoutGroup("BasicStats")] public float harvest;      //��Ȯ
    [FoldoutGroup("BasicStats")] public float speed;        //�̵��ӵ�
    #endregion
    #region DetailStats
    [FoldoutGroup("DetailStats")] public float consumableHeal; //�Ҹ�ǰ ȸ����
    [FoldoutGroup("DetailStats")] public float meterialHeal;   //��� ȹ�� �� ȸ�� Ȯ��
    [FoldoutGroup("DetailStats")] public float expGain;        //����ġ �߰� ȹ�淮
    [FoldoutGroup("DetailStats")] public float magnetRange;    //�ڼ� ����
    [FoldoutGroup("DetailStats")] public float priceSale;      //���� ���� ���ҷ�
    [FoldoutGroup("DetailStats")] public float explosiveDamage;//���� �����
    [FoldoutGroup("DetailStats")] public float explosiveSize;  //���� ����
    [FoldoutGroup("DetailStats")] public int chain;            //����
    [FoldoutGroup("DetailStats")] public int penetrate;        //����
    [FoldoutGroup("DetailStats")] public float penetrateDamage;//���� �����
    [FoldoutGroup("DetailStats")] public float bossDamage;     //���� �����
    [FoldoutGroup("DetailStats")] public float knockBack;      //�˹�
    [FoldoutGroup("DetailStats")] public float doubleMeterial; //��� �ι� ȹ�� Ȯ��
    [FoldoutGroup("DetailStats")] public float lootInMeterial; //���� ���� �� ��� ȹ�淮
    [FoldoutGroup("DetailStats")] public float freeReroll;     //���� ����
    [FoldoutGroup("DetailStats")] public float tree;           //����
    [FoldoutGroup("DetailStats")] public float enemyAmount;    //�� ��
    [FoldoutGroup("DetailStats")] public float enemySpeed;     //�� �ӵ�
    [FoldoutGroup("DetailStats")] public float instantMagnet;  //��� ��� ȹ��
    [FoldoutGroup("DetailStats")] public Stat.ItemTag[] characterItemTags;
    #endregion
    #region UpgradeRiseStats
    [FoldoutGroup("UpgradeRiseStats")] public float maxHealth_Upgrade;    //�ִ� ü��
    [FoldoutGroup("UpgradeRiseStats")] public float playerHealth_Upgrade; //���� ü��
    [FoldoutGroup("UpgradeRiseStats")] public float regeneration_Upgrade; //ü�� ���
    [FoldoutGroup("UpgradeRiseStats")] public float bloodSucking_Upgrade; //����
    [FoldoutGroup("UpgradeRiseStats")] public float persentDamage_Upgrade;//�ۼ�Ʈ �����
    [FoldoutGroup("UpgradeRiseStats")] public float meleeDamage_Upgrade;  //�ٰŸ� �����
    [FoldoutGroup("UpgradeRiseStats")] public float rangeDamage_Upgrade;  //���Ÿ� �����
    [FoldoutGroup("UpgradeRiseStats")] public float elementalDamage_Upgrade;//���� �����
    [FoldoutGroup("UpgradeRiseStats")] public float attackSpeed_Upgrade;  //���� �ӵ�
    [FoldoutGroup("UpgradeRiseStats")] public float criticalChance_Upgrade;//ġ��Ÿ Ȯ��
    [FoldoutGroup("UpgradeRiseStats")] public float engine_Upgrade;       //�����Ͼ�
    [FoldoutGroup("UpgradeRiseStats")] public float range_Upgrade;        //����(��Ÿ�)
    [FoldoutGroup("UpgradeRiseStats")] public float armor_Upgrade;        //����
    [FoldoutGroup("UpgradeRiseStats")] public float evasion_Upgrade;      //ȸ��
    [FoldoutGroup("UpgradeRiseStats")] public float accuracy_Upgrade;     //���߷�
    [FoldoutGroup("UpgradeRiseStats")] public float lucky_Upgrade;        //���
    [FoldoutGroup("UpgradeRiseStats")] public float harvest_Upgrade;      //��Ȯ
    [FoldoutGroup("UpgradeRiseStats")] public float speed_Upgrade;        //�̵��ӵ�
    #endregion
    [Header("Level")]
    public int playerLevel;
    public int levelUpChance; //���̺� ���� �� ���� �� �� Ƚ��
    public int lootChance; //���ڱ� ����
    public float curExp;  //���� ����ġ
    public float maxExp;  //�ִ� ����ġ
    [HideInInspector] public float overExp; //������ �� ���� ����ġ
    [Header("Money")]
    public int money; //��
    public int interest; //����
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
    [FoldoutGroup("ActiveItems")] public bool isUglyTooth; //������ �̻� ���� �� �� Ÿ�� �ø��� ���ǵ� -10% (3ȸ ��ø)
    [FoldoutGroup("ActiveItems")] public bool isLumberJack; //���� �� ���� ���� �� ������ �� �濡 �ı���
    [FoldoutGroup("ActiveItems")] public bool isWeirdGhost; // �̻��� ���� ���� �� true�� �Ǹ� ���̺� ���� �� ü���� 1�̵� 
    [FoldoutGroup("ActiveItems")] public int minesCount; //���� ������ ����
    [FoldoutGroup("ActiveItems")] public int turretCount; //�ͷ� ������ ����
    [FoldoutGroup("ActiveItems")] public int snakeCount; //�� ������ ���� (�ϳ� �� ȭ�� ���� �� �����Ǵ� ���� �� +1)
    [FoldoutGroup("ActiveItems")] public bool isScaredSausage;
    [FoldoutGroup("ActiveItems")] public float scaredSausageChance; //�̸��� �ҽ����� ȭ�� Ȯ�� (�ϳ��� 25%)
    [FoldoutGroup("ActiveItems")] public float scaredSausageDamage; //�̸��� �ҽ����� ƽ �� ȭ�� ����� (�ϳ��� 1)
    [FoldoutGroup("ActiveItems")] public int scaredSausageDamageCount; //�̸��� �ҽ����� ƽ Ƚ�� 
    #endregion
    /// <summary>
    /// �÷��̾� �ǰ� �� ����� ���
    /// </summary>
    /// <param name="damage">���� ������ ���� �����</param>
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
            //������ 0 �ʰ�
            if (GameManager.instance.player_Info.armor > 0)
            {
                float enduce = 1 / (1 + (GameManager.instance.player_Info.armor / 15));
                enduce = 1 - enduce;
                damages -= damages * enduce;
                GameManager.instance.playerInfo.playerHealth -= damages;
            }
            //������ 0 �̸�
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
            string dodgeText = "<color=#4CFF52>ȸ��</color>";
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
            Debug.Log(itemType.ToString() + " ã�� �� ����");
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
    /// ��Ʈ���� ĳ���� �� ���� �о����
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
    /// ���� ��Ʈ, ������, ���׷��̵�� �ö󰡴� ���� ���
    /// </summary>
    public void StatCalculate()
    {
        if (GameManager.instance.player_Info != null)
            ItemSearch();

        Player_Action info = GameManager.instance.player_Info;
        //������ ���� ���
        for (int i = 0; i < info.itemInventory.Count; i++)
        {
            for (int k = 0; k < info.itemInventory[i].curCount; k++)
            {
                for (int j = 0; j < info.itemInventory[i].riseCount; j++)
                {
                    switch (info.itemInventory[i].riseStat[j])
                    {
                        case Stat.PlayerStat.MAXHEALTH:
                            if (GameManager.instance.character == Player.Character.RANGER) //������ ü�� ������ -25%
                                maxHealth += info.itemInventory[i].riseStats[j] * 0.75f;
                            else
                                maxHealth += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.REGENERATION:
                            if (GameManager.instance.character == Player.Character.BULL) //Ȳ�� ��� ������ +50%
                                regeneration += info.itemInventory[i].riseStats[j] * 1.5f;
                            else
                                regeneration += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.BLOOD_SUCKING:
                            bloodSucking += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PERSENT_DAMAGE:
                            if (GameManager.instance.character == Player.Character.ENGINEER) //�����Ͼ� ����� ������ -50%
                                persentDamage += info.itemInventory[i].riseStats[j] * 0.5f;
                            else
                                persentDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.MELEE_DAMAGE:
                            meleeDamage += info.itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE_DAMAGE:
                            if (GameManager.instance.character == Player.Character.RANGER) //������ ���Ÿ� ����� ������ +50%
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
                            if (GameManager.instance.character == Player.Character.ENGINEER) //�����Ͼ� �����Ͼ ������ +25%
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

        //���� ��Ʈ ���� ���
        SettCalculate();
        void SettCalculate()
        {
            GameManager game = GameManager.instance;
            float riseStat = 0;
            //���� = ȸ��
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
            //���� = �����Ͼ
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
            if (GameManager.instance.character == Player.Character.ENGINEER) //�����Ͼ� �����Ͼ ������ +25%
                engine += riseStat * 1.25f;
            else
                engine += riseStat;
            riseStat = 0;
            //�� = ����
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
            //���߹� = ���� ũ��
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
            //��Ȯ = ġ��Ÿ��
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
            //���� = ü��
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
            if (GameManager.instance.character == Player.Character.RANGER) //������ �ִ� ü�� ������ -25%
                maxHealth += riseStat * 0.75f;
            else
                maxHealth += riseStat;
            riseStat = 0;
            //���� = ���� �����
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
                //���� ���Ⱑ ���� ���
                if (isSame == false)
                {
                    sortWeapon.Add(myWeapon.index);
                }
            }

            attackSpeed += 20 * sortWeapon.Count;
        }
    }

    /// <summary>
    /// ������ ���
    /// </summary>
    private int ghostCount = 0; //�̻��� ������ ȹ���ߴ��� üũ�ϱ� ���� ����
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
    /// ���� ��Ʈ ���
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
    /// PlayerInfo�� �ʱ�ȭ�ϴ� �Լ�
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
