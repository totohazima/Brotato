using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadUI : MonoBehaviour
{
    /// <summary>
    /// 새롭게 게임을 시작할 경우
    /// 저장되있던 데이터를 적용하지 않음
    /// </summary>
    public void NewGameStart()
    {
        MainSceneManager.instance.playerSettingMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 저장된 게임을 시작할 경우
    /// </summary>
    public void LoadGameStart()
    {
        GameManager.instance.easySave.LoadScene();
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
