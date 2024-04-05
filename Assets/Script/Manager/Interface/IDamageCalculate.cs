using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageCalculate
{
    void DamageCalculator(float damage, int per, float accuracy, bool isCritical,float criticalDamage, float knockBack, Vector3 bulletPos);
}

