using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSettingWeapon : Weapon, ICustomUpdateMono
{
    public Weapons index; //무기 번호
    public GameObject weaponPrefab;
    public WeaponScrip weaponScrip;
    MainSceneManager main;
    WeaponStatImporter importer;
    Image image;
    public Sprite weaponImage;
    public GameObject weaponPrefabs;
    void Awake()
    {
        main = MainSceneManager.instance;
        //importer = WeaponStatImporter.instance;
        image = GetComponent<Image>();
        weaponImage = weaponScrip.weaponImage;
        weaponPrefabs = weaponScrip.weaponPrefab;

        StatSetting((int)index, 0);
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }
    public void CustomUpdate()
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
        if(main.selectWeapon != null)
        {
            Destroy(main.selectedWeapon);
            main.selectedWeapon = null;
        }

        main.selectWeapon = gameObject;

        GameObject obj = Instantiate(weaponPrefab);
        main.selectedWeapon = obj;
        obj.transform.SetParent(main.weaponSetGroup);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1, 1, 1);
    }
}
