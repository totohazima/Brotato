using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataImporter : MonoBehaviour
{
    public static GameDataImporter instance;

    void Awake()
    {
        instance = this;

    }
}
