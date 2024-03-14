using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private int per;
    private float range;
    public float accuracy;
    public float criticalChance;
    public float criticalDamage;
    public float knockBack;
    Rigidbody rigid;
    Vector3 direction;

    private Vector3 startPos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        startPos = transform.position;
    }

    
    public void FixedUpdate()
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

        float distance = Vector3.Distance(startPos, transform.position);
        if (distance >= range && range != -1000)
        {
            gameObject.SetActive(false);
        }
    }
    public void Init(float damage, int per, float range, float accuracy, float criticalChance, float criticalDamage, float knockBack, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        this.range = range;
        this.accuracy = accuracy;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
        this.knockBack = knockBack;
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

        if (collision.CompareTag("Enemy"))
        {
            IDamageCalculate damageCal = collision.GetComponentInParent<IDamageCalculate>();
            if(damageCal != null)
            {
                damageCal.DamageCalculator(damage, per, accuracy, criticalChance, criticalDamage, knockBack, transform.position);
            }

            per--;
            if (per < 0)
            {
                if (rigid != null)
                {
                    rigid.velocity = Vector3.zero;
                }
                gameObject.SetActive(false);
            }

            //per--;
            //Enemy enemy = collision.GetComponentInParent<Enemy>();

            //float critical = criticalChance;
            //float nonCritical = 100 - critical;
            //float[] chanceLise = { critical, nonCritical };
            //int index = GameManager.instance.Judgment(chanceLise);

            //float finalDamage = 0;
            //if (index == 0)
            //{
            //    finalDamage = damage * criticalDamage;
            //}
            //else
            //{
            //    finalDamage = damage;
            //}

            //enemy.curHealth -= finalDamage;

            //if (per < 0)
            //{
            //    if (rigid != null)
            //    {
            //        rigid.velocity = Vector3.zero;
            //    }
            //    gameObject.SetActive(false);
            //}
        }
    }
}
