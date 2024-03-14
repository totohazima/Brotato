using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : Enemy, ICustomUpdateMono, IDamageCalculate
{
    public Animator anim;
    public CapsuleCollider coll;
    public SpriteRenderer sprite;
    public Transform textPopUpPos;
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
 
    public virtual void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = speed / 2500;
        if (ugliyToothSlow >= 1)
        {
            moveSpeed = moveSpeed - ((moveSpeed / 100) * (ugliyToothSlow * 10));
        }
        //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;

        ///�̵� ����
        float x = Mathf.Clamp(transform.position.x, game.xMin, game.xMax);
        float y = Mathf.Clamp(transform.position.y, game.yMin, game.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
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
        string damageText = null;
        if (index == 0)
        {
            finalDamage = damage * criticalDamage;
            damageText = "<color=yellow>" + finalDamage.ToString("F0") + "</color>";
        }
        else
        {
            finalDamage = damage;
            damageText = finalDamage.ToString("F0");
        }

        Transform text = DamageTextManager.instance.TextCreate(0, damageText).transform;
        text.position = textPopUpPos.position;

        curHealth -= finalDamage;
        StartCoroutine(KnockBack(bulletPos, 100 * (knockBack / 100)));
    }

  
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