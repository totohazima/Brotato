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
    public Transform info;
    public GameObject item_Info;
    public Item_Info infoObj;
    [HideInInspector] public ItemScrip scriptable;
    [HideInInspector] public RectTransform myRect;
    [HideInInspector] public RectTransform infoRect;
    [HideInInspector] public Vector2 originInfo_OffsetMax;
    
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

    void Awake()
    {
        myRect = gameObject.GetComponent<RectTransform>();
        infoRect = info.GetComponent<RectTransform>();
        originInfo_OffsetMax = infoRect.offsetMax;
        infoObj = item_Info.GetComponent<Item_Info>();
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

    public void PointDown()
    {
        frame.effectColor = Color.white;
        ShowItemInfo();
    }
    public void PointUp()
    {
        //Destroy(infoObj.gameObject);
        item_Info.transform.SetParent(infoObj.masterItem);
        item_Info.SetActive(false);
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
        infoObj.Init(scriptable, transform.position);
        infoObj.masterItem = info;

        item_Info.SetActive(true);
        ForceRebuildLayouts(infoRect, infoObj.bgRect);
        // 캔버스 상 좌표에서 0 이하인 경우
        if (myRect.localPosition.y <= 0)
        {
            //y값을 측정해 ItemInfo가 딱 맞는 위치에 소환되게 함
            ForceRebuildLayouts(infoRect, infoObj.bgRect);
            infoRect.offsetMax = originInfo_OffsetMax;
            float calcY = infoObj.itemInfoUI_Rect.anchoredPosition.y - infoObj.originItemInfo_PosY; //(0, -50)
            float top = -infoRect.offsetMax.y/*(0, -40)*/ + calcY;
            infoRect.offsetMax = new Vector2(0, -top);
        }
        else
        {
            //y값을 측정해 ItemInfo가 딱 맞는 위치에 소환되게 함
            ForceRebuildLayouts(infoRect, infoObj.bgRect);
            float heightPos = infoObj.bgRect.rect.height;
            infoRect.offsetMax = new Vector2(0, -heightPos);
        }

        item_Info.transform.SetParent(StageManager.instance.itemInfoManager);
    }
    public void ForceRebuildLayouts(params RectTransform[] rectTransforms)
    {
        foreach (var rectTransform in rectTransforms)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
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
