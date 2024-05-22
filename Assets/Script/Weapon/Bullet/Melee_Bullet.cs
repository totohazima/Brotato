using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Bullet : Bullet
{
    public override void FixedUpdate()
    {
        return;
    }

    public override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamageCalculate damageCal = collision.GetComponentInParent<IDamageCalculate>();
            if (damageCal != null)
            {
                damageCal.DamageCalculator(damage, per, accuracy, bloodSucking,isCritical, criticalDamage, knockBack, transform.position);
            }
        }
    }

}
