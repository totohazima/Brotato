using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMove : MonoBehaviour
{
    public Transform startPoint;
    public Transform controlPoint;
    public Transform controlPoint2;
    public Transform middlePoint;
    public Transform endPoint;
    public float duration;// ��� ���� �̵��ϴ� �� �ɸ��� �ð�

    void Start()
    {
        StartCoroutine(MoveAlongCurve());
    }

    IEnumerator MoveAlongCurve()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 point = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint.position, middlePoint.position);

            transform.position = point;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // ������ �������� ��ġ�� ��Ȯ�� ����
        transform.position = middlePoint.position;
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 point = CalculateQuadraticBezierPoint(t, middlePoint.position, controlPoint2.position, endPoint.position);

            transform.position = point;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // ������ �������� ��ġ�� ��Ȯ�� ����
        transform.position = endPoint.position;
        
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}

