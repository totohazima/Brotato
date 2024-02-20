using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyScanner : MonoBehaviour
{
    public float radius; // 적 탐지 거리
    public Transform target;
    bool isScan;
    float shortDis;

    void FixedUpdate()
    {
        if(target != null)
        {
            if(target.parent.gameObject.activeSelf == false)
            {
                target = null;
            }
        }

        if(isScan == false)
        {
            Scan();
            
        }
    }

    void Scan()
    {
        isScan = true;
        //설정한 범위 내에 몬스터를 감지
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, 1 << 6);

        if (colliders.Length > 0)
        {
            shortDis = Vector3.Distance(transform.position, colliders[0].transform.position);
            if(colliders.Length == 1) //타겟이 하나만 있는 경우
            {
                target = colliders[0].transform;
            }
            else if (colliders.Length > 1)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    Transform col = colliders[i].transform;
                    float dis = Vector3.Distance(transform.position, col.position);

                    //현재 타겟을 찾았을 경우 해당 타겟이 범위 밖으로 나가거나 죽기전까지 계속 타격
                    if (col == target)
                    {
                        target = col;
                        break;
                    }

                    if (dis < shortDis)
                    {
                        target = col;
                        shortDis = dis;
                    }
                }
            }
        }
        isScan = false;
    }

    /// <summary>
    /// 범위 확인 용 기즈모
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
