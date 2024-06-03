using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    public void GameOver()
    {
        GameManager.instance.GameManagerClear();
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("Stage", UnloadSceneOptions.None);
    }
}
