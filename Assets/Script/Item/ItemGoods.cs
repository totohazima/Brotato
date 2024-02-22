using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ItemGoods : MonoBehaviour
{
    public string itemCode;
    public Image itemImage;
    public Text itemName;
    public Transform itemInfoUI;
    int maxCount;
    public Text itemCountType; //-100 아이템, 1초과 한계(0), 1 독특한
    public int itemInfoCount;
    public string[] itemInfo; //아이템 텍스트
    public Text itemPrice;
    public Text[] infoText;
    public Image lockUI;
    public bool isLock;
    int itemNum; //itemManager에서 아이템을 찾기 위함
    public List<Text> texts;
    void OnDisable()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            Destroy(texts[i]);
        }
        texts.Clear();
    }
    public void Init(string code, Sprite image, int index)
    {
        itemCode = code;
        itemImage.sprite = image;
        itemNum = index;
        TextSetting(itemCode);
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
        itemName.text = import.itemName[index1];
        maxCount = import.maxCount[index1];

        int index2 = 0;
        for (int i = 0; i < import.itemCode2.Length; i++)
        {
            if (code == import.itemCode2[i])
            {
                index2 = i;
            }
        }
        
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

        itemInfoCount = import.infoCount[index2];

        itemInfo = new string[itemInfoCount];
        int j = 0;
        while (j <= itemInfoCount - 1)
        {
            itemInfo[j] = import.infoText[index2 + j];
            j++;
        }

        for (int i = 0; i < itemInfoCount; i++)
        {
            Text text = Instantiate(infoText[0]);
            text.text = itemInfo[i];
            text.transform.SetParent(itemInfoUI);
            texts.Add(text);
        }
    }
    
    public void BuyItem()
    {
        ItemManager.instance.ItemObtain(itemNum);
        ItemManager.instance.ItemListUp(ShopManager.instance.tabsScroll[1]);
        UnLockIng();
        ShopManager.instance.goodsList.Remove(gameObject);
        gameObject.SetActive(false);
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
        lockUI.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    }
    void UnLockIng()
    {
        ShopManager.instance.lockList.Remove(gameObject);
        isLock = false;
        lockUI.color = new Color(150 / 255f, 150 / 255f, 150 / 255f);
    }
}
