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
    [HideInInspector]
    public int itemNum; //itemManager에서 아이템을 찾기 위함
    public Outline line;
    ItemScrip scriptable;
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
        itemCode = scriptable.itemCode.ToString();
        itemImage.sprite = scriptable.itemSprite;
        itemNum = index;

        
        TextSetting(itemCode);
    }

    public void UI_Update()
    {
        ShopBasePriceImporter priceImporter = ShopBasePriceImporter.instance;
        for (int z = 0; z < priceImporter.itemCode.Length; z++)
        {
            if (itemCode.ToString() == priceImporter.itemCode[z])
            {
                itemBasePrice = priceImporter.itemBasePrice[z];
                break;
            }
        }
        int wave = GameManager.instance.waveLevel + 1;
        itemPrice = (itemBasePrice + wave + (itemBasePrice * 0.1f * wave)) * 1;
        itemPrice = itemPrice * ((100 + ItemEffect.instance.Coupon()) / 100);
        itemPrice = MathF.Round(itemPrice);
        itemPriceText.text = itemPrice.ToString("F0");
    }
    public void TextSetting(string code)
    {
        ItemStatImporter import = ItemStatImporter.instance;
        int index1 = 0;
        for (int i = 0; i < import.itemCode.Length; i++)
        {
            if (code == import.itemCode[i])
            {
                index1 = i;
            }
        }
        itemName.text = scriptable.itemName;
        maxCount = import.maxCount[index1];
        itemPriceText.text = itemPrice.ToString("F0");
        
        if(maxCount == -100)
        {
            itemCountType.text = "아이템";
        }
        else if(maxCount > 1)
        {
            itemCountType.text = "한계(" + maxCount + ")";
        }
        else if(maxCount == 1)
        {
            itemCountType.text = "독특한";
        }

        itemInfoCount = scriptable.infoText.Length;

        itemInfo = new string[itemInfoCount];

        for (int i = 0; i < itemInfoCount; i++)
        {
            TextMeshProUGUI text = Instantiate(infoText[0], itemInfoUI);
            itemInfo[i] = scriptable.infoText[i];
            text.text = itemInfo[i];
        }
    }
    
    public void BuyItem()
    {
        if (GameManager.instance.money >= itemPrice)
        {
            GameManager.instance.money -= (int)itemPrice;
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
