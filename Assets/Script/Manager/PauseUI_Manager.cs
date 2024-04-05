using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI_Manager : MonoBehaviour
{
    public RectTransform statUI;

    public void GamePause()
    {
        GameManager.instance.isPause = true;
        statUI.anchoredPosition = new Vector3(100, 0, 0);
    }

    public void GameProgress()
    {
        GameManager.instance.isPause = false;
        statUI.anchoredPosition = new Vector3(-100, 0, 0);
    }
}
