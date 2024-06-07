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
    [HideInInspector] public int maxWeaponCount; //�ִ� ���� ����
    public PlayerInfo playerInfo;
    public Player_Action player_Info;
    public Player.Character character;
    public GameObject weaponPrefab;
    public GameObject optionUI;
    public Transform playerTrans;
    public bool isEnd; //���̺� ��
    public bool isPause; //�Ͻ�����
    [Header("#Difficult_Info")]
    public int difficult_Level; //���̵�
    public bool isSpecialEnemySpawn; //���ο� ���� ����
    public bool isEliteSpawn; //����Ʈ�� ������ ����
    public int eliteEnemyWave; //����Ʈ�� ������ �����ϴ� ���̺� ��
    public float enemyRiseDamage; //�� ������ ����ġ %
    public float enemyRiseHealth; //�� ü�� ����ġ %
    public bool doubleBoss; //������ 2���� (�� ������ ü���� 25% ����)
    [Header("#Prefab")]
    public GameObject dontDestoryOBJ;
    public GameObject option;
    [Header("#Test")] //QA�� ���� ���� public ����
    public float harvestVariance_Amount; //��Ȯ ����ġ(���̺긶�� �ش��ġ�� ���ϸ� ��Ȯ���� ���Ŀ��� �������� %�� ������)
    public float riseHarvest_Num; //���̺긶�� ��Ȯ ����ġ 5 -> 5%
    public float decreaseHarvest_Num; //10���̺� ���� ��Ȯ ����ġ 20 -> 20%
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
    private void GameStart() //Title���� Main����
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
        
        //���� ���� �߰�
        if (weaponPrefab != null)
        {
            GameObject startWeapon = Instantiate(weaponPrefab);
            startWeapon.transform.SetParent(player_Info.weaponMainPos);
            player_Info.weapons.Add(startWeapon);
        }

        //������ ���� �߰�
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
        //�����Ͼ� ��ġ �߰�
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
        //��ȭ������ ������ ���� �߰�
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

        //���� �ε�
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

        //������ �ε�
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
