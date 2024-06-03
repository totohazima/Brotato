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
        GameManager game = GameManager.instance;

        game.isPause = false;
        game.isStart = false;
        game.harvestVariance_Amount = 0;

        game.player = null;
        game.player_Info = null;

        game.weapon = null;
        game.weapon_Obj = null;
        game.weaponPrefab = null;

        game.diffiicult = null;
        game.difficult_Obj = null;

        SceneManager.LoadSceneAsync("MainScene",LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("Stage",UnloadSceneOptions.None);
        
    }
}
