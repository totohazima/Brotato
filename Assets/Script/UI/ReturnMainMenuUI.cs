using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnMainMenuUI : MonoBehaviour
{
    //public void ReturnUI_On()
    //{
    //    gameObject.SetActive(true);
    //    StageManager.instance.pauseUI.SetActive(false);
    //    StageManager.instance.StatUI_Off();
    //}
    public void ReturnUI_Off()
    {
        gameObject.SetActive(false);
        StageManager.instance.pauseUI.SetActive(true);
        StageManager.instance.StatUI_On();
    }
    public void ReturnMainMenu()
    {
        GameManager.instance.isPause = false;
        GameManager.instance.isStart = false;
        GameManager.instance.harvestVariance_Amount = 0;

        SceneManager.LoadSceneAsync("MainScene",LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("Stage",UnloadSceneOptions.None);
        
    }
}
