using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSettingWeapon : Weapon, ICustomUpdateMono
{
    public Weapons index; //무기 번호
    public GameObject weaponPrefab;
    public WeaponScrip weaponScrip;
    GameManager game;
    Image image;
    public Sprite weaponImage;
    public GameObject weaponPrefabs;
    void Awake()
    {
        game = GameManager.instance;
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
        if (game.weapon == this)
        {
            image.color = new Color(200 / 255f, 200 / 255f, 200 / 255f);
        }
        else if (game.weapon != this)
        {
            image.color = new Color(70 / 255f, 70 / 255f, 70 / 255f);
        }
    }
    

    public void ClickWeapon()
    {
        if(game.weapon != null)
        {
            Destroy(game.weapon_Obj);
        }

        game.weapon = this;

        GameObject obj = Instantiate(weaponPrefab);
        game.weapon_Obj = obj;
        obj.transform.SetParent(MainSceneManager.instance.weaponSetGroup);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1, 1, 1);
    }
}
