using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Weapon_Object_Pause : Weapon_Object, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, UI_Upadte
{
    public GameObject selectImage;
    public override void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        UIUpdateManager.uiUpdates.Add(this);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        UIUpdateManager.uiUpdates.Remove(this);
    }
    public void UI_Update()
    {
        switch (weapon_Object.weaponTier)
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

        if (PauseUI.instance.selectObj_Weapon == null)
        {
            if (infoObj != null)
            {
                if (infoObj.masterItem != null)
                {
                    weapon_Info.transform.SetParent(infoObj.masterItem);
                }
                weapon_Info.SetActive(false);
            }
            if (selectImage != null)
            {
                selectImage.SetActive(false);
            }
        }
    }
    public override void CustomUpdate()
    {
        return;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        PauseUI.instance.selectObj_Weapon = this;
        ShowWeaponInfo();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selectImage.SetActive(false);
        PauseUI.instance.selectObj_Weapon = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PauseUI.instance.selectObj_Weapon != null)
        {
            selectImage.SetActive(true);
            ShowWeaponInfo();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PauseUI.instance.selectObj_Weapon != null)
        {
            selectImage.SetActive(false);
            weapon_Info.transform.SetParent(infoObj.masterItem);
            weapon_Info.SetActive(false);
        }
    }

    public override void ShowWeaponInfo()
    {
        infoObj.Init(weaponData[(int)weapon_Object.index], weapon_Object, transform.position, isCombined);
        infoObj.masterItem = info;

        weapon_Info.SetActive(true);
        //ũ�Ⱑ �ʰ� �����Ǵ� WeaponInfo�� ������ ������Ʈ�ǰ� ��
        ForceRebuildLayouts(infoObj.bgRect);
        AdjustWeaponInfoPosition();
    }

    public override void AdjustWeaponInfoPosition()
    {
        // ������Ʈ�� UI �ȿ��� ���� �ִ��� �Ʒ��� �ִ��� üũ
        Vector3 worldPos = myRect.TransformPoint(myRect.anchoredPosition);
        Vector3 pauseLocalPos = PauseUI.instance.scrollsRect[(int)PauseUI.tabName.WeaponTab].InverseTransformPoint(worldPos);

        //ĵ���� �� ��ǥ���� 0 ������ ���
        if (pauseLocalPos.y <= PauseUI.instance.scrollsRect[(int)PauseUI.tabName.WeaponTab].anchoredPosition.y)
        {
            //height ���� ������ WeaponInfo�� �� �´� ��ġ�� ��ȯ�ǰ� ��
            ForceRebuildLayouts(infoObj.bgRect);
            infoRect.offsetMax = originInfo_OffsetMax; //(0,213)
            float calcHeight = infoObj.originBG_Height/*(209.54f)*/ - infoObj.bgRect.rect.height;
            float top = infoRect.offsetMax.y - calcHeight;
            infoRect.offsetMax = new Vector2(0, top);
        }
        else
        {
            //height ���� ������ WeaponInfo�� �� �´� ��ġ�� ��ȯ�ǰ� ��
            ForceRebuildLayouts(infoObj.bgRect);
            float heightPos = myRect.rect.height;
            infoRect.offsetMax = new Vector2(0, -heightPos);
        }

        weapon_Info.transform.SetParent(StageManager.instance.itemInfoManager);
    }
}
