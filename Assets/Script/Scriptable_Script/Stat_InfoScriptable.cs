using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Info", menuName = "StatInfo/stat")]
public class Stat_InfoScriptable : ScriptableObject
{
    public string statName;
    public Sprite statImage;
    public string[] statPlusText;
    public string[] statMinusText;
}
