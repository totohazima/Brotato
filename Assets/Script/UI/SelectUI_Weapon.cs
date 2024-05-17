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
    /// ������ ĳ���� ���� ������ �� �ִ� ���� ����
    /// </summary>
    void OnEnable()
    {
        //�ٰŸ� ����� ����
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
        //���Ÿ� ����� ����
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
    public override void RandomSelect() //�������� ���� ����
    {
        int i = Random.Range(1, forSettingWeapons.Length);
        ForSettingWeapon setting = forSettingWeapons[i];
        setting.ClickWeapon();
    }

    public override void NextMenu()
    {
        if (GameManager.instance.weapon == null) //���
        {
            Debug.Log("���� ���� �ʿ�");
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
