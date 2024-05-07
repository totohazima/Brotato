using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class ItemStatInfoTable : GameDataTable<ItemStatInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public Item.ItemType itemCode;
        public string itemName;
        public int itemMaxCount;
        public int statRiseCount;
        public int statDecreaseCount;
        public Stat.PlayerStat[] riseStatCode;
        public float[] riseNum;
        public Stat.PlayerStat[] decreaseStatCode;
        public float[] decreaseNum;
    }
}
