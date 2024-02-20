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
    public bool IsUglyTooth; //못생긴 이빨 구매 시 적 타격 시마다 스피드 -10% (3회 중첩)
    public bool isWeirdGhost; // 이상한 유령 구매 시 true가 되며 웨이브 시작 시 체력이 1이됨
    void Awake()
    {
        instance = this;
    }

    public void CountCheck()
    {
        List<Item> item = GameManager.instance.playerInfo.items;

        bagCount = 0;
        couponCount = 0;
        monkeyCount = 0;
        gentleCount = 0;
        minesCount = 0;
        turretCount = 0; 
        for (int i = 0; i < item.Count; i++)
        {
            switch(item[i].itemType)
            {
                case Item.ItemType.BAG:
                    bagCount++; break;
                case Item.ItemType.COUPON:
                    couponCount++; break;
                case Item.ItemType.CUTE_MONKEY:
                    monkeyCount++; break;
                case Item.ItemType.GENTLE_ALIEN:
                    gentleCount++; break;
                case Item.ItemType.LAND_MINES:
                    minesCount++; break;
                case Item.ItemType.TURRET:
                    turretCount++; break;
                case Item.ItemType.UGLY_TOOTH:
                    IsUglyTooth = true; break;

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
        float effect = 5;
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
        int effect = 1;
        effect *= minesCount;
        return effect;
    }
    public int Turret() //터렛
    {
        int effect = 1;
        effect *= turretCount;
        return effect;
    }
}
