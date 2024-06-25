using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;

    public Camera mainCamera;
    public Transform playerSetGroup;
    public Transform weaponSetGroup;
    public Transform difficultSetGroup;

    [Header("# Player")]
    public GameObject selectedPlayer; //������
    public GameObject selectPlayer;
    public GameObject playerSettingMenu;
    public GameObject playerGroup;
    [Header("# Weapon")]
    public GameObject selectedWeapon;   //������
    public GameObject selectWeapon;
    public GameObject weaponSettingMenu;
    public GameObject weaponGroup;
    [Header("# Difficult")]
    public GameObject selectedDifficult; //������
    public GameObject selectDifficult;
    public GameObject difficultSettingMenu;
    public GameObject difficultGroup;
    public Canvas canvas;
    public GameObject gameLoadUI;


    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    void Start()
    {
        if (GameManager.instance.IsSceneLoaded("Title") == true)
        {
            SceneManager.UnloadSceneAsync("Title");
        }
    }
    public void StartBtn() //���� ����
    {
        if (GameManager.instance.easySave.isSaved == true)
        {
            gameLoadUI.SetActive(true);
        }
        else
        {
            playerSettingMenu.SetActive(true);
        }
    }

    
}
