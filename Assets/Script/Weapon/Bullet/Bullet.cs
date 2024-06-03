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
    private float penetrateDamage; //���� �� �������� ���̴� ��ġ(���� ������� ���� �� ����)

    [Header("# StatusEffect")]
    public StatusEffect.EffectType[] effectType;
    public int infectedCount;
    public float burnDamage;
    public int burnCount;
    public bool isSausage;
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
        if(GameManager.instance.isEnd == true)
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
        if(damage < 1)
        {
            damage = 1;
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

        if (GameManager.instance.playerInfo.isScaredSausage == true)
        {
            float burnChance = GameManager.instance.playerInfo.scaredSausageChance;
            float nonBurning = 100 - burnChance;
            float[] chance = { burnChance, nonBurning };
            int index2 = Judgment(chance);

            if(index2 == 0)
            {
                isSausage = true;
            }
            else
            {
                isSausage = false;
            }
        }
        else
        {
            isSausage = false;
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
            else //������� �ʾ��� ���
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

    public int Judgment(float[] rando)
    {
        int count = rando.Length;
        float max = 0;
        for (int i = 0; i < count; i++)
            max += rando[i];

        float range = UnityEngine.Random.Range(0f, (float)max);
        //0.1, 0.2, 30, 40
        double chance = 0;
        for (int i = 0; i < count; i++)
        {
            chance += rando[i];
            if (range > chance)
                continue;

            return i;
        }

        return -1;
    }
}
