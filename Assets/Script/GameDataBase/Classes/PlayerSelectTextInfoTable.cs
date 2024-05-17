using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;

public class PlayerSelectTextInfoTable : GameDataTable<PlayerSelectTextInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public Player.Character characterCode;
        public string characterName;
        public string[] characterInfo;
    }
}
