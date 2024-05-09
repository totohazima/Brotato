using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class UpgradeStatInfoTable : GameDataTable<UpgradeStatInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string upgradeCode;
        public string upgradeName;
        public string upgradeEffect;
        public float[] tierEffect;
    }
}