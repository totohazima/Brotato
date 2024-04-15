using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_Info", menuName = "Wave_Info/wave")]
public class Wave_Scriptable : ScriptableObject
{
    public GameObject[] spawnEnemys;
    public float[] enemyPersentage; //�� ���ֵ� ��ȯ�� Ȯ��;
    public bool isBossSpawn; //true�� ��� �ش� �������� ���� ����
}
