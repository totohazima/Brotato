using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public float range;
    public float accuracy;
    public float criticalChance;
    public float criticalDamage;
    public bool isCritical;
    public float knockBack;
    private float penetrateDamage; //관통 후 데미지가 깍이는 수치(원래 대미지를 넘을 순 없다)
    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public Vector3 direction;

    [HideInInspector] public Vector3 startPos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public virtual void FixedUpdate()
    {
        if(StageManager.instance.isEnd == true)
        {
            gameObject.SetActive(false);
        }

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
    public virtual void Init(float damage, int per, float range, float accuracy, float criticalChance, float criticalDamage, float knockBack, float penetrateDamage, Vector3 dir)
    {
        if(damage < 0)
        {
            damage = 0;
        }
        this.damage = damage;
        this.per = per;
        this.range = range;
        this.accuracy = accuracy;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
        this.knockBack = knockBack;
        this.penetrateDamage = penetrateDamage;

        float critical = criticalChance;
        float nonCritical = 100 - critical;
        float[] chanceLise = { critical, nonCritical };
        int index = StageManager.instance.Judgment(chanceLise);

        if(index == 0)
        {
            isCritical = true;
        }
        else
        {
            isCritical = false;
        }

        startPos = transform.position;
        if (per >= 0)
        {
            direction = dir;
            rigid.velocity = dir;
        }
    }

    public virtual void OnTriggerEnter(Collider collision)
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
                damageCal.DamageCalculator(damage, per, accuracy, isCritical, criticalDamage, knockBack, transform.position);
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
            else //사라지지 않았을 경우
            {
                if(penetrateDamage <= 0)
                {
                    damage = damage + (damage * (penetrateDamage / 100));
                }
                else
                {
                    return;
                }
            }
            
        }
    }
}
