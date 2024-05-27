using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameClearUI : MonoBehaviour
{
    public void GameClear()
    {
        GameManager.instance.isPause = false;
        GameManager.instance.isStart = false;
        GameManager.instance.harvestVariance_Amount = 0;
        GameManager.instance.player_Info = null;
        GameManager.instance.weaponPrefab = null;
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("Stage", UnloadSceneOptions.None);
    }
}
