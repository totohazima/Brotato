using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker_Boss : EnemyAction
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
        bullet = new Transform[10];
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

        degs += 0.7f;
        for (int i = 0; i < bullet.Length; i++)
        {
            float deg = (360 / bullet.Length) + degs;
            Vector3 pos = ConvertAngleToVector(deg, i);
            bullet[i].transform.position = new Vector3(transform.position.x + pos.x, transform.position.y + pos.y, transform.position.z + pos.z);
            bullet[i].localScale = new Vector2(40 * (1 + (i * 0.1f)),40 * (1 + (i * 0.1f)));
        }
    }

    public override IEnumerator Died(bool isDeSpawned)
    {
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].gameObject.SetActive(false);
        }
        return base.Died(isDeSpawned);
    }
    private Vector3 ConvertAngleToVector(float _deg, int i)//각도로 좌표 구하기
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * ((5 + (0.1f * i)) * i), Mathf.Sin(rad) * ((5 + (0.1f * i)) * i), 0);
    }
}
