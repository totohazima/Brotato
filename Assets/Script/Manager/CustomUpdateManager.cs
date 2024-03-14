using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    [HideInInspector]
    public static List<ICustomUpdateMono> customUpdates = new List<ICustomUpdateMono>();

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        for (int i = 0; i < customUpdates.Count; i++)
        {
            customUpdates[i].CustomUpdate();
        }
    }


}
