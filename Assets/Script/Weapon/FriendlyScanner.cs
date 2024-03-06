using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyScanner : MonoBehaviour,ICustomUpdateMono
{
    public float radius; // �� Ž�� �Ÿ�
    public Transform target; //������ ���� Ÿ��
    [SerializeField]
    Transform beforeTarget; //���� Ÿ��
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
        //������ ���� ���� ���͸� ����
        colliders = Physics.OverlapSphere(transform.position, radius, 1 << 6);

        if (colliders.Length > 0)
        {
            shortDis = Vector3.Distance(transform.position, colliders[0].transform.position);
            if(colliders.Length == 1) //Ÿ���� �ϳ��� �ִ� ���
            {
                target = colliders[0].transform;
            }
            else if (colliders.Length > 1)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    Transform col = colliders[i].transform;
                    float dis = Vector3.Distance(transform.position, col.position);

                    //���� Ÿ���� ã���� ��� �ش� Ÿ���� ���� ������ �����ų� �ױ������� ��� Ÿ��
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
    /// ���� Ȯ�� �� �����
    /// </summary>
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
#endif
}
