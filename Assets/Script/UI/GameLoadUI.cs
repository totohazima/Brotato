using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadUI : MonoBehaviour
{
    /// <summary>
    /// ���Ӱ� ������ ������ ���
    /// ������ִ� �����͸� �������� ����
    /// </summary>
    public void NewGameStart()
    {
        MainSceneManager.instance.playerSettingMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ����� ������ ������ ���
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
