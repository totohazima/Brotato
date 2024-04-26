using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class WeaponStatInfoTable : GameDataTable<WeaponStatInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public int weaponNum;
        public string weaponCode;
        public float[] weaponBaseDamage;
        public int[] weaponBaseBulletCount;
        public int riseDamageCount;
        public string[] riseDamageType;
        public float[] riseDamage1, riseDamage2, riseDamage3, riseDamage4;
        public float[] baseCriticalChance;
        public float[] baseCriticalDamage;
        public float[] baseCoolTime;
        public float[] baseKnockback;
        public float[] baseRange;
        public int penetration;
        public float penetrationDamage;
        public string attackType;
        public string weaponType;
    }
}
