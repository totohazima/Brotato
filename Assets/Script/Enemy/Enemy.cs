using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyName name;
    public int level;
    public float maxHealth;
    public float curHealth;
    public float healthPerWave; //���̺� �� ü�� ����ġ
    public float damage;
    public float coolTime;
    public float armor;
    public float range;
    public float evasion;
    public float acuraccy;
    public float speed;

    public enum EnemyName
    {
        BASIC,
    }

}
