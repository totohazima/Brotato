using Only1Games.GDBA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBaseStatInfoTable : GameDataTable<EnemyBaseStatInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string monsterCode;
        public float baseHp;
        public float baseDamage;
        public float baseCoolTime;
        public float baseArmor;
        public float baseRange;
        public float baseEvasion;
        public float baseAccuracy;
        public float baseMinSpeed;
        public float baseMaxSpeed;
        public float baseMoneyDropCount;
        public float baseMoneyValue;
        public float baseExp;
        public float baseConsumableDropPersent;
        public float baseLootDropPersent;
    }
}
