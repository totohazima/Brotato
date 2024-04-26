using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Info : MonoBehaviour
{
    public string itemCode;
    public Image itemImage;
    public Text itemName;
    public Transform itemInfoUI;
    public Text itemCountType;
    private int itemInfoCount;
    private string[] itemInfo;
    public TextMeshProUGUI[] infoText;
    private int maxCount;
    [HideInInspector]
    //public int itemNum; //itemManager에서 아이템을 찾기 위함
    ItemScrip scriptable;
    public RectTransform itemInfoUI_Rect;
    Vector3 itemPos;
    List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();

    void OnDisable()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            Destroy(texts[i].gameObject);
        }
        texts.Clear();
    }
    public void Init(ItemScrip scrip, Vector3 pos)
    {
        scriptable = scrip;
        itemCode = scriptable.itemCode.ToString();
        itemName.text = scriptable.itemName;
        itemImage.sprite = scriptable.itemSprite;
        //itemNum = index;
        itemPos = pos;
        TextSetting(itemCode);
        //StartCoroutine(TextSetting(itemCode));
    }

    public void TextSetting(string code)
    {
        ItemStatInfoTable.Data import = GameManager.instance.gameDataBase.itemStatInfoTable.table[(int)scriptable.itemCode];

        //int index1 = 0;
        //for (int i = 0; i < import.itemCode.Length; i++)
        //{
        //    if (code == import.itemCode[i])
        //    {
        //        index1 = i;
        //    }
        //}
        //itemName.text = import.itemName[index1];

        maxCount = import.itemMaxCount;

        //int index2 = 0;
        //for (int i = 0; i < import.itemCode2.Length; i++)
        //{
        //    if (code == import.itemCode2[i])
        //    {
        //        index2 = i;
        //    }
        //}

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
            text.text = scriptable.infoText[i];
            texts.Add(text);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(itemInfoUI_Rect);
        float textHeight = itemInfoUI_Rect.rect.height;
        float x = 0;
        float y = 0;
        if (Camera.main.ScreenToWorldPoint(itemPos).x >= 0)
        {
            x = itemPos.x - 200;
        }
        else if (Camera.main.ScreenToWorldPoint(itemPos).x < 0)
        {
            x = itemPos.x + 200;
        }

        if (Camera.main.ScreenToWorldPoint(itemPos).y >= 0)
        {
            y = itemPos.y - 200 - textHeight;
        }
        else if (Camera.main.ScreenToWorldPoint(itemPos).y < 0)
        {
            y = itemPos.y + 200 + textHeight;

        }
        transform.position = new Vector3(x, y);
    }
}
