using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponGoods : Weapon
{
    public Weapons index;

    public Text damageUI;
    public Text criticalUI;
    public Text coolDownUI;
    public Text knockBackUI;
    public Text penetrateUI;
    public Text bloodSuckingUI;

    public TextMeshProUGUI damageNumUI;
    public Text criticalNumUI;
    public Text coolDownNumUI;
    public Text knockBackNumUI;
    public Text penetrateNumUI;
    public Text bloodSuckingNumUI;
    public void Init()
    {
        
    }

    void Update()
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
            damageNumUI.text = damage + "(";
        }
        else if(damage > afterDamage)//원본보다 낮을 경우
        {
            damageNumUI.text = "<color=red>" + afterDamage + "</color> | " + damage + " (";
        }
        else //원본보다 높을 경우
        {
            damageNumUI.text = "<color=#4CFF52>" + afterDamage + "</color> | " + damage + " (";
        }

        for (int i = 0; i < multipleDamaeCount; i++)
        {
            if(multipleDamage[i] == 100) //100%는 숫자가 나오지 않음
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
            coolDownNumUI.text = afterCoolTime + "s";
        }
        else if (coolTime < afterCoolTime)
        {
            coolDownNumUI.text = "<color=red>" + afterCoolTime + "</color>s";
        }
        else
        {
            coolDownNumUI.text = "<color=#4CFF52>" + afterCoolTime + "</color>s";
        }

        if (knockBack <= 0)
        {
            knockBackUI.gameObject.SetActive(false);
        }
        else
        {
            knockBackUI.gameObject.SetActive(true);
            knockBackNumUI.text = "<color=#4CFF52>" + afterKnockBack + "</color>";
        }
        if (penetrate <= 0)
        {
            penetrateUI.gameObject.SetActive(false);
        }
        else
        {
            penetrateUI.gameObject.SetActive(true);
            penetrateNumUI.text = "<color=#4CFF52>" + afterPenetrate + "</color>";
        }
        if (bloodSucking <= 0)
        {
            bloodSuckingUI.gameObject.SetActive(false);
        }
        else
        {
            bloodSuckingUI.gameObject.SetActive(true);
            bloodSuckingNumUI.text = "<color=#4CFF52>" + afterBloodSucking + "</color>%";
        }


    }
}
