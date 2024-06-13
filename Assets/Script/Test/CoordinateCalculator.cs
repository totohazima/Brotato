using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateCalculator : MonoBehaviour
{
    // �־��� ��ǥ��
    public Vector3 originalPosition;

    void Start()
    {
        // ���� �������� 2�� �� �� �Ÿ��� �ִ� ��ǥ�� ����մϴ�
        Vector3 newPosition = CalculateDoubleDistancePosition(originalPosition);

        // ��� ���
        Debug.Log("Original Position: " + originalPosition);
        Debug.Log("New Position: " + newPosition);
    }

    private Vector3 CalculateDoubleDistancePosition(Vector3 original)
    {
        // ���͸� 2��� �����ϸ��մϴ�
        Vector3 doubledVector = original * 2;

        return doubledVector;
    }
}
