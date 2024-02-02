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
    public int playerLevel; //�÷��̾� ����
    public int curExp;  //���� ����ġ
    public int maxExp;  //�ִ� ����ġ
    public int levelUpChance; //���̺� ���� �� ���� �� �� Ƚ��
    public int waveLevel;   //���̺� ����
    public float[] waveTime;    //���̺� �ð�
    public int Money;   //��
    public int interest; //����
    float timer; //�ð�
    bool isPause; //�Ͻ�����
    bool isEnd; //���̺� ��
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

        if (timer >= waveTime[waveLevel]) //���̺� Ŭ���� ��
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
