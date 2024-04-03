using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy1 : EnemyAction
{
    Transform[] bullet;
    float degs;
    public override void OnEnable()
    {
        base.OnEnable();
        degs = 0;
        bullet = new Transform[5];
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i] = PoolManager.instance.Get(11).transform;
            bullet[i].position = transform.position;
            bullet[i].GetComponent<EnemyBullet>().Init(damage, 100000, 100000, accuracy, Vector3.zero);
        }
    }

    public override void CustomUpdate()
    {
        base.CustomUpdate();

        degs += 2;
        for (int i = 0; i < bullet.Length; i++)
        {
            float deg = (360 * i / bullet.Length - 90) + degs;
            Vector3 pos = ConvertAngleToVector(deg);
            bullet[i].transform.position = new Vector3(transform.position.x + pos.x, transform.position.y + pos.y, transform.position.z + pos.z);
        }
    }

    private Vector3 ConvertAngleToVector(float _deg)//각도로 좌표 구하기
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * 20f, Mathf.Sin(rad) * 20f, 0);
    }
}
