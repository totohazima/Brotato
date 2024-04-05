using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdateManager : MonoBehaviour
{
    [HideInInspector]
    public static List<UI_Upadte> uiUpdates = new List<UI_Upadte>();
    void Update()
    {
        for (int i = 0; i < uiUpdates.Count; i++)
        {
            uiUpdates[i].UI_Update();
        }
    }
}
