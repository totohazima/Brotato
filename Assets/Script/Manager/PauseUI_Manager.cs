using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI_Manager : MonoBehaviour, UI_Upadte
{
    public static PauseUI_Manager instance;
    public GameObject pauseUI;
    public RectTransform statUI;
    public Text title;


    public GameObject selectTab;
    public GameObject[] tabs;
    public Text[] tabTitles;
    public GameObject[] underLines;

    public GameObject[] scrolls;
    public Transform[] scrollContents;

    public Item_Object_Pause selectObj_Item;
    public Weapon_Object_Pause selectObj_Weapon;
    void Awake()
    {
        instance = this;
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
        title.text = "위험 " + StageManager.instance.difficult;
        tabTitles[0].text = "무기(" + scrollContents[0].childCount + "/6)";
        tabTitles[1].text = "아이템(" + scrollContents[1].childCount + ")";
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
        StageManager.instance.isPause = true;
        statUI.anchoredPosition = new Vector3(100, 0, 0);
    }

    public void GameProgress()
    {
        pauseUI.SetActive(false);
        StageManager.instance.isPause = false;
        statUI.anchoredPosition = new Vector3(-100, 0, 0);
    }

   
}
