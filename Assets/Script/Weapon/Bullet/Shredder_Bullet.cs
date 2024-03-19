using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder_Bullet : Bullet
{
    float boomChance;
    public void Init(float damage, int per, float range, float accuracy, float criticalChance, float criticalDamage, float knockBack, float penetrateDamage, Vector3 dir, float boomChance)
    {
        base.Init(damage, per, range, accuracy, criticalChance, criticalDamage, knockBack, penetrateDamage, dir);
        this.boomChance = boomChance;
    }
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

        if (collision.CompareTag("Enemy"))
        {
            float notBoom = 100 - boomChance;
            float[] chanceLise = { notBoom, boomChance };
            int index = GameManager.instance.Judgment(chanceLise);

            if (index == 0)
            {
                return;
            }
            else if (index == 1)
            {
                GameObject booms = PoolManager.instance.Get(6);
                booms.transform.position = transform.position;

                Bullet bullet = booms.GetComponent<Bullet>();
                float damages = damage * (1 + (GameManager.instance.playerInfo.explosiveDamage / 100));
                bullet.Init(damages, 10000, -1000, 100, 0, 0, 0, 0, Vector3.zero);
            }
        }
    }
}
