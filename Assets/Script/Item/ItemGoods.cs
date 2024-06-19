using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class ItemGoods : MonoBehaviour, UI_Upadte
{
    public Item.ItemType itemCode;
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
    private bool isPriceEnd;
    void OnEnable()
    {
        UIUpdateManager.uiUpdates.Add(this);
        isPriceEnd = false;
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
        itemCode = scriptable.itemCode;
        itemImage.sprite = scriptable.itemSprite;
        itemNum = index;

        
        TextSetting();
    }

    public void UI_Update()
    {
        //잠겼을 땐 가격이 그대로 유지되어야 함
        if (GameManager.instance.isEnd && isLock)
        {
            // GameManager.instance.isEnd가 true이고, 상품이 잠긴 경우
            // 가격 설정을 건너뛰어야 합니다.
            isPriceEnd = true;  // 가격 설정이 이미 끝난 상태로 설정합니다.
        }
        else if (GameManager.instance.isEnd && !isLock && !isPriceEnd)
        {
            // GameManager.instance.isEnd가 true이고, 상품이 잠긴 상태가 아니며
            // 가격 설정이 아직 되지 않은 경우에만 가격 설정을 합니다.
            PriceSetting();
            isPriceEnd = true;  // 가격 설정을 마쳤음을 표시합니다.
        }

        if (itemPrice > GameManager.instance.playerInfo.money)
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
    }

    public void BuyItem()
    {
        if (GameManager.instance.playerInfo.money >= itemPrice)
        {
            GameManager.instance.playerInfo.money -= (int)itemPrice;
            //ItemManager.instance.ItemObtain(itemCode);
            GameManager.instance.playerInfo.ItemObtain(itemCode);
            ItemManager.instance.ItemListUp();
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
    private void PriceSetting()
    {
        ItemBasePriceInfoTable.Data priceImport = GameManager.instance.gameDataBase.itemBasePriceInfoTable.table[itemNum];
        itemBasePrice = priceImport.itemBasePrice;
        int wave = StageManager.instance.waveLevel + 1;
        itemPrice = (itemBasePrice + wave + (itemBasePrice * 0.1f * wave)) * 1;
        itemPrice = itemPrice * ((100 + StageManager.instance.playerInfo.priceSale) / 100);
        itemPrice = MathF.Round(itemPrice);
    }
}
