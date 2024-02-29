using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTab : MonoBehaviour
{
    public TabType type;
    public Transform tabContent;
    public Transform verticalList;
    ShopManager shop;
    public enum TabType
    {
        WEAPON_TAB,
        ITEM_TAB,
        GIFTCARD_TAB,
    }
 
    public void TabClicked()
    {
        shop = ShopManager.instance;
        for (int i = 0; i < shop.tabs.Length; i++) //탭 클릭 시 전원 false
        {
            shop.tabs[i].gameObject.SetActive(false);
            shop.tabsImage[i].color = new Color((30/255f), (30 / 255f), (30 / 255f));
            shop.tabsText[i].color = new Color(1f, 1f, 1f);
        }

        switch(type) //지정된 type에 따라 특정 탭만 true시킴
        {
            case TabType.WEAPON_TAB:
                shop.tabs[(int)TabType.WEAPON_TAB].gameObject.SetActive(true);
                shop.selectTab = shop.tabs[(int)TabType.WEAPON_TAB];
                shop.tabsImage[(int)TabType.WEAPON_TAB].color = new Color((210/255f), (210 / 255f), (210 / 255f));
                shop.tabsText[(int)TabType.WEAPON_TAB].color = new Color(0f, 0f, 0f);
                break;
            case TabType.ITEM_TAB:
                shop.tabs[(int)TabType.ITEM_TAB].gameObject.SetActive(true);
                shop.selectTab = shop.tabs[(int)TabType.ITEM_TAB];
                shop.tabsImage[(int)TabType.ITEM_TAB].color = new Color((210 / 255f), (210 / 255f), (210 / 255f));
                shop.tabsText[(int)TabType.ITEM_TAB].color = new Color(0f, 0f, 0f);
                break;
            case TabType.GIFTCARD_TAB:
                shop.tabs[(int)TabType.GIFTCARD_TAB].gameObject.SetActive(true);
                shop.selectTab = shop.tabs[(int)TabType.GIFTCARD_TAB];
                shop.tabsImage[(int)TabType.GIFTCARD_TAB].color = new Color((210 / 255f), (210 / 255f), (210 / 255f));
                shop.tabsText[(int)TabType.GIFTCARD_TAB].color = new Color(0f, 0f, 0f);
                break;
        }    
    }

    public void verticalListOn()
    {
        verticalList.gameObject.SetActive(true);
    }
    public void verticalListOff()
    {
        verticalList.gameObject.SetActive(false);
    }

}
