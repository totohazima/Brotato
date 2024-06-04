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
                if (dis > 24) //�Ÿ� �ۿ��� �׳� �̵�
                {
                    Move();
                }
                else
                {
                    if (isCharge == false) //�뽬�� ���Ⱚ ���ϱ�
                    {
                        StartCoroutine(ChargeVec());
                    }
                    else if (isCharge == true) //(timer >= coolTime + 3) �� �Ǳ� ������ �뽬
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

        ///�̵� ����
        float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
        float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private IEnumerator ChargeVec()
    {
        float speed = Random.Range(4000, 4000);
        moveSpeed = speed / 2500;

        Vector3 dirVec = target.position - rigid.position;
        dashVec = dirVec.normalized * moveSpeed;

        yield return new WaitForSeconds(0.5f);
        isCharge = true;
        isDontPush = true;
        yield return new WaitForSeconds(0.7f);
        timer = 0;
        isCharge = false;
        isDontPush = false;
        dashVec = Vector3.zero;
    }

    private void Charge()
    {
        rigid.MovePosition(rigid.position + dashVec);
        rigid.velocity = Vector3.zero;

        ///�̵� ����
        float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
        float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public override IEnumerator Died()
    {
        isCharge = false;
        dashVec = Vector3.zero;
        rigid.velocity = Vector3.zero;
        TimeReset();
        return base.Died();
    }
}
