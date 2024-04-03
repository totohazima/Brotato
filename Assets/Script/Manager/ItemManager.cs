using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public ItemScrip[] items;
    public Item invenItem; //���� ��� ������ ������ ������Ʈ
    public Weapon_Object invenWeapon; //���� ��� ������ ���� ������Ʈ
    public List<Item.ItemType> maxItemList;
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
        ItemScrip getItem = items[index];
        Item checkItem = null;

        for (int i = 0; i < game.playerInfo.itemInventory.Count; i++)
        {
            Item Item = game.playerInfo.itemInventory[i];
            if (getItem.itemCode == Item.itemType)
            {
                checkItem = Item;
                isGet = true;
            }
        }

        
        if (isGet == false)
        {
            ItemScrip item = items[index];
            GameObject objItem = Instantiate(invenItem.gameObject);
            objItem.transform.SetParent(transform);
            Item invenItems = objItem.GetComponent<Item>();
            invenItems.Init(item);
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
    public void WeaponListUp(Transform horizontalList, Transform verticalList)
    {
        for (int i = 0; i < horizontalList.childCount; i++)
        {
            GameObject item = horizontalList.GetChild(i).gameObject;
            Destroy(item);

            GameObject item2 = verticalList.GetChild(i).gameObject;
            Destroy(item2);
        }
        
        List<GameObject> inventory = game.playerInfo.weapons;
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            Weapon_Action info = inventory[i].GetComponent<Weapon_Action>();
            invenWeapon.weapon_Object = info;
            Instantiate(invenWeapon.gameObject, horizontalList);
            Instantiate(invenWeapon.gameObject, verticalList);
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

