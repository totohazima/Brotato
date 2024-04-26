using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class ItemBasePriceInfoTable : GameDataTable<ItemBasePriceInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string itemCode;
        public float itemBasePrice;

    }
}
