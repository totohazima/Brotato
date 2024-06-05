using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "GameDataBase/playerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("PlayerState")]
    public bool isDie; //�÷��̾� ���
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
    public Vector2 engineerBuildingPos;
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
    public void ItemSearch()
    {
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
                    isWeirdGhost = true;
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

    public void OnInspectorGUI()
    {
        if (GUILayout.Button("PlayerInfo �ʱ�ȭ"))
        {
            Reset_PlayerInfo();
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
