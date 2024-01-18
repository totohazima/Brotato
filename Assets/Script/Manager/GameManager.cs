using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
        if(isPause == true)//일시정지 활성화
        {
            Time.timeScale = 0;
        }
        else if(isPause == false)//일시정지 비활성화
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

    }
}
