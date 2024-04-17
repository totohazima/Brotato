using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponGoods : Weapon, ICustomUpdateMono
{
    public Weapons index;
    public WeaponType atkType;
    public Image weaponImage;
    public Text weaponName;
    public Text weaponSetType;

    public Text damageUI;
    public Text criticalUI;
    public Text coolDownUI;
    public Text knockBackUI;
    public Text rangeUI;
    public Text penetrateUI;
    public Text bloodSuckingUI;

    public TextMeshProUGUI damageNumUI;
    public Text criticalNumUI;
    public Text coolDownNumUI;
    public Text knockBackNumUI;
    public Text rangeNumUI;
    public Text penetrateNumUI;
    public Text bloodSuckingNumUI;
    public TextMeshProUGUI infoUI;

    private bool isLock;
    public Image lockUI;
    public Outline line;
    public Text priceText;
    WeaponScrip scriptable;
    [SerializeField]
    WeaponScrip[] weaponData;
    void OnEnable() //생성시 티어를 정한다 (현재 1티어만 존재)
    {
        CustomUpdateManager.customUpdates.Add(this);

    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void Init(WeaponScrip scrip/*string name, string setName, Weapons code, Sprite image, WeaponType type*/)
    {
        scriptable = scrip;
        weaponName.text = scrip.weaponName;
        weaponSetType.text = scrip.setType;
        index = scrip.weaponNickNames;
        weaponImage.sprite = scrip.weaponImage;
        atkType = scrip.attackType;
        StatSetting((int)index, 0);
    }
    public void CustomUpdate()
    {
        Player player = GameManager.instance.playerInfo;

        float[] multiple = new float[multipleDamaeCount];
        float temporaryDamage = 0;
        for (int i = 0; i < multipleDamaeCount; i++)
        {
            switch (multipleDamageType[i])
            {
                case DamageType.MELEE:
                    multiple[i] = player.meleeDamage * (multipleDamage[i] / 100);
                    break;
                case DamageType.RANGE:
                    multiple[i] =  player.rangeDamage * (multipleDamage[i] / 100);
                    break;
                case DamageType.HEALTH:
                    multiple[i] = player.maxHealth * (multipleDamage[i] / 100);
                    break;
                case DamageType.ENGINE:
                    multiple[i] = player.engine * (multipleDamage[i] / 100);
                    break;
            }
            temporaryDamage += multiple[i];
        }
        
        afterDamage = (damage + temporaryDamage) * (1 + (player.persentDamage / 100));
        afterCriticalChance = criticalChance + player.criticalChance;
        afterCoolTime = coolTime - (coolTime * (player.attackSpeed / 100));
        afterRange = range + player.range;
        afterKnockBack = knockBack + player.KnockBack;
        afterPenetrate = penetrate + player.penetrate;
        afterBloodSucking = bloodSucking + player.bloodSucking;

        UiVisualize();
    }

    void UiVisualize()
    {
        //대미지 UI
        if (damage == afterDamage)//같은 대미지
        {
            if (bulletCount > 1)
            {
                damageNumUI.text = damage + "x" + bulletCount + "(";
            }
            else
            {
                damageNumUI.text = damage + "(";
            }
        }
        else if(damage > afterDamage)//원본보다 낮을 경우
        {
            if (bulletCount > 1)
            {
                damageNumUI.text = "<color=red>" + afterDamage.ToString("F0") + "x" + bulletCount + "</color> | " + damage + "x" + bulletCount + " (";
            }
            else
            {
                damageNumUI.text = "<color=red>" + afterDamage.ToString("F0") + "</color> | " + damage + " (";
            }    
        }
        else //원본보다 높을 경우
        {
            if (bulletCount > 1)
            {
                damageNumUI.text = "<color=#4CFF52>" + afterDamage.ToString("F0") + "x" + bulletCount + "</color> | " + damage + "x" + bulletCount + " (";
            }
            else
            {
                damageNumUI.text = "<color=#4CFF52>" + afterDamage.ToString("F0") + "</color> | " + damage + " (";
            }
        }

        for (int i = 0; i < multipleDamaeCount; i++)
        {
            if(multipleDamage[i] != 100) //100%는 숫자가 나오지 않음
            {
                damageNumUI.text += multipleDamage[i] + "%";
            }

            switch (multipleDamageType[i])
            {
                case DamageType.MELEE:
                    damageNumUI.text += "<sprite=0>";
                    break;
                case DamageType.RANGE:
                    damageNumUI.text += "<sprite=1>";
                    break;
                case DamageType.HEALTH:
                    damageNumUI.text += "<sprite=2>";
                    break;
                case DamageType.ENGINE:
                    damageNumUI.text += "<sprite=3>";
                    break;
            }
        }
        damageNumUI.text += ")";

        //크리티컬 UI
        criticalNumUI.text = "x" + criticalDamage + "(";
        if(criticalChance == afterCriticalChance)
        {
            criticalNumUI.text += afterCriticalChance + "% 확률)";
        }
        else if(criticalChance > afterCriticalChance)
        {
            criticalNumUI.text += "<color=red>" + afterCriticalChance + "</color>% 확률)";
        }
        else if (criticalChance < afterCriticalChance)
        {
            criticalNumUI.text += "<color=#4CFF52>" + afterCriticalChance + "</color>% 확률)";
        }

        //쿨타임 UI
        if(coolTime == afterCoolTime)
        {
            coolDownNumUI.text = afterCoolTime.ToString("F2") + "s";
        }
        else if (coolTime < afterCoolTime)
        {
            coolDownNumUI.text = "<color=red>" + afterCoolTime.ToString("F2") + "</color>s";
        }
        else
        {
            coolDownNumUI.text = "<color=#4CFF52>" + afterCoolTime.ToString("F2") + "</color>s";
        }

        //넉백
        if (afterKnockBack <= 0)
        {
            knockBackUI.gameObject.SetActive(false);
        }
        else
        {
            knockBackUI.gameObject.SetActive(true);
            knockBackNumUI.text = "<color=#4CFF52>" + afterKnockBack + "</color>";
        }

        //범위
        if (range == afterRange)
        {
            rangeNumUI.text = afterRange.ToString();
        }
        else if (range > afterRange)
        {
            rangeNumUI.text = "<color=red>" + afterRange + "</color>|<color=grey>" + range + "</color>";
        }
        else
        {
            rangeNumUI.text = "<color=#4CFF52>" + afterRange + "</color>|<color=grey>" + range + "</color>";
        }
        rangeNumUI.text += "(" + typeText + ")";

        //관통
        if (atkType == WeaponType.MELEE)
        {
            penetrateUI.gameObject.SetActive(false);
        }
        else
        {
            if (afterPenetrate <= 0)
            {
                penetrateUI.gameObject.SetActive(false);
            }
            else
            {
                penetrateUI.gameObject.SetActive(true);
                penetrateNumUI.text = "<color=#4CFF52>" + afterPenetrate + "</color>";
            }
        }

        //흡혈
        if (afterBloodSucking <= 0)
        {
            bloodSuckingUI.gameObject.SetActive(false);
        }
        else
        {
            bloodSuckingUI.gameObject.SetActive(true);
            bloodSuckingNumUI.text = "<color=#4CFF52>" + afterBloodSucking + "</color>%";
        }

        //무기 설명
        if (scriptable.tier1_Info[0] != "") //설명이 있을 경우
        {
            infoUI.gameObject.SetActive(true);
            switch (index)
            {
                case Weapon.Weapons.SHREDDER:
                    infoUI.text = scriptable.tier1_Info[0] + " <color=#4CFF52>" + scriptable.tier1_InfoStat[0] + "</color>" + scriptable.tier1_Info[1];
                    break;
                case Weapon.Weapons.WRENCH:
                    infoUI.text = scriptable.tier1_InfoStat[0] + "(" + scriptable.tier1_InfoStat[1] + "<sprite=3>) " + scriptable.tier1_Info[0];
                    break;
                case Weapon.Weapons.DRIVER:
                    infoUI.text = scriptable.tier1_Info[0] + " <color=#4CFF52>" + scriptable.tier1_InfoStat[0].ToString("F2") + "</color>" + scriptable.tier1_Info[1];
                    break;
            }

        }
        else
        {
            infoUI.gameObject.SetActive(false);
        }

        //가격 설정
        ShopBasePriceImporter priceImporter = ShopBasePriceImporter.instance;
        for (int z = 0; z < priceImporter.weaponCode.Length; z++)
        {
            if (weaponCode == priceImporter.weaponCode[z])
            {
                weaponBasePrice = priceImporter.weaponBasePrice[z];
                break;
            }
        }
        int wave = GameManager.instance.waveLevel + 1;
        weaponPrice = (weaponBasePrice + wave + (weaponBasePrice * 0.1f * wave)) * 1;
        weaponPrice = weaponPrice * ((100 + ItemEffect.instance.Coupon()) / 100);
        weaponPrice = System.MathF.Round(weaponPrice);
        priceText.text = weaponPrice.ToString("F0");
    }


    public void BuyWeapon()
    {
        if (GameManager.instance.playerInfo.isFullWeapon == false && GameManager.instance.money >= weaponPrice)
        {
            GameManager.instance.money -= (int)weaponPrice;
            GameObject weapon = Instantiate(weaponData[(int)index].weaponPrefab);
            weapon.transform.SetParent(GameManager.instance.playerInfo.weaponMainPos);
            GameManager.instance.playerInfo.weapons.Add(weapon);
            UnLockIng();
            ShopManager.instance.goodsList.Remove(gameObject);
            ItemManager.instance.WeaponListUp();
            //ItemManager.instance.WeaponListUp(ShopManager.instance.tabsScroll[0], ShopManager.instance.verticalTabsScroll[0], PauseUI_Manager.instance.scrollContents[0]);
            WeaponManager.instance.WeaponSetSearch();
            GameManager.instance.playerInfo.StatCalculate();
            gameObject.SetActive(false);
        }
        else if(GameManager.instance.money >= weaponPrice)
        {
            List<GameObject> weapons = GameManager.instance.playerInfo.weapons;
            for (int i = 0; i < weapons.Count; i++)
            {
                Weapon_Action weapon = weapons[i].GetComponent<Weapon_Action>();
                if(weapon.index == index)
                {
                    if(weapon.weaponTier == weaponTier && weapon.weaponTier < 3)
                    {
                        GameManager.instance.money -= (int)weaponPrice;
                        weapon.weaponTier++;
                        UnLockIng();
                        ShopManager.instance.goodsList.Remove(gameObject);
                        ItemManager.instance.WeaponListUp();
                        WeaponManager.instance.WeaponSetSearch();
                        GameManager.instance.playerInfo.StatCalculate();
                        gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
    public void Lock()
    {
        if (isLock == false)
        {
            LockIng();
        }
        else
        {
            UnLockIng();
        }
    }
    void LockIng()
    {
        ShopManager.instance.lockList.Add(gameObject);
        isLock = true;
        line.enabled = true;
        lockUI.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    }
    void UnLockIng()
    {
        ShopManager.instance.lockList.Remove(gameObject);
        isLock = false;
        line.enabled = false;
        lockUI.color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
    }
}
