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
    public int itemNum; //itemManager에서 아이템을 찾기 위함

    private RectTransform rectTrans;
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
    public void Init(string code, Sprite image, int index, Vector3 pos)
    {
        itemCode = code;
        itemImage.sprite = image;
        itemNum = index;
        itemPos = pos;
        StartCoroutine(TextSetting(itemCode));
    }

    public IEnumerator TextSetting(string code)
    {
        rectTrans = itemInfoUI.GetComponent<RectTransform>();
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
            TextMeshProUGUI text = Instantiate(infoText[0], itemInfoUI);
            text.text = itemInfo[i];
            texts.Add(text);
        }

        yield return new WaitForSeconds(0.01f);
        float x = 0;
        float y = 0;
        if (Camera.main.ScreenToWorldPoint(itemPos).x >= 0)
        {
            x = itemPos.x - 120;
        }
        else if(Camera.main.ScreenToWorldPoint(itemPos).x < 0)
        {
            x = itemPos.x + 120;
        }

        if (Camera.main.ScreenToWorldPoint(itemPos).y >= 0)
        {
            y = itemPos.y - 160 - rectTrans.rect.height;
        }
        else if (Camera.main.ScreenToWorldPoint(itemPos).y < 0)
        {
            y = itemPos.y + 160 + rectTrans.rect.height;
            
        }
        transform.position = new Vector3(x, y);
    }
}
