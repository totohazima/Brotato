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
    public float duration;// 곡선을 따라 이동하는 데 걸리는 시간

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
        // 마지막 지점으로 위치를 정확히 설정
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
        // 마지막 지점으로 위치를 정확히 설정
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

