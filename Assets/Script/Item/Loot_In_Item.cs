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
    public Text itemCountType; //-100 ������, 1�ʰ� �Ѱ�(0), 1 ��Ư��
    int itemInfoCount; //�ؽ�Ʈ ����
    string[] itemInfo; //������ �ؽ�Ʈ
    public TextMeshProUGUI[] infoText; //�ؽ�Ʈ ������Ʈ
    [HideInInspector]
    public int itemNum; //itemManager���� �������� ã�� ����
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
            itemCountType.text = "������";
        }
        else if (maxCount > 1)
        {
            itemCountType.text = "�Ѱ�(" + maxCount + ")";
        }
        else if (maxCount == 1)
        {
            itemCountType.text = "��Ư��";
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
