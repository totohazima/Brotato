using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatInfo_UI : MonoBehaviour, UI_Upadte
{
    public static StatInfo_UI instance;
    public RectTransform rect;
    [SerializeField]
    private Transform selectTab;
    public Image basicTab;
    public Text basicTitle;
    public Image detailTab;
    public Text detailTitle;
    public GameObject closeImage;
    public GameObject basicStatUI;
    public GameObject DetailStatUI;

    public Transform[] basic_Status;
    private Text[] basic_Status_Name;
    private Text[] basic_Status_Num;

    [HideInInspector] public GameObject basic_Status_Info;
    [HideInInspector] public Image basic_Status_InfoImage;
    [HideInInspector] public bool isShowInfo;

    public Transform[] detail_Status;
    private Text[] detail_Status_Name;
    private Text[] detail_Status_Num;

    Player player;
    void Awake()
    {
        instance = this;

        basic_Status_Name = new Text[basic_Status.Length];
        basic_Status_Num = new Text[basic_Status.Length];
        for (int i = 0; i < basic_Status.Length; i++)
        {
            basic_Status_Name[i] = basic_Status[i].GetChild(0).GetComponent<Text>();
            basic_Status_Num[i] = basic_Status[i].GetChild(1).GetComponent<Text>();
        }

        detail_Status_Name = new Text[detail_Status.Length];
        detail_Status_Num = new Text[detail_Status.Length];
        for (int i = 0; i < detail_Status.Length; i++)
        {
            detail_Status_Name[i] = detail_Status[i].GetChild(0).GetComponent<Text>();
            detail_Status_Num[i] = detail_Status[i].GetChild(1).GetComponent<Text>();
        }

        
    }

    void OnEnable()
    {
        UIUpdateManager.uiUpdates.Add(this);
    }
    void OnDisable()
    {
        UIUpdateManager.uiUpdates.Remove(this);
    }
    public void UI_Update()
    {
        if (isShowInfo == true)
        {
            basic_Status_Info.SetActive(true);
        }
        else
        {
            if (basic_Status_Info != null)
            {
                basic_Status_Info.SetActive(false);
            }
        }
        ViewBasicStats();
        ViewDetailStats();
        ViewTabs();
    }
    public void click()
    {
        Debug.Log("스탯 클릭 확인");
    }
    private void ViewTabs()
    {
        basicTab.color = Color.black;
        basicTitle.color = Color.white;
        detailTab.color = Color.black;
        detailTitle.color = Color.white;

        if(selectTab.gameObject == basicTab.gameObject)
        {
            basicTab.color = Color.white;
            basicTitle.color = Color.black;
        }
        else if(selectTab.gameObject == detailTab.gameObject)
        {
            detailTab.color = Color.white;
            detailTitle.color = Color.black;
        }
    }
    private void ViewBasicStats()
    {
        player = GameManager.instance.player_Info;

        if (player != null)
        {
            basic_Status_Num[0].text = (GameManager.instance.playerInfo.playerLevel + 1).ToString("F0");
            basic_Status_Num[1].text = player.maxHealth.ToString("F0");
            basic_Status_Num[2].text = player.regeneration.ToString("F0");
            basic_Status_Num[3].text = player.bloodSucking.ToString("F0");
            basic_Status_Num[4].text = player.persentDamage.ToString("F0");
            basic_Status_Num[5].text = player.meleeDamage.ToString("F0");
            basic_Status_Num[6].text = player.rangeDamage.ToString("F0");
            basic_Status_Num[7].text = player.elementalDamage.ToString("F0");
            basic_Status_Num[8].text = player.attackSpeed.ToString("F0");
            basic_Status_Num[9].text = player.criticalChance.ToString("F0");
            basic_Status_Num[10].text = player.engine.ToString("F0");
            basic_Status_Num[11].text = player.range.ToString("F0");
            basic_Status_Num[12].text = player.armor.ToString("F0");
            basic_Status_Num[13].text = player.evasion.ToString("F0");
            basic_Status_Num[14].text = player.accuracy.ToString("F0");
            basic_Status_Num[15].text = player.lucky.ToString("F0");
            basic_Status_Num[16].text = player.harvest.ToString("F0");
            basic_Status_Num[17].text = player.speed.ToString("F0");

            for (int i = 1; i < basic_Status.Length; i++)
            {
                if (int.Parse(basic_Status_Num[i].text) > 0)
                {
                    basic_Status_Name[i].color = new Color(72 / 255f, 255 / 255f, 82 / 255f); //#4CFF52 컬러
                    basic_Status_Num[i].color = new Color(72 / 255f, 255 / 255f, 82 / 255f);
                }
                else if (int.Parse(basic_Status_Num[i].text) < 0)
                {
                    basic_Status_Name[i].color = Color.red;
                    basic_Status_Num[i].color = Color.red;
                }
                else
                {
                    basic_Status_Name[i].color = Color.white;
                    basic_Status_Num[i].color = Color.white;
                }

            }
        }
    }
    private void ViewDetailStats()
    {
        player = StageManager.instance.playerInfo;
        //ItemEffect effect = ItemEffect.instance;

        detail_Status_Num[0].text = player.consumableHeal.ToString("F0");
        detail_Status_Num[1].text = player.meterialHeal.ToString("F0");
        detail_Status_Num[2].text = player.expGain.ToString("F0");
        detail_Status_Num[3].text = player.magnetRange.ToString("F0");
        detail_Status_Num[4].text = player.priceSale.ToString("F0");
        detail_Status_Num[5].text = player.explosiveDamage.ToString("F0");
        detail_Status_Num[6].text = player.explosiveSize.ToString("F0");
        detail_Status_Num[7].text = player.chain.ToString("F0");
        detail_Status_Num[8].text = player.penetrate.ToString("F0");
        detail_Status_Num[9].text = player.penetrateDamage.ToString("F0");
        detail_Status_Num[10].text = player.bossDamage.ToString("F0");
        detail_Status_Num[11].text = player.knockBack.ToString("F0");
        detail_Status_Num[12].text = player.doubleMeterial.ToString("F0");
        detail_Status_Num[13].text = player.lootInMeterial.ToString("F0");
        detail_Status_Num[14].text = player.freeReroll.ToString("F0");
        detail_Status_Num[15].text = player.tree.ToString("F0");
        detail_Status_Num[16].text = player.enemyAmount.ToString("F0");
        detail_Status_Num[17].text = player.enemySpeed.ToString("F0");

        for (int i = 0; i < detail_Status.Length; i++)
        {
            if (int.Parse(detail_Status_Num[i].text) > 0)
            {
                if (i == 4 || i == 17)
                {
                    detail_Status_Name[i].color = Color.red;
                    detail_Status_Num[i].color = Color.red;
                }
                else
                {
                    detail_Status_Name[i].color = new Color(72 / 255f, 255 / 255f, 82 / 255f); //#4CFF52 컬러
                    detail_Status_Num[i].color = new Color(72 / 255f, 255 / 255f, 82 / 255f);
                }
            }
            else if (int.Parse(detail_Status_Num[i].text) < 0)
            {
                if (i == 4 || i == 17)
                {
                    detail_Status_Name[i].color = new Color(72 / 255f, 255 / 255f, 82 / 255f); //#4CFF52 컬러
                    detail_Status_Num[i].color = new Color(72 / 255f, 255 / 255f, 82 / 255f);
                }
                else
                {
                    detail_Status_Name[i].color = Color.red;
                    detail_Status_Num[i].color = Color.red;
                }
            }
            else
            {
                detail_Status_Name[i].color = Color.white;
                detail_Status_Num[i].color = Color.white;
            }

        }
    }
    public void BasicTab_Select()
    {
        selectTab = basicTab.transform;
        basicStatUI.SetActive(true);
        DetailStatUI.SetActive(false);
    }
    public void DetailcTab_Select()
    {
        selectTab = detailTab.transform;
        basicStatUI.SetActive(false);
        DetailStatUI.SetActive(true);
    }

    public void StatOpen()
    {
        closeImage.SetActive(true);
        LeanTween.move(rect, new Vector3(100, 0, 0), 0.3f).setEase(LeanTweenType.easeOutSine);
    }

    public void StatClose()
    {
        closeImage.SetActive(false);
        LeanTween.move(rect, new Vector3(-100, 0, 0), 0.3f).setEase(LeanTweenType.easeOutSine);
    }
}
