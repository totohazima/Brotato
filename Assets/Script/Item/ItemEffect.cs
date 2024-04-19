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
    public bool IsUglyTooth; //������ �̻� ���� �� �� Ÿ�� �ø��� ���ǵ� -10% (3ȸ ��ø)
    public bool isWeirdGhost; // �̻��� ���� ���� �� true�� �Ǹ� ���̺� ���� �� ü���� 1�̵�
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
    public int Bag() //���� ȹ�� �� ��� +15
    {
        int effect = 15;
        effect *= bagCount;
        return effect;
    }
    public float Coupon() //���� ���� 5% ����
    {
        float effect = -5;
        effect *= couponCount;
        return effect;
    }
    public float CuteMonkey() //��� ȹ�� �� 8% Ȯ���� ü�� 1ȸ��
    {
        float effect = 8;
        effect *= monkeyCount;
        return effect;
    }
    public int GentleAlien() // ������ �ϳ� �� ��+5%
    {
        int effect = 5;
        effect *= gentleCount;
        return effect;
    }
    public int LandMines() // ����
    {
        int effect = minesCount;
        return effect;
    }
    public int Turret() //�ͷ�
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
