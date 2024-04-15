using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;

    public Camera mainCamera;
    public Transform playerSetGroup;
    public Transform weaponSetGroup;
    public Transform difficultSetGroup;

    [Header("# Player")]
    public GameObject selectedPlayer; //프리팹
    public GameObject selectPlayer;
    public GameObject playerSettingMenu;
    public GameObject playerGroup;
    public GameObject[] players;
    Transform playerTrans;
    [Header("# Weapon")]
    public GameObject selectedWeapon;   //프리팹
    public GameObject selectWeapon;
    public GameObject weaponSettingMenu;
    public GameObject weaponGroup;
    public GameObject[] weapons;
    Transform weaponTrans;
    [Header("# Difficult")]
    public GameObject selectedDifficult; //프리팹
    public GameObject selectDifficult;
    public GameObject difficultSettingMenu;
    public GameObject difficultGroup;
    public GameObject[] difficults;
    Transform difficultTrans;
    public GameObject option;
    public Canvas canvas;

    [Header("프리팹")]
    public GameObject obj;
    //public GameObject prefab_UpdateManager;
    //public GameObject importer;
    //public GameObject prefab_Option;
    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GameObject objs = Instantiate(obj);
        option = objs.transform.GetChild(2).gameObject;
        //GameObject obj1 = Instantiate(prefab_UpdateManager);
        //GameObject obj2 = Instantiate(importer);
        //GameObject obj3 = Instantiate(prefab_Option);
        //option = obj3;

        playerTrans = playerGroup.transform;
        players = new GameObject[playerTrans.childCount];
        for(int i = 0; i < players.Length; i++)
        {
            players[i] = playerTrans.GetChild(i).gameObject;
        }

        weaponTrans = weaponGroup.transform;
        weapons = new GameObject[weaponTrans.childCount];
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = weaponTrans.GetChild(i).gameObject;
        }

        difficultTrans = difficultGroup.transform;
        difficults = new GameObject[difficultTrans.childCount];
        for (int i = 0; i < difficults.Length; i++)
        {
            difficults[i] = difficultTrans.GetChild(i).gameObject;
        }
    }

    public void RandomPlayer()  //랜덤 캐릭터
    {
        int i = Random.Range(1, players.Length);
        selectPlayer = players[i];
        selectPlayer.GetComponent<ForSettingPlayer>().ClickPlayer();
    }
    public void RandomWeapon()  //랜덤 무기
    {
        int i = Random.Range(1, weapons.Length);
        selectWeapon = weapons[i];
        selectWeapon.GetComponent<ForSettingWeapon>().ClickWeapon();
    }

    public void StartBtn() //게임 시작
    {
        playerSettingMenu.SetActive(true);
    }

    public void ReturnMenu() //메뉴 돌아가기
    {
        Destroy(selectedPlayer);
        selectedPlayer = null;
        selectPlayer = null;
        playerSettingMenu.SetActive(false);
    }

    public void ReturnPlayerMenu()
    {
        Destroy(selectedWeapon);
        selectedWeapon = null;
        selectWeapon = null;
        selectedPlayer.transform.SetParent(playerSetGroup);
        weaponSettingMenu.SetActive(false);
        playerSettingMenu.SetActive(true);
    }
    public void ReturnWeaponMenu()
    {
        Destroy(selectedDifficult);
        selectedDifficult = null;
        selectDifficult = null;
        selectedPlayer.transform.SetParent(weaponSetGroup);
        selectedWeapon.transform.SetParent(weaponSetGroup);

        difficultSettingMenu.SetActive(false);
        weaponSettingMenu.SetActive(true);
    }
    public void GoWeaponMenu() //무기 창 넘어가기
    {
        if (selectedPlayer == null) //경고 창
        {
            Debug.Log("캐릭터 선택 필요");
        }
        else if (selectedPlayer != null)
        {
            selectedPlayer.transform.SetParent(weaponSetGroup);
            playerSettingMenu.SetActive(false);
            weaponSettingMenu.SetActive(true);
        }
    }

    public void GoDifficultMenu()
    {
        if (selectedWeapon == null) //경고 창
        {
            Debug.Log("무기 선택 필요");
        }
        else if (selectedWeapon != null)
        {
            selectedPlayer.transform.SetParent(difficultSetGroup);
            selectedWeapon.transform.SetParent(difficultSetGroup);
            weaponSettingMenu.SetActive(false);
            difficultSettingMenu.SetActive(true);
        }
    }

    public void GameStartBtn() //스테이지 시작버튼
    {
        if (selectedDifficult == null)
        {
            Debug.Log("난이도 선택 필요");
        }
        else if (selectedDifficult != null)
        {
            canvas.gameObject.SetActive(false);
            Camera.main.gameObject.SetActive(false);
            LoadingSceneManager.LoadScene("Stage");
        }
    }
}
