using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;

public class WaveStatInfoTable : GameDataTable<WaveStatInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public float maxEnemySpawn;
        public float enemySpawnCount;
        public float enemySpawnTime;
        public float waveTime;
        public float waveTreeStat; 
    }
}
