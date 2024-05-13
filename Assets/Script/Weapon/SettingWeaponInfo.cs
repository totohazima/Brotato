using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingWeaponInfo : MonoBehaviour
{
    Weapon weapon;
    public Image myImage;
    WeaponScrip weaponScrip;

    public Text name;
    public Text weaponType;
    public TextMeshProUGUI damage;
    public Text criticalChance;
    public Text coolTime;
    public GameObject knockBackUI;
    public Text knockBack;
    public Text range;
    public GameObject penetrateUI;
    public Text penetrateNum;
    public TextMeshProUGUI infoUI;

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
        weaponScrip = MainSceneManager.instance.selectWeapon.GetComponent<ForSettingWeapon>().weaponScrip;

        myImage.sprite = weaponScrip.weaponImage;
        infoName = weaponScrip.weaponName;
        infoType = weaponScrip.setType;
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

        if (weapon.bulletCount > 1)
        {
            damage.text = infoDamage + "x" + weapon.bulletCount + "(";
        }
        else
        {
            damage.text = infoDamage + "(";
        }
        for (int i = 0; i < weapon.multipleDamaeCount; i++)
        {
            if(weapon.multipleDamage[i] != 100) //100%는 숫자가 나오지 않음
            {
                damage.text += weapon.multipleDamage[i] + "%";
            }
            
            switch(weapon.multipleDamageType[i])
            {
                case Weapon.DamageType.MELEE_DAMAGE:
                    damage.text += "<sprite=0>";
                    break;
                case Weapon.DamageType.RANGE_DAMAGE:
                    damage.text += "<sprite=1>";
                    break;
                case Weapon.DamageType.ELEMENTAL:
                    damage.text += "<sprite=2>";
                    break;
                case Weapon.DamageType.HEALTH:
                    damage.text += "<sprite=3>";
                    break;
                case Weapon.DamageType.ENGINE:
                    damage.text += "<sprite=4>";
                    break;
                case Weapon.DamageType.RANGE:
                    damage.text += "<sprite=5>";
                    break;
                case Weapon.DamageType.ARMOR:
                    damage.text += "<sprite=6>";
                    break;
                case Weapon.DamageType.LUCK:
                    damage.text += "<sprite=7>";
                    break;
            }
        }
        damage.text += ")";

        criticalChance.text = "x" + weapon.criticalDamage + " (" + weapon.criticalChance + "% 확률)";
        coolTime.text = infoCoolTime + "s";
        if(infoKnockBack > 0)
        {
            knockBackUI.SetActive(true);
            knockBack.text = infoKnockBack.ToString();
        }
        else
        {
            knockBackUI.SetActive(false);
        }
        
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

        //무기 설명
        if (weaponScrip.tier1_Info[0] != "") //설명이 있을 경우
        {
            infoUI.gameObject.SetActive(true);
            switch (weaponScrip.weaponNickNames)
            {
                case Weapon.Weapons.SHREDDER:
                    infoUI.text = weaponScrip.tier1_Info[0] + " <color=#4CFF52>" + weaponScrip.tier1_InfoStat[0] + "</color>" + weaponScrip.tier1_Info[1];
                    break;
                case Weapon.Weapons.WRENCH:
                    infoUI.text = weaponScrip.tier1_InfoStat[0] + "(" + weaponScrip.tier1_InfoStat[1] + "<sprite=4>) " + weaponScrip.tier1_Info[0];
                    break;
                case Weapon.Weapons.DRIVER:
                    infoUI.text = weaponScrip.tier1_Info[0] + " <color=#4CFF52>" + weaponScrip.tier1_InfoStat[0].ToString("F2") + "</color>" + weaponScrip.tier1_Info[1];
                    break;
            }

        }
        else
        {
            infoUI.gameObject.SetActive(false);
        }
    }
}
