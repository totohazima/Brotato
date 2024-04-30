using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public static ItemEffect instance;

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
}
