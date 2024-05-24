using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public float range;
    public float accuracy;
    public float bloodSucking;
    public float criticalChance;
    public float criticalDamage;
    public bool isCritical;
    public float knockBack;
    private float penetrateDamage; //관통 후 데미지가 깍이는 수치(원래 대미지를 넘을 순 없다)

    [Header("# StatusEffect")]
    public StatusEffect.EffectType[] effectType;
    public int infectedCount;
    public float burnDamage;
    public int burnCount;
    public float slowEffect;

    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 startPos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void OnDisable()
    {
        StatusReset();
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
    public virtual void Init(float damage, int per, float range, float accuracy, float bloodSucking, float criticalChance, float criticalDamage, float knockBack, float penetrateDamage, Vector3 dir)
    {
        if(damage < 0)
        {
            damage = 0;
        }
        this.damage = damage;
        this.per = per;
        this.range = range;
        this.accuracy = accuracy;
        this.bloodSucking = bloodSucking;
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

    public virtual void StatusEffecInit(params StatusEffect.EffectType[] effectTypes)
    {
        effectType = new StatusEffect.EffectType[effectTypes.Length];
        for (int i = 0; i < effectType.Length; i++)
        {
            effectType[i] = effectTypes[i];
        }
    }
    public void BurnInit(int infectedCount,float burnDamage, int burnCount)
    {
        this.infectedCount = infectedCount;
        this.burnDamage = burnDamage;
        this.burnCount = burnCount;
    }
    public void SlowInit(float slowEffect)
    {
        this.slowEffect = slowEffect;
    }

    public void StatusReset()
    {
        effectType = new StatusEffect.EffectType[0];
        burnDamage = 0;
        burnCount = 0;
        slowEffect = 0;
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
                damageCal.DamageCalculator(damage, per, accuracy, bloodSucking, isCritical, criticalDamage, knockBack, transform.position);
                damageCal.StatusEffectCalculator(effectType, this);
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
