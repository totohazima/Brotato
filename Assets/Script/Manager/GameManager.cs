using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
using ES3Types;
using UnityEngine.SceneManagement;
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
    public PlayerInfo playerInfo;
    public Player_Action player_Info;
    public Player.Character character;
    public GameObject weaponPrefab;
    public GameObject optionUI;
    public Transform playerTrans;
    public bool isEnd; //웨이브 끝
    public bool isPause; //일시정지
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
    public SaveLoadExample easySave;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        UIUpdateManager.uiUpdates.Add(this);
        GameObject obj = Instantiate(dontDestoryOBJ);
        option = obj.transform.GetChild(1).gameObject;
        optionUI = option.gameObject;
        playerInfo.Reset_PlayerInfo();
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

        if (playerInfo.maxExp != 0 && playerInfo.playerLevel < 20)
        {
            if (playerInfo.curExp >= playerInfo.maxExp)
            {
                playerInfo.overExp = playerInfo.curExp - playerInfo.maxExp;
                playerInfo.curExp = playerInfo.overExp;
                playerInfo.overExp = 0;
                playerInfo.playerLevel++;
                playerInfo.levelUpChance++;
            }
        }

        if (isStart == true)
        {
            if (playerInfo.playerHealth <= 0)
            {
                playerInfo.isDie = true;
                StartCoroutine(Died());
            }
            else
            {
                playerInfo.isDie = false;
            }
        }
    }
    private void GameStart() //Title에서 Main으로
    {
        LoadingSceneManager.LoadScene("MainScene");
    }
    private IEnumerator Died()
    {
        easySave.DeleteData();
        isEnd = true;
        StageManager.instance.gameOverUI.SetActive(true);
        yield return 0;
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
        playerInfo.playerHealth = StageManager.instance.playerInfo.maxHealth_Origin;
        playerInfo.money = 30;
        
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
            for (int i = 0; i < playerInfo.itemGroup_Scriptable.items.Length; i++)
            {
                if(playerInfo.itemGroup_Scriptable.items[i].itemCode == Item.ItemType.LUMBERJACK_SHIRT)
                {
                    getItem = playerInfo.itemGroup_Scriptable.items[i];
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

        playerInfo.EngineerTurretPosSetting();

        playerInfo.WeaponSetSearch();
        player_Info.StatCalculate();
        LoadingSceneManager.CloseScene("MainScene");
    }

    public IEnumerator GameLoad()
    {
        yield return 0;
        isStart = true;
        if (character == Player.Character.MULTITASKER)
        {
            maxWeaponCount = 12;
        }
        else
        {
            maxWeaponCount = 6;
        }
        playerInfo.playerHealth = player_Info.maxHealth_Origin;
        StageManager.instance.waveLevel = ES3.Load<int>("Wave",easySave.saveFilePath);

        //무기 로드
        int weaponCount = ES3.Load<int>("WeaponCount", easySave.saveFilePath);
        for (int i = 0; i < weaponCount; i++)
        {
            Weapon.Weapons weapons = ES3.Load<Weapon.Weapons>($"Weapon_{i}_Code", easySave.saveFilePath);
            int saveWeaponTier = ES3.Load<int>($"Weapon_{i}_Tier", easySave.saveFilePath);
            GameObject saveWeapon = null;

            switch (weapons)
            {
                case Weapon.Weapons.PISTOL:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Pistol_Weapon");
                    break;
                case Weapon.Weapons.DOUBLESHOTGUN:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/ShotGun_Weapon");
                    break;
                case Weapon.Weapons.SPEAR:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Spear_Weapon");
                    break;
                case Weapon.Weapons.SHREDDER:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Shredder_Weapon");
                    break;
                case Weapon.Weapons.PUNCH:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Fist_Weapon");
                    break;
                case Weapon.Weapons.WRENCH:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Wrench_Weapon");
                    break;
                case Weapon.Weapons.DRIVER:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Driver_Weapon");
                    break;
                case Weapon.Weapons.WAND:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Wand_Weapon");
                    break;
                case Weapon.Weapons.TORCH:
                    saveWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Torch_Weapon");
                    break;
            }

            if(saveWeapon != null)
            {
                saveWeapon.GetComponent<Weapon>().weaponTier = saveWeaponTier;
                GameObject weapon = Instantiate(saveWeapon);
                weapon.transform.SetParent(player_Info.weaponMainPos);
                player_Info.weapons.Add(weapon);
            }
        }

        //아이템 로드
        int itemCount = ES3.Load<int>("ItemCount", easySave.saveFilePath);
        for (int i = 0; i < itemCount; i++)
        {
            Item.ItemType itemCode = ES3.Load<Item.ItemType>($"Item_{i}_Code", easySave.saveFilePath);
            int item_Count = ES3.Load<int>($"Item_{i}_Count", easySave.saveFilePath);
            ItemScrip getItem = null;
            for (int j = 0; j < playerInfo.itemGroup_Scriptable.items.Length; j++)
            {
                if (playerInfo.itemGroup_Scriptable.items[j].itemCode == itemCode)
                {
                    getItem = playerInfo.itemGroup_Scriptable.items[j];
                    break;
                }
            }

            GameObject obj = Resources.Load<GameObject>("Prefabs/Item/Item_Object");
            Transform itemObj = Instantiate(obj.transform);
            itemObj.SetParent(transform);
            Item item = itemObj.GetComponent<Item>();
            item.Init(getItem);
            item.curCount = item_Count;
            player_Info.itemInventory.Add(item);
        }

        //StageManager.instance.ShopOpen();

        playerInfo.WeaponSetSearch();
        player_Info.StatCalculate();
        easySave.isLoaded = false;
        LoadingSceneManager.CloseScene("MainScene");
    }

    
    
    public void GameManagerClear()
    {
        isEnd = false;
        isPause = false;
        isStart = false;
        harvestVariance_Amount = 0;
        player = null;
        player_Info = null;
        weapon = null;
        weapon_Obj = null;
        weaponPrefab = null;
        diffiicult = null;
        difficult_Obj = null;
    }
}
