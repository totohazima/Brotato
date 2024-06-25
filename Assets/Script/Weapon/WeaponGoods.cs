using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponGoods : Weapon, UI_Upadte
{
    public Weapons index;
    public WeaponType atkType;
    public Image backgroundImage;
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

    public Image lockUI;
    public Outline line;
    public Text priceText;
    private bool isPriceEnd;
    private bool isLock;
    WeaponScrip scriptable;
    [SerializeField]
    WeaponScrip[] weaponData;
    void OnEnable() //생성시 티어를 정한다 (현재 1티어만 존재)
    {
        UIUpdateManager.uiUpdates.Add(this);
        isPriceEnd = false;
    }
    void OnDisable()
    {
        UIUpdateManager.uiUpdates.Remove(this);

    }

    public void Init(WeaponScrip scrip/*string name, string setName, Weapons code, Sprite image, WeaponType type*/)
    {
        scriptable = scrip;
        weaponName.text = scrip.weaponName;
        weaponSetType.text = scrip.setType;
        index = scrip.weaponNickNames;
        weaponImage.sprite = scrip.weaponImage;
        atkType = scrip.attackType;

        //무기티어 확률계산
        WeaponPercentageInfoTable.Data[] percentData = new WeaponPercentageInfoTable.Data[GameManager.instance.gameDataBase.weaponPercentageInfoTable.table.Length];
        for (int i = 0; i < percentData.Length; i++)
        {
            percentData[i] = GameManager.instance.gameDataBase.weaponPercentageInfoTable.table[i];
        }
        int waveNum = StageManager.instance.waveLevel + 1;
        float totalChance = 100;
        float tier1 = 0;
        float tier2 = 0;
        float tier3 = 0;
        float tier4 = 0;

        for (int i = percentData.Length - 1; i >= 0; i--)
        {
            WeaponPercentageInfoTable.Data data = percentData[i];
            if (waveNum >= data.available_Wave)
            {
                float chance = (data.chancePerWave * (waveNum - data.minWave) + data.baseChance) * (1 + (StageManager.instance.playerInfo.lucky / 100));
                float divide =  totalChance / 100;
                if(chance > data.maxChance)
                {
                    chance = data.maxChance;
                }

                chance = chance * divide;
                if (chance > totalChance)
                {
                    chance = totalChance;
                    if (totalChance <= 0)
                    {
                        totalChance -= chance;
                    }
                }
                else
                {
                    totalChance -= chance;
                }

                switch (i)
                {
                    case 3:
                        tier4 = chance;
                        break;
                    case 2:
                        tier3 = chance;
                        break;
                    case 1:
                        tier2 = chance;
                        break;
                    case 0:
                        tier1 = chance;
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 3:
                        tier4 = 0;
                        break;
                    case 2:
                        tier3 = 0;
                        break;
                    case 1:
                        tier2 = 0;
                        break;
                    case 0:
                        tier1 = 0;
                        break;
                }
            }
        }
        ///확률 보여주기용
        for (int i = 0; i < GameManager.instance.weaponTierChance.Length; i++)
        {
            switch (i)
            {
                case 0:
                    GameManager.instance.weaponTierChance[i] = Mathf.Round(tier1 * 100) * 0.01f;
                    break;
                case 1:
                    GameManager.instance.weaponTierChance[i] = Mathf.Round(tier2 * 100) * 0.01f;
                    break;
                case 2:
                    GameManager.instance.weaponTierChance[i] = Mathf.Round(tier3 * 100) * 0.01f;
                    break;
                case 3:
                    GameManager.instance.weaponTierChance[i] = Mathf.Round(tier4 * 100) * 0.01f;
                    break;
            }
        }
        ///

        float[] chanceLise = { tier1, tier2, tier3, tier4 };
        int tier = StageManager.instance.Judgment(chanceLise);
        weaponTier = tier;
        StatSetting((int)index, tier);
    }
    public void UI_Update()
    {
        Player player = StageManager.instance.playerInfo;

        float[] multiple = new float[multipleDamaeCount];
        float temporaryDamage = 0;
        for (int i = 0; i < multipleDamaeCount; i++)
        {
            switch (multipleDamageType[i])
            {
                case DamageType.MELEE_DAMAGE:
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
        if (afterDamage < 1)
        {
            afterDamage = 1;
        }
        afterCriticalChance = criticalChance + player.criticalChance;
        afterCoolTime = coolTime - (coolTime * (player.attackSpeed / 100));
        afterRange = range + player.range;
        afterKnockBack = knockBack + player.knockBack;
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
                case DamageType.MELEE_DAMAGE:
                    damageNumUI.text += "<sprite=0>";
                    break;
                case DamageType.RANGE_DAMAGE:
                    damageNumUI.text += "<sprite=1>";
                    break;
                case DamageType.ELEMENTAL:
                    damageNumUI.text += "<sprite=2>";
                    break;
                case DamageType.HEALTH:
                    damageNumUI.text += "<sprite=3>";
                    break;
                case DamageType.ENGINE:
                    damageNumUI.text += "<sprite=4>";
                    break;
                case DamageType.RANGE:
                    damageNumUI.text += "<sprite=5>";
                    break;
                case DamageType.ARMOR:
                    damageNumUI.text += "<sprite=6>";
                    break;
                case DamageType.LUCK:
                    damageNumUI.text += "<sprite=7>";
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
            GameManager game = GameManager.instance;
            infoUI.gameObject.SetActive(true);
            switch (index)
            {
                case Weapons.SHREDDER:
                    switch (weaponTier)
                    {
                        case 0:
                            infoUI.text = scriptable.tier1_Info[0] + " <color=#4CFF52>" + scriptable.tier1_InfoStat[0] + "</color>" + scriptable.tier1_Info[1];
                            break;
                        case 1:
                            infoUI.text = scriptable.tier2_Info[0] + " <color=#4CFF52>" + scriptable.tier2_InfoStat[0] + "</color>" + scriptable.tier2_Info[1];
                            break;
                        case 2:
                            infoUI.text = scriptable.tier3_Info[0] + " <color=#4CFF52>" + scriptable.tier3_InfoStat[0] + "</color>" + scriptable.tier3_Info[1];
                            break;
                        case 3:
                            infoUI.text = scriptable.tier4_Info[0];
                            break;
                    }
                    break;
                case Weapons.WRENCH:
                    switch (weaponTier)
                    {
                        case 0:
                            infoUI.text = scriptable.tier1_InfoStat[0] + "(" + scriptable.tier1_InfoStat[1] + "<sprite=4>) " + scriptable.tier1_Info[0];
                            break;
                        case 1:
                            infoUI.text = scriptable.tier2_InfoStat[0] + "(" + scriptable.tier2_InfoStat[1] + "<sprite=4>) " + scriptable.tier2_Info[0] + scriptable.tier2_InfoStat[2] + scriptable.tier2_Info[1];
                            break;
                        case 2:
                            infoUI.text = scriptable.tier3_InfoStat[0] + "(" + scriptable.tier3_InfoStat[1] + "<sprite=4>) " + scriptable.tier3_Info[0] + scriptable.tier3_InfoStat[2] + scriptable.tier3_Info[1];
                            break;
                        case 3:
                            infoUI.text = scriptable.tier4_InfoStat[0] + "(" + scriptable.tier4_InfoStat[1] + "<sprite=4>) " + scriptable.tier4_Info[0] + scriptable.tier4_InfoStat[2] + scriptable.tier4_Info[1];
                            break;
                    }
                    break;
                case Weapons.DRIVER:
                    switch (weaponTier)
                    {
                        case 0:
                            infoUI.text = scriptable.tier1_Info[0] + " <color=#4CFF52>" + scriptable.tier1_InfoStat[0].ToString("F2") + "</color>" + scriptable.tier1_Info[1];
                            break;
                        case 1:
                            infoUI.text = scriptable.tier2_Info[0] + " <color=#4CFF52>" + scriptable.tier2_InfoStat[0].ToString("F2") + "</color>" + scriptable.tier2_Info[1];
                            break;
                        case 2:
                            infoUI.text = scriptable.tier3_Info[0] + " <color=#4CFF52>" + scriptable.tier3_InfoStat[0].ToString("F2") + "</color>" + scriptable.tier3_Info[1];
                            break;
                        case 3:
                            infoUI.text = scriptable.tier4_Info[0] + " <color=#4CFF52>" + scriptable.tier4_InfoStat[0].ToString("F2") + "</color>" + scriptable.tier4_Info[1];
                            break;
                    }
                    break;
                case Weapons.WAND:
                    string damageTxt = null;
                    switch (weaponTier)
                    {
                        case (0):
                            damageTxt = (scriptable.tier1_InfoStat[0] + (game.playerAct.elementalDamage * (scriptable.tier1_InfoStat[2] / 100)) * (1 + (game.playerAct.persentDamage / 100))).ToString("F0");
                            infoUI.text = "<color=#4CFF52>" + damageTxt + "</color>x<color=#4CFF52>" + scriptable.tier1_InfoStat[1] + "</color>";
                            infoUI.text += "(<sprite=2>)" + scriptable.tier1_Info[0];
                            break;
                        case (1):
                            damageTxt = (scriptable.tier2_InfoStat[0] + (game.playerAct.elementalDamage * (scriptable.tier2_InfoStat[2] / 100)) * (1 + (game.playerAct.persentDamage / 100))).ToString("F0");
                            infoUI.text = "<color=#4CFF52>" + damageTxt + "</color>x<color=#4CFF52>" + scriptable.tier2_InfoStat[1] + "</color>";
                            infoUI.text += "(<sprite=2>)" + scriptable.tier2_Info[0];
                            break;
                        case (2):
                            damageTxt = (scriptable.tier3_InfoStat[0] + (game.playerAct.elementalDamage * (scriptable.tier3_InfoStat[2] / 100)) * (1 + (game.playerAct.persentDamage / 100))).ToString("F0");
                            infoUI.text = "<color=#4CFF52>" + damageTxt + "</color>x<color=#4CFF52>" + scriptable.tier3_InfoStat[1] + "</color>";
                            infoUI.text += "(<sprite=2>)" + scriptable.tier3_Info[0];
                            break;
                        case (3):
                            damageTxt = (scriptable.tier4_InfoStat[0] + (game.playerAct.elementalDamage * (scriptable.tier4_InfoStat[2] / 100)) * (1 + (game.playerAct.persentDamage / 100))).ToString("F0");
                            infoUI.text = "<color=#4CFF52>" + damageTxt + "</color>x<color=#4CFF52>" + scriptable.tier4_InfoStat[1] + "</color>";
                            infoUI.text += "(<sprite=2>)" + scriptable.tier4_Info[0];
                            break;
                    }
                    break;
                case Weapons.TORCH:
                    string damageTxt2 = null;
                    switch (weaponTier)
                    {
                        case (0):
                            damageTxt2 = (scriptable.tier1_InfoStat[0] + (game.playerAct.elementalDamage * (scriptable.tier1_InfoStat[2] / 100)) * (1 + (game.playerAct.persentDamage / 100))).ToString("F0");
                            infoUI.text = "<color=#4CFF52>" + damageTxt2 + "</color>x<color=#4CFF52>" + scriptable.tier1_InfoStat[1] + "</color>";
                            infoUI.text += "(<sprite=2>)" + scriptable.tier1_Info[0];
                            break;
                        case (1):
                            damageTxt2 = (scriptable.tier2_InfoStat[0] + (game.playerAct.elementalDamage * (scriptable.tier2_InfoStat[2] / 100)) * (1 + (game.playerAct.persentDamage / 100))).ToString("F0");
                            infoUI.text = "<color=#4CFF52>" + damageTxt2 + "</color>x<color=#4CFF52>" + scriptable.tier2_InfoStat[1] + "</color>";
                            infoUI.text += "(<sprite=2>)" + scriptable.tier2_Info[0];
                            break;
                        case (2):
                            damageTxt2 = (scriptable.tier3_InfoStat[0] + (game.playerAct.elementalDamage * (scriptable.tier3_InfoStat[2] / 100)) * (1 + (game.playerAct.persentDamage / 100))).ToString("F0");
                            infoUI.text = "<color=#4CFF52>" + damageTxt2 + "</color>x<color=#4CFF52>" + scriptable.tier3_InfoStat[1] + "</color>";
                            infoUI.text += "(<sprite=2>)" + scriptable.tier3_Info[0];
                            break;
                        case (3):
                            damageTxt2 = (scriptable.tier4_InfoStat[0] + (game.playerAct.elementalDamage * (scriptable.tier4_InfoStat[2] / 100)) * (1 + (game.playerAct.persentDamage / 100))).ToString("F0");
                            infoUI.text = "<color=#4CFF52>" + damageTxt2 + "</color>x<color=#4CFF52>" + scriptable.tier4_InfoStat[1] + "</color>";
                            infoUI.text += "(<sprite=2>)" + scriptable.tier4_Info[0];
                            break;
                    }
                    break;
            }

        }
        else
        {
            infoUI.gameObject.SetActive(false);
        }

        //잠겼을 땐 가격이 그대로 유지되어야 함
        if (GameManager.instance.isEnd && isLock)
        {
            // GameManager.instance.isEnd가 true이고, 상품이 잠긴 경우
            // 가격 설정을 건너뛰어야 합니다.
            isPriceEnd = true;  // 가격 설정이 이미 끝난 상태로 설정합니다.
        }
        else if (GameManager.instance.isEnd && !isLock && !isPriceEnd)
        {
            // GameManager.instance.isEnd가 true이고, 상품이 잠긴 상태가 아니며
            // 가격 설정이 아직 되지 않은 경우에만 가격 설정을 합니다.
            PriceSetting();
            isPriceEnd = true;  // 가격 설정을 마쳤음을 표시합니다.
        }

        if (weaponPrice > GameManager.instance.playerInfo.money)
        {
            priceText.text = "<color=red>" + weaponPrice.ToString("F0") + "</color>";
        }
        else
        {
            priceText.text = weaponPrice.ToString("F0");
        }

        //티어 이미지 설정
        if (isLock == true)
        {
            line.effectColor = Color.white;
            lockUI.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        }
        else
        {
            lockUI.color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
            switch (weaponTier)
            {
                case 0:
                    backgroundImage.color = Color.black;
                    line.effectColor = Color.black;
                    weaponName.color = Color.white;
                    break;
                case 1:
                    backgroundImage.color = new Color(5 / 255f, 25 / 255f, 40 / 255f);
                    line.effectColor = new Color(21 / 255f, 178 / 255f, 232 / 255f);
                    weaponName.color = new Color(21 / 255f, 178 / 255f, 232 / 255f);
                    break;
                case 2:
                    backgroundImage.color = new Color(20 / 255f, 10 / 255f, 45 / 255f);
                    line.effectColor = new Color(204 / 255f, 0 / 255f, 255 / 255f);
                    weaponName.color = new Color(204 / 255f, 0 / 255f, 255 / 255f);
                    break;
                case 3:
                    backgroundImage.color = new Color(45 / 255f, 10 / 255f, 10 / 255f);
                    line.effectColor = new Color(250 / 255f, 7 / 255f, 11 / 255f);
                    weaponName.color = new Color(250 / 255f, 7 / 255f, 11 / 255f);
                    break;
            }
        }
    }


    public void BuyWeapon()
    {
        if (StageManager.instance.playerInfo.isFullWeapon == false && GameManager.instance.playerInfo.money >= weaponPrice)
        {
            GameManager.instance.playerInfo.money -= (int)weaponPrice;
            GameObject weapon = Instantiate(weaponData[(int)index].weaponPrefab);
            Weapon weaponInfo = weapon.GetComponent<Weapon>();
            weaponInfo.weaponTier = weaponTier;
            weapon.transform.SetParent(StageManager.instance.playerInfo.weaponMainPos);

            GameManager.instance.playerAct.weapons.Add(weapon.GetComponent<Weapon_Action>());
            UnLockIng();
            ShopManager.instance.goodsList.Remove(gameObject);
            ItemManager.instance.WeaponListUp();
            GameManager.instance.playerInfo.WeaponSetSearch();
            StageManager.instance.playerInfo.StatCalculate();
            gameObject.SetActive(false);
        }
        else if(GameManager.instance.playerInfo.money >= weaponPrice)
        {
            List<Weapon_Action> weapons = GameManager.instance.playerAct.weapons;
            for (int i = 0; i < weapons.Count; i++)
            {
                Weapon_Action weapon = weapons[i];//.GetComponent<Weapon_Action>();
                if(weapon.index == index)
                {
                    if(weapon.weaponTier == weaponTier && weapon.weaponTier < 3)
                    {
                        GameManager.instance.playerInfo.money -= (int)weaponPrice;
                        weapon.weaponTier++;
                        UnLockIng();
                        ShopManager.instance.goodsList.Remove(gameObject);
                        ItemManager.instance.WeaponListUp();
                        GameManager.instance.playerInfo.WeaponSetSearch();
                        StageManager.instance.playerInfo.StatCalculate();
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
        //line.enabled = true;
        //lockUI.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    }
    void UnLockIng()
    {
        ShopManager.instance.lockList.Remove(gameObject);
        isLock = false;
        //line.enabled = false;
        //lockUI.color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
    }

    /// <summary>
    /// 가격 설정
    /// </summary>
    private void PriceSetting()
    {
        WeaponBasePriceInfoTable.Data priceInfoTable = GameManager.instance.gameDataBase.weaponBasePriceInfoTable.table[weaponNum];
        weaponBasePrice = priceInfoTable.weaponBasePrice[weaponTier];
        int wave = StageManager.instance.waveLevel + 1;
        weaponPrice = (weaponBasePrice + wave + (weaponBasePrice * 0.1f * wave)) * 1;
        weaponPrice = weaponPrice * ((100 + StageManager.instance.playerInfo.priceSale) / 100);
        weaponPrice = System.MathF.Round(weaponPrice);
    }
}
