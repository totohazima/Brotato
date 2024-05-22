using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    public void GameOver()
    {
        GameManager.instance.isPause = false;
        GameManager.instance.isStart = false;
        GameManager.instance.harvestVariance_Amount = 0;

        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("Stage", UnloadSceneOptions.None);
    }
}