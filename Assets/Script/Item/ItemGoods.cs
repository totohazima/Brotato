using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class ItemGoods : MonoBehaviour, UI_Upadte
{
    public string itemCode;
    public Image itemImage;
    public Text itemName;
    public Transform itemInfoUI;
    int maxCount;
    public Text itemCountType; //-100 아이템, 1초과 한계(0), 1 독특한
    int itemInfoCount; //텍스트 갯수
    string[] itemInfo; //아이템 텍스트
    private float itemBasePrice;
    private float itemPrice;
    public Text itemPriceText;
    public TextMeshProUGUI[] infoText; //텍스트 오브젝트
    public Image lockUI;
    public bool isLock;
    public int itemNum; //itemManager에서 아이템을 찾기 위함
    public Outline line;
    public ItemScrip scriptable;
    void OnEnable()
    {
        UIUpdateManager.uiUpdates.Add(this);
    }
    void OnDisable()
    {
        UIUpdateManager.uiUpdates.Remove(this);
        if (isLock == false) //잠긴 아이템은 상점이 꺼져도 텍스트 삭제X
        {
            for (int i = 0; i < itemInfoUI.childCount; i++)
            {
                Destroy(itemInfoUI.GetChild(i).gameObject);
            }
        }

    }
    public void Init(ItemScrip scrip, int index)
    {
        scriptable = scrip;
        //itemCode = scriptable.itemCode.ToString();
        itemImage.sprite = scriptable.itemSprite;
        itemNum = index;

        
        TextSetting();
    }

    public void UI_Update()
    {
        ItemBasePriceInfoTable.Data priceImport = GameManager.instance.gameDataBase.itemBasePriceInfoTable.table[itemNum];
        itemBasePrice = priceImport.itemBasePrice;
        //for (int z = 0; z < priceImporter.itemCode.Length; z++)
        //{
        //    if (itemCode.ToString() == priceImporter.itemCode[z])
        //    {
        //        itemBasePrice = priceImporter.itemBasePrice[z];
        //        break;
        //    }
        //}
        int wave = StageManager.instance.waveLevel + 1;
        itemPrice = (itemBasePrice + wave + (itemBasePrice * 0.1f * wave)) * 1;
        itemPrice = itemPrice * ((100 + StageManager.instance.playerInfo.priceSale) / 100);
        itemPrice = MathF.Round(itemPrice);
        if (itemPrice > StageManager.instance.money)
        {
            itemPriceText.text = "<color=red>" + itemPrice.ToString("F0") + "</color>";
        }
        else
        {
            itemPriceText.text = itemPrice.ToString("F0");
        }
    }
    public void TextSetting()
    {
        ItemStatInfoTable.Data import = GameManager.instance.gameDataBase.itemStatInfoTable.table[itemNum];

        itemName.text = import.itemName;
        maxCount = import.itemMaxCount;
        itemPriceText.text = itemPrice.ToString("F0");

        if (maxCount == -100)
        {
            itemCountType.text = "아이템";
        }
        else if (maxCount > 1)
        {
            itemCountType.text = "한계(" + maxCount + ")";
        }
        else if (maxCount == 1)
        {
            itemCountType.text = "독특한";
        }

        ItemTextInfoTable.Data textImporter = GameManager.instance.gameDataBase.ItemTextInfoTable.table[itemNum];
        itemInfoCount = textImporter.textCount;
        itemInfo = new string[itemInfoCount];
        for (int i = 0; i < itemInfoCount; i++)
        {
            TextMeshProUGUI text = Instantiate(infoText[0], itemInfoUI);
            itemInfo[i] = textImporter.text[i];
            text.text = itemInfo[i];
        }

        //itemInfoCount = scriptable.infoText.Length;
        //itemInfo = new string[itemInfoCount];
        //for (int i = 0; i < itemInfoCount; i++)
        //{
        //    TextMeshProUGUI text = Instantiate(infoText[0], itemInfoUI);
        //    itemInfo[i] = scriptable.infoText[i];
        //    text.text = itemInfo[i];
        //}
    }

    public void BuyItem()
    {
        if (StageManager.instance.money >= itemPrice)
        {
            StageManager.instance.money -= (int)itemPrice;
            ItemManager.instance.ItemObtain(itemNum);
            ItemManager.instance.ItemListUp();
            //ItemManager.instance.ItemListUp(ShopManager.instance.tabsScroll[1], ShopManager.instance.verticalTabsScroll[1], PauseUI_Manager.instance.scrollContents[1]);
            UnLockIng();
            ShopManager.instance.goodsList.Remove(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }

    public void Lock()
    {
        if(isLock == false)
        { 
            LockIng();
        }
        else
        {
            UnLockIng();
        }
    }
    void LockIng()
    {
        ShopManager.instance.lockList.Add(gameObject);
        isLock = true;
        line.enabled = true;
        lockUI.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    }
    void UnLockIng()
    {
        ShopManager.instance.lockList.Remove(gameObject);
        isLock = false;
        line.enabled = false;
        lockUI.color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
    }
}
