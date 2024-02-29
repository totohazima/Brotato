using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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
    public Player playerInfo;
    public int playerLevel; //�÷��̾� ����
    public bool isDie; //�÷��̾� ���
    public float curHp; //���� ü��
    public float maxHp; //�ִ� ü��
    public float curExp;  //���� ����ġ
    float overExp; //������ �� ���� ����ġ
    public float maxExp;  //�ִ� ����ġ
    public int levelUpChance; //���̺� ���� �� ���� �� �� Ƚ��
    public int waveLevel;   //���̺� ����
    public float[] waveTime;    //���̺� �ð�
    public int Money;   //��
    public int interest; //����
    public int lootChance; //���ڱ� ����
    float timer; //�ð�
    public bool isPause; //�Ͻ�����
    public bool isEnd; //���̺� ��
    [Header("# GameObject")]
    public Camera stageMainCamera;
    public GameObject playerPrefab;
    public GameObject mainPlayer;
    public GameObject poolManager;
    BoxCollider coll;
    Transform main;
    [HideInInspector]
    public PoolManager pool;
    void Awake()
    {
        instance = this;
        SceneManager.UnloadSceneAsync("LoadingScene", UnloadSceneOptions.None);
        pool = poolManager.GetComponent<PoolManager>();
        coll = GetComponent<BoxCollider>();
        mainPlayer = Resources.Load<GameObject>("Prefabs/Player");
        Instantiate(mainPlayer);
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        main = mainPlayer.transform;
        playerInfo = mainPlayer.GetComponent<Player>();
        timer = waveTime[waveLevel];

        StartCoroutine(StageStart());
    }

    void Update()
    {
        if(isPause == true)//�Ͻ����� Ȱ��ȭ
        {
            Time.timeScale = 0;
        }
        else if(isPause == false)//�Ͻ����� ��Ȱ��ȭ
        {
            Time.timeScale = 1;
        }
        Vector3 playerPos = mainPlayer.transform.position;
        stageMainCamera.transform.position = new Vector3(playerPos.x, playerPos.y, -10f);
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
    }

    void FixedUpdate()
    {
        if (isEnd == false)
        {
            timer -= Time.deltaTime;

            waveTimerUI.gameObject.SetActive(true);
            coll.enabled = true;
        }
        else if(isEnd == true)
        {
            waveTimerUI.gameObject.SetActive(false);
            coll.enabled = false;
        }

        

        if (timer <= 0) //���̺� Ŭ���� ��
        {
            if(waveLevel == 9)
            {
                GameEnd();
                return;
            }
            timer = waveTime[waveLevel];
            isEnd = true;
            curHp = maxHp;
            FriendlyRemove();

            ///test
            ///���̺� ���� �� ����ġ ����
            curExp += 100;

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
    }
    IEnumerator Died()
    {
        //isPause = true;
        yield return new WaitForSeconds(0f);
    }
    void FriendlyRemove()
    {
        SpawnManager spawn = SpawnManager.instance;

        for (int i = 0; i < spawn.mines.Count; i++) //����
        {
            spawn.mines[i].SetActive(false);
        }
        for (int i = 0; i < spawn.turrets.Count; i++) //�ͷ�
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
        LevelNum.text = "LV." + playerLevel.ToString("F0");

        waveTimerUI.text = timer.ToString("F0");
        waveLevelUI.text = "���̺� " + (waveLevel + 1).ToString("F0");

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
    public void LevelUp()
    {
        levelUpUI.SetActive(true);
        StartCoroutine(LevelUpManager.instance.UpgradeSetting());
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
        spawn.WaveSelect(waveLevel + 1);
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
            //���� �� ��� �߰�
            curHp -= damage;
        }
        else
        {
            Debug.Log("ȸ��");
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
