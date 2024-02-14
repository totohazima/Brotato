using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public Animator anim;
    public CapsuleCollider coll;
    public Rigidbody rigid;
    Transform target;
    float moveSpeed;
    GameManager game;
    float hitTimer;
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
    }

    void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = speed / 30000;
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
    }

    IEnumerator Died()
    {
        float randomX, randomY;
        for (int i = 0; i < moneyDropRate; i++)
        {
            GameObject Meterial = PoolManager.instance.Get(2);
            randomX = Random.Range(-3f, 3f);
            randomY = Random.Range(-3f, 3f);
            Meterial.transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY);
        }

        float consum = consumableDropRate / 100;
        float loot = lootDropRate / 100;
        float notDrop = (100 - (consum + loot)) / 100;

        float[] chanceLise = { notDrop, consum, loot };
        int index = game.Judgment(chanceLise);

        switch(index)
        {
            case 0:
                break;
            case 1:
                GameObject consumable = PoolManager.instance.Get(3);
                randomX = Random.Range(-3f, 3f);
                randomY = Random.Range(-3f, 3f);
                consumable.transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY);
                break;
            case 2:
                GameObject lootCrate = PoolManager.instance.Get(4);
                randomX = Random.Range(-3f, 3f);
                randomY = Random.Range(-3f, 3f);
                lootCrate.transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY);
                break;
        }

        gameObject.SetActive(false);
        yield return 0;
    }
    IEnumerator KnockBack(float power)
    {
        yield return new WaitForFixedUpdate();
        Vector3 playerPos = game.mainPlayer.transform.position;
        Vector3 dir = transform.position - playerPos;
        rigid.AddRelativeForce(dir.normalized * power, ForceMode.Impulse);
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
                float damages = (1 + (game.playerInfo.meleeDamage)) * (1 + game.playerInfo.persentDamage);
                curHealth -= damages;
            }
        }
    }
}
