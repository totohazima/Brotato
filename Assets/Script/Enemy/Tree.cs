using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : EnemyAction
{
    public override void CustomUpdate()
    {
        if (curHealth <= 0)
        {
            isDie = true;
            StartCoroutine(Died());
        }
        else
        {
            isDie = false;
        }

        if (isDie)
        {
            return;
        }

    }

    public override void DamageCalculator(float damage, int per, float accuracy, bool isCritical, float criticalDamage, float knockBack, Vector3 bulletPos)
    {
        float finalDamage = 0;
        string damageText = null;
        if (isCritical == true)
        {
            finalDamage = damage * criticalDamage;
            damageText = "<color=yellow>" + finalDamage.ToString("F0") + "</color>";
        }
        else
        {
            finalDamage = damage;
            damageText = finalDamage.ToString("F0");
        }

        Transform text = DamageTextManager.instance.TextCreate(0, damageText).transform;
        text.position = textPopUpPos.position;

        curHealth--;
    }
}
