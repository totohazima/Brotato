using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCameraManager : MonoBehaviour, ICustomUpdateMono
{ 
    float halfWidth;
    float halfHeight;
    Vector3 offset = new Vector3(0, 1);
    StageManager game;
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        game = StageManager.instance;
        halfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        halfHeight = Camera.main.orthographicSize;
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
        
    }
    public void CustomUpdate()
    {
        Vector3 playerPos = game.mainPlayer.transform.position;

        Vector3 desPos = new Vector3(
            Mathf.Clamp(playerPos.x + offset.x, game.xMin + halfWidth * 5f, game.xMax - halfWidth * 5f),
            Mathf.Clamp(playerPos.y + offset.y, game.yMin + halfHeight * 3.5f, game.yMax - halfHeight * 3.5f), 
            -10);

        //transform.position = new Vector3(desPos.x, desPos.y, desPos.z);
        transform.position = new Vector3(playerPos.x, playerPos.y + 3, -10f);

    }

}
