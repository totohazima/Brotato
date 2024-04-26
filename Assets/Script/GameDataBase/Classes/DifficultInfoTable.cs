using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class DifficultInfoTable : GameDataTable<DifficultInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string difficultName;
        public bool specialEnemySpawn;
        public bool eliteEnemySpawn;
        public int eliteEnemySpawnCount;
        public float enemyDamageRise;
        public float enemyHpRise;
        public bool twinBoss;
    }
}
