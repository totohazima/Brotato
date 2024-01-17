using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSettingWeapon : Weapon
{
    public Weapons index; //무기 번호

    MainSceneManager main;
    WeaponStatImporter importer;
    Image image;
    public Image weaponImage;
    
    void Awake()
    {
        main = MainSceneManager.instance;
        importer = WeaponStatImporter.instance;
        image = GetComponent<Image>();
        weaponImage = transform.GetChild(0).GetComponent<Image>();

        StatSetting((int)index);
    }
    void Update()
    {
        if (main.selectWeapon == gameObject)
        {
            image.color = new Color(200 / 255f, 200 / 255f, 200 / 255f);
        }
        else if (main.selectWeapon != gameObject)
        {
            image.color = new Color(70 / 255f, 70 / 255f, 70 / 255f);
        }
    }
    

    public void ClickWeapon()
    {
        main.weaponSettingMenu.SetActive(false);
        main.selectWeapon = gameObject;
        main.weaponSettingMenu.SetActive(true);
    }
}
