using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class Loot_In_Item : MonoBehaviour
{
    public string itemCode;
    public Image itemImage;
    public Text itemName;
    public Transform itemInfoUI;
    int maxCount;
    public Text itemCountType; //-100 아이템, 1초과 한계(0), 1 독특한
    int itemInfoCount; //텍스트 갯수
    string[] itemInfo; //아이템 텍스트
    public TextMeshProUGUI[] infoText; //텍스트 오브젝트
    [HideInInspector]
    public int itemNum; //itemManager에서 아이템을 찾기 위함
    ItemScrip scriptable;
    float itemBasePrice;
    public float recyclePrice;
    void OnDisable()
    {
        for (int i = 0; i < itemInfoUI.childCount; i++)
        {
            Destroy(itemInfoUI.GetChild(i).gameObject);
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

    public void TextSetting(string code)
    {
        ItemStatInfoTable.Data import = GameManager.instance.gameDataBase.itemStatInfoTable.table[itemNum];

        itemName.text = scriptable.itemName;
        maxCount = import.itemMaxCount;

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

        itemInfoCount = scriptable.infoText.Length;

        itemInfo = new string[itemInfoCount];

        for (int i = 0; i < itemInfoCount; i++)
        {
            TextMeshProUGUI text = Instantiate(infoText[0], itemInfoUI);
            itemInfo[i] = scriptable.infoText[i];
            text.text = itemInfo[i];
        }

        ItemBasePriceInfoTable.Data priceImporter = GameManager.instance.gameDataBase.itemBasePriceInfoTable.table[itemNum];

        int wave = StageManager.instance.waveLevel + 1;
        itemBasePrice = priceImporter.itemBasePrice;
        float itemPrice = (itemBasePrice + wave + (itemBasePrice * 0.1f * wave)) * 1;
        itemPrice = Mathf.Round(itemPrice);
        recyclePrice = (itemPrice * 0.25f);
        recyclePrice = Mathf.Round(recyclePrice);
    }


}
