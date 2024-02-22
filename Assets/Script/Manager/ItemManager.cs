using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public Item[] items;
    public Item invenItem; //���� ��� ������ ������ ������Ʈ
    List<GameObject>[] poolItems;
    GameManager game;
    void Awake()
    {
        instance = this;
        game = GameManager.instance;
        poolItems = new List<GameObject>[items.Length];

        for (int i = 0; i < poolItems.Length; i++)
        {
            poolItems[i] = new List<GameObject>();
        }
    }
    void Update()
    {
        
    }
    public void ItemObtain(int index)
    {
        bool isGet = false;
        Item getItem = items[index];
        Item checkItem = null;

        for (int i = 0; i < game.playerInfo.itemInventory.Count; i++)
        {
            Item Item = game.playerInfo.itemInventory[i];
            if (getItem.itemType == Item.itemType)
            {
                checkItem = Item;
                isGet = true;
            }
        }

        
        if (isGet == false)
        {
            Item item = items[index];
            GameObject objItem = Instantiate(invenItem.gameObject);
            objItem.transform.SetParent(transform);
            Item invenItems = objItem.GetComponent<Item>();
            invenItems.Init(item.itemType, item.GetComponent<SpriteRenderer>().sprite);
            invenItems.curCount++;
            game.playerInfo.itemInventory.Add(invenItems);
            GameManager.instance.playerInfo.StatCalculate();
        }
        else if(isGet == true)
        {
            checkItem.curCount++;
        }
    }

    public void ItemListUp(Transform trans) 
    {
        
        for (int i = 0; i < trans.childCount; i++)
        {
            GameObject item = trans.GetChild(i).gameObject;
            Destroy(item);
        }

        List<Item> inventory = game.playerInfo.itemInventory;
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject item = Instantiate(inventory[i].gameObject);
            item.transform.SetParent(trans);
        }
    }

    //public GameObject Get(int index)
    //{
    //    GameObject select = null;

    //    //������ Ǯ�� ����ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����

    //    foreach (GameObject item in poolItems[index])
    //    {
    //        if (!item.gameObject.activeSelf)
    //        {
    //            //�߰��ϸ� select ������ �Ҵ�
    //            select = item;
    //            select.SetActive(true);
    //            break;
    //        }
    //    }
    //    //�� ã������
    //    if (!select)
    //    {
    //        //���Ӱ� �����ϰ� select�� �Ҵ�
    //        select = Instantiate(items[index].gameObject, transform);
    //        poolItems[index].Add(select);
    //    }
    //    return select;
    //}
}

