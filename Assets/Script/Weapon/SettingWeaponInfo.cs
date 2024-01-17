using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWeaponInfo : MonoBehaviour
{
    Weapon weapon;
    public Image myImage;
    Image weaponImage;

    public Text name;
    public Text damage;
    public Text criticalChance;
    public Text coolTime;
    public Text knockBack;
    public Text range;

    string infoName;
    float infoDamage;
    float infoCriticalChance;
    float infoCoolTime;
    float infoKnockBack;
    float infoRange;

    void OnEnable()
    {
        weapon = MainSceneManager.instance.selectWeapon.GetComponent<Weapon>();
        weaponImage = MainSceneManager.instance.selectWeapon.GetComponent<ForSettingWeapon>().weaponImage;

        myImage.sprite = weaponImage.sprite;
        infoName = weapon.name;
        infoDamage = weapon.damage;
        infoCriticalChance = weapon.criticalChance;
        infoCoolTime = weapon.coolTIme;
        infoKnockBack = weapon.knockBack;
        infoRange = weapon.range;

        name.text = infoName.ToString();
        damage.text = infoDamage.ToString();
        criticalChance.text = infoCriticalChance.ToString();
        coolTime.text = infoCoolTime.ToString();
        knockBack.text = infoKnockBack.ToString();
        range.text = infoRange.ToString() + " (" + weapon.typeText + ")";
    }
}
