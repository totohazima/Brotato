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
    StageManager stage;
    void Awake()
    {
        instance = this;
        stage = StageManager.instance;
    }

    //public void ItemObtain(Item.ItemType itemType)
    //{
    //    bool isGet = false;
    //    ItemScrip getItem = null;
    //    int index = 0;
    //    //ItemScrip getItem = GameManager.instance.itemGroup_Scriptable.items[index];
    //    for (int i = 0; i < GameManager.instance.playerInfo.itemGroup_Scriptable.items.Length; i++)
    //    {
    //        if(GameManager.instance.playerInfo.itemGroup_Scriptable.items[i].itemCode == itemType)
    //        {
    //            index = i;
    //            getItem = GameManager.instance.playerInfo.itemGroup_Scriptable.items[i];
    //        }
    //    }
    //    if(getItem == null)
    //    {
    //        Debug.Log(itemType.ToString() + " 찾을 수 없음");
    //        return;
    //    }
        
    //    Item checkItem = null;

    //    for (int i = 0; i < GameManager.instance.playerInfo.itemInventory.Count; i++)
    //    {
    //        Item Item = GameManager.instance.playerInfo.itemInventory[i];
    //        if (getItem.itemCode == Item.itemType)
    //        {
    //            checkItem = Item;
    //            isGet = true;
    //        }
    //    }

        
    //    if (isGet == false)
    //    {
    //        ItemScrip item = GameManager.instance.playerInfo.itemGroup_Scriptable.items[index];
    //        GameObject objItem = Instantiate(invenItem.gameObject);
    //        objItem.transform.SetParent(transform);
    //        Item invenItems = objItem.GetComponent<Item>();
    //        invenItems.Init(item);
    //        invenItems.curCount++;
    //        GameManager.instance.playerInfo.itemInventory.Add(invenItems);

    //        GameManager.instance.player_Info.StatCalculate();
    //    }
    //    else if(isGet == true)
    //    {
    //        checkItem.curCount++;
    //        GameManager.instance.player_Info.StatCalculate();
    //    }
        
    //}
    public void WeaponListUp() 
    {
        for (int i = 0; i < ListUpUI_Weapon.instance.poolingObject.Count; i++)
        {
            ListUpUI_Weapon.ReturnObject(ListUpUI_Weapon.instance.poolingObject[i]);
        }
        for (int i = 0; i < ListUpUI_Weapon.instance.poolingObject_Pause.Count; i++)
        {
            ListUpUI_Weapon.ReturnObject_Pause(ListUpUI_Weapon.instance.poolingObject_Pause[i]);
        }
        
        List<GameObject> inventory = GameManager.instance.player_Info.weapons;
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            Weapon_Action info = inventory[i].GetComponent<Weapon_Action>();
            invenWeapon.weapon_Object = info;

            Weapon_Object weapon_Object1 = ListUpUI_Weapon.GetWeaponObj();
            weapon_Object1.weapon_Object = info;
            weapon_Object1.transform.SetParent(weaponScrollTrans[0]);
            weapon_Object1.transform.localScale = new Vector3(1, 1, 1);

            Weapon_Object weapon_Object2 = ListUpUI_Weapon.GetWeaponObj();
            weapon_Object2.weapon_Object = info;
            weapon_Object2.transform.SetParent(weaponScrollTrans[1]);
            weapon_Object2.transform.localScale = new Vector3(1, 1, 1);

            Weapon_Object_Pause weapon_ObjectPause = ListUpUI_Weapon.GetWeaponObj_Pause();
            weapon_ObjectPause.weapon_Object = info;
            weapon_ObjectPause.transform.SetParent(weaponScrollTrans[2]);
            weapon_ObjectPause.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void ItemListUp() 
    {
        //for (int i = 0; i < ListUpUI_Item.instance.poolingObject.Count; i++)
        //{
        //    ListUpUI_Item.ReturnObject(ListUpUI_Item.instance.poolingObject[i]);
        //}
        //for (int i = 0; i < ListUpUI_Item.instance.poolingObject_Pause.Count; i++)
        //{
        //    ListUpUI_Item.ReturnObject_Pause(ListUpUI_Item.instance.poolingObject_Pause[i]);
        //}
        //List<Item> inventory = GameManager.instance.player_Info.itemInventory;
        //for (int i = inventory.Count - 1; i >= 0; i--)
        //{
        //    Item item_Object = ListUpUI_Item.GetItemObj();
        //    item_Object.Init(inventory[i].scriptable);
        //    item_Object.transform.SetParent(itemScrollTrans[0]);
        //    item_Object.transform.localScale = new Vector3(1, 1, 1);

        //    Item item_Object2 = ListUpUI_Item.GetItemObj();
        //    item_Object2.Init(inventory[i].scriptable);
        //    item_Object2.transform.SetParent(itemScrollTrans[1]);
        //    item_Object2.transform.localScale = new Vector3(1, 1, 1);

        //    Item_Object_Pause item_ObjectPause = ListUpUI_Item.GetItemObj_Pause();
        //    item_ObjectPause.Init(inventory[i].scriptable);
        //    item_ObjectPause.transform.SetParent(itemScrollTrans[2]);
        //    item_ObjectPause.transform.localScale = new Vector3(1, 1, 1);
        //}
        ///이 부분 추후 위 함수처럼 수정해야 함
        for (int i = 0; i < itemScrollTrans[0].childCount; i++)
        {
            GameObject item = itemScrollTrans[0].GetChild(i).gameObject;
            Destroy(item);

            GameObject item2 = itemScrollTrans[1].GetChild(i).gameObject;
            Destroy(item2);

            GameObject item3 = itemScrollTrans[2].GetChild(i).gameObject;
            Destroy(item3);
        }

        List<Item> inventory = GameManager.instance.player_Info.itemInventory;
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

   
}

