using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, ICustomUpdateMono
{
    public ItemType itemType;
    public string itemCode;
    public string itemName;
    private float itemBasePrice;
    public float itemPrice;
    public int curCount;
    public int maxCount;
    public bool isFull; //������ ������ ���� �Ծ��� ��� true
    public SpriteRenderer itemSprite;
    public Image itemImage;
    public Text itemCount;
    public int riseCount;
    public Stat.PlayerStat[] riseStat;
    public float[] riseStats;

    public int descendCount;
    public Stat.PlayerStat[] descendStat;
    public float[] descendStats;

    public bool isMax;
    public Outline frame;
    public Item_Info itemInfo;

    [HideInInspector]
    public ItemScrip scriptable;
    public enum ItemType
    {
        ALIEN_TONGUE,
        ALIEN_WORM,
        BABY_GECKO,
        BAG,
        BEANIE,
        BOOK,
        BOXING_GLOVE,
        BROKEN_MOUTH,
        CAKE,
        CLAW_TREE,
        COFFEE,
        COUPON,
        CUTE_MONKEY,
        DEFECTIVE_STEROIDS,
        DUCT_TAPE,
        DYNAMITE,
        GENTLE_ALIEN,
        GLASSES,
        GOAT_SKULL,
        GUMMY_BERSERKER,
        HEAD_INJURY,
        HEDGEHOG,
        HELMET,
        INJECTION,
        INSANITY,
        LAND_MINES,
        LEMONADE,
        LENS,
        PENCIL,
        PLANT,
        SCAR,
        SHARP_BULLET,
        TREE,
        TURRET,
        UGLY_TOOTH,
        WEIRD_FOOD,
        WEIRD_GHOST,
    }

    public void Init(ItemScrip scrip)
    {
        scriptable = scrip;
        itemType = scrip.itemCode;
        itemImage.sprite = scrip.itemSprite;
        StatSetting(itemType.ToString());
    }

    public virtual void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    public virtual void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }
    public virtual void CustomUpdate()
    {
        if (itemCount != null)
        {
            if(curCount <= 1)
            {
                itemCount.gameObject.SetActive(false);
            }
            else
            {
                itemCount.text = "x" + curCount;
                itemCount.gameObject.SetActive(true);
            }

            if (maxCount != -100)
            {
                if (curCount >= maxCount)
                {
                    if (isMax == false)
                    {
                        ItemManager.instance.maxItemList.Add(itemType);
                        isMax = true;
                    }
                    
                }
                else
                {
                    if (isMax == true)
                    {
                        ItemManager.instance.maxItemList.Remove(itemType);
                        isMax = false;
                    }
                    
                }
            }

        }
        int wave = StageManager.instance.waveLevel + 1;
        itemPrice = (itemBasePrice + (wave) + (itemBasePrice * 0.1f * wave)) * 1;
        itemPrice = MathF.Round(itemPrice);
        
    }
    Item_Info infoObj = null;

    public void PointDown()
    {
        frame.effectColor = Color.white;
        ShowItemInfo();
    }
    public void PointUp()
    {
        Destroy(infoObj.gameObject);
        frame.effectColor = Color.black;
    }
    public void PointClick()
    {
        int itemCount = transform.parent.childCount;
        Outline[] line = new Outline[itemCount];
        Transform content = transform.parent;
        for (int i = 0; i < itemCount; i++)
        {
            line[i] = content.GetChild(i).GetComponent<Outline>();
            line[i].effectColor = Color.black;
        }
        frame.effectColor = Color.white;
    }
    public virtual void ShowItemInfo()//Ŭ�� �� ������ ������ �����ִ� �뵵
    {
        //Ŭ�� �ϰ� ���� �� �������� �Ͼ� �׵θ��� ���´�
        //Ŭ�� �߿��� itemGoods�� ������ UI�� ��Ÿ����(����, ��ݹ�ư ����)
        //UI�� �߽��� �������� x�� +�� �������� y�� +�� ������ �Ʒ��� �����Ѵ�. (�ݴ��� ��쿣 ���ݴ�� ����)
        //Ŭ�� ���� �� �Ͼ� �׵θ��� ���� UI�� ������.
        infoObj = Instantiate(itemInfo, StageManager.instance.itemInfoManager);
        infoObj.Init(scriptable, transform.position);
    }
    public void StatSetting(string names)
    {
        ItemStatImporter import = ItemStatImporter.instance;
        int index = 0;
        for (int z = 0; z < import.itemCode.Length; z++)
        {
            if(names == import.itemCode[z])
            {
                index = z;
            }
        }

        itemCode = import.itemCode[index];
        //itemName = import.itemName[index];
        maxCount = import.maxCount[index];

        riseCount = import.riseCount[index];
        descendCount = import.descendCount[index];

        riseStat = new Stat.PlayerStat[riseCount];
        riseStats = new float[riseCount];
        descendStat = new Stat.PlayerStat[descendCount];
        descendStats = new float[descendCount];

        string[] name = new string[riseCount];
        string[] name2 = new string[descendCount];

        int i = 0;
        while (i < riseCount)
        {
            name[i] = import.riseStatType[index + i];
            riseStat[i] = (Stat.PlayerStat)Enum.Parse(typeof(Stat.PlayerStat), name[i]);
            riseStats[i] = import.riseStats[index + i];
            i++;
        }
        i = 0;
        while (i < descendCount)
        {
            name2[i] = import.descendStatType[index + i];
            descendStat[i] = (Stat.PlayerStat)Enum.Parse(typeof(Stat.PlayerStat), name2[i]);
            descendStats[i] = import.descendStats[index + i];
            i++;
        }

        ShopBasePriceImporter priceImporter = ShopBasePriceImporter.instance;

        for (int z = 0; z < priceImporter.itemCode.Length; z++)
        {
            if(itemType.ToString() == priceImporter.itemCode[z])
            {
                itemBasePrice = priceImporter.itemBasePrice[z];
                break;
            }
        }
    }  
}
