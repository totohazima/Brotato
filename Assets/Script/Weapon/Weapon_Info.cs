using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon_Info : MonoBehaviour
{
    public Weapon.Weapons weaponCode;
    public Weapon.WeaponType atkType;
    public Transform weaponInfoUI;
    public Transform buttons;
    private WeaponScrip weaponScrip;
    private Weapon_Action weaponInfo;
    public RectTransform bgRect;

    public Image backGround;
    public Outline Frame;
    public Image weaponImage;
    public Text weaponName;
    public Text weaponType;
    public Text damageUI;
    public Text criticalUI;
    public Text coolDownUI;
    public Text knockBackUI;
    public Text rangeUI;
    public Text penetrateUI;
    public Text bloodSuckingUI;
    public GameObject combine_ButtonUI;

    public TextMeshProUGUI damageNumUI;
    public Text criticalNumUI;
    public Text coolDownNumUI;
    public Text knockBackNumUI;
    public Text rangeNumUI;
    public Text penetrateNumUI;
    public Text bloodSuckingNumUI;
    public TextMeshProUGUI infoUI;
    public TextMeshProUGUI recycle_NumUI;
    public GameObject[] settOptionUI;
    public GameObject settUI;
    public GameObject btnGroups;
    public GameObject closeBG;
    private Vector3 itemPos;
    bool isCombined;
    public void Init(WeaponScrip scrip, Weapon_Action weapon_Action, Vector3 pos, bool combined)
    {
        weaponScrip = scrip;
        weaponInfo = weapon_Action;

        weaponCode = weaponScrip.weaponNickNames;
        atkType = weaponScrip.attackType;
        weaponImage.sprite = weaponScrip.weaponImage;
        weaponName.text = weaponScrip.weaponName;
        weaponType.text = weaponScrip.setType;
        itemPos = pos;
        if (GameManager.instance.isPause == false)
        {
            settUI.SetActive(true);
            btnGroups.SetActive(true);
            closeBG.SetActive(true);

            isCombined = combined;
            if (isCombined == true)
            {
                combine_ButtonUI.SetActive(true);
            }
            else
            {
                combine_ButtonUI.SetActive(false);
            }

            for (int i = 0; i < settOptionUI.Length; i++)
            {
                settOptionUI[i].SetActive(false);
            }
            for (int i = 0; i < weaponScrip.weaponSetType.Length; i++)
            {
                switch (weaponScrip.weaponSetType[i])
                {
                    case Weapon.SettType.UNARMED:
                        settOptionUI[0].SetActive(true);
                        break;
                    case Weapon.SettType.TOOL:
                        settOptionUI[1].SetActive(true);
                        break;
                    case Weapon.SettType.GUN:
                        settOptionUI[2].SetActive(true);
                        break;
                    case Weapon.SettType.EXPLOSIVE:
                        settOptionUI[3].SetActive(true);
                        break;
                    case Weapon.SettType.PRECISION:
                        settOptionUI[4].SetActive(true);
                        break;
                    case Weapon.SettType.NATIVE:
                        settOptionUI[5].SetActive(true);
                        break;
                }
            }
        }
        else if(GameManager.instance.isPause == true)
        {
            settUI.SetActive(false);
            btnGroups.SetActive(false);
            closeBG.SetActive(false);
        }
        WeaponInfoSetting();
        //StartCoroutine(WeaponInfoSetting());
    }

    public void WeaponInfoSetting()
    {
        switch (weaponInfo.weaponTier)
        {
            case 0:
                backGround.color = Color.black;
                Frame.effectColor = Color.black;
                weaponName.color = Color.white;
                break;
            case 1:
                backGround.color = new Color(5 / 255f, 25 / 255f, 40 / 255f);
                Frame.effectColor = new Color(90 / 255f, 175 / 255f, 250 / 255f);
                weaponName.color = new Color(90 / 255f, 175 / 255f, 250 / 255f);
                break;
            case 2:
                backGround.color = new Color(20 / 255f, 10 / 255f, 45 / 255f);
                Frame.effectColor = new Color(185 / 255f, 90 / 255f, 250 / 255f);
                weaponName.color = new Color(185 / 255f, 90 / 255f, 250 / 255f);
                break;
            case 3:
                backGround.color = new Color(45 / 255f, 10 / 255f, 10 / 255f);
                Frame.effectColor = new Color(250 / 255f, 60 / 255f, 75 / 255f);
                weaponName.color = new Color(250 / 255f, 60 / 255f, 75 / 255f);
                break;
        }

        //대미지 UI
        if (weaponInfo.damage == weaponInfo.afterDamage)//같은 대미지
        {
            if (weaponInfo.afterBulletCount > 1)
            {
                damageNumUI.text = weaponInfo.damage + "x" + weaponInfo.afterBulletCount + "(";
            }
            else
            {
                damageNumUI.text = weaponInfo.damage + "(";
            }
        }
        else if (weaponInfo.damage > weaponInfo.afterDamage)//원본보다 낮을 경우
        {
            if (weaponInfo.afterBulletCount > 1)
            {
                damageNumUI.text = "<color=red>" + weaponInfo.afterDamage.ToString("F0") + "x" + weaponInfo.afterBulletCount + "</color> | " + weaponInfo.damage + "x" + weaponInfo.afterBulletCount + " (";
            }
            else
            {
                damageNumUI.text = "<color=red>" + weaponInfo.afterDamage.ToString("F0") + "</color> | " + weaponInfo.damage + " (";
            }
        }
        else //원본보다 높을 경우
        {
            if (weaponInfo.afterBulletCount > 1)
            {
                damageNumUI.text = "<color=#4CFF52>" + weaponInfo.afterDamage.ToString("F0") + "x" + weaponInfo.afterBulletCount + "</color> | " + weaponInfo.damage + "x" + weaponInfo.afterBulletCount + " (";
            }
            else
            {
                damageNumUI.text = "<color=#4CFF52>" + weaponInfo.afterDamage.ToString("F0") + "</color> | " + weaponInfo.damage + " (";
            }
        }

        for (int i = 0; i < weaponInfo.multipleDamaeCount; i++)
        {
            if (weaponInfo.multipleDamage[i] != 100) //100%는 숫자가 나오지 않음
            {
                damageNumUI.text += weaponInfo.multipleDamage[i] + "%";
            }

            switch (weaponInfo.multipleDamageType[i])
            {
                case Weapon.DamageType.MELEE:
                    damageNumUI.text += "<sprite=0>";
                    break;
                case Weapon.DamageType.RANGE:
                    damageNumUI.text += "<sprite=1>";
                    break;
                case Weapon.DamageType.HEALTH:
                    damageNumUI.text += "<sprite=2>";
                    break;
                case Weapon.DamageType.ENGINE:
                    damageNumUI.text += "<sprite=3>";
                    break;
            }
        }
        damageNumUI.text += ")";

        //크리티컬 UI
        criticalNumUI.text = "x" + weaponInfo.criticalDamage + "(";
        if (weaponInfo.criticalChance == weaponInfo.afterCriticalChance)
        {
            criticalNumUI.text += weaponInfo.afterCriticalChance + "% 확률)";
        }
        else if (weaponInfo.criticalChance > weaponInfo.afterCriticalChance)
        {
            criticalNumUI.text += "<color=red>" + weaponInfo.afterCriticalChance + "</color>% 확률)";
        }
        else if (weaponInfo.criticalChance < weaponInfo.afterCriticalChance)
        {
            criticalNumUI.text += "<color=#4CFF52>" + weaponInfo.afterCriticalChance + "</color>% 확률)";
        }

        //쿨타임 UI
        if (weaponInfo.coolTime == weaponInfo.afterCoolTime)
        {
            coolDownNumUI.text = weaponInfo.afterCoolTime.ToString("F2") + "s";
        }
        else if (weaponInfo.coolTime < weaponInfo.afterCoolTime)
        {
            coolDownNumUI.text = "<color=red>" + weaponInfo.afterCoolTime.ToString("F2") + "</color>s";
        }
        else
        {
            coolDownNumUI.text = "<color=#4CFF52>" + weaponInfo.afterCoolTime.ToString("F2") + "</color>s";
        }

        //넉백
        if (weaponInfo.afterKnockBack <= 0)
        {
            knockBackUI.gameObject.SetActive(false);
        }
        else
        {
            knockBackUI.gameObject.SetActive(true);
            knockBackNumUI.text = "<color=#4CFF52>" + weaponInfo.afterKnockBack + "</color>";
        }

        //범위
        float fixRange = weaponInfo.afterRange * 15;
        if (weaponInfo.range == fixRange)
        {
            rangeNumUI.text = fixRange.ToString("F0");
        }
        else if (weaponInfo.range > fixRange)
        {
            rangeNumUI.text = "<color=red>" + fixRange.ToString("F0") + "</color>|<color=grey>" + weaponInfo.range.ToString("F0") + "</color>";
        }
        else
        {
            rangeNumUI.text = "<color=#4CFF52>" + fixRange.ToString("F0") + "</color>|<color=grey>" + weaponInfo.range.ToString("F0") + "</color>";
        }
        rangeNumUI.text += "(" + weaponInfo.typeText + ")";

        //관통
        if (atkType == Weapon.WeaponType.MELEE)
        {
            penetrateUI.gameObject.SetActive(false);
        }
        else
        {
            if (weaponInfo.afterPenetrate <= 0)
            {
                penetrateUI.gameObject.SetActive(false);
            }
            else
            {
                penetrateUI.gameObject.SetActive(true);
                penetrateNumUI.text = "<color=#4CFF52>" + weaponInfo.afterPenetrate + "</color>";
            }
        }

        //흡혈
        if (weaponInfo.afterBloodSucking <= 0)
        {
            bloodSuckingUI.gameObject.SetActive(false);
        }
        else
        {
            bloodSuckingUI.gameObject.SetActive(true);
            bloodSuckingNumUI.text = "<color=#4CFF52>" + weaponInfo.afterBloodSucking + "</color>%";
        }

        //무기 설명
        if (weaponScrip.tier1_Info[0] != "") //설명이 있을 경우
        {
            infoUI.gameObject.SetActive(true);
            switch (weaponCode)
            {
                case Weapon.Weapons.SHREDDER:
                    switch (weaponInfo.weaponTier)
                    {
                        case (0):
                            infoUI.text = weaponScrip.tier1_Info[0] + " <color=#4CFF52>" + weaponScrip.tier1_InfoStat[0] + "</color>" + weaponScrip.tier1_Info[1];
                            break;
                        case (1):
                            infoUI.text = weaponScrip.tier2_Info[0] + " <color=#4CFF52>" + weaponScrip.tier2_InfoStat[0] + "</color>" + weaponScrip.tier2_Info[1];
                            break;
                        case (2):
                            infoUI.text = weaponScrip.tier3_Info[0] + " <color=#4CFF52>" + weaponScrip.tier3_InfoStat[0] + "</color>" + weaponScrip.tier3_Info[1];
                            break;
                        case (3):
                            infoUI.text = weaponScrip.tier4_Info[0];
                            break;
                    }
                    break;
                case Weapon.Weapons.WRENCH:
                    switch (weaponInfo.weaponTier)
                    {
                        case (0):
                            infoUI.text = weaponScrip.tier1_InfoStat[0] + "(" + weaponScrip.tier1_InfoStat[1] + "<sprite=3>) " + weaponScrip.tier1_Info[0];
                            break;
                        case (1):
                            infoUI.text = weaponScrip.tier2_InfoStat[0] + "(" + weaponScrip.tier2_InfoStat[1] + "<sprite=3>) " + weaponScrip.tier2_Info[0] + " " + weaponScrip.tier2_InfoStat[2] + weaponScrip.tier2_Info[1];
                            break;
                        case (2):
                            infoUI.text = weaponScrip.tier3_InfoStat[0] + "(" + weaponScrip.tier3_InfoStat[1] + "<sprite=3>) " + weaponScrip.tier3_Info[0] + " " + weaponScrip.tier3_InfoStat[2] + weaponScrip.tier3_Info[1];
                            break;
                        case (3):
                            infoUI.text = weaponScrip.tier4_InfoStat[0] + "(" + weaponScrip.tier4_InfoStat[1] + "<sprite=3>) " + weaponScrip.tier4_Info[0] + " " + weaponScrip.tier4_InfoStat[2] + weaponScrip.tier4_Info[1];
                            break;
                    }
                    break;
                case Weapon.Weapons.DRIVER:
                    switch (weaponInfo.weaponTier)
                    {
                        case (0):
                            infoUI.text = weaponScrip.tier1_Info[0] + " <color=#4CFF52>" + weaponScrip.tier1_InfoStat[0].ToString("F2") + "</color>" + weaponScrip.tier1_Info[1];
                            break;
                        case (1):
                            infoUI.text = weaponScrip.tier2_Info[0] + " <color=#4CFF52>" + weaponScrip.tier2_InfoStat[0].ToString("F2") + "</color>" + weaponScrip.tier2_Info[1];
                            break;
                        case (2):
                            infoUI.text = weaponScrip.tier3_Info[0] + " <color=#4CFF52>" + weaponScrip.tier3_InfoStat[0].ToString("F2") + "</color>" + weaponScrip.tier3_Info[1];
                            break;
                        case (3):
                            infoUI.text = weaponScrip.tier4_Info[0] + " <color=#4CFF52>" + weaponScrip.tier4_InfoStat[0].ToString("F2") + "</color>" + weaponScrip.tier4_Info[1];
                            break;
                    }
                    break;
            }

        }
        else
        {
            infoUI.gameObject.SetActive(false);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(bgRect);
        float textHeight = bgRect.rect.height;
        float x = 0;
        float y = 0;
        if (Camera.main.ScreenToWorldPoint(itemPos).x >= 0)
        {
            x = itemPos.x - 200;
        }
        else if (Camera.main.ScreenToWorldPoint(itemPos).x < 0)
        {
            x = itemPos.x + 230;
        }

        if (Camera.main.ScreenToWorldPoint(itemPos).y >= 0)
        {
            y = itemPos.y - textHeight * 9;
        }
        else if (Camera.main.ScreenToWorldPoint(itemPos).y < 0)
        {
            if (isCombined == true)
            {
                y = itemPos.y + textHeight * 8.5f;
            }
            else
            {
                y = itemPos.y + textHeight * 6f;
            }

        }
        transform.position = new Vector3(x, y);
    }
    

    public void Combine()
    {
        List<GameObject> weapons = GameManager.instance.playerInfo.weapons;
        for (int i = 0; i < weapons.Count; i++)
        {
            Weapon_Action weapon = weapons[i].GetComponent<Weapon_Action>();
            if (weapon != weaponInfo)
            {
                if (weapon.weaponTier == weaponInfo.weaponTier && weapon.index == weaponInfo.index)
                {
                    weapon.weaponTier++;
                    GameManager.instance.playerInfo.weapons.Remove(weaponInfo.gameObject);
                    Destroy(weaponInfo.gameObject);
                    ItemManager.instance.WeaponListUp();
                    WeaponManager.instance.WeaponSetSearch();
                    GameManager.instance.playerInfo.StatCalculate();
                    Destroy(gameObject);
                    break;
                }
            }

        }
    }
    public void ReCycle()
    {
        GameManager.instance.playerInfo.weapons.Remove(weaponInfo.gameObject);
        Destroy(weaponInfo.gameObject);
        ItemManager.instance.WeaponListUp();
        WeaponManager.instance.WeaponSetSearch();
        GameManager.instance.playerInfo.StatCalculate();
        Destroy(gameObject);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
