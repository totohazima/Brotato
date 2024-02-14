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
    public float damagePerWave;//���̺� �� ������ ����ġ 
    public float coolTime;
    public float armor;
    public float range;
    public float evasion;
    public float accuracy;
    public float minSpeed;
    public float maxSpeed;
    public float moneyDropRate; //��ȭ ��� ����
    public float consumableDropRate; //�Ҹ�ǰ ��� Ȯ��;
    public float lootDropRate; //����ǰ ��� Ȯ��;
    public bool isDie;
    public bool isHit; //�÷��̾�� �΋H�� ��� 1�� ���� �÷��̾�� �ǰ������� ���� ����
    public enum EnemyName
    {
        BASIC,
    }

    public void StatSetting(int index)
    {
        EnemyStatImporter enemy = EnemyStatImporter.instance;

        maxHealth = enemy.health[index];
        curHealth = maxHealth;
        healthPerWave = enemy.healthWave[index];
        damage = enemy.damage[index];
        damagePerWave = enemy.damageWave[index];
        coolTime = enemy.coolTime[index];
        armor = enemy.armor[index];
        range = enemy.range[index];
        evasion = enemy.evasion[index];
        accuracy = enemy.accuracy[index];
        minSpeed = enemy.minSpeed[index];
        maxSpeed = enemy.maxSpeed[index];
        moneyDropRate = enemy.moneyDropNum[index];
        consumableDropRate = enemy.consumDropRate[index];
        lootDropRate = enemy.LootDropRate[index];
    }
}
