using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyScanner : MonoBehaviour,ICustomUpdateMono
{
    public float radius; // 적 탐지 거리
    public Transform target; //실질적 공격 타겟
    [SerializeField]
    Transform beforeTarget; //이전 타겟
    [SerializeField]
    bool isScan;
    float shortDis;
    public Collider[] colliders;

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
        if(target != null)
        {
            if(target.parent.gameObject.activeSelf == false)
            {
                target = null;
                beforeTarget = null;
            }
        }
        Scan();
        //if (isScan == false)
        //{
        //    StartCoroutine(Scan());
        //}
    }

    void Scan()
    {
        //isScan = true;
        //설정한 범위 내에 몬스터를 감지
        colliders = Physics.OverlapSphere(transform.position, radius, 1 << 6);

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
                    if (col == beforeTarget)
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
        beforeTarget = target;
        //yield return new WaitForSeconds(0.1f);
        //isScan = false;
    }

    /// <summary>
    /// 범위 확인 용 기즈모
    /// </summary>
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
#endif
}
