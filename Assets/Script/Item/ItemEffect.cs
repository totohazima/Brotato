using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public static ItemEffect instance;

    int bagCount;
    int couponCount;
    int monkeyCount;
    int gentleCount;
    int minesCount;
    int turretCount;
    int treeCount;
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
                case Item.ItemType.BAG:
                    bagCount = item[i].curCount; break;
                case Item.ItemType.COUPON:
                    couponCount = item[i].curCount; break;
                case Item.ItemType.CUTE_MONKEY:
                    monkeyCount = item[i].curCount; break;
                case Item.ItemType.GENTLE_ALIEN:
                    gentleCount = item[i].curCount; break;
                case Item.ItemType.LAND_MINES:
                    minesCount = item[i].curCount; break;
                case Item.ItemType.TURRET:
                    turretCount = item[i].curCount; break;
                case Item.ItemType.TREE:
                    treeCount = item[i].curCount; break;
                case Item.ItemType.UGLY_TOOTH:
                    IsUglyTooth = true; break;
                case Item.ItemType.WEIRD_GHOST:
                    isWeirdGhost = true; break;
            }
            
        }
    }
    public int Bag() //상자 획득 시 재료 +15
    {
        int effect = 15;
        effect *= bagCount;
        return effect;
    }
    public float Coupon() //상점 가격 5% 할인
    {
        float effect = -5;
        effect *= couponCount;
        return effect;
    }
    public float CuteMonkey() //재료 획득 시 8% 확률로 체력 1회복
    {
        float effect = 8;
        effect *= monkeyCount;
        return effect;
    }
    public int GentleAlien() // 아이템 하나 당 적+5%
    {
        int effect = 5;
        effect *= gentleCount;
        return effect;
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
    public int Tree()
    {
        int effect = treeCount;
        return effect;
    }
}
