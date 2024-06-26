using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stat")]
    public EnemyName name;
    public Stat.enemyType enemyType;
    public int level;
    public float maxHealth;
    public float curHealth;
    [HideInInspector] public float healthPerWave; //웨이브 당 체력 증가치
    public float damage;
    [HideInInspector] public float damagePerWave;//웨이브 당 데미지 증가치 
    public float coolTime;
    public float armor;
    public float range;
    public float evasion;
    public float accuracy;
    public float minSpeed;
    public float maxSpeed;
    [HideInInspector] public float moneyDropRate; //재화 드랍 개수
    [HideInInspector] public int moneyValue;
    [HideInInspector] public int expValue;
    [HideInInspector] public float consumableDropRate; //소모품 드랍 확률;
    [HideInInspector] public float lootDropRate; //전리품 드랍 확률;
    public bool isDie;

    [Header("Debuff")]
    public int ugliyToothSlow; //못생긴 이빨 효과 슬로우 최대 30%

    public enum EnemyName
    {
        BASIC,
        RANGER,
        CHARGER,
        TANKER,
        PREDATOR,   //보스
        INVOKER,    //보스
        TREE,   //중립
    }

    public void StatSetting(EnemyName index, Stat.enemyType type)
    {
        StatReset();
        void StatReset()
        {
            EnemyBaseStatInfoTable.Data enemy = null;
            EnemyGrowthStatInfoTable.Data grow = null;
            for (int i = 0; i < GameManager.instance.gameDataBase.enemyBaseStatInfoTable.table.Length; i++)
            {
                if (GameManager.instance.gameDataBase.enemyBaseStatInfoTable.table[i].monsterCode == index)
                {
                    enemy = GameManager.instance.gameDataBase.enemyBaseStatInfoTable.table[i];

                }
            }
            for (int i = 0; i < GameManager.instance.gameDataBase.enemyGrowthStatInfoTable.table.Length; i++)
            {
                if (GameManager.instance.gameDataBase.enemyGrowthStatInfoTable.table[i].monsterCode == index)
                {
                    grow = GameManager.instance.gameDataBase.enemyGrowthStatInfoTable.table[i];
                }
            }

            enemyType = enemy.enemyType;
            maxHealth = enemy.baseHp;
            damage = enemy.baseDamage;
            coolTime = enemy.baseCoolTime;
            armor = enemy.baseArmor;
            range = enemy.baseRange;
            evasion = enemy.baseEvasion;
            accuracy = enemy.baseAccuracy;
            minSpeed = enemy.baseMinSpeed;
            maxSpeed = enemy.baseMaxSpeed;
            moneyDropRate = enemy.baseMoneyDropCount;
            moneyValue = (int)enemy.baseMoneyValue;
            expValue = (int)enemy.baseExp;
            consumableDropRate = enemy.baseConsumableDropPersent;
            lootDropRate = enemy.baseLootDropPersent;

            healthPerWave = grow.hpRisePer;
            damagePerWave = grow.attackRisePer;
        }
        ////일반, 중립 몹 스탯 적용
        //if (type == Stat.enemyType.NORMAL_ENEMY || type == Stat.enemyType.NEUTRALITY_ENEMY)
        //{
        //    StatReset();
        //    void StatReset()
        //    {
        //        EnemyBaseStatInfoTable.Data enemy = null;
        //        EnemyGrowthStatInfoTable.Data grow = null;
        //        for (int i = 0; i < GameManager.instance.gameDataBase.enemyBaseStatInfoTable.table.Length; i++)
        //        {
        //            if (GameManager.instance.gameDataBase.enemyBaseStatInfoTable.table[i].monsterCode == index)
        //            {
        //                enemy = GameManager.instance.gameDataBase.enemyBaseStatInfoTable.table[i];

        //            }
        //        }
        //        for (int i = 0; i < GameManager.instance.gameDataBase.enemyGrowthStatInfoTable.table.Length; i++)
        //        {
        //            if (GameManager.instance.gameDataBase.enemyGrowthStatInfoTable.table[i].monsterCode == index)
        //            {
        //                grow = GameManager.instance.gameDataBase.enemyGrowthStatInfoTable.table[i];
        //            }
        //        }

        //        enemyType = enemy.enemyType;
        //        maxHealth = enemy.baseHp;
        //        damage = enemy.baseDamage;
        //        coolTime = enemy.baseCoolTime;
        //        armor = enemy.baseArmor;
        //        range = enemy.baseRange;
        //        evasion = enemy.baseEvasion;
        //        accuracy = enemy.baseAccuracy;
        //        minSpeed = enemy.baseMinSpeed;
        //        maxSpeed = enemy.baseMaxSpeed;
        //        moneyDropRate = enemy.baseMoneyDropCount;
        //        moneyValue = (int)enemy.baseMoneyValue;
        //        expValue = (int)enemy.baseExp;
        //        consumableDropRate = enemy.baseConsumableDropPersent;
        //        lootDropRate = enemy.baseLootDropPersent;

        //        healthPerWave = grow.hpRisePer;
        //        damagePerWave = grow.attackRisePer;
        //    }
        //}
        ////보스 몹 스탯 적용
        //else if (type == Stat.enemyType.BOSS_ENEMY)
        //{
        //    StatReset();
        //    void StatReset()
        //    {
        //        BossBaseStatInfoTable.Data enemy = null;
        //        BossGrowthStatInfoTable.Data grow = null;
        //        for (int i = 0; i < GameManager.instance.gameDataBase.bossBaseStatInfoTable.table.Length; i++)
        //        {
        //            if (GameManager.instance.gameDataBase.bossBaseStatInfoTable.table[i].monsterCode == index)
        //            {
        //                enemy = GameManager.instance.gameDataBase.bossBaseStatInfoTable.table[i];

        //            }
        //        }
        //        for (int i = 0; i < GameManager.instance.gameDataBase.bossGrowthStatInfoTable.table.Length; i++)
        //        {
        //            if (GameManager.instance.gameDataBase.bossGrowthStatInfoTable.table[i].monsterCode == index)
        //            {
        //                grow = GameManager.instance.gameDataBase.bossGrowthStatInfoTable.table[i];
        //            }
        //        }

        //        enemyType = enemy.enemyType;
        //        maxHealth = enemy.baseHp;
        //        damage = enemy.baseDamage;
        //        coolTime = enemy.baseCoolTime;
        //        armor = enemy.baseArmor;
        //        range = enemy.baseRange;
        //        evasion = enemy.baseEvasion;
        //        accuracy = enemy.baseAccuracy;
        //        minSpeed = enemy.baseMinSpeed;
        //        maxSpeed = enemy.baseMaxSpeed;
        //        moneyDropRate = enemy.baseMoneyDropCount;
        //        moneyValue = (int)enemy.baseMoneyValue;
        //        expValue = (int)enemy.baseExp;
        //        consumableDropRate = enemy.baseConsumableDropPersent;
        //        lootDropRate = enemy.baseLootDropPersent;

        //        healthPerWave = grow.hpRisePer;
        //        damagePerWave = grow.attackRisePer;
        //    }
        //}

        WaveStat();
        //void WaveStat()
        //{
        //    int wave = StageManager.instance.waveLevel;

        //    float num = maxHealth;
        //    for (int i = 0; i < wave; i++)
        //    {
        //        num *= 1 + (healthPerWave / 100);
        //    }
        //    maxHealth = num;

        //    float num2 = damage;
        //    for (int i = 0; i < wave; i++)
        //    {
        //        num2 *= 1 + (damagePerWave / 100);
        //    }
        //    damage = num2;

        //    maxHealth += maxHealth * (GameManager.instance.enemyRiseHealth / 100);
        //    damage += damage * (GameManager.instance.enemyRiseDamage / 100);

        //    //보스 2마리 소환 시 체력 25% 감소
        //    if (type == Stat.enemyType.BOSS_ENEMY && GameManager.instance.doubleBoss == true)
        //    {
        //        maxHealth = maxHealth * 0.75f;
        //    }
        //}
        void WaveStat()
        {
            int wave = StageManager.instance.waveLevel;

            float num = maxHealth;
            for (int i = 0; i < wave; i++)
            {
                num += healthPerWave;
            }
            maxHealth = num;

            float num2 = damage;
            for (int i = 0; i < wave; i++)
            {
                num2 += damagePerWave; 
            }
            damage = num2;

            maxHealth += maxHealth * (GameManager.instance.enemyRiseHealth / 100);
            damage += damage * (GameManager.instance.enemyRiseDamage / 100);

            //보스 2마리 소환 시 체력 25% 감소
            if (type == Stat.enemyType.BOSS_ENEMY && GameManager.instance.doubleBoss == true)
            {
                maxHealth = maxHealth * 0.75f;
            }
        }
        curHealth = maxHealth;
    }
}
