using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;

    public Transform playerSetGroup;
    public Transform weaponSetGroup;

    [Header("# Player")]
    public GameObject selectedPlayer; //������
    public GameObject selectPlayer;
    public GameObject playerSettingMenu;
    public GameObject playerGroup;
    public GameObject[] players;
    Transform playerTrans;
    [Header("# Weapon")]
    public GameObject selectedWeapon;   //������
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

    public void RandomPlayer()  //���� ĳ����
    {
        int i = Random.Range(1, players.Length);
        selectPlayer = players[i];
        selectPlayer.GetComponent<ForSettingPlayer>().ClickPlayer();
    }
    public void RandomWeapon()  //���� ����
    {
        int i = Random.Range(1, weapons.Length);
        selectWeapon = weapons[i];
        selectWeapon.GetComponent<ForSettingWeapon>().ClickWeapon();
    }

    public void StartBtn() //���� ����
    {
        playerSettingMenu.SetActive(true);
    }

    public void ReturnMenu() //�޴� ���ư���
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
    public void GoWeaponMenu() //���� â �Ѿ��
    {
        if (selectedPlayer == null) //��� â
        {
            Debug.Log("ĳ���� ���� �ʿ�");
        }
        else if (selectedPlayer != null)
        {
            selectedPlayer.transform.SetParent(weaponSetGroup);
            playerSettingMenu.SetActive(false);
            weaponSettingMenu.SetActive(true);
        }
    }

    public void GameStartBtn() //�������� ���۹�ư
    {
        if (selectedWeapon == null)
        {
            Debug.Log("���� ���� �ʿ�");
        }
        else if (selectedWeapon != null)
        {
            canvas.gameObject.SetActive(false);
            Camera.main.gameObject.SetActive(false);
            LoadingSceneManager.LoadScene("Stage");
        }
    }
}
