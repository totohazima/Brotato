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

    public override void DamageCalculator(float damage, int per, float accuracy, bool isCrutical, float criticalDamage, float knockBack, Vector3 bulletPos)
    {
        curHealth--;
    }
}
