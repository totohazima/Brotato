using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI_Weapon : SelectUI
{
    private ForSettingWeapon[] forSettingWeapons;
    void Awake()
    {
        forSettingWeapons = new ForSettingWeapon[selectables.Length];
        for (int i = 0; i < selectables.Length; i++)
        {
            forSettingWeapons[i] = selectables[i].GetComponent<ForSettingWeapon>();
        }
    }
    /// <summary>
    /// 선택한 캐릭에 따라 선택할 수 있는 무기 조절
    /// </summary>
    void OnEnable()
    {
        //근거리 무기는 제외
        if(GameManager.instance.character == Player.Character.RANGER)
        {
            for (int i = 0; i < selectables.Length; i++)
            {
                if(forSettingWeapons[i].weaponScrip.attackType == Weapon.WeaponType.MELEE)
                {
                    selectables[i].SetActive(false);
                }
                else
                {
                    selectables[i].SetActive(true);
                }
            }
        }
        //원거리 무기는 제외
        else if (GameManager.instance.character == Player.Character.GLADIATOR)
        {
            for (int i = 0; i < selectables.Length; i++)
            {
                if (forSettingWeapons[i].weaponScrip.attackType == Weapon.WeaponType.RANGE)
                {
                    selectables[i].SetActive(false);
                }
                else
                {
                    selectables[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < selectables.Length; i++)
            {
                selectables[i].SetActive(true);
            }
        }
    }
    public override void RandomSelect() //랜덤으로 무기 선택
    {
        List<ForSettingWeapon> randomList = new List<ForSettingWeapon>();

        switch (GameManager.instance.character)
        {
            //레인저: 근접무기 탑재 불가
            case Player.Character.RANGER:
                for (int i = 0; i < forSettingWeapons.Length; i++)
                {
                    if(forSettingWeapons[i].weaponScrip.attackType == Weapon.WeaponType.RANGE)
                    {
                        randomList.Add(forSettingWeapons[i]);
                    }
                }
                break;
            //검투사: 원거리 무기 탑재 불가
            case Player.Character.GLADIATOR:
                for (int i = 0; i < forSettingWeapons.Length; i++)
                {
                    if (forSettingWeapons[i].weaponScrip.attackType == Weapon.WeaponType.MELEE)
                    {
                        randomList.Add(forSettingWeapons[i]);
                    }
                }
                break;
            default:
                for (int i = 0; i < forSettingWeapons.Length; i++)
                {
                    randomList.Add(forSettingWeapons[i]);
                }
                break;
        }
        
        int j = Random.Range(0, randomList.Count);
        randomList[j].ClickWeapon();
    }

    public override void NextMenu()
    {
        if (GameManager.instance.weapon == null) //경고
        {
            Debug.Log("무기 선택 필요");
        }
        else if (GameManager.instance.weapon != null)
        {
            GameManager.instance.player_Obj.transform.SetParent(MainSceneManager.instance.difficultSetGroup);
            GameManager.instance.weapon_Obj.transform.SetParent(MainSceneManager.instance.difficultSetGroup);
            MainSceneManager.instance.difficultSettingMenu.SetActive(true);
            MainSceneManager.instance.weaponSettingMenu.SetActive(false);
        }
    }

    public override void BeforeMenu()
    {
        Destroy(GameManager.instance.weapon_Obj);
        GameManager.instance.weapon = null;
        GameManager.instance.weapon_Obj = null;
        GameManager.instance.player_Obj.transform.SetParent(MainSceneManager.instance.playerSetGroup);
        MainSceneManager.instance.playerSettingMenu.SetActive(true);
        MainSceneManager.instance.weaponSettingMenu.SetActive(false);
    }
}
