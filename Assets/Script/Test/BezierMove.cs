using System.Collections;
using UnityEngine;

public class BezierMove : MonoBehaviour
{
    public Transform startPoint;        // 시작 위치
    public Transform controlPoint;      // 베지에 곡선 제어점
    public Transform controlPoint2;     // 베지에 곡선 제어점2
    public Transform target;            // 목표 위치
    public Transform endPoint;          // 도착 위치
    public Transform baseObj;           // 움직이는 객체 (무기 등)
    public bool isLeft;                 // 타겟이 왼쪽에 있는지 여부

    private void Start()
    {
        StartCoroutine(WheelAttack());
    }

    private IEnumerator WheelAttack()
    {
        if (target != null)
        {
            yield return new WaitForSeconds(1f); // 시작 후 1초 대기

            // 타겟 방향으로 회전
            Vector3 targetPos = target.position;
            Vector3 dir = targetPos - transform.position;
            float angles = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angles), 0.2f).setEase(LeanTweenType.easeInOutQuad);

            yield return new WaitForSeconds(0.2f); // 회전 후 잠시 대기

            // 타겟이 왼쪽에 있는지 여부 확인
            isLeft = targetPos.x < transform.position.x;

            float duration = 1.0f; // 움직임의 총 소요 시간
            float elapsedTime = 0f; // 경과 시간 초기화
            float dis = Vector3.Distance(transform.position, targetPos); // 시작 위치에서 목표 위치까지의 거리

            // 타겟 위치에 따라서 시작점과 끝점의 각도 조정
            Vector3 startOffset, endOffset;

            if (isLeft)
            {
                startOffset = GetPositionAtAngle(targetPos, 134 + angles, dis / 2f);
                endOffset = GetPositionAtAngle(targetPos, -90 + angles, dis / 2f);
            }
            else
            {
                startOffset = GetPositionAtAngle(targetPos, 46.5f + angles, dis / 2f);
                endOffset = GetPositionAtAngle(targetPos, -90 + angles, dis / 2f);
            }

            // 시작점과 끝점 설정
            startPoint.position = transform.position + startOffset;
            endPoint.position = transform.position + endOffset;

            // 첫 번째 베지에 곡선 구간: startPoint -> targetPos
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPoint.position, startPoint.position);

                baseObj.position = point;
                baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 두 번째 베지에 곡선 구간: targetPos -> endPoint
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                Vector3 point = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint2.position, endPoint.position);

                baseObj.position = point;
                baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 160), t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 세 번째 베지에 곡선 구간: endPoint -> transform.position
            yield return new WaitForSeconds(0.1f);
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
        }
    }

    // 베지에 곡선에서 특정 시점의 좌표를 계산하는 함수
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

    // 각도를 계산하는 함수
    public float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 vectorPos = end - start;
        return Mathf.Atan2(vectorPos.y, vectorPos.x) * Mathf.Rad2Deg;
    }

    // 각도로부터 벡터를 계산하는 함수
    public Vector3 GetPositionAtAngle(Vector2 start, float angle, float distance)
    {
        float angleInRadians = angle * Mathf.Deg2Rad;
        float x = start.x + distance * Mathf.Cos(angleInRadians);
        float y = start.y + distance * Mathf.Sin(angleInRadians);

        x = x - transform.position.x;
        y = y - transform.position.y;

        return new Vector3(x, y, 0);
    }
}
