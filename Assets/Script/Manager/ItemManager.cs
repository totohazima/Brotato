using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public Item[] items;
    public Item invenItem; //상점 등에서 보여줄 아이템 오브젝트
    List<GameObject>[] poolItems;
    GameManager game;
    void Awake()
    {
        instance = this;
        game = GameManager.instance;

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

            GameManager.instance.playerInfo.StatCalculate(invenItems);
        }
        else if(isGet == true)
        {
            checkItem.curCount++;
            GameManager.instance.playerInfo.StatCalculate(checkItem);
        }
        
    }

    public void ItemListUp(Transform horizontalList, Transform verticalList) 
    {
        
        for (int i = 0; i < horizontalList.childCount; i++)
        {
            GameObject item = horizontalList.GetChild(i).gameObject;
            Destroy(item);

            GameObject item2 = verticalList.GetChild(i).gameObject;
            Destroy(item2);
        }
         
        List<Item> inventory = game.playerInfo.itemInventory;
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            Instantiate(inventory[i].gameObject, horizontalList);
            Instantiate(inventory[i].gameObject, verticalList);
        }
    }

    //public GameObject Get(int index)
    //{
    //    GameObject select = null;

    //    //선택한 풀의 놀고있는(비활성화 된) 게임오브젝트 접근

    //    foreach (GameObject item in poolItems[index])
    //    {
    //        if (!item.gameObject.activeSelf)
    //        {
    //            //발견하면 select 변수에 할당
    //            select = item;
    //            select.SetActive(true);
    //            break;
    //        }
    //    }
    //    //못 찾았으면
    //    if (!select)
    //    {
    //        //새롭게 생성하고 select에 할당
    //        select = Instantiate(items[index].gameObject, transform);
    //        poolItems[index].Add(select);
    //    }
    //    return select;
    //}
}

