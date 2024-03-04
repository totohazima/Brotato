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
        infoCoolTime = weapon.coolTime;
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

        damage.text = infoDamage + "(";
        for (int i = 0; i < weapon.multipleDamaeCount; i++)
        {
            if(weapon.multipleDamage[i] != 100) //100%는 숫자가 나오지 않음
            {
                damage.text += weapon.multipleDamage[i] + "%";
            }
            
            switch(weapon.multipleDamageType[i])
            {
                case Weapon.DamageType.MELEE:
                    damage.text += "<sprite=0>";
                    break;
                case Weapon.DamageType.RANGE:
                    damage.text += "<sprite=1>";
                    break;
                case Weapon.DamageType.HEALTH:
                    damage.text += "<sprite=2>";
                    break;
                case Weapon.DamageType.ENGINE:
                    damage.text += "<sprite=3>";
                    break;
            }
        }
        damage.text += ")";

        criticalChance.text = "x" + weapon.criticalDamage + " (" + weapon.criticalChance + "% 확률)";
        coolTime.text = infoCoolTime + "s";
        knockBack.text = infoKnockBack.ToString();
        range.text = infoRange.ToString() + " (" + weapon.typeText + ")";
        if (infoPenetrate > 0)
        {
            penetrateUI.SetActive(true);
            penetrateNum.text = infoPenetrate + "(-" + penetrateDamage + "% 대미지)";
        }
        else
        {
            penetrateUI.SetActive(false);
        }
    }
}
