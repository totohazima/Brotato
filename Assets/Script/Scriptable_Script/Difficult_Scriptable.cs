using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficult_Info", menuName = "Difficult_Info/difficult")]
public class Difficult_Scriptable : ScriptableObject
{
    public int difficult_Level;
    public Wave_Scriptable[] waveInfo;
}
