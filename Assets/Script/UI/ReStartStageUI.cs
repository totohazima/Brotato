using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReStartStageUI : MonoBehaviour
{
    //public void ReStartUI_On()
    //{
    //    gameObject.SetActive(true);
    //    StageManager.instance.pauseUI.SetActive(false);
    //    StageManager.instance.StatUI_Off();
    //}
    public void ReStartUI_Off()
    {
        gameObject.SetActive(false);
        StageManager.instance.pauseUI.SetActive(true);
        StageManager.instance.StatUI_On();
    }

    public void GameReStart()
    {
        GameManager.instance.isPause = false;
        GameManager.instance.isStart = false;
        SceneManager.UnloadSceneAsync("Stage");
        SceneManager.LoadSceneAsync("Stage");
    }
}
