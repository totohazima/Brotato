using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMove : MonoBehaviour
{
    public Transform startPoint;
    public Transform controlPoint;
    public Transform endPoint;
    public float duration;
    private float elapsedTime = 0.0f;
    void Awake()
    {
        float dis = Vector3.Distance(transform.position, controlPoint.position);
        float time = dis / 1;
        duration = time * 0.2f;

    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;
        if (t > 1.0f)
        {
            t = 1.0f;
        }

        Vector3 newPosition = CalculateBezierPoint(t, startPoint.position, controlPoint.position, endPoint.position);
        transform.position = newPosition;

    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)^2 * P0
        p += 2 * u * t * p1; // 2(1-t)t * P1
        p += tt * p2; // t^2 * P2

        return p;
    }
}
