using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameClearUI : MonoBehaviour
{
    public void GameClear()
    {
        GameManager.instance.GameManagerClear();
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("Stage", UnloadSceneOptions.None);
    }
}
