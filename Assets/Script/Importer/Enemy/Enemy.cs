using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyName name;
    public int level;
    public float maxHealth;
    public float curHealth;
    public float healthPerWave; //웨이브 당 체력 증가치
    public float damage;
    public float damagePerWave;//웨이브 당 데미지 증가치 
    public float coolTime;
    public float armor;
    public float range;
    public float evasion;
    public float accuracy;
    public float minSpeed;
    public float maxSpeed;
    public float moneyDropRate; //재화 드랍 개수
    public float consumableDropRate; //소모품 드랍 확률;
    public float lootDropRate; //전리품 드랍 확률;
    public bool isDie;
    public bool isHit; //플레이어와 부딫힐 경우 1초 동안 플레이어와 피격판정이 되지 않음
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
