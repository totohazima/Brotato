using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScanner : MonoBehaviour, ICustomUpdateMono
{
    public float radius; // �� Ž�� �Ÿ�
    public Transform target; //������ ���� Ÿ��
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

        // ���� ����� ���� ã�� ���� ������
        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;

        foreach (Collider col in colliders)
        {
            // ���� �׾��� �� Collider�� ������� �ش� ���� ����
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

        // ���� ����� ���� �ִ� ��� Ÿ�� ����
        if (nearestTarget != null)
        {
            target = nearestTarget;
        }
        else
        {
            // Ž���� ���� ���� ��� Ÿ�� �ʱ�ȭ
            target = null;
        }
    }
    //private void Scan()
    //{
    //    colliders = Physics.OverlapSphere(transform.position, radius, 1 << 6);

    //    if (colliders.Length > 0)
    //    {
    //        shortDis = Vector3.Distance(transform.position, colliders[0].transform.position);
    //        if (colliders.Length == 1) //Ÿ���� �ϳ��� �ִ� ���
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
    /// ���� Ȯ�� �� �����
    /// </summary>
    //#if UNITY_EDITOR
    //    void OnDrawGizmosSelected()
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawSphere(transform.position, radius);
    //    }
    //#endif
}
