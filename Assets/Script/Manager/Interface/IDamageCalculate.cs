using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageCalculate
{
    void DamageCalculator(float damage, int per, float accuracy, float criticalChance, float criticalDamage, float knockBack, Vector3 bulletPos);
}

