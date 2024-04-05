using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI_Manager : MonoBehaviour
{
    public GameObject pauseUI;
    public RectTransform statUI;

    public void GamePause()
    {
        pauseUI.SetActive(true);
        GameManager.instance.isPause = true;
        statUI.anchoredPosition = new Vector3(100, 0, 0);
    }

    public void GameProgress()
    {
        pauseUI.SetActive(false);
        GameManager.instance.isPause = false;
        statUI.anchoredPosition = new Vector3(-100, 0, 0);
    }
}
