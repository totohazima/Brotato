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
        public float attackSpeed;
        public float criticalChance;
        public float engine;
        public float range;
        public float armor;
        public float evasion;
        public float accuracy;
        public float speed;
    }
}
