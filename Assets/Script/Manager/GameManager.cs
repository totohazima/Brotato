using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class GameManager : MonoBehaviour, UI_Upadte
{
    public static GameManager instance;
    public GameDataBase gameDataBase;
    public CharacterSprite_Scriptable[] characterSprite;
    public Wave_Scriptable[] wave_Scriptables = new Wave_Scriptable[10];
    public bool isStart;
    [Header("#Player_Info")]
    [HideInInspector] public ForSettingPlayer player;
    [HideInInspector] public ForSettingWeapon weapon;
    [HideInInspector] public Difficult diffiicult;
    [HideInInspector] public GameObject weapon_Obj;
    [HideInInspector] public GameObject player_Obj;
    [HideInInspector] public GameObject difficult_Obj;
    [HideInInspector] public int maxWeaponCount; //최대 무기 갯수
    public PlayerAction player_Info;
    public Player.Character character;
    public GameObject weaponPrefab;
    public GameObject optionUI;
    public bool isDie; //플레이어 사망
    public bool isPause; //일시정지
    [Header("#Weapon_Info")]
    public Vector2 engineerBuildingPos;
    public int unArmed_Set;
    public int tool_Set;
    public int gun_Set;
    public int explosive_Set;
    public int precision_Set;
    public int native_Set;
    public int elemental_Set;
    [Header("#Item_Info")]
    public ItemGroup_Scriptable itemGroup_Scriptable;
    public bool isUglyTooth; //못생긴 이빨 구매 시 적 타격 시마다 스피드 -10% (3회 중첩)
    public bool isLumberJack; //럼버 잭 셔츠 구매 시 나무가 한 방에 파괴됨
    public bool isWeirdGhost; // 이상한 유령 구매 시 true가 되며 웨이브 시작 시 체력이 1이됨 
    public int minesCount; //지뢰 아이템 갯수
    public int turretCount; //터렛 아이템 갯수
    public int snakeCount; //뱀 아이템 갯수 (하나 당 화상 적용 시 전염되는 몬스터 수 +1)
    [Header("#Difficult_Info")]
    public int difficult_Level; //난이도
    public bool isSpecialEnemySpawn; //새로운 적의 출현
    public bool isEliteSpawn; //엘리트와 무리가 등장
    public int eliteEnemyWave; //엘리트와 무리가 등장하는 웨이브 수
    public float enemyRiseDamage; //적 데미지 증가치 %
    public float enemyRiseHealth; //적 체력 증가치 %
    public bool doubleBoss; //보스가 2마리 (한 마리는 체력이 25% 감소)
    [Header("#Prefab")]
    public GameObject dontDestoryOBJ;
    public GameObject option;
    [Header("#Test")] //QA를 위해 만든 public 변수
    public float harvestVariance_Amount; //수확 변동치(웨이브마다 해당수치가 변하며 수확스탯 계산식에서 마지막에 %로 곱해줌)
    public float riseHarvest_Num; //웨이브마다 수확 증가치 5 -> 5%
    public float decreaseHarvest_Num; //10웨이브 이후 수확 감소치 20 -> 20%
    public float[] weaponTierChance = new float[4];
    public float[] upgradeChance = new float[4];
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        UIUpdateManager.uiUpdates.Add(this);
        GameObject obj = Instantiate(dontDestoryOBJ);
        option = obj.transform.GetChild(1).gameObject;
        optionUI = option.gameObject;

        GameStart();
    }

    public void UI_Update()
    {
        if (player != null)
        {
            character = player.index;
        }
        if(weapon != null)
        {
            weaponPrefab = weapon.weaponPrefabs;
        }
        if(diffiicult != null)
        {
            difficult_Level = diffiicult.difficultLevel;
            DifficultInfoTable.Data import = gameDataBase.difficultInfoTable.table[difficult_Level];

            isSpecialEnemySpawn = import.specialEnemySpawn;
            isEliteSpawn = import.eliteEnemySpawn;
            eliteEnemyWave = import.eliteEnemySpawnCount;
            enemyRiseDamage = import.enemyDamageRise;
            enemyRiseHealth = import.enemyHpRise;
            doubleBoss = import.twinBoss;
        }
    }
    private void GameStart() //Title에서 Main으로
    {
        LoadingSceneManager.LoadScene("MainScene");
    }
    public IEnumerator WaveStart()
    {
        yield return 0;
        isStart = true;
        if(character == Player.Character.MULTITASKER)
        {
            maxWeaponCount = 12;
        }
        else
        {
            maxWeaponCount = 6;
        }
        StageManager.instance.curHp = StageManager.instance.playerInfo.maxHealth_Origin;
        StageManager.instance.money = 30;
        
        //시작 무기 추가
        if (weaponPrefab != null)
        {
            GameObject startWeapon = Instantiate(weaponPrefab);
            startWeapon.transform.SetParent(player_Info.weaponMainPos);
            player_Info.weapons.Add(startWeapon);
        }

        //레인저 권총 추가
        if (character == Player.Character.RANGER)
        {
            GameObject plusWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Pistol_Weapon");
            if (plusWeapon != null)
            {
                GameObject weapon = Instantiate(plusWeapon);
                weapon.transform.SetParent(player_Info.weaponMainPos);
                player_Info.weapons.Add(weapon);
            }
        }
        //엔지니어 렌치 추가
        else if (character == Player.Character.ENGINEER)
        {
            GameObject plusWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Wrench_Weapon");
            if (plusWeapon != null)
            {
                GameObject weapon = Instantiate(plusWeapon);
                weapon.transform.SetParent(player_Info.weaponMainPos);
                player_Info.weapons.Add(weapon);
            }
        }
        //평화주의자 럼버잭 셔츠 추가
        else if(character == Player.Character.PACIFIST)
        {
            ItemScrip getItem = null;
            for (int i = 0; i < itemGroup_Scriptable.items.Length; i++)
            {
                if(itemGroup_Scriptable.items[i].itemCode == Item.ItemType.LUMBERJACK_SHIRT)
                {
                    getItem = itemGroup_Scriptable.items[i];
                    break;
                }
            }

            GameObject obj = Resources.Load<GameObject>("Prefabs/Item/Item_Object");
            GameObject itemObj = Instantiate(obj);
            itemObj.transform.SetParent(transform);
            Item item = itemObj.GetComponent<Item>();
            item.Init(getItem);
            item.curCount++;
            player_Info.itemInventory.Add(item);
        }

        float randomX = Random.Range(StageManager.instance.xMin, StageManager.instance.xMax);
        float randomY = Random.Range(StageManager.instance.yMin, StageManager.instance.yMax);
        Vector3 point = new Vector3(randomX, randomY);
        engineerBuildingPos = point;

        WeaponSetSearch();
        player_Info.StatCalculate();
        LoadingSceneManager.CloseScene("MainScene");
    }
    public void ItemSearch()
    {
        List<Item> item = player_Info.itemInventory;

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

        if (player_Info != null)
        {
            Weapon_Action[] weapon = new Weapon_Action[player_Info.weapons.Count];
            for (int i = 0; i < weapon.Length; i++)
            {
                weapon[i] = StageManager.instance.playerInfo.weapons[i].GetComponent<Weapon_Action>();
            }

            for (int i = 0; i < weapon.Length; i++)
            {
                for (int j = 0; j < weapon[i].setTypes.Length; j++)
                {
                    switch (weapon[i].setTypes[j])
                    {
                        case Weapon.SettType.UNARMED:
                            unArmed_Set++;
                            break;
                        case Weapon.SettType.TOOL:
                            tool_Set++;
                            break;
                        case Weapon.SettType.GUN:
                            gun_Set++;
                            break;
                        case Weapon.SettType.EXPLOSIVE:
                            explosive_Set++;
                            break;
                        case Weapon.SettType.PRECISION:
                            precision_Set++;
                            break;
                        case Weapon.SettType.NATIVE:
                            native_Set++;
                            break;
                        case Weapon.SettType.ELEMENTALS:
                            elemental_Set++;
                            break;
                    }
                }
            }
        }
    }
}
