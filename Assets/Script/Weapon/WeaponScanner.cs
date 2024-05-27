using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScanner : MonoBehaviour, ICustomUpdateMono
{
    public float radius; // 적 탐지 거리
    public Transform target; //실질적 공격 타겟
    float shortDis;
    private Collider[] colliders;
    float timeInterval = 0.2f;
    float timer = 0;

    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }
    public void CustomUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= timeInterval)
        {
            Scan();
            timer = 0;
        }
    }
    private void Scan()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, 1 << 6);

        // 가장 가까운 적을 찾기 위한 변수들
        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;

        foreach (Collider col in colliders)
        {
            // 적이 죽었을 때 Collider가 사라지면 해당 적은 무시
            if (col == null || !col.gameObject.activeSelf)
            {
                continue;
            }

            float dis = Vector3.Distance(transform.position, col.transform.position);
            if (dis < shortestDistance)
            {
                shortestDistance = dis;
                nearestTarget = col.transform;
            }
        }

        // 가장 가까운 적이 있는 경우 타겟 설정
        if (nearestTarget != null)
        {
            target = nearestTarget;
        }
        else
        {
            // 탐지된 적이 없는 경우 타겟 초기화
            target = null;
        }
    }
    //private void Scan()
    //{
    //    colliders = Physics.OverlapSphere(transform.position, radius, 1 << 6);

    //    if (colliders.Length > 0)
    //    {
    //        shortDis = Vector3.Distance(transform.position, colliders[0].transform.position);
    //        if (colliders.Length == 1) //타겟이 하나만 있는 경우
    //        {
    //            target = colliders[0].transform;
    //        }
    //        else if (colliders.Length > 1)
    //        {
    //            for (int i = 0; i < colliders.Length; i++)
    //            {
    //                Transform col = colliders[i].transform;
    //                float dis = Vector3.Distance(transform.position, col.position);

    //                if (dis < shortDis)
    //                {
    //                    target = col;
    //                    shortDis = dis;
    //                }
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// 범위 확인 용 기즈모
    /// </summary>
#if UNITY_EDITOR
    int segments = 100;
    Color gizmoColor = Color.green;
    bool drawWhenSelected = true;

    void OnDrawGizmosSelected()
    {
        if (drawWhenSelected)
        {
            Gizmos.color = gizmoColor;
            DrawHollowCircle(transform.position, radius, segments);
        }
    }
    void DrawHollowCircle(Vector3 center, float radius, int segments)
    {
        float angle = 0f;
        Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);

        for (int i = 1; i <= segments; i++)
        {
            angle = i * Mathf.PI * 2f / segments;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            Gizmos.DrawLine(lastPoint, newPoint);
            lastPoint = newPoint;
        }
    }
#endif
}
