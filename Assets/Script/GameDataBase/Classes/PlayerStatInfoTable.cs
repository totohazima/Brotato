using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class PlayerStatInfoTable : GameDataTable<PlayerStatInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string playerCode;
        public string name;
        public float health;
        public float hpRegen;
        public float bloodSucking;
        public float persentDamage;
        public float meleeDamage;
        public float rangeDamage;
        public float elementalDamage;
        public float attackSpeed;
        public float criticalChance;
        public float engine;
        public float range;
        public float armor;
        public float evasion;
        public float accuracy;
        public float speed;

        public float consumableHeal;
        public float meterialHeal;
        public float expGain;
        public float magnetRange;
        public float priceSale;
        public float explosiveDamage;
        public float explosiveSize;
        public int chain;
        public int penetrate;
        public float penetrateDamage;
        public float bossDamage;
        public float knockBack;
        public float doubleMeterial;
        public float lootInMeterial;
        public float freeReroll;
        public float tree;
        public float enemyAmount;
        public float enemySpeed;
        public float instantMagnet;
    }
}
