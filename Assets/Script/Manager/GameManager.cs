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
    [Header("# Variable")]
    public int playerLevel; //플레이어 레벨
    public int curExp;  //현재 경험치
    public int maxExp;  //최대 경험치
    public int levelUpChance; //웨이브 종료 후 레벨 업 할 횟수
    public int waveLevel;   //웨이브 레벨
    public float[] waveTime;    //웨이브 시간
    public int Money;   //돈
    public int interest; //이자
    float timer; //시간
    bool isPause; //일시정지
    bool isEnd; //웨이브 끝
    [Header("# GameObject")]
    public Camera stageMainCamera;
    public GameObject playerPrefab;
    public GameObject mainPlayer;
    public GameObject poolManager;
    BoxCollider coll;
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

        MoneyUI.text = Money.ToString("F0");
        interestNum.text = interest.ToString("F0");

        maxExp = 50 + (30 * (playerLevel - 1));
        ExpBarUI.maxValue = maxExp;
        ExpBarUI.value = curExp;
        LevelNum.text = "LV." + playerLevel.ToString("F0");

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
    }

    void FixedUpdate()
    {
        if (isEnd == false)
        {
            timer += Time.deltaTime;
            coll.enabled = true;
        }
        else if(isEnd == true)
        {
            coll.enabled = false;
        }

        waveTimerUI.text = timer.ToString("F0");
        waveLevelUI.text = "WAVE " + (waveLevel + 1).ToString("F0");

        if (timer >= waveTime[waveLevel]) //웨이브 클리어 시
        {
            timer = 0;
            waveLevel++;
            isEnd = true;
        }
    }

    public void LevelUp()
    {

        for (int i = 0; i < levelUpChance; i++)
        {

        }
    }
}
