using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "GameDataBase/playerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("PlayerState")]
    public bool isDie; //플레이어 사망
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
        if (GUILayout.Button("PlayerInfo 초기화"))
        {
            Reset_PlayerInfo();
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
