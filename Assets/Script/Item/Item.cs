using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemType itemType;
    public string itemCode;
    public string itemName;
    public int curCount;
    public int maxCount;
    public bool isFull; //정해진 수량을 전부 먹었을 경우 true
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
        TURRET,
        UGLY_TOOTH,
        WEIRD_FOOD,
        WEIRD_GHOST,
    }

    //void OnEnable()
    //{
    //    StatSetting(itemType.ToString());
    //}

    public void Init(ItemType type, Sprite itemSprite)
    {
        itemType = type;
        itemImage.sprite = itemSprite;
        StatSetting(itemType.ToString());
    }

    void Update()
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

            if (curCount == maxCount)
            {
                if(isMax == false)
                {
                    ShopManager.instance.maxItemList.Add((int)itemType);
                }
                isMax = true;
            }
            else
            {
                if (isMax == true)
                {
                    ShopManager.instance.maxItemList.Remove((int)itemType);
                }
                isMax = false;
            }

            
        }

        
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
        itemName = import.itemName[index];
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
        while (i <= riseCount - 1)
        {
            name[i] = import.riseStatType[index + i];
            riseStat[i] = (Stat.PlayerStat)Enum.Parse(typeof(Stat.PlayerStat), name[i]);
            riseStats[i] = import.riseStats[index + i];
            i++;
        }
        i = 0;
        while (i <= descendCount - 1)
        {
            name2[i] = import.descendStatType[index + i];
            descendStat[i] = (Stat.PlayerStat)Enum.Parse(typeof(Stat.PlayerStat), name2[i]);
            descendStats[i] = import.descendStats[index + i];
            i++;
        }

        ///for문 방식으로 할 시 데이터가 온전히 불러와지지 않음
        //for (int i = 0; i < riseCount - 1; i++) 
        //{
        //    name[i] = Import.riseStatType[index + i];
        //    riseStat[i] = (Stat.PlayerStat)Enum.Parse(typeof(Stat.PlayerStat), name[i]);
        //    riseStats[i] = Import.riseStats[index + i];
        //}

        //for (int i = 0; i < descendCount - 1; i++)        
        //{
        //    name2[i] = Import.descendStatType[index + i];
        //    descendStat[i] = (Stat.PlayerStat)Enum.Parse(typeof(Stat.PlayerStat), name2[i]);
        //    descendStats[i] = Import.descendStats[index + i];
        //}
    }  
}
