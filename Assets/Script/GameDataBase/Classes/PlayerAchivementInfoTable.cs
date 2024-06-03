using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class PlayerAchivementInfoTable : GameDataTable<PlayerAchivementInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public Player.Character characterCode;
        public string characterAchivement_Text;
        public bool isEarlyUnlock;
    }
}
