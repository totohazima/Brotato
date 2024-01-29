using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# UI")]
    public Text waveLevelUI;
    public Text waveTimerUI;
    public Text MoneyUI;
    public Text interest; //����
    [Header("# Variable")]
    public int playerLevel;
    public int curExp;
    public int maxExp;
    public int waveLevel;
    public float[] waveTime;
    float timer;
    bool isPause;
    void Awake()
    {
        instance = this;
        SceneManager.UnloadSceneAsync("LoadingScene", UnloadSceneOptions.None);
        playerLevel = 1;
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

        maxExp = 50 + (30 * (playerLevel - 1));
        if (playerLevel < 20)
        {
            if (curExp >= maxExp)
            {
                LevelUp();
            }
        }

    }


    public void LevelUp()
    {
        Debug.Log("������");
    }
}
