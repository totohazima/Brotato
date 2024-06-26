using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LootOpen_Manager : MonoBehaviour
{
    public RectTransform stats;
    public Transform itemPos;
    public GameObject[] goods;
    public TextMeshProUGUI recycle_NumUI;
    private Loot_In_Item loot_In_Item;
    List<GameObject>[] list;
    int[] itemIndex;
    int checkIndex;
    GameObject item;
    void Awake()
    {
        list = new List<GameObject>[goods.Length];

        for (int i = 0; i < list.Length; i++)
        {
            list[i] = new List<GameObject>();
        }
    }
    void OnEnable()
    {
        itemIndex = null;
        checkIndex = 0;
        StageManager.instance.StatUI_On();
        LootSetting();
    }
    private void LootSetting()
    {
        int count = GameManager.instance.playerInfo.lootChance;
        int[] indexes = new int[count];
        List<ItemScrip> unBanList = new List<ItemScrip>();

        for (int i = 0; i < GameManager.instance.playerInfo.itemGroup_Scriptable.items.Length; i++) //뽑을 아이템에 최대 수량에 도달한 아이템이 있는지 체크
        {
            unBanList.Add(GameManager.instance.playerInfo.itemGroup_Scriptable.items[i]);
            Item.ItemType type = GameManager.instance.playerInfo.itemGroup_Scriptable.items[i].itemCode;
        }

        for (int i = 0; i < unBanList.Count; i++)
        {
            for (int j = 0; j < ItemManager.instance.maxItemList.Count; j++)
            {
                if (unBanList[i].itemCode == ItemManager.instance.maxItemList[j])
                {
                    unBanList.Remove(unBanList[i]);
                }
            }
        }

        ShuffleList(unBanList);
        ItemScrip[] itemList = new ItemScrip[count];
        for (int i = 0; i < count; i++)
        {
            itemList[i] = unBanList[i];
        }
        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < GameManager.instance.playerInfo.itemGroup_Scriptable.items.Length; i++)
            {
                if (GameManager.instance.playerInfo.itemGroup_Scriptable.items[i] == itemList[j])
                {
                    indexes[j] = i;
                }
        }
        }


        itemIndex = new int[count];
        for (int i = 0; i < count; i++)
        {
            itemIndex[i] = indexes[i];
        }
        LootOpen();
    }
    private List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }
    private void LootOpen()
    {
        ItemScrip items = GameManager.instance.playerInfo.itemGroup_Scriptable.items[itemIndex[checkIndex]];
        GameObject product = Get(0);
        Loot_In_Item lootItem = product.GetComponent<Loot_In_Item>();
        loot_In_Item = lootItem;
        lootItem.Init(items, itemIndex[checkIndex]);

        lootItem.transform.SetParent(itemPos);
        RectTransform rects = lootItem.GetComponent<RectTransform>();
        rects.anchorMin = new Vector2(0.5f, 0.5f);
        rects.anchorMax = new Vector2(0.5f, 0.5f);
        rects.anchoredPosition = Vector3.zero;
        item = lootItem.gameObject;

        recycle_NumUI.text = "<sprite=0>+" + lootItem.recyclePrice.ToString("F0") + " 재활용";
        checkIndex++;
    }

    public void UseItem()
    {
        int i = checkIndex - 1;
        Item.ItemType itemType;
        itemType = GameManager.instance.playerInfo.itemGroup_Scriptable.items[itemIndex[i]].itemCode;
        //ItemManager.instance.ItemObtain(itemType);
        GameManager.instance.playerInfo.ItemObtain(itemType);
        item.SetActive(false);
        GameManager.instance.playerInfo.lootChance--;

        if (GameManager.instance.playerInfo.lootChance > 0)
        {
            LootOpen();
        }
        else
        {
            StageManager.instance.StatUI_Off();
            gameObject.SetActive(false);
            StageManager.instance.ShopOpen();
        }
    }
    public void SellItem()
    {
        item.SetActive(false);
        GameManager.instance.playerInfo.money += (int)loot_In_Item.recyclePrice;

        GameManager.instance.playerInfo.lootChance--;
        if (GameManager.instance.playerInfo.lootChance > 0)
        {
            LootOpen();
        }
        else
        {
            StageManager.instance.StatUI_Off();
            gameObject.SetActive(false);
            StageManager.instance.ShopOpen();
        }
    }
    public void AdMob()
    {
        return;
    }


    public GameObject Get(int index)
    {
        GameObject select = null;

        //선택한 풀의 놀고있는(비활성화 된) 게임오브젝트 접근

        foreach (GameObject item in list[index])
        {
            if (!item.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았으면
        if (!select)
        {
            //새롭게 생성하고 select에 할당
            select = Instantiate(goods[index], transform);
            list[index].Add(select);
        }
        return select;
    }

}
