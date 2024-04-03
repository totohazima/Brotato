using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : EnemyAction
{
    [SerializeField] bool isCharge;
    [SerializeField] float timer;
    [SerializeField] Vector3 dashVec;
    public override void OnEnable()
    {
        base.OnEnable();
        TimeReset();
    }
    private void TimeReset()
    {
        timer = coolTime;
    }
    public override void CustomUpdate()
    {
        if (curHealth <= 0)
        {
            isDie = true;
            StartCoroutine(Died());
        }
        else
        {
            isDie = false;
        }

        if (isDie)
        {
            return;
        }

        if (isHit == true)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= 1f)
            {
                isHit = false;
                hitTimer = 0f;
            }
        }
        float dis = Vector3.Distance(transform.position, target.position);

        timer += Time.deltaTime;

        if (isCharge == true)
        {
            Charge();
        }
        else if (isCharge == false)
        {
            if (timer >= coolTime)
            {
                if (dis > 24) //거리 밖에서 그냥 이동
                {
                    Move();
                }
                else
                {
                    if (isCharge == false) //대쉬할 방향값 구하기
                    {
                        StartCoroutine(ChargeVec());
                    }
                    else if (isCharge == true) //(timer >= coolTime + 3) 이 되기 전까지 대쉬
                    {
                        Charge();
                    }
                }
            }
            else
            {
                Move();
            }
        }


        if (isCharge == false)
        {
            if (target.position.x < transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }

    }

    public override void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = speed / 2500;
        if (ugliyToothSlow >= 1)
        {
            moveSpeed = moveSpeed - ((moveSpeed / 100) * (ugliyToothSlow * 10));
        }

        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;

        ///이동 제한
        float x = Mathf.Clamp(transform.position.x, game.xMin, game.xMax);
        float y = Mathf.Clamp(transform.position.y, game.yMin, game.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private IEnumerator ChargeVec()
    {
        float speed = Random.Range(3000, 3000);
        moveSpeed = speed / 2500;

        Vector3 dirVec = target.position - rigid.position;
        dashVec = dirVec.normalized * moveSpeed;

        yield return new WaitForSeconds(0.5f);
        isCharge = true;
        yield return new WaitForSeconds(1f);
        timer = 0;
        isCharge = false;
        dashVec = Vector3.zero;
    }

    private void Charge()
    {
        if (ugliyToothSlow >= 1)
        {
            moveSpeed = moveSpeed - ((moveSpeed / 100) * (ugliyToothSlow * 10));
        }

        rigid.MovePosition(rigid.position + dashVec);
        rigid.velocity = Vector3.zero;

        ///이동 제한
        float x = Mathf.Clamp(transform.position.x, game.xMin, game.xMax);
        float y = Mathf.Clamp(transform.position.y, game.yMin, game.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public override IEnumerator Died()
    {
        isCharge = false;
        dashVec = Vector3.zero;
        rigid.velocity = Vector3.zero;
        TimeReset();
        yield return base.Died();
    }
}
