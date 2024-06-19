using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScanner : MonoBehaviour, ICustomUpdateMono
{
    public Transform currentTarget;
    public float radius; // �� Ž�� �Ÿ�
    public List<Transform> detectedTargets = new List<Transform>(); // ������ Ÿ�ٵ��� ���
    private float timeInterval = 0.2f;
    private float timer = 0;

    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }

    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
        ClearTargets();
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
        // ���� Ÿ���� �ְ�, Ȱ��ȭ ���°� �ƴϰų� �̹� ���� ���� ��� ����
        if (currentTarget != null && (!currentTarget.gameObject.activeSelf || !detectedTargets.Contains(currentTarget)))
        {
            RemoveTarget(currentTarget);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, 1 << 6);

        // ���� ����� Ÿ�� ã��
        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;

        foreach (Collider col in colliders)
        {
            if (col == null || !col.gameObject.activeSelf)
            {
                continue;
            }

            Transform targetTransform = col.transform;
            float dis = Vector3.Distance(transform.position, targetTransform.position);

            // �̹� ���� ���� Ÿ���� �ƴϰ�, ���� ����� Ÿ���� ���
            if (dis < shortestDistance && !detectedTargets.Contains(targetTransform))
            {
                shortestDistance = dis;
                nearestTarget = targetTransform;
            }
        }

        // ���� ����� Ÿ���� ���� Ÿ������ ����
        if (nearestTarget != null)
        {
            SetTarget(nearestTarget);
        }
        else
        {
            ClearTargets();
        }
    }

    private void SetTarget(Transform newTarget)
    {
        ClearTargets();
        currentTarget = newTarget;
        detectedTargets.Add(currentTarget);
        StageManager.instance.trackedTargets.Add(currentTarget);
    }

    private void RemoveTarget(Transform targetToRemove)
    {
        detectedTargets.Remove(targetToRemove);
        StageManager.instance.trackedTargets.Remove(targetToRemove);
        currentTarget = null;
    }

    private void ClearTargets()
    {
        foreach (Transform targetToRemove in detectedTargets)
        {
            StageManager.instance.trackedTargets.Remove(targetToRemove);
        }
        detectedTargets.Clear();
        currentTarget = null;
    }

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

