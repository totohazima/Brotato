using System.Collections;
using UnityEngine;

public class BezierMove : MonoBehaviour
{
    public Transform startPoint;        // ���� ��ġ
    public Transform controlPoint;      // ������ � ������
    public Transform controlPoint2;     // ������ � ������2
    public Transform target;            // ��ǥ ��ġ
    public Transform endPoint;          // ���� ��ġ
    public Transform baseObj;           // �����̴� ��ü (���� ��)
    public bool isLeft;                 // Ÿ���� ���ʿ� �ִ��� ����

    private void Start()
    {
        StartCoroutine(WheelAttack());
    }

    private IEnumerator WheelAttack()
    {
        if (target != null)
        {
            yield return new WaitForSeconds(1f); // ���� �� 1�� ���

            // Ÿ�� �������� ȸ��
            Vector3 targetPos = target.position;
            Vector3 dir = targetPos - transform.position;
            float angles = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angles), 0.2f).setEase(LeanTweenType.easeInOutQuad);

            yield return new WaitForSeconds(0.2f); // ȸ�� �� ��� ���

            // Ÿ���� ���ʿ� �ִ��� ���� Ȯ��
            isLeft = targetPos.x < transform.position.x;

            float duration = 1.0f; // �������� �� �ҿ� �ð�
            float elapsedTime = 0f; // ��� �ð� �ʱ�ȭ
            float dis = Vector3.Distance(transform.position, targetPos); // ���� ��ġ���� ��ǥ ��ġ������ �Ÿ�

            // Ÿ�� ��ġ�� ���� �������� ������ ���� ����
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

            // �������� ���� ����
            startPoint.position = transform.position + startOffset;
            endPoint.position = transform.position + endOffset;

            // ù ��° ������ � ����: startPoint -> targetPos
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPoint.position, startPoint.position);

                baseObj.position = point;
                baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // �� ��° ������ � ����: targetPos -> endPoint
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

            // �� ��° ������ � ����: endPoint -> transform.position
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

    // ������ ����� Ư�� ������ ��ǥ�� ����ϴ� �Լ�
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

    // ������ ����ϴ� �Լ�
    public float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 vectorPos = end - start;
        return Mathf.Atan2(vectorPos.y, vectorPos.x) * Mathf.Rad2Deg;
    }

    // �����κ��� ���͸� ����ϴ� �Լ�
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
