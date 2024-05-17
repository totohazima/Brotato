using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    void OnEnable()
    {
        float x = 80 * (1 + (GameManager.instance.player_Info.explosiveSize / 100));
        float y = 80 * (1 + (GameManager.instance.player_Info.explosiveSize / 100));
        gameObject.transform.localScale = new Vector3(x, y);
    }
}
