using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Object_Pause : Item, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, UI_Upadte
{
    public GameObject selectImage;
    Item_Info infoObj_Pause;
    public override void OnEnable()
    {
        base.OnEnable();
        UIUpdateManager.uiUpdates.Add(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        UIUpdateManager.uiUpdates.Add(this);
    }

    public void UI_Update()
    {
        if (itemCount != null)
        {
            if (curCount <= 1)
            {
                itemCount.gameObject.SetActive(false);
            }
            else
            {
                itemCount.text = "x" + curCount;
                itemCount.gameObject.SetActive(true);
            }
        }

        if(PauseUI.instance.selectObj_Item == null)
        {
            if(infoObj_Pause != null)
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
        PauseUI.instance.selectObj_Item = this;
        ShowItemInfo();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selectImage.SetActive(false);
        PauseUI.instance.selectObj_Item = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(PauseUI.instance.selectObj_Item != null)
        {
            selectImage.SetActive(true);
            ShowItemInfo();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PauseUI.instance.selectObj_Item != null)
        {
            selectImage.SetActive(false);
            Destroy(infoObj_Pause.gameObject);
        }
    }

    public override void ShowItemInfo()
    {
        infoObj_Pause = Instantiate(itemInfo, StageManager.instance.itemInfoManager);
        infoObj_Pause.Init(scriptable, transform.position);
    }
}
