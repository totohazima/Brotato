using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Object : MonoBehaviour, ICustomUpdateMono
{
    public Weapon_Action weapon_Object;
    public Image backGround;
    public Image weapon_Image;
    public GameObject combined_Mark;
    public WeaponScrip[] weaponData;
    public Transform info;
    public GameObject weapon_Info;
    [SerializeField]
    Outline frame;
    [SerializeField] List<Weapon_Action> weaponList = new List<Weapon_Action>();
    [SerializeField] bool isCombined;
    RectTransform myRect;
    RectTransform infoRect;
    Vector2 originInfo_OffsetMax;

    void Awake()
    {
        myRect = gameObject.GetComponent<RectTransform>();
        infoRect = info.GetComponent<RectTransform>();
        originInfo_OffsetMax = infoRect.offsetMax;
    }
    public virtual void OnEnable() //생성시 티어를 정한다 (현재 1티어만 존재)
    {
        CustomUpdateManager.customUpdates.Add(this);
        for (int i = 0; i < StageManager.instance.playerInfo.weapons.Count; i++)
        {
            weaponList.Add(StageManager.instance.playerInfo.weapons[i].GetComponent<Weapon_Action>());
        }
    }
    public virtual void OnDisable()
    {
        weaponList.Clear();
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public virtual void CustomUpdate()
    {
        switch(weapon_Object.weaponTier)
        {
            case 0:
                backGround.color = new Color(48 / 255f, 48 / 255f, 48 / 255f);
                break;
            case 1:
                backGround.color = new Color(80 / 255f, 120 / 255f, 150 / 255f);
                break;
            case 2:
                backGround.color = new Color(100 / 255f, 60 / 255f, 110 / 255f);
                break;
            case 3:
                backGround.color = new Color(125 / 255f, 60 / 255f, 60 / 255f);
                break;
        }
        weapon_Image.sprite = weaponData[(int)weapon_Object.index].weaponImage;

        for (int i = 0; i < weaponList.Count; i++)
        {
            Weapon_Action weapon = weaponList[i];
            if (weapon != weapon_Object)
            {
                if (weapon_Object.index == weapon.index && weapon_Object.weaponTier == weapon.weaponTier && weapon_Object.weaponTier < 3)
                {
                    //combined_Mark.SetActive(true);
                    isCombined = true;
                    break;
                }
            }
            //combined_Mark.SetActive(false);
            isCombined = false;
        }

        if(isCombined == true)
        {
            combined_Mark.SetActive(true);
        }
        else
        {
            combined_Mark.SetActive(false);
        }
    }
    Weapon_Info infoObj = null;
    public void PointDown()
    {
        frame.effectColor = Color.white;
        ShowWeaponInfo();
    }
    public void PointUp()
    {
        //Destroy(infoObj.gameObject);
        frame.effectColor = Color.black;
    }
    public void PointClick()
    {
        int itemCount = transform.parent.childCount;
        Outline[] line = new Outline[itemCount];
        Transform content = transform.parent;
        for (int i = 0; i < itemCount; i++)
        {
            line[i] = content.GetChild(i).GetComponent<Outline>();
            line[i].effectColor = Color.black;
        }
        frame.effectColor = Color.white;
    }
    public virtual void ShowWeaponInfo()//클릭 시 아이템 정보를 보여주는 용도
    { 
        infoObj = weapon_Info.GetComponent<Weapon_Info>();
        infoObj.Init(weaponData[(int)weapon_Object.index], weapon_Object, transform.position, isCombined);
        infoObj.masterItem = info;

        weapon_Info.SetActive(true);
        //크기가 늦게 조절되는 WeaponInfo를 강제로 업데이트되게 함
        ForceRebuildLayouts(infoObj.bgRect);
        AdjustWeaponInfoPosition();
    }

    public virtual void AdjustWeaponInfoPosition()
    {
        //캔버스 상 좌표에서 0 이하인 경우
        if (myRect.localPosition.y <= 0)
        {
            //height 값을 측정해 WeaponInfo가 딱 맞는 위치에 소환되게 함
            ForceRebuildLayouts(infoObj.bgRect);
            infoRect.offsetMax = originInfo_OffsetMax; //(0,213)
            float calcHeight = infoObj.originBG_Height/*(209.54f)*/ - infoObj.bgRect.rect.height;
            float top = infoRect.offsetMax.y - calcHeight;
            infoRect.offsetMax = new Vector2(0, top);
        }
        else
        {
            //height 값을 측정해 WeaponInfo가 딱 맞는 위치에 소환되게 함
            ForceRebuildLayouts(infoObj.bgRect);
            float heightPos = myRect.rect.height;
            infoRect.offsetMax = new Vector2(0, -heightPos);
        }

        weapon_Info.transform.SetParent(StageManager.instance.itemInfoManager);
    }
    public void ForceRebuildLayouts(params RectTransform[] rectTransforms)
    {
        foreach (var rectTransform in rectTransforms)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}
