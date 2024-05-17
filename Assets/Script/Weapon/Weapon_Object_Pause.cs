using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Weapon_Object_Pause : Weapon_Object, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, UI_Upadte
{
    public GameObject selectImage;
    Weapon_Info infoObj_Pause;
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
            if (infoObj_Pause != null)
            {
                Destroy(infoObj_Pause.gameObject);
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
        ShowItemInfo();
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
            ShowItemInfo();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PauseUI.instance.selectObj_Weapon != null)
        {
            selectImage.SetActive(false);
            Destroy(infoObj_Pause.gameObject);
        }
    }

    public override void ShowItemInfo()
    {
        infoObj_Pause = Instantiate(weapon_Info, StageManager.instance.itemInfoManager);
        infoObj_Pause.Init(weaponData[(int)weapon_Object.index], weapon_Object, transform.position, false);
    }
}
