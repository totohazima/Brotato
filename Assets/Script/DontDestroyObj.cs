using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObj : MonoBehaviour
{
    void Awake()
    {
        var obj = FindObjectsOfType<DontDestroyObj>();

        for (int i = 0; i < obj.Length; i++)
        {
            if(obj[i].gameObject == gameObject)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
        //if(obj.Length == 1)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}
