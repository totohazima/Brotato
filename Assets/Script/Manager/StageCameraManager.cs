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
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        Debug.Log("CameraLeftPos: " + bottomLeft.x);
        Debug.Log("CameraRightPos: " + topRight.x);
        Debug.Log("CameraTopPos: " + topRight.y);
        Debug.Log("CameraBottomPos: " + bottomLeft.y);

        Vector3 playerPos = StageManager.instance.mainPlayer.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + 3, -10);

        /////이동 제한
        //if(StageManager.instance.xMin > bottomLeft.x)
        //{

        //}
        //else if(StageManager.instance.xMax)

        
        
    }
}
