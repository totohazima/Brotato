using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public Item invenItem; //상점 등에서 보여줄 아이템 오브젝트
    public Weapon_Object invenWeapon; //상점 등에서 보여줄 무기 오브젝트
    public Item_Object_Pause pauseItem; //일시정지 등에서 보여줄 아이템 오브젝트
    public Weapon_Object_Pause pauseWeapon; //일시정지 등에서 보여줄 무기 오브젝트
    public List<Item.ItemType> maxItemList;

    public Transform[] weaponScrollTrans;
    public Transform[] itemScrollTrans;

    List<GameObject>[] poolItems;

    StageManager stage;
    void Awake()
    {
        instance = this;
        stage = StageManager.instance;

    }

    public void ItemObtain(int index)
    {
        bool isGet = false;
        ItemScrip getItem = GameManager.instance.itemGroup_Scriptable.items[index];
        Item checkItem = null;

        for (int i = 0; i < stage.playerInfo.itemInventory.Count; i++)
        {
            Item Item = stage.playerInfo.itemInventory[i];
            if (getItem.itemCode == Item.itemType)
            {
                checkItem = Item;
                isGet = true;
            }
        }

        
        if (isGet == false)
        {
            ItemScrip item = GameManager.instance.itemGroup_Scriptable.items[index];
            GameObject objItem = Instantiate(invenItem.gameObject);
            objItem.transform.SetParent(transform);
            Item invenItems = objItem.GetComponent<Item>();
            invenItems.Init(item);
            invenItems.curCount++;
            stage.playerInfo.itemInventory.Add(invenItems);

            StageManager.instance.playerInfo.StatCalculate();
        }
        else if(isGet == true)
        {
            checkItem.curCount++;
            StageManager.instance.playerInfo.StatCalculate();
        }
        
    }
    public void WeaponListUp(/*Transform horizontalList, Transform verticalList, Transform pauseList*/) //pauList만 다른 아이템으로 교체
    {
        for (int i = 0; i < weaponScrollTrans[0].childCount; i++)
        {
            GameObject item = weaponScrollTrans[0].GetChild(i).gameObject;
            Destroy(item);

            GameObject item2 = weaponScrollTrans[1].GetChild(i).gameObject;
            Destroy(item2);

            GameObject item3 = weaponScrollTrans[2].GetChild(i).gameObject;
            Destroy(item3);
        }
        
        List<GameObject> inventory = stage.playerInfo.weapons;
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            Weapon_Action info = inventory[i].GetComponent<Weapon_Action>();
            invenWeapon.weapon_Object = info;
            Instantiate(invenWeapon.gameObject, weaponScrollTrans[0]);
            Instantiate(invenWeapon.gameObject, weaponScrollTrans[1]);

            Weapon_Object_Pause pause = pauseWeapon;
            pause.weapon_Object = info;
            Instantiate(pause.gameObject, weaponScrollTrans[2]);
        }
    }
    public void ItemListUp(/*Transform horizontalList, Transform verticalList, Transform pauseList*/) 
    {
        
        for (int i = 0; i < itemScrollTrans[0].childCount; i++)
        {
            GameObject item = itemScrollTrans[0].GetChild(i).gameObject;
            Destroy(item);

            GameObject item2 = itemScrollTrans[1].GetChild(i).gameObject;
            Destroy(item2);

            GameObject item3 = itemScrollTrans[2].GetChild(i).gameObject;
            Destroy(item3);
        }
         
        List<Item> inventory = stage.playerInfo.itemInventory;
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            Instantiate(inventory[i].gameObject, itemScrollTrans[0]);
            Instantiate(inventory[i].gameObject, itemScrollTrans[1]);

            Item_Object_Pause pause = pauseItem;
            pause.itemType = inventory[i].itemType;
            pause.curCount = inventory[i].curCount;
            pause.maxCount = inventory[i].maxCount;
            pause.Init(inventory[i].scriptable);
            Instantiate(pause.gameObject, itemScrollTrans[2]);

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

