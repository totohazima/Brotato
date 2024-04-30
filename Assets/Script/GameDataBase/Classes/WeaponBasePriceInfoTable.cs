using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class WeaponBasePriceInfoTable : GameDataTable<WeaponBasePriceInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string weaponCode;
        public float[] weaponBasePrice;
    }
}
