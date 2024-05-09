using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class WeaponPercentageInfoTable : GameDataTable<WeaponPercentageInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string tier;
        public int available_Wave;
        public int minWave;
        public float baseChance;
        public float chancePerWave;
        public float maxChance;
    }
}
