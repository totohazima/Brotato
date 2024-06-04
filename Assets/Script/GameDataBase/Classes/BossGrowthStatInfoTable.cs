using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class BossGrowthStatInfoTable : GameDataTable<BossGrowthStatInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public Enemy.EnemyName monsterCode;
        public float hpRisePer;
        public float attackRisePer;
    }
}
