using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public static ItemEffect instance;

    int minesCount;
    int turretCount;
    public bool IsUglyTooth; //못생긴 이빨 구매 시 적 타격 시마다 스피드 -10% (3회 중첩)
    public bool isWeirdGhost; // 이상한 유령 구매 시 true가 되며 웨이브 시작 시 체력이 1이됨
    void Awake()
    {
        instance = this;
    }

    public void CountCheck()
    {
        List<Item> item = StageManager.instance.playerInfo.itemInventory;

        for (int i = 0; i < item.Count; i++)
        {
            switch(item[i].itemType)
            {
                case Item.ItemType.LAND_MINES:
                    minesCount = item[i].curCount; 
                    break;
                case Item.ItemType.TURRET:
                    turretCount = item[i].curCount; 
                    break;
                case Item.ItemType.UGLY_TOOTH:
                    IsUglyTooth = true; 
                    break;
                case Item.ItemType.WEIRD_GHOST:
                    isWeirdGhost = true; 
                    break;
            }
            
        }
    }

    public int LandMines() // 지뢰
    {
        int effect = minesCount;
        return effect;
    }
    public int Turret() //터렛
    {
        int effect = turretCount;
        return effect;
    }
}
