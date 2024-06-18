using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BezierMove : MonoBehaviour 
{
    public Transform startPoint;
    public Transform controlPoint;
    public Transform controlPoint2;
    public Transform target;
    public Transform endPoint;
    public Transform baseObj;
    public bool isLeft;
    private void Start()
    {
        StartCoroutine(WheelAttack());
    }

    private IEnumerator WheelAttack()
    {

        if (target != null)
        {
            yield return new WaitForSeconds(1f);
            Vector3 targetPos = target.position;
            if (targetPos.x < transform.position.x)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }
            float duration = 0;
            float elapsedTime = 0;
            float dis = Vector3.Distance(transform.position, targetPos);
            float angle = GetAngle(transform.position, targetPos);
            Debug.Log(dis);
            //duration = 0.014f * dis;
            duration = 1f;
            ///적이 왼쪽에 있을 경우
            if (isLeft == true)
            {
                Vector3 start = GetPositionAtAngle(targetPos, 134, dis / 2f);
                Vector3 end = GetPositionAtAngle(targetPos, -90f, dis / 2f);
                Vector3 startVector = start;
                Vector3 endVector = end;

                Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
                //Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
                Vector3 controlVector_1 = new Vector3(con1Pos.x, start.y, 0);
                Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

                startPoint.position = transform.position + startVector;
                //controlPoint.position = transform.position + controlVector_1;
                //controlPoint2.position = transform.position + controlVector_2;
                endPoint.position = transform.position + endVector;
                Debug.Log(GetAngle(targetPos, startPoint.position));
                Debug.Log(GetAngle(targetPos, endPoint.position));
                // 첫 번째 구간: startPos -> targetPos
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPoint.position, startPoint.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                elapsedTime = 0f;

                // 두 번째 구간: targetPos -> endPos
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint2.position, endPoint.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 160), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // 세 번째 구간: endPos -> transform.position
                yield return new WaitForSeconds(0.05f);
                elapsedTime = 0f;
                while (elapsedTime < duration / 3)
                {
                    float t = elapsedTime / (duration / 3);
                    Vector3 point = CalculateQuadraticBezierPoint(t, endPoint.position, transform.position, transform.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                dis = 0;
                duration = 0;
                elapsedTime = 0;
            }
            ///적이 오른쪽에 있을 경우
            else
            {
                //Vector3 start = ConvertAngleToVector(angle + -133, dis / 5f);
                Vector3 start = GetPositionAtAngle(targetPos, 46.5f, dis / 2f);
                Vector3 end = GetPositionAtAngle(targetPos, -90, dis / 2f);
                Vector3 startVector = start;
                Vector3 endVector = end;

                Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
                //Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
                Vector3 controlVector_1 = new Vector3(con1Pos.x, start.y, 0);
                Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

                startPoint.position = transform.position + startVector;
                //controlPoint.position = transform.position + controlVector_1;
                //controlPoint2.position = transform.position + controlVector_2;
                endPoint.position = transform.position + endVector;

                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, Vector3.zero, controlPoint.position, startPoint.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                //yield return new WaitForSeconds(0.01f);
                elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint2.position, endPoint.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -160), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }

            // 세 번째 구간: endPos -> transform.position
            yield return new WaitForSeconds(0.05f);
            elapsedTime = 0f;
            while (elapsedTime < duration / 3)
            {
                float t = elapsedTime / (duration / 3);
                Vector3 point = CalculateQuadraticBezierPoint(t, endPoint.position, transform.position, transform.position);

                baseObj.position = point;
                baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            dis = 0;
            duration = 0;
            elapsedTime = 0;
        }
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
    public float GetAngle(Vector2 start, Vector2 end)//각도구하기
    {
        Vector2 vectorPos = end - start;
        return Mathf.Atan2(vectorPos.y, vectorPos.x) * Mathf.Rad2Deg;
    }

    public Vector3 ConvertAngleToVector(float _deg, float width)//각도로 좌표 구하기
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * width, Mathf.Sin(rad) * width, 2);
    }
    public Vector3 GetPositionAtAngle(Vector2 start, float angle, float distance)
    {
        // 각도를 라디안으로 변환
        float angleInRadians = angle * Mathf.Deg2Rad;

        // 새로운 좌표 계산
        float x = start.x + distance * Mathf.Cos(angleInRadians);
        float y = start.y + distance * Mathf.Sin(angleInRadians);

        x = x - transform.position.x;
        y = y - transform.position.y;
        return new Vector3(x, y, 0);
    }
}

