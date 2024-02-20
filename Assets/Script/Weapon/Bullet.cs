using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public float accuracy;
    public float criticalChance;
    public float criticalDamage;
    Rigidbody rigid;
    Vector3 direction;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.instance.isPause == true)
        {
            if (rigid != null)
            {
                rigid.velocity = Vector3.zero;
            }
        }
        else if (GameManager.instance.isPause == false)
        {
            if (rigid != null)
            {
                rigid.velocity = direction;
            }
        }
    }
    public void Init(float damage, int per, float accuracy, float criticalChance, float criticalDamage, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        this.accuracy = accuracy;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
        if (per >= 0)
        {
            direction = dir;
            rigid.velocity = dir;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
        per--;
        Enemy enemy = collision.GetComponentInParent<Enemy>();

        float critical = criticalChance;
        float nonCritical = 100 - critical;
        float[] chanceLise = { critical, nonCritical };
        int index = GameManager.instance.Judgment(chanceLise);

        float finalDamage = 0;
        if (index == 0)
        {
            finalDamage = damage * criticalDamage;
        }
        else
        {
            finalDamage = damage;
        }

        enemy.curHealth -= finalDamage;

        if (per < 0)
        {
            if (rigid != null)
            {
                rigid.velocity = Vector3.zero;
            }
            gameObject.SetActive(false);
        }
    }

    
}
