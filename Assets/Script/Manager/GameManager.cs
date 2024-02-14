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
    [Header("# Variable")]
    public Player playerInfo;
    public int playerLevel; //플레이어 레벨
    public bool isDie; //플레이어 사망
    public float curHp; //현재 체력
    public float maxHp; //최대 체력
    public int curExp;  //현재 경험치
    public int maxExp;  //최대 경험치
    public int levelUpChance; //웨이브 종료 후 레벨 업 할 횟수
    public int waveLevel;   //웨이브 레벨
    public float[] waveTime;    //웨이브 시간
    public int Money;   //돈
    public int interest; //이자
    public int lootChance; //상자깡 찬스
    float timer; //시간
    public bool isPause; //일시정지
    public bool isEnd; //웨이브 끝
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
        playerLevel = 1;
        SceneManager.UnloadSceneAsync("LoadingScene", UnloadSceneOptions.None);
        pool = poolManager.GetComponent<PoolManager>();
        coll = GetComponent<BoxCollider>();
        mainPlayer = Resources.Load<GameObject>("Prefabs/Player");
        Instantiate(mainPlayer);
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        main = mainPlayer.transform;
        playerInfo = mainPlayer.GetComponent<Player>();

        StartCoroutine(StageStart());
    }

    void Update()
    {
        if(isPause == true)//일시정지 활성화
        {
            Time.timeScale = 0;
        }
        else if(isPause == false)//일시정지 비활성화
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
                playerLevel++;
                curExp = 0;
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
            timer += Time.deltaTime;

            waveTimerUI.gameObject.SetActive(true);
            coll.enabled = true;
        }
        else if(isEnd == true)
        {
            waveTimerUI.gameObject.SetActive(false);
            coll.enabled = false;
        }

        

        if (timer >= waveTime[waveLevel]) //웨이브 클리어 시
        {
            timer = 0;
            waveLevel++;
            isEnd = true;
            curHp = maxHp;
            main.position = Vector3.zero;

            ///test
            ///웨이브 종료 시 레벨 업 1회
            levelUpChance++;

            if (levelUpChance > 0)
            {
                StartCoroutine(LevelUp());
            }
        }
    }
    IEnumerator Died()
    {
        //isPause = true;
        yield return new WaitForSeconds(0f);
    }
    void UiVisualize()
    {
        maxHp = playerInfo.maxHealth;
        HpBarUI.maxValue = maxHp;
        HpBarUI.value = curHp;
        HpNum.text = curHp.ToString("F0") + " / " + maxHp.ToString("F0");

        MoneyUI.text = Money.ToString("F0");
        interestNum.text = interest.ToString("F0");

        maxExp = 50 + (30 * (playerLevel - 1));
        ExpBarUI.maxValue = maxExp;
        ExpBarUI.value = curExp;
        LevelNum.text = "LV." + playerLevel.ToString("F0");

        waveTimerUI.text = timer.ToString("F0");
        waveLevelUI.text = "WAVE " + (waveLevel + 1).ToString("F0");
    }
    public IEnumerator LevelUp()
    {
        yield return new WaitForSeconds(0.1f);
        
        levelUpUI.SetActive(true);
        StartCoroutine(LevelUpManager.instance.UpgradeSetting());
    }

    IEnumerator StageStart()
    {
        yield return new WaitForSeconds(0f);
        curHp = playerInfo.maxHealth;
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
