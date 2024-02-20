using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyScanner : MonoBehaviour
{
    public float radius; // �� Ž�� �Ÿ�
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
        //������ ���� ���� ���͸� ����
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, 1 << 6);

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
    /// ���� Ȯ�� �� �����
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
