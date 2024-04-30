using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;

public class ItemTextInfoTable : GameDataTable<ItemTextInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public Item.ItemType itemCode;
        public int textCount;
        public string[] text;
    }
}
