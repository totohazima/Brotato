using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy, ICustomUpdateMono, IDamageCalculate
{
    public Animator anim;
    public CapsuleCollider coll;
    public SpriteRenderer sprite;
    private Transform target;
    private float moveSpeed;
    private GameManager game;
    private float hitTimer;
    private Rigidbody rigid;

    void Awake()
    {
        game = GameManager.instance;
        rigid = GetComponent<Rigidbody>();
        target = game.mainPlayer.transform;
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        StatSetting((int)name);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        if(curHealth <= 0)
        {
            isDie = true;
            StartCoroutine(Died());
        }
        else
        {
            isDie = false;
        }

        if(isDie)
        {
            return;
        }

        if(isHit == true)
        {
            hitTimer += Time.deltaTime;
            if(hitTimer >= 1f)
            {
                isHit = false;
                hitTimer = 0f;
            }
        }
        Move();
        
        if(target.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
 
    private void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = speed / 5000;
        if (ugliyToothSlow >= 1)
        {
            moveSpeed = moveSpeed - ((moveSpeed / 100) * (ugliyToothSlow * 10));
        }
        //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;
    }

    private IEnumerator KnockBack(Vector3 playerPos, float power)
    {
        yield return new WaitForFixedUpdate();
        Vector3 dir = transform.position - playerPos;
        rigid.AddRelativeForce(dir.normalized * power, ForceMode.Impulse);
    }
    public void DamageCalculator(float damage, int per, float accuracy, float criticalChance, float criticalDamage, float knockBack, Vector3 bulletPos)
    {
        if (ItemEffect.instance.IsUglyTooth == true)
        {
            if (ugliyToothSlow < 3)
            {
                ugliyToothSlow++;
            }
        }
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

        curHealth -= finalDamage;

        StartCoroutine(KnockBack(bulletPos, 100 * (knockBack / 100)));
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Bullet"))
    //    {
    //        Bullet bulletInfo = other.GetComponent<Bullet>();

    //        if (ItemEffect.instance.IsUglyTooth == true)
    //        {
    //            if (ugliyToothSlow < 3)
    //            {
    //                ugliyToothSlow++;
    //            }
    //        }

    //        StartCoroutine(KnockBack(other.transform.position, 100 * (bulletInfo.knockBack / 100)));

    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isHit == false)
            {
                //game.HitCalculate(damage);
                isHit = true;

                //Test
                curHealth -= 10;
            }
        }
    }


}
