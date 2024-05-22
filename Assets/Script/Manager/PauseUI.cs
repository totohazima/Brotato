using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour, UI_Upadte
{
    public static PauseUI instance;
    public GameObject pauseUI;
    public RectTransform statUI;
    public Text title;
    public GameObject returnMainMenu_UI;
    public GameObject restartMenu_UI;
    public GameObject selectTab;
    public GameObject[] tabs;
    public Text[] tabTitles;
    public GameObject[] underLines;

    public GameObject[] scrolls;
    public RectTransform[] scrollsRect;
    public Transform[] scrollContents;

    public Item_Object_Pause selectObj_Item;
    public Weapon_Object_Pause selectObj_Weapon;

    public enum tabName
    {
        WeaponTab = 0,
        ItemTab = 1,
        GiftCardTab = 2,
    }
    void Awake()
    {
        instance = this;
        scrollsRect = new RectTransform[scrolls.Length];
        for (int i = 0; i < scrolls.Length; i++)
        {
            scrollsRect[i] = scrolls[i].GetComponent<RectTransform>();
        }
    }
    void OnEnable()
    {
        UIUpdateManager.uiUpdates.Add(this);
        ItemManager.instance.WeaponListUp();
        ItemManager.instance.ItemListUp();
    }
    void OnDisable()
    {
        UIUpdateManager.uiUpdates.Remove(this);
    }
    public void UI_Update()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabTitles[i].color = Color.gray;
            underLines[i].SetActive(false);
            scrolls[i].SetActive(false);
        }

        for (int i = 0; i < tabs.Length; i++)
        {
            if(selectTab == tabs[i])
            {
                tabTitles[i].color = Color.white;
                underLines[i].SetActive(true);
                scrolls[i].SetActive(true);
            }
        };

        if(selectObj_Item != null)
        {
            selectObj_Item.selectImage.SetActive(true);
        }
        if (selectObj_Weapon != null)
        {
            selectObj_Weapon.selectImage.SetActive(true);
        }
        UIVisualize();
    }

    void UIVisualize()
    {
        title.text = "위험 " + GameManager.instance.difficult_Level;
        tabTitles[(int)tabName.WeaponTab].text = "무기(" + scrollContents[0].childCount + "/" + GameManager.instance.maxWeaponCount +")";
        tabTitles[(int)tabName.ItemTab].text = "아이템(" + scrollContents[1].childCount + ")";
    }

    public void WeaponTab_VIew()
    {
        selectTab = tabs[0];
    }
    public void ItemTab_VIew()
    {
        selectTab = tabs[1];
    }
    public void CardTab_VIew()
    {
        selectTab = tabs[2];
    }
    public void GamePause()
    {
        pauseUI.SetActive(true);
        GameManager.instance.isPause = true;
        statUI.anchoredPosition = new Vector3(100, 0, 0);
    }
    public void ReStartUI_On()
    {
        restartMenu_UI.SetActive(true);
        StageManager.instance.pauseUI.SetActive(false);
        StageManager.instance.StatUI_Off();
    }
    public void Option_On()
    {
        GameManager.instance.optionUI.transform.SetParent(StageManager.instance.ui_Canvas);
        GameManager.instance.optionUI.transform.localScale = new Vector3(1, 1, 1);
        RectTransform rect = GameManager.instance.optionUI.GetComponent<RectTransform>();
        rect.offsetMax = Vector3.zero;
        rect.offsetMin = Vector3.zero;
        GameManager.instance.optionUI.SetActive(true);
    }
    public void ReturnUI_On()
    {
        returnMainMenu_UI.SetActive(true);
        StageManager.instance.pauseUI.SetActive(false);
        StageManager.instance.StatUI_Off();
    }
    public void GameProgress()
    {
        pauseUI.SetActive(false);
        GameManager.instance.isPause = false;
        statUI.anchoredPosition = new Vector3(-100, 0, 0);
    }

   
}
