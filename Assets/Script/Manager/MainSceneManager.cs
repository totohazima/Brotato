using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager instance;
    public GameObject weaponGroup;
    public GameObject[] weapons;
    Transform groupTrans;
    public GameObject selectWeapon;
    public GameObject weaponSettingMenu;
    public GameObject weaponInfo;
    public Canvas canvas;
    void Awake()
    {
        instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        groupTrans = weaponGroup.transform;
        weapons = new GameObject[groupTrans.childCount];
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = groupTrans.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if(selectWeapon != null)
        {
            weaponInfo.SetActive(true);
        }
        else if(selectWeapon == null)
        {
            weaponInfo.SetActive(false);
        }
    }
    public void RandomWeapon()
    {
        weaponSettingMenu.SetActive(false);
        int i = Random.Range(1, weapons.Length);
        selectWeapon = weapons[i];
        weaponSettingMenu.SetActive(true);
    }

    public void StartBtn()
    {
        weaponSettingMenu.SetActive(true);
    }

    public void ReturnMenu()
    {
        selectWeapon = null;
        weaponSettingMenu.SetActive(false);
    }

    public void GameStartBtn()
    {
        canvas.gameObject.SetActive(false);
        Camera.main.gameObject.SetActive(false);
        LoadingSceneManager.LoadScene("Stage");
    }
}
