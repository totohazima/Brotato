using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ICustomUpdateMono
{
    public static GameManager instance;
    [Header("# UI")]
    public Slider HpBarUI;
    public Text HpNum;
    public Slider ExpBarUI;
    public Text LevelNum;
    public Text waveLevelUI;
    public Text waveTimerUI;
    public Text MoneyUI;
    public GameObject interestUI;
    public Text interestNum;
    public GameObject levelUpUI;
    public GameObject levelUpMarkUi;
    public GameObject[] levelMarks;
    public GameObject lootCrateMarkUi;
    public GameObject[] lootMarks;
    public GameObject shopUI;
    public GameObject GameClearUI;
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
    [Header("# GameObject")]
    public Camera stageMainCamera;
    public GameObject playerPrefab;
    public GameObject mainPlayer;
    public GameObject poolManager;
    public GameObject joyStickRayCaster;
    public Transform itemInfoManager;
    private Transform main;
    [HideInInspector]
    public PoolManager pool;

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

        GameObject startWeapon = Instantiate(MainSceneManager.instance.selectWeapon.GetComponent<ForSettingWeapon>().weaponPrefabs);
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
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }
    public void CustomUpdate()
    {
        if(isPause == true)//일시정지 활성화
        {
            Time.timeScale = 0;
        }
        else if(isPause == false)//일시정지 비활성화
        {
            Time.timeScale = 1;
        }

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

        if (curHp <= 0)
        {
            isDie = true;
            StartCoroutine(Died());
        }
        else
        {
            isDie = false;
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
            curExp += 100;

            StartCoroutine(DropItemLootingTime());
            
        }
    }
    private IEnumerator DropItemLootingTime() //웨이브 종료 시 떨어진 드랍템이 자동으로 들어오는 시간
    {
        yield return new WaitForSeconds(1f);
        if (levelUpChance > 0)
        {
            main.position = Vector3.zero;
            LevelUp();
        }
        else
        {
            main.position = Vector3.zero;
            ShopOpen();
        }
    }
    
    IEnumerator Died()
    {
        //isPause = true;
        yield return new WaitForSeconds(0f);
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

        spawn.mines.Clear();
        spawn.turrets.Clear();
    }
    void UiVisualize()
    {
        maxHp = playerInfo.maxHealth;
        HpBarUI.maxValue = maxHp;
        HpBarUI.value = curHp;
        HpNum.text = curHp.ToString("F0") + " / " + maxHp.ToString("F0");

        MoneyUI.text = Money.ToString("F0");
        interestNum.text = interest.ToString("F0");

        maxExp = 50 + (30 * (playerLevel));
        ExpBarUI.maxValue = maxExp;
        ExpBarUI.value = curExp;
        LevelNum.text = "LV." + playerLevel + 1;

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
        //StartCoroutine(LevelUpManager.instance.UpgradeSetting());
    }

    public void ShopOpen()
    {
        shopUI.SetActive(true);
        ShopManager.instance.ShopReRoll();
        //ShopManager.instance.ShopGoodsSetting();
        ItemManager.instance.ItemListUp(ShopManager.instance.tabsScroll[1], ShopManager.instance.verticalTabsScroll[1]);
    }
    IEnumerator StageStart()
    {
        yield return 0;
        curHp = playerInfo.maxHealth;
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

        StartCoroutine(spawn.MineSetting());
        StartCoroutine(spawn.TurretSetting());
    }
    void GameEnd()
    {
        GameClearUI.SetActive(true);
        isEnd = true;
    }

    public void ReturnMainMenu()
    {
        MainSceneManager main = MainSceneManager.instance;

        Destroy(main.selectedPlayer);
        Destroy(main.selectedWeapon);
        main.selectPlayer = null;
        main.selectedPlayer = null;
        main.selectWeapon = null;
        main.selectedWeapon = null;

        main.weaponSettingMenu.SetActive(false);
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
