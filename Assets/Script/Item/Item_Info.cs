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
    public TextMeshProUGUI[] infoText;
    public RectTransform itemInfoUI_Rect;
    public RectTransform bgRect;
    public Transform masterItem;
    [HideInInspector] public float originItemInfo_PosY;
    private int itemInfoCount;
    private string[] itemInfo;
    private int maxCount;
    Vector3 itemPos;
    List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    ItemScrip scriptable;
    void Awake()
    {
        originItemInfo_PosY = itemInfoUI_Rect.anchoredPosition.y;
    }
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
        itemImage.sprite = scriptable.itemSprite;
        itemPos = pos;
        TextSetting();
    }

    public void TextSetting()
    {
        ItemStatInfoTable.Data import = GameManager.instance.gameDataBase.itemStatInfoTable.table[(int)scriptable.itemCode];

        itemName.text = import.itemName;
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

        ItemTextInfoTable.Data textImporter = GameManager.instance.gameDataBase.ItemTextInfoTable.table[(int)scriptable.itemCode];
        itemInfoCount = textImporter.textCount;
        itemInfo = new string[itemInfoCount];
        for (int i = 0; i < itemInfoCount; i++)
        {
            TextMeshProUGUI text = Instantiate(infoText[0], itemInfoUI);
            text.text = textImporter.text[i];
            texts.Add(text);
        }
    }
}
