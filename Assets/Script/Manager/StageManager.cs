using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour, ICustomUpdateMono, UI_Upadte
{
    public static StageManager instance;
    public List<Transform> trackedTargets = new List<Transform>();
    [Header("# UI")]
    public Slider hpBarUI;
    public Text hpNum;
    public Slider expBarUI;
    public Text levelNum;
    public Text waveLevelUI;
    public Text waveTimerUI;
    public Text moneyUI;
    public GameObject interestUI;
    public Text interestNum;
    public GameObject levelUpUI;
    public GameObject levelUpMarkUi;
    public GameObject[] levelMarks;
    public GameObject lootOpenUI;
    public GameObject lootCrateMarkUi;
    public GameObject[] lootMarks;
    public GameObject shopUI;
    public RectTransform statUI;
    public GameObject pauseUI;
    public GameObject pauseIcon;
    public GameObject returnMainMenu_UI;
    public GameObject restartUI;
    public GameObject gameClearUI;
    public GameObject gameOverUI;
    [Header("# Variable")]
    public Player_Action playerInfo;
    public float maxHp; //최대 체력
    public int waveLevel;   //웨이브 레벨
    public float[] waveTime;    //웨이브 시간
    public int inWaveLoot_Amount; //현재 웨이브에서 나온 상자의 개수
    public float timer; //시간

    [Header("# GameObject")]
    private Transform main;
    [HideInInspector] public PoolManager pool;
    public Camera stageMainCamera;
    public GameObject playerPrefab;
    public GameObject mainPlayer;
    public GameObject poolManager;
    public GameObject joyStickRayCaster;
    public Transform itemInfoManager;
    public Transform ui_Canvas;
    public JoyStick joystick;
    public Transform[] wallPos; //0 = 위, 1 = 아래, 2 왼쪽, 3 = 오른쪽
    public float xMin, xMax, yMin, yMax;
    public WaveStatInfoTable waveStat;
    void Awake()
    {
        instance = this;
        pool = poolManager.GetComponent<PoolManager>();
        mainPlayer = Instantiate(playerPrefab);
        main = mainPlayer.transform;
        playerInfo = mainPlayer.GetComponent<Player_Action>();
        GameManager.instance.playerAct = playerInfo;
        GameManager.instance.playerTrans = playerInfo.weaponMainPos;
        //curHp = 1; //사망 방지
        

        waveStat = GameManager.instance.gameDataBase.waveStatInfoTable;

        waveTime = new float[waveStat.table.Length];
        for (int i = 0; i < waveTime.Length; i++)
        {
            waveTime[i] = waveStat.table[i].waveTime;
        }
        timer = waveTime[0];

        xMin = wallPos[2].position.x;
        xMax = wallPos[3].position.x;
        yMin = wallPos[1].position.y;
        yMax = wallPos[0].position.y;

        if (GameManager.instance.easySave.isLoaded == true)
        {
            StartCoroutine(GameManager.instance.GameLoad());
        }
        else
        {
            StartCoroutine(GameManager.instance.WaveStart());
        }
        
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        UIUpdateManager.uiUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
        UIUpdateManager.uiUpdates.Remove(this);
    }

    public void UI_Update()
    {
        if (GameManager.instance.isPause == true)//일시정지 활성화
        {
            Time.timeScale = 0;
        }
        else if (GameManager.instance.isPause == false)//일시정지 비활성화
        {
            Time.timeScale = 1;
        }

        if(GameManager.instance.isEnd == true)
        {
            pauseIcon.SetActive(false);
        }
        else
        {
            pauseIcon.SetActive(true);
        }
    }
    public void CustomUpdate()
    {

        if (GameManager.instance.isEnd == false)
        {
            timer -= Time.deltaTime;

            waveTimerUI.gameObject.SetActive(true);
            joyStickRayCaster.SetActive(true);
        }
        else if (GameManager.instance.isEnd == true)
        {
            waveTimerUI.gameObject.SetActive(false);
            joyStickRayCaster.SetActive(false);
            FriendlyRemove();
        }


        if(GameManager.instance.playerInfo.playerHealth > maxHp)
        {
            GameManager.instance.playerInfo.playerHealth = maxHp;
        }
        UiVisualize();
        

        if(GameManager.instance.playerInfo.interest > 0)
        {
            interestUI.SetActive(true);
        }
        else if(GameManager.instance.playerInfo.interest <= 0)
        {
            interestUI.SetActive(false);
        }

        

        if (timer <= 0) //웨이브 클리어 시
        {
            if (waveLevel == 19)
            {
                GameEnd();
                return;
            }

            timer = waveTime[waveLevel];
            GameManager.instance.isEnd = true;
            GameManager.instance.playerInfo.playerHealth = maxHp;
            
            //수확 스탯
            if (playerInfo.harvest > 0)
            {
                int harvest = (int)Mathf.Round(playerInfo.harvest);
                GameManager.instance.playerInfo.money += harvest;
                GameManager.instance.playerInfo.curExp += playerInfo.harvest;
                GameManager.instance.harvestVariance_Amount += GameManager.instance.riseHarvest_Num;
            }
            else if(playerInfo.harvest < 0)
            {
                int harvest = (int)Mathf.Round(playerInfo.harvest);
                GameManager.instance.playerInfo.money -= harvest;
            }
            playerInfo.StatCalculate();
            //평화주의자: 살아있는 몹 하나당 +0.65 재료, XP
            if(GameManager.instance.character == Player.Character.PACIFIST)
            {
                int enemyCount = SpawnManager.instance.enemys.Count;
                float gainMoney = 0.65f * enemyCount;
                GameManager.instance.playerInfo.money += (int)Mathf.Round(gainMoney);
                GameManager.instance.playerInfo.curExp += 0.65f * enemyCount;
            }

            StartCoroutine(DropItemLootingTime());
            
        }

    }
    private IEnumerator DropItemLootingTime() //웨이브 종료 시 떨어진 드랍템이 자동으로 들어오는 시간
    {
        yield return new WaitForSeconds(2f);
        main.position = Vector3.zero;
        if (GameManager.instance.playerInfo.levelUpChance > 0)
        {  
            LevelUp();
        }
        else if (GameManager.instance.playerInfo.lootChance > 0)
        {
            LootMenuOpen();
        }
        else
        {
            ShopOpen();
        }
    }

    void FriendlyRemove()
    {
        SpawnManager spawn = SpawnManager.instance;

        for (int i = 0; i < spawn.mines.Count; i++) //지뢰
        {
            spawn.mines[i].SetActive(false);
        }
        for (int i = 0; i < spawn.turrets.Count; i++) //터렛
        {
            spawn.turrets[i].SetActive(false);
        }
        for (int i = 0; i < spawn.trees.Count; i++) //나무
        {
            spawn.trees[i].SetActive(false);
        }

        spawn.mines.Clear();
        spawn.turrets.Clear();
        spawn.trees.Clear();
    }


    void UiVisualize()
    {
        maxHp = playerInfo.maxHealth;
        hpBarUI.maxValue = maxHp;
        hpBarUI.value = GameManager.instance.playerInfo.playerHealth;
        hpNum.text = Mathf.Ceil(GameManager.instance.playerInfo.playerHealth).ToString("F0") + " / " + maxHp.ToString("F0");

        moneyUI.text = GameManager.instance.playerInfo.money.ToString("F0");
        interestNum.text = GameManager.instance.playerInfo.interest.ToString("F0");

        GameManager.instance.playerInfo.maxExp = 50 + (30 * (GameManager.instance.playerInfo.playerLevel));
        expBarUI.maxValue = GameManager.instance.playerInfo.maxExp;
        expBarUI.value = GameManager.instance.playerInfo.curExp;
        levelNum.text = "LV." + (GameManager.instance.playerInfo.playerLevel);

        if (timer < 5)
        {
            waveTimerUI.color = Color.red;
        }
        else
        {
            waveTimerUI.color = Color.white;
        }
        waveTimerUI.text = timer.ToString("F0");
        
        waveLevelUI.text = "웨이브 " + (waveLevel + 1).ToString("F0");

        if(GameManager.instance.playerInfo.levelUpChance >= 1)
        {
            levelUpMarkUi.SetActive(true);
            for (int i = 0; i < 7; i++)
            {
                levelMarks[i].SetActive(false);
            }
            for(int i = 0; i < GameManager.instance.playerInfo.levelUpChance; i++)
            {
                if (i <= 6)
                {
                    levelMarks[i].SetActive(true);
                }
            }
        }
        else
        {
            levelUpMarkUi.SetActive(false);
            for (int i = 0; i < 7; i++)
            {
                levelMarks[i].SetActive(false);
            }
        }

        if(GameManager.instance.playerInfo.lootChance >= 1)
        {
            lootCrateMarkUi.SetActive(true);
            for (int i = 0; i < 7; i++)
            {
                lootMarks[i].SetActive(false);
            }
            for (int i = 0; i < GameManager.instance.playerInfo.lootChance; i++)
            {
                if (i <= 6)
                {
                    lootMarks[i].SetActive(true);
                }
            }
        }
        else
        {
            lootCrateMarkUi.SetActive(true);
            for (int i = 0; i < 7; i++)
            {
                lootMarks[i].SetActive(false);
            }
        }
    }
    void LevelUp()
    {
        levelUpUI.SetActive(true);
    }
    public void LootMenuOpen()
    {
        lootOpenUI.SetActive(true);
    }
    public void ShopOpen()
    {
        shopUI.SetActive(true);
        ShopManager.instance.ShopReRoll();
        ItemManager.instance.ItemListUp();

        GameManager.instance.easySave.SaveScene();
    }
    

    public void nextWave()
    {
        GameManager.instance.playerInfo.playerHealth = maxHp;
        if(GameManager.instance.playerInfo.isWeirdGhost == true)
        {
            GameManager.instance.playerInfo.playerHealth = 1;
            GameManager.instance.playerInfo.isWeirdGhost = false;
        }
        waveLevel++;
        timer = waveTime[waveLevel];
        GameManager.instance.isEnd = false;
        inWaveLoot_Amount = 0;
        SpawnManager spawn = SpawnManager.instance;
        spawn.WaveSelect(waveLevel);
        ///젠틀한 에일리언 효과 미정
        //spawn.enemyLimit *= 1 + (ItemEffect.instance.GentleAlien() / 100);
        //spawn.spawnTime = waveTime[waveLevel] / spawn.enemyLimit;
        if (spawn.scrip[waveLevel].isBossSpawn == true)
        {
            if (waveLevel != 19)
            {
                if (GameManager.instance.doubleBoss == true)
                {
                    StartCoroutine(spawn.BossSpawn(2, Enemy.EnemyName.PREDATOR));
                }
                else
                {
                    StartCoroutine(spawn.BossSpawn(1, Enemy.EnemyName.PREDATOR));
                }
            }
            if (waveLevel == 19)
            {
                if (GameManager.instance.doubleBoss == true)
                {
                    StartCoroutine(spawn.BossSpawn(2, Enemy.EnemyName.INVOKER));
                }
                else
                {
                    StartCoroutine(spawn.BossSpawn(1, Enemy.EnemyName.INVOKER));
                }
            }
        }
        GameManager.instance.playerInfo.EngineerTurretPosSetting();
        StartCoroutine(spawn.MineSpawn(GameManager.instance.playerInfo.minesCount));
        StartCoroutine(spawn.TurretSetting());
    }

    public void StatUI_On()
    {
        statUI.anchoredPosition = new Vector3(100, 0, 0);
    }
    public void StatUI_Off()
    {
        statUI.anchoredPosition = new Vector3(-100, 0, 0);
    }
   
    void GameEnd()
    {
        gameClearUI.SetActive(true);
        GameManager.instance.easySave.DeleteData();
        GameManager.instance.isEnd = true;
    }

    public int Judgment(float[] rando)
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
