using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerEnemy : EnemyAction
{
    bool isReady;
    float timer;
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
        base.CustomUpdate();

        timer += Time.deltaTime;
        if(timer >= coolTime && isReady == true)
        {
            Fire();
            timer = 0;
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

        float dis = Vector3.Distance(transform.position, target.position);
        if(dis >= 22) //�Ÿ��� �֋�
        {
            isReady = false;
            rigid.MovePosition(rigid.position + nextVec); 
        }
        else if(dis < 22 && dis > 18) //���� �Ÿ��϶�
        {
            isReady = true;
        }
        else //�ʹ� ����� ��
        {
            rigid.MovePosition(rigid.position + -nextVec); //-nextVec �ݴ�� ��
            isReady = false;
        }
        rigid.velocity = Vector3.zero;

        ///�̵� ����
        float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
        float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private void Fire()
    {
        Vector3 dir = stage.mainPlayer.transform.position - transform.position;
        dir = dir.normalized;
        Transform bullet = PoolManager.instance.Get(11).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dir);
        bullet.GetComponent<EnemyBullet>().Init(damage, 0, range, accuracy, dir * 30);
    }
}
