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



    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //playerTrans = playerGroup.transform;
        //players = new GameObject[playerTrans.childCount];
        //for(int i = 0; i < players.Length; i++)
        //{
        //    players[i] = playerTrans.GetChild(i).gameObject;
        //}

        //weaponTrans = weaponGroup.transform;
        //weapons = new GameObject[weaponTrans.childCount];
        //for(int i = 0; i < weapons.Length; i++)
        //{
        //    weapons[i] = weaponTrans.GetChild(i).gameObject;
        //}

        //difficultTrans = difficultGroup.transform;
        //difficults = new GameObject[difficultTrans.childCount];
        //for (int i = 0; i < difficults.Length; i++)
        //{
        //    difficults[i] = difficultTrans.GetChild(i).gameObject;
        //}
    }
    void Start()
    {
        SceneManager.UnloadSceneAsync("TitleScene");
    }
    public void StartBtn() //���� ����
    {
        playerSettingMenu.SetActive(true);
    }

    //public void ReturnMenu() //�޴� ���ư���
    //{
    //    Destroy(selectedPlayer);
    //    selectedPlayer = null;
    //    selectPlayer = null;
    //    playerSettingMenu.SetActive(false);
    //}

    //public void ReturnPlayerMenu()
    //{
    //    Destroy(selectedWeapon);
    //    selectedWeapon = null;
    //    selectWeapon = null;
    //    selectedPlayer.transform.SetParent(playerSetGroup);
    //    weaponSettingMenu.SetActive(false);
    //    playerSettingMenu.SetActive(true);
    //}
    //public void ReturnWeaponMenu()
    //{
    //    Destroy(selectedDifficult);
    //    selectedDifficult = null;
    //    selectDifficult = null;
    //    selectedPlayer.transform.SetParent(weaponSetGroup);
    //    selectedWeapon.transform.SetParent(weaponSetGroup);

    //    difficultSettingMenu.SetActive(false);
    //    weaponSettingMenu.SetActive(true);
    //}
    //public void GoWeaponMenu() //���� â �Ѿ��
    //{
    //    if (selectedPlayer == null) //��� â
    //    {
    //        Debug.Log("ĳ���� ���� �ʿ�");
    //    }
    //    else if (selectedPlayer != null)
    //    {
    //        selectedPlayer.transform.SetParent(weaponSetGroup);
    //        playerSettingMenu.SetActive(false);
    //        weaponSettingMenu.SetActive(true);
    //    }
    //}

    //public void GoDifficultMenu()
    //{
    //    if (selectedWeapon == null) //��� â
    //    {
    //        Debug.Log("���� ���� �ʿ�");
    //    }
    //    else if (selectedWeapon != null)
    //    {
    //        selectedPlayer.transform.SetParent(difficultSetGroup);
    //        selectedWeapon.transform.SetParent(difficultSetGroup);
    //        weaponSettingMenu.SetActive(false);
    //        difficultSettingMenu.SetActive(true);
    //    }
    //}

    //public void GameStartBtn() //�������� ���۹�ư
    //{
    //    if (selectedDifficult == null)
    //    {
    //        Debug.Log("���̵� ���� �ʿ�");
    //    }
    //    else if (selectedDifficult != null)
    //    {
    //        canvas.gameObject.SetActive(false);
    //        Camera.main.gameObject.SetActive(false);
    //        LoadingSceneManager.LoadScene("Stage");
    //    }
    //}
}
