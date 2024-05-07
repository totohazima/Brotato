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
    public bool isFull; //정해진 수량을 전부 먹었을 경우 true
    public SpriteRenderer itemSprite;
    public Image itemImage;
    public Text itemCount;
    public int riseCount;
    public Stat.PlayerStat[] riseStat;
    public float[] riseStats;

    public int decreaseCount;
    public Stat.PlayerStat[] decreaseStat;
    public float[] decreaseStats;

    public bool isMax;
    public Outline frame;
    public Item_Info itemInfo;

    [HideInInspector]
    public ItemScrip scriptable;
    public enum ItemType
    {
        ALIEN_TONGUE,
        ALIEN_WORM,
        BABY_ELEPHANT,
        BABY_GECKO,
        BAG,
        BAT,
        BEANIE,
        BOILING_WATER,
        BOOK,
        BOXING_GLOVE,
        BROKEN_MOUTH,
        BUTTERFLY,
        CAKE,
        CHARCOAL,
        CLAW_TREE,
        COFFEE,
        COUPON,
        CUTE_MONKEY,
        DEFECTIVE_STEROIDS,
        DUCT_TAPE,
        DYNAMITE,
        FERTILIZER,
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
        LOST_DUCK,
        LUMBERJACK_SHIRT,
        MUSHROOM,
        MUTATION,
        PEACEFUL_BEE,
        PENCIL,
        PLANT,
        PROPELLER_HAT,
        SCAR,
        SCARED_SAUSAGE,
        SHARP_BULLET,
        SNAKE,
        TERRIFIED_ONION,
        TOXIC_SLUDGE,
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
        StatSetting((int)itemType);
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
    public virtual void ShowItemInfo()//클릭 시 아이템 정보를 보여주는 용도
    {
        //클릭 하고 있을 시 아이템의 하얀 테두리가 나온다
        //클릭 중에는 itemGoods와 동일한 UI가 나타난다(가격, 잠금버튼 없는)
        //UI는 중심을 기준으로 x가 +면 왼쪽으로 y가 +면 아이템 아래로 생성한다. (반대의 경우엔 정반대로 생성)
        //클릭 해제 시 하얀 테두리만 남고 UI는 꺼진다.
        infoObj = Instantiate(itemInfo, StageManager.instance.itemInfoManager);
        infoObj.Init(scriptable, transform.position);
    }
    public void StatSetting(int index)
    {
        ItemStatInfoTable.Data import = GameManager.instance.gameDataBase.itemStatInfoTable.table[index];

        itemCode = import.itemCode.ToString();
        maxCount = import.itemMaxCount;

        riseCount = import.statRiseCount;
        decreaseCount = import.statDecreaseCount;

        riseStat = new Stat.PlayerStat[riseCount];
        riseStats = new float[riseCount];
        decreaseStat = new Stat.PlayerStat[decreaseCount];
        decreaseStats = new float[decreaseCount];

        Stat.PlayerStat[] name1 = new Stat.PlayerStat[riseCount];
        Stat.PlayerStat[] name2 = new Stat.PlayerStat[decreaseCount];

        int i = 0;
        while (i < riseCount)
        {
            name1[i] = import.riseStatCode[i];
            riseStat[i] = name1[i];
            riseStats[i] = import.riseNum[i];
            i++;
        }
        i = 0;
        while (i < decreaseCount)
        {
            name2[i] = import.decreaseStatCode[i];
            decreaseStat[i] = name2[i];
            decreaseStats[i] = import.decreaseNum[ i];
            i++;
        }

        ItemBasePriceInfoTable.Data priceImport = GameManager.instance.gameDataBase.itemBasePriceInfoTable.table[index];

        int wave = StageManager.instance.waveLevel + 1;
        itemBasePrice = priceImport.itemBasePrice;
        itemPrice = (itemBasePrice + wave + (itemBasePrice * 0.1f * wave)) * 1;
        itemPrice = MathF.Round(itemPrice);
    }  
}
