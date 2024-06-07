using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCameraManager : MonoBehaviour, ICustomUpdateMono
{
    public Camera mainCamera;

    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        //Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        //Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        //Debug.Log("CameraLeftPos: " + bottomLeft.x);
        //Debug.Log("CameraRightPos: " + topRight.x);
        //Debug.Log("CameraTopPos: " + topRight.y);
        //Debug.Log("CameraBottomPos: " + bottomLeft.y);

        Vector3 playerPos = StageManager.instance.mainPlayer.transform.position;
        Vector3 targetPos = new Vector3(playerPos.x, playerPos.y + 3, -10);


        float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float cameraHalfHeight = mainCamera.orthographicSize;

        // 스테이지 경계 제한
        float minX = (StageManager.instance.xMin - 9) + cameraHalfWidth;
        float maxX = (StageManager.instance.xMax + 9) - cameraHalfWidth;
        float minY = (StageManager.instance.yMin - 6) + cameraHalfHeight;
        float maxY = (StageManager.instance.yMax + 18) - cameraHalfHeight;

        // 카메라 위치를 스테이지 경계 내로 제한
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        // 카메라 위치를 업데이트
        transform.position = targetPos;
    }
}

