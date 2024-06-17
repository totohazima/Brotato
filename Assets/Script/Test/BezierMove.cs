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
    public bool isLeft;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(WheelAttack());
    }

    private IEnumerator WheelAttack()
    {

        if (target != null)
        {

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

            //duration = 0.014f * dis;
            duration = 1f;
            ///���� ���ʿ� ���� ���
            if (isLeft == true)
            {
                Vector3 start = ConvertAngleToVector(angle + 90, dis / 3);
                Vector3 end = ConvertAngleToVector(angle - 90, dis / 1.5f);
                Vector3 startVector = new Vector3(start.x, start.y, 0);
                Vector3 endVector = new Vector3(end.x, end.y, 0);

                Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
                //Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
                Vector3 controlVector_1 = new Vector3(con1Pos.x, start.y, 0);
                Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

                startPoint.position = transform.position + endVector;
                controlPoint.position = transform.position + controlVector_2;
                controlPoint2.position = transform.position + controlVector_1;
                endPoint.position = transform.position + startVector;
                // ���� ���� �� �ʱ� ȸ�� ����
                //baseObj.localRotation = Quaternion.Euler(0, 0, -160);
                //yield return new WaitForSeconds(duration);
                // ù ��° ����: startPos -> targetPos
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint.position, targetPos);

                    transform.position = point;
                    transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                //�ֵθ� ���⸦ �����ϱ� ����
                yield return new WaitForSeconds(0.01f);
                elapsedTime = 0f;

                // �� ��° ����: targetPos -> endPos
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, targetPos, controlPoint2.position, endPoint.position);

                    transform.position = point;
                    transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 160), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }
            ///���� �����ʿ� ���� ���
            else
            {
                Vector3 start = ConvertAngleToVector(angle + 90, dis / 1.5f);
                Vector3 end = ConvertAngleToVector(angle - 90, dis / 3);
                Vector3 startVector = new Vector3(start.x, start.y, 0);
                Vector3 endVector = new Vector3(end.x, end.y, 0);

                Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
                //Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
                Vector3 controlVector_1 = new Vector3(con1Pos.x, start.y, 0);
                Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

                startPoint.position = transform.position + startVector;
                controlPoint.position = transform.position + controlVector_1;
                controlPoint2.position = transform.position + controlVector_2;
                endPoint.position = transform.position + endVector;
                // ���� ���� �� �ʱ� ȸ�� ����
                //baseObj.localRotation = Quaternion.Euler(0, 0, 160);
                yield return new WaitForSeconds(duration);
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint.position, targetPos);

                    transform.position = point;
                    transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                //yield return new WaitForSeconds(0.01f);
                elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, targetPos, controlPoint2.position, endPoint.position);

                    transform.position = point;
                    transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -160), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }

            // �� ��° ����: endPos -> transform.position
            LeanTween.moveLocal(transform.gameObject, Vector3.zero, 0.05f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.rotateLocal(transform.gameObject, Vector3.zero, 0.05f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);

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
    public float GetAngle(Vector2 start, Vector2 end)//�������ϱ�
    {
        Vector2 vectorPos = end - start;
        return Mathf.Atan2(vectorPos.y, vectorPos.x) * Mathf.Rad2Deg;
    }

    public Vector3 ConvertAngleToVector(float _deg, float width)//������ ��ǥ ���ϱ�
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * width, Mathf.Sin(rad) * width, 2);
    }
}

