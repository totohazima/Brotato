using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetImporter : MonoBehaviour
{
    public TextAsset[] sheets;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
