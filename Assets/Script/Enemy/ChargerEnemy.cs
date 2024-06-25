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
            StartCoroutine(Died(false));
        }
        else
        {
            isDie = false;
        }

        if (isDie)
        {
            return;
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
        moveSpeed = speed / 2000;
        if (ugliyToothSlow >= 1)
        {
            moveSpeed = moveSpeed - ((moveSpeed / 100) * (ugliyToothSlow * 10));
        }

        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;

        ///이동 제한
        float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
        float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private IEnumerator ChargeVec()
    {
        isDontPush = true;
        float speed = Random.Range(minSpeed * 10, maxSpeed * 10);
        moveSpeed = speed / 2500;

        Vector3 dirVec = target.position - rigid.position;
        dashVec = dirVec.normalized * moveSpeed;

        yield return new WaitForSeconds(0.3f);
        isCharge = true;
        yield return new WaitForSeconds(0.65f);
        timer = 0;
        isCharge = false;
        isDontPush = false;
        dashVec = Vector3.zero;
    }

    //private void Charge()
    //{
    //    rigid.MovePosition(rigid.position + dashVec);
    //    rigid.velocity = Vector3.zero;

    //    ///이동 제한
    //    float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
    //    float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
    //    transform.position = new Vector3(x, y, transform.position.z);
    //}


    private void Charge()
    {
        float acceleration = 150f; // 가속도를 조절하는 변수
        // 현재 속도를 서서히 증가시킵니다.
        float maxSpeed = moveSpeed * 100;
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed, acceleration * Time.deltaTime);

        // 이동 방향에 현재 속도를 곱하여 이동합니다.
        rigid.MovePosition(rigid.position + dashVec * moveSpeed * Time.deltaTime);
        rigid.velocity = Vector3.zero;

        ///이동 제한
        float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
        float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }
    public override IEnumerator Died(bool isDeSpawned)
    {
        isCharge = false;
        dashVec = Vector3.zero;
        rigid.velocity = Vector3.zero;
        TimeReset();
        return base.Died(isDeSpawned);
    }

}
