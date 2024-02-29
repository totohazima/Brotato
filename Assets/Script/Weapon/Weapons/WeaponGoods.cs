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
        afterRange = range + player.range;
        afterKnockBack = knockBack + player.KnockBack;
        afterPenetrate = penetrate + player.penetrate;
        afterBloodSucking = bloodSucking + player.bloodSucking;

        UiVisualize();
    }

    void UiVisualize()
    {
        
        damageNumUI.text = afterDamage + " | " + damage + " (" + multipleDamage[0] + "%<sprite=0>";

        if (knockBack <= 0)
        {
            knockBackUI.gameObject.SetActive(false);
        }
        else
        {
            knockBackUI.gameObject.SetActive(true);
        }
        if (penetrate <= 0)
        {
            penetrateUI.gameObject.SetActive(false);
        }
        else
        {
            penetrateUI.gameObject.SetActive(true);
        }
        if (bloodSucking <= 0)
        {
            bloodSuckingUI.gameObject.SetActive(false);
        }
        else
        {
            bloodSuckingUI.gameObject.SetActive(true);
        }


    }
}
