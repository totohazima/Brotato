using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ICustomUpdateMono
{
    public static GameManager instance;
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
    public PlayerAction playerInfo;
    public int playerLevel; //플레이어 레벨
    public bool isDie; //플레이어 사망
    public float curHp; //현재 체력
    public float maxHp; //최대 체력
    public float curExp;  //현재 경험치
    private float overExp; //레벨업 후 남은 경험치
    public float maxExp;  //최대 경험치
    public int levelUpChance; //웨이브 종료 후 레벨 업 할 횟수
    public int waveLevel;   //웨이브 레벨
    public float[] waveTime;    //웨이브 시간
    public int Money;   //돈
    public int interest; //이자
    public int lootChance; //상자깡 찬스
    private float timer; //시간
    public bool isPause; //일시정지
    public bool isEnd; //웨이브 끝
    public int difficult; //난이도
    public bool isSpecialEnemySpawn; //새로운 적의 출현
    public bool isEliteSpawn; //엘리트와 무리가 등장
    public int eliteEnemyWave; //엘리트와 무리가 등장하는 웨이브 수
    public float enemyRiseDamage; //적 데미지 증가치 %
    public float enemyRiseHealth; //적 체력 증가치 %
    public bool doubleBoss; //보스가 2마리 (한 마리는 체력이 25% 감소)
    private int[] eliteWaveNum;
    private bool isStart; //게임이 처음 시작할 때
    [Header("# GameObject")]
    public Camera stageMainCamera;
    public GameObject playerPrefab;
    public GameObject mainPlayer;
    public GameObject poolManager;
    public GameObject joyStickRayCaster;
    public Transform itemInfoManager;
    private Transform main;
    public Transform ui_Canvas;
    [HideInInspector]
    public PoolManager pool;
    [SerializeField] private GameObject optionUI;
    public Transform[] wallPos; //0 = 위, 1 = 아래, 2 왼쪽, 3 = 오른쪽
    public float xMin, xMax, yMin, yMax;
    void Awake()
    {
        instance = this;
        SceneManager.UnloadSceneAsync("LoadingScene", UnloadSceneOptions.None);
        pool = poolManager.GetComponent<PoolManager>();
        mainPlayer = Instantiate(playerPrefab);
        main = mainPlayer.transform;
        playerInfo = mainPlayer.GetComponent<PlayerAction>();
        optionUI = MainSceneManager.instance.option;
        GameObject startWeapon = Instantiate(MainSceneManager.instance.selectWeapon.GetComponent<ForSettingWeapon>().weaponPrefabs);

        difficult = MainSceneManager.instance.selectedDifficult.GetComponent<Difficult>().difficultLevel;
        isSpecialEnemySpawn = DifficultImporter.instance.isSpecialEnemy[difficult];
        isEliteSpawn = DifficultImporter.instance.isEliteSpawn[difficult];
        eliteEnemyWave = DifficultImporter.instance.isEliteWaveCount[difficult];
        enemyRiseDamage = DifficultImporter.instance.enemyRiseDamage[difficult];
        enemyRiseHealth = DifficultImporter.instance.enemyRiseHealth[difficult];
        doubleBoss = DifficultImporter.instance.doubleBoss[difficult];

        eliteWaveNum = new int[eliteEnemyWave];
        startWeapon.transform.SetParent(playerInfo.weaponMainPos);
        playerInfo.weapons.Add(startWeapon);

        xMin = wallPos[2].position.x;
        xMax = wallPos[3].position.x;
        yMin = wallPos[1].position.y;
        yMax = wallPos[0].position.y;
;
        waveTime = new float[WaveStatImporter.instance.waveTime.Length];
        for(int i = 0; i < WaveStatImporter.instance.waveTime.Length; i++)
        {
            waveTime[i] = WaveStatImporter.instance.waveTime[i];
        }
        timer = waveTime[0];

        StartCoroutine(StageStart());
    }
    void Start()
    {
        while (true)
        {
            bool isSame = false;
            for (int i = 0; i < eliteEnemyWave; i++)
            {
                eliteWaveNum[i] = Random.Range(5, 9);
            }

            for (int i = 0; i < eliteEnemyWave; i++)
            {
                for (int j = 0; j < eliteEnemyWave; j++)
                {
                    if (i == j)
                    {
                    }
                    else if (eliteWaveNum[i] == eliteWaveNum[j])
                    {
                        isSame = true;
                    }
                }
            }

            if (isSame == false)
            {
                break;
            }
        }
        System.Array.Sort(eliteWaveNum);
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
        if (isPause == true)//일시정지 활성화
        {
            Time.timeScale = 0;
        }
        else if (isPause == false)//일시정지 비활성화
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

        if (isStart == true)
        {
            if (curHp <= 0)
            {
                isDie = true;
                StartCoroutine(Died());
            }
            else
            {
                isDie = false;
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

            ///test
            ///웨이브 종료 시 경험치 제공
            //curExp += 100;

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
        for(int i = 0; i < spawn.trees.Count; i++) //나무
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

        moneyUI.text = Money.ToString("F0");
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
        //ShopManager.instance.ShopGoodsSetting();
        //ItemManager.instance.ItemListUp(ShopManager.instance.tabsScroll[1], ShopManager.instance.verticalTabsScroll[1], PauseUI_Manager.instance.scrollContents[1]);
    }
    IEnumerator StageStart()
    {
        yield return 0;
        //playerInfo.StatCalculate();
        isStart = true;
        curHp = playerInfo.maxHealth_Origin;
        for (int i = 0; i < playerInfo.weapons.Count; i++)
        {
            if (playerInfo.weapons[i].GetComponent<Weapon_Action>().index == Weapon.Weapons.WRENCH)
            {
                StartCoroutine(playerInfo.weapons[i].GetComponent<Wrench_Weapon>().SpawnTurret());
            }
        }
    }

    public void nextWave()
    {
        curHp = maxHp;
        if(ItemEffect.instance.isWeirdGhost == true)
        {
            curHp = 1;
            ItemEffect.instance.isWeirdGhost = false;
        }
        waveLevel++;
        timer = waveTime[waveLevel];
        isEnd = false;

        SpawnManager spawn = SpawnManager.instance;
        spawn.WaveSelect(waveLevel);
        spawn.enemyLimit *= 1 + (ItemEffect.instance.GentleAlien() / 100);
        spawn.spawnTime = waveTime[waveLevel] / spawn.enemyLimit;

        ///엘리트 몹 스폰 나중에
        //for (int i = 0; i < eliteWaveNum.Length; i++)
        //{
        //    if (waveLevel == eliteWaveNum[i])
        //    {
        //        StartCoroutine(spawn.EliteSpawn(1));
        //    }

        //}
        if(waveLevel == 9)
        {
            if (doubleBoss == true)
            {
                StartCoroutine(spawn.BossSpawn(2));
            }
            else
            {
                StartCoroutine(spawn.BossSpawn(1));
            }
        }
        StartCoroutine(spawn.MineSetting());
        StartCoroutine(spawn.TurretSetting());

        for (int i = 0; i < playerInfo.weapons.Count; i++)
        {
            if(playerInfo.weapons[i].GetComponent<Weapon_Action>().index == Weapon.Weapons.WRENCH)
            {
                StartCoroutine(playerInfo.weapons[i].GetComponent<Wrench_Weapon>().SpawnTurret());
            }
        }
    }
    public void ReturnUI_On()
    {
        returnMainMenu_UI.SetActive(true);
        pauseUI.SetActive(false);
        statUI.anchoredPosition = new Vector3(-100, 0, 0);
    }
    public void ReturnUI_Off()
    {
        returnMainMenu_UI.SetActive(false);
        pauseUI.SetActive(true);
        statUI.anchoredPosition = new Vector3(100, 0, 0);
    }

    public void ReStartUI_On()
    {
        restartUI.SetActive(true);
        pauseUI.SetActive(false);
        statUI.anchoredPosition = new Vector3(-100, 0, 0);
    }
    public void ReStartUI_Off()
    {
        restartUI.SetActive(false);
        pauseUI.SetActive(true);
        statUI.anchoredPosition = new Vector3(100, 0, 0);
    }

    public void Option_On()
    {
        optionUI.transform.SetParent(ui_Canvas);
        optionUI.transform.localScale = new Vector3(1, 1, 1);
        RectTransform rect = optionUI.GetComponent<RectTransform>();
        rect.offsetMax = Vector3.zero;
        rect.offsetMin = Vector3.zero;
        optionUI.SetActive(true);
    }
    void GameEnd()
    {
        gameClearUI.SetActive(true);
        isEnd = true;
    }
    public void GameReStart()
    {
        LoadingSceneManager.CloseScene("Stage");
        LoadingSceneManager.LoadScene("Stage");
    }
    public void ReturnMainMenu()
    {
        MainSceneManager main = MainSceneManager.instance;
        isPause = false;
        Destroy(main.selectedPlayer);
        Destroy(main.selectedWeapon);
        Destroy(main.selectedDifficult);

        main.selectPlayer = null;
        main.selectedPlayer = null;
        main.selectWeapon = null;
        main.selectedWeapon = null;
        main.selectDifficult = null;
        main.selectedDifficult = null;

        main.difficultSettingMenu.SetActive(false);
        main.canvas.gameObject.SetActive(true);
        main.mainCamera.gameObject.SetActive(true);
        LoadingSceneManager.CloseScene("Stage");
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
            //추후 방어구 계산 추가
            curHp -= damage;
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
