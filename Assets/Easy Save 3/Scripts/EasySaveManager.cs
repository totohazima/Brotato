using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySaveManager : MonoBehaviour
{
    private static EasySaveManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
