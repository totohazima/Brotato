using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;

    public Transform playerSetGroup;
    public Transform weaponSetGroup;

    public GameObject selectPlayer; //������
    public GameObject playerSettingMenu;

    public GameObject weaponGroup;
    public GameObject[] weapons;
    Transform groupTrans;
    public GameObject selectedWeapon;   //������
    public GameObject selectWeapon;
    public GameObject weaponSettingMenu;
    public Canvas canvas;
    void Awake()
    {
        instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        GameObject obj = Resources.Load<GameObject>("Prefabs/Importer");
        Instantiate(obj);

        groupTrans = weaponGroup.transform;
        weapons = new GameObject[groupTrans.childCount];
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = groupTrans.GetChild(i).gameObject;
        }
    }

    public void RandomWeapon()//���� ����;
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
        Destroy(selectPlayer);
        selectPlayer = null;
        playerSettingMenu.SetActive(false);
    }

    public void ReturnPlayerMenu()
    {
        Destroy(selectedWeapon);
        selectedWeapon = null;
        selectWeapon = null;
        selectPlayer.transform.SetParent(playerSetGroup);
        weaponSettingMenu.SetActive(false);
        playerSettingMenu.SetActive(true);
    }
    public void GoWeaponMenu() //���� â �Ѿ��
    {
        if (selectPlayer == null) //��� â
        {

        }
        else if (selectPlayer != null)
        {
            selectPlayer.transform.SetParent(weaponSetGroup);
            playerSettingMenu.SetActive(false);
            weaponSettingMenu.SetActive(true);
        }
    }

    public void GameStartBtn() //�������� ���۹�ư
    {
        canvas.gameObject.SetActive(false);
        Camera.main.gameObject.SetActive(false);
        LoadingSceneManager.LoadScene("Stage");
    }
}
