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
        public int itemMaxCount;
        public int statRiseCount;
        public int statDecreaseCount;
        public string[] riseStatCode;
        public float[] riseNum;
        public string[] decreaseStatCode;
        public float[] decreaseNum;
    }
}
