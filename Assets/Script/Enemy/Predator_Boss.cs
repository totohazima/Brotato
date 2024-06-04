using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator_Boss : EnemyAction
{
    Transform[] bullet;
    float degs;
    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(bulletSetting());
    }

    /// <summary>
    /// 총알이 중앙에 생성돼 사망하는 현상 방지
    /// 추후 풀매니저 변경 시 제거
    /// </summary>
    private IEnumerator bulletSetting()
    {
        degs = 0;
        bullet = new Transform[5];
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i] = PoolManager.instance.Get(11).transform;
            bullet[i].position = transform.position;
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].GetComponent<EnemyBullet>().Init(damage, 100000, 100000, accuracy, Vector3.zero);
        }
    }
    public override void CustomUpdate()
    {
        base.CustomUpdate();

        degs += 1.5f;
        for (int i = 0; i < bullet.Length; i++)
        {
            float deg = (360 * i / bullet.Length - 90) + degs;
            Vector3 pos = ConvertAngleToVector(deg);
            bullet[i].transform.position = new Vector3(transform.position.x + pos.x, transform.position.y + pos.y, transform.position.z + pos.z);
        }
    }

    public override IEnumerator Died()
    {
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].gameObject.SetActive(false);
        }
        return base.Died();
    }
    private Vector3 ConvertAngleToVector(float _deg)//각도로 좌표 구하기
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * 40f, Mathf.Sin(rad) * 40f, 0);
    }
}
