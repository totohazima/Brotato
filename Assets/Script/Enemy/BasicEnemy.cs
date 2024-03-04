using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public Animator anim;
    public CapsuleCollider coll;
    public SpriteRenderer sprite;
    Transform target;
    float moveSpeed;
    GameManager game;
    float hitTimer;
    Rigidbody rigid;

    void Awake()
    {
        game = GameManager.instance;
        rigid = GetComponent<Rigidbody>();
        target = game.mainPlayer.transform;
    }
    void OnEnable()
    {
        StatSetting((int)name);
    }
    void Update()
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

    void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = speed / 5000;
        if (ugliyToothSlow >= 1)
        {
            moveSpeed = moveSpeed - ((moveSpeed / 100) * (ugliyToothSlow * 10));
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
        //Vector3 dirVec = target.position - rigid.position;
        //Vector3 nextVec = dirVec.normalized * moveSpeed;
        //rigid.MovePosition(rigid.position + nextVec);
        //rigid.velocity = Vector3.zero;
    }

    
    IEnumerator KnockBack(float power)
    {
        yield return new WaitForFixedUpdate();
        Vector3 playerPos = game.mainPlayer.transform.position;
        Vector3 dir = transform.position - playerPos;
        rigid.AddRelativeForce(dir.normalized * power, ForceMode.Impulse);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            if(ItemEffect.instance.IsUglyTooth == true)
            {
                if(ugliyToothSlow < 3)
                {
                    ugliyToothSlow++;
                }
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isHit == false)
            {
                //game.HitCalculate(damage);
                StartCoroutine(KnockBack(3f));
                isHit = true;

                //Test
                float damages = 10;
                curHealth -= damages;
            }
        }
    }
}
