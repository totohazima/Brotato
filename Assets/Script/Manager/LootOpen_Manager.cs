using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOpen_Manager : MonoBehaviour
{
    public RectTransform stats;
    public Transform itemPos;
    public GameObject[] goods;
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
        LootSetting();
        stats.anchoredPosition = new Vector3(100, 0, 0);
    }
    private void LootSetting()
    {
        bool sameIndex = false;

        int count = GameManager.instance.lootChance;
        int[] index = new int[count];
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                index[i] = Random.Range(0, ItemManager.instance.items.Length);
            }

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if(i != j)
                    {
                        if(index[i] == index[j])
                        {
                            sameIndex = true;
                        }
                    }
                }
            }

            if(sameIndex == false)
            {
                break;
            }
        }

        itemIndex = new int[count];
        for (int i = 0; i < count; i++)
        {
            itemIndex[i] = index[i];
        }
        LootOpen();
    }

    private void LootOpen()
    {
        ItemScrip items = ItemManager.instance.items[itemIndex[checkIndex]];
        GameObject product = Get(0);
        Loot_In_Item lootItem = product.GetComponent<Loot_In_Item>();
        lootItem.Init(items, itemIndex[checkIndex]);

        lootItem.transform.SetParent(itemPos);
        RectTransform rects = lootItem.GetComponent<RectTransform>();
        rects.anchorMin = new Vector2(0.5f, 0.5f);
        rects.anchorMax = new Vector2(0.5f, 0.5f);
        rects.anchoredPosition = Vector3.zero;
        item = lootItem.gameObject;

        checkIndex++;
    }

    public void UseItem()
    {
        int i = checkIndex - 1;
        ItemManager.instance.ItemObtain(itemIndex[i]);
        //ItemManager.instance.ItemListUp(ShopManager.instance.tabsScroll[1], ShopManager.instance.verticalTabsScroll[1]);
        item.SetActive(false);
        GameManager.instance.lootChance--;

        if (GameManager.instance.lootChance > 0)
        {
            LootOpen();
        }
        else
        {
            stats.anchoredPosition = new Vector3(-100, 0, 0);
            gameObject.SetActive(false);
            GameManager.instance.ShopOpen();
        }
    }
    public void SellItem()
    {
        item.SetActive(false);
        GameManager.instance.lootChance--;

        if (GameManager.instance.lootChance > 0)
        {
            LootOpen();
        }
        else
        {
            stats.anchoredPosition = new Vector3(-100, 0, 0);
            gameObject.SetActive(false);
            GameManager.instance.ShopOpen();
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
