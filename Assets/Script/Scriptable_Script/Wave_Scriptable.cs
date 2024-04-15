using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_Info", menuName = "Wave_Info/wave")]
public class Wave_Scriptable : ScriptableObject
{
    public GameObject[] spawnEnemys;
    public float[] enemyPersentage; //각 유닛들 소환될 확률;
    public bool isBossSpawn; //true일 경우 해당 스테이지 보스 등장
}
