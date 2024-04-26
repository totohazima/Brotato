using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class EnemyGrowthStatInfoTable : GameDataTable<EnemyGrowthStatInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string monsterCode;
        public float hpRisePer;
        public float attackRisePer;
    }
}
