using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour, ICustomUpdateMono
{
    public static StageManager instance;
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
    private float overExp; //레벨업 후 남은 경험치
    public PlayerAction playerInfo;
    public int playerLevel; //플레이어 레벨
    public float curHp; //현재 체력
    public float maxHp; //최대 체력
    public float curExp;  //현재 경험치
    public float maxExp;  //최대 경험치
    public int levelUpChance; //웨이브 종료 후 레벨 업 할 횟수
    public int waveLevel;   //웨이브 레벨
    public float[] waveTime;    //웨이브 시간
    public int money;   //돈
    public int interest; //이자
    public int lootChance; //상자깡 찬스
    public int inWaveLoot_Amount; //현재 웨이브에서 나온 상자의 개수
    public float timer; //시간
    public bool isEnd; //웨이브 끝

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
    public Transform[] wallPos; //0 = 위, 1 = 아래, 2 왼쪽, 3 = 오른쪽
    public JoyStick joystick;
    public float xMin, xMax, yMin, yMax;
    public WaveStatInfoTable waveStat;
    void Awake()
    {
        instance = this;
        pool = poolManager.GetComponent<PoolManager>();
        mainPlayer = Instantiate(playerPrefab);
        main = mainPlayer.transform;
        playerInfo = mainPlayer.GetComponent<PlayerAction>();
        GameManager.instance.player_Info = playerInfo;
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

        StartCoroutine(GameManager.instance.WaveStart());

        
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }
    public void LateUpdate()
    {
        if (GameManager.instance.isPause == true)//일시정지 활성화
        {
            Time.timeScale = 0;
        }
        else if (GameManager.instance.isPause == false)//일시정지 비활성화
        {
            Time.timeScale = 1;
        }

        if(isEnd == true)
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

        if (isEnd == false)
        {
            timer -= Time.deltaTime;

            waveTimerUI.gameObject.SetActive(true);
            joyStickRayCaster.SetActive(true);
        }
        else if (isEnd == true)
        {
            waveTimerUI.gameObject.SetActive(false);
            joyStickRayCaster.SetActive(false);
        }


        if(curHp > maxHp)
        {
            curHp = maxHp;
        }
        UiVisualize();
        if (playerLevel < 20)
        {
            if (curExp >= maxExp)
            {
                overExp = curExp - maxExp;
                curExp = overExp;
                overExp = 0;
                playerLevel++;
                levelUpChance++;
            }
        }

        if(interest > 0)
        {
            interestUI.SetActive(true);
        }
        else if(interest <= 0)
        {
            interestUI.SetActive(false);
        }

        if (GameManager.instance.isStart == true)
        {
            if (curHp <= 0)
            {
                GameManager.instance.isDie = true;
                StartCoroutine(Died());
            }
            else
            {
                GameManager.instance.isDie = false;
            }
        }

        if (timer <= 0) //웨이브 클리어 시
        {
            if (waveLevel == 9)
            {
                GameEnd();
                return;
            }
            timer = waveTime[waveLevel];
            isEnd = true;
            curHp = maxHp;
            FriendlyRemove();

            //수확 스탯
            if (playerInfo.harvest > 0)
            {
                int harvest = (int)Mathf.Round(playerInfo.harvest);
                money += harvest;
                curExp += playerInfo.harvest;
                GameManager.instance.harvestVariance_Amount += GameManager.instance.riseHarvest_Num;
            }
            else if(playerInfo.harvest < 0)
            {
                int harvest = (int)Mathf.Round(playerInfo.harvest);
                money -= harvest;
            }
            playerInfo.StatCalculate();
            //평화주의자: 살아있는 몹 하나당 +0.65 재료, XP
            if(GameManager.instance.character == Player.Character.PACIFIST)
            {
                int enemyCount = SpawnManager.instance.enemys.Count;
                float gainMoney = 0.65f * enemyCount;
                money += (int)Mathf.Round(gainMoney);
                curExp += 0.65f * enemyCount;
            }

            StartCoroutine(DropItemLootingTime());
            
        }

    }
    private IEnumerator DropItemLootingTime() //웨이브 종료 시 떨어진 드랍템이 자동으로 들어오는 시간
    {
        yield return new WaitForSeconds(1f);
        main.position = Vector3.zero;
        if (levelUpChance > 0)
        {  
            LevelUp();
        }
        else if (lootChance > 0)
        {
            LootMenuOpen();
        }
        else
        {
            ShopOpen();
        }
    }
    
    IEnumerator Died()
    {
        isEnd = true;
        gameOverUI.SetActive(true);
        yield return 0;
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
        hpBarUI.value = curHp;
        hpNum.text = curHp.ToString("F0") + " / " + maxHp.ToString("F0");

        moneyUI.text = money.ToString("F0");
        interestNum.text = interest.ToString("F0");

        maxExp = 50 + (30 * (playerLevel));
        expBarUI.maxValue = maxExp;
        expBarUI.value = curExp;
        levelNum.text = "LV." + (playerLevel + 1);

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

        if(levelUpChance >= 1)
        {
            levelUpMarkUi.SetActive(true);
            for (int i = 0; i < 7; i++)
            {
                levelMarks[i].SetActive(false);
            }
            for(int i = 0; i < levelUpChance; i++)
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

        if(lootChance >= 1)
        {
            lootCrateMarkUi.SetActive(true);
            for (int i = 0; i < 7; i++)
            {
                lootMarks[i].SetActive(false);
            }
            for (int i = 0; i < lootChance; i++)
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
    }
    

    public void nextWave()
    {
        curHp = maxHp;
        if(GameManager.instance.isWeirdGhost == true)
        {
            curHp = 1;
            GameManager.instance.isWeirdGhost = false;
        }
        waveLevel++;
        timer = waveTime[waveLevel];
        isEnd = false;
        inWaveLoot_Amount = 0;
        SpawnManager spawn = SpawnManager.instance;
        spawn.WaveSelect(waveLevel);
        ///젠틀한 에일리언 효과 미정
        //spawn.enemyLimit *= 1 + (ItemEffect.instance.GentleAlien() / 100);
        //spawn.spawnTime = waveTime[waveLevel] / spawn.enemyLimit;
        if (spawn.scrip[waveLevel].isBossSpawn == true)
        {
            if (GameManager.instance.doubleBoss == true)
            {
                StartCoroutine(spawn.BossSpawn(2));
            }
            else
            {
                StartCoroutine(spawn.BossSpawn(1));
            }
        }
        GameManager.instance.engineerBuildingPos = spawn.FriendlySpawnPosition();
        StartCoroutine(spawn.MineSetting());
        StartCoroutine(spawn.TurretSetting());

        //for (int i = 0; i < playerInfo.weapons.Count; i++)
        //{
        //    if(playerInfo.weapons[i].GetComponent<Weapon_Action>().index == Weapon.Weapons.WRENCH)
        //    {
        //        StartCoroutine(playerInfo.weapons[i].GetComponent<Wrench_Weapon>().SpawnTurret());
        //    }
        //}
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
        isEnd = true;
    }

    
    public void HitCalculate(float damage)
    {
        float hit, dodge;
        if(playerInfo.evasion >= 60)
        {
            dodge = 60;
            hit = 100 - dodge;
        }
        else
        {
            dodge = playerInfo.evasion;
            hit = 100 - dodge;
        }
        float[] chance = { hit, dodge };
        int index = Judgment(chance);
        if(index == 0)
        {
            //방어력이 0 초과
            if(playerInfo.armor > 0)
            {
                float enduce = playerInfo.armor / 100;
                damage -= (damage * enduce);
                curHp -= damage;
            }
            //방어력이 0 미만
            else if(playerInfo.armor < 0)
            {
                float enduce = playerInfo.armor / 100;
                damage -= (damage * enduce);
                curHp -= damage;
            }
            else
            {
                curHp -= damage;
            }
        }
        else
        {
            Debug.Log("회피");
        }
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
