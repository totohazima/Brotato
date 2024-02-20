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
    public bool IsUglyTooth; //������ �̻� ���� �� �� Ÿ�� �ø��� ���ǵ� -10% (3ȸ ��ø)
    public bool isWeirdGhost; // �̻��� ���� ���� �� true�� �Ǹ� ���̺� ���� �� ü���� 1�̵�
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
    public int Bag() //���� ȹ�� �� ��� +15
    {
        int effect = 15;
        effect *= bagCount;
        return effect;
    }
    public float Coupon() //���� ���� 5% ����
    {
        float effect = 5;
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
        int effect = 1;
        effect *= minesCount;
        return effect;
    }
    public int Turret() //�ͷ�
    {
        int effect = 1;
        effect *= turretCount;
        return effect;
    }
}