using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatImporter : MonoBehaviour
{
    public static PlayerStatImporter instance;

    void Awake()
    {
        instance = this;
    }
}
