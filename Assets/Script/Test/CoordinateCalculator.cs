using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateCalculator : MonoBehaviour
{
    // 주어진 좌표값
    public Vector3 originalPosition;

    void Start()
    {
        // 원점 기준으로 2배 더 먼 거리에 있는 좌표를 계산합니다
        Vector3 newPosition = CalculateDoubleDistancePosition(originalPosition);

        // 결과 출력
        Debug.Log("Original Position: " + originalPosition);
        Debug.Log("New Position: " + newPosition);
    }

    private Vector3 CalculateDoubleDistancePosition(Vector3 original)
    {
        // 벡터를 2배로 스케일링합니다
        Vector3 doubledVector = original * 2;

        return doubledVector;
    }
}
