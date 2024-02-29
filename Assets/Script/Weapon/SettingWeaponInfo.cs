using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingWeaponInfo : MonoBehaviour
{
    Weapon weapon;
    public Image myImage;
    Image weaponImage;

    public Text name;
    public Text weaponType;
    public TextMeshProUGUI damage;
    public Text criticalChance;
    public Text coolTime;
    public Text knockBack;
    public Text range;
    public GameObject penetrateUI;
    public Text penetrateNum;

    string infoName;
    string infoType;
    float infoDamage;
    float infoCriticalChance;
    float infoCoolTime;
    float infoKnockBack;
    float infoRange;
    int infoPenetrate;
    float penetrateDamage;

    void OnEnable()
    {
        weapon = MainSceneManager.instance.selectWeapon.GetComponent<Weapon>();
        weaponImage = MainSceneManager.instance.selectWeapon.GetComponent<ForSettingWeapon>().weaponImage;

        myImage.sprite = weaponImage.sprite;
        infoName = weapon.name;
        infoType = weapon.weaponType;
        infoDamage = weapon.damage;
        infoCriticalChance = weapon.criticalChance;
        infoCoolTime = weapon.coolTIme;
        infoKnockBack = weapon.knockBack;
        infoRange = weapon.range;
        infoPenetrate = weapon.penetrate;
        penetrateDamage = weapon.penetrateDamage;

        UIVisualize();

        
    }

    void UIVisualize()
    {
        name.text = infoName.ToString();
        weaponType.text = infoType.ToString();
        damage.text = infoDamage.ToString();
        //damage.text = infoDamage + "(";
        //for (int i = 0; i < weapon.multipleDamaeCount; i++)
        //{
        //    damage.text += weapon.multipleDamage[i] + "%";
        //}
        //damage.text += ")";

        criticalChance.text = "x" + weapon.criticalDamage + " (" + weapon.criticalChance + "% È®·ü)";
        coolTime.text = infoCoolTime + "s";
        knockBack.text = infoKnockBack.ToString();
        range.text = infoRange.ToString() + " (" + weapon.typeText + ")";
        if (infoPenetrate > 0)
        {
            penetrateUI.SetActive(true);
            penetrateNum.text = infoPenetrate + "(-" + penetrateDamage + "% ´ë¹ÌÁö)";
        }
        else
        {
            penetrateUI.SetActive(false);
        }
    }
}
