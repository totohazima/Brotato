using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;

    public Transform playerSetGroup;
    public Transform weaponSetGroup;

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

    public Canvas canvas;
    void Awake()
    {
        instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        GameObject obj = Resources.Load<GameObject>("Prefabs/Importer");
        Instantiate(obj);

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

    public void GameStartBtn() //스테이지 시작버튼
    {
        if (selectedWeapon == null)
        {
            Debug.Log("무기 선택 필요");
        }
        else if (selectedWeapon != null)
        {
            canvas.gameObject.SetActive(false);
            Camera.main.gameObject.SetActive(false);
            LoadingSceneManager.LoadScene("Stage");
        }
    }
}
