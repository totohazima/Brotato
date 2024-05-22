using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Object_Pause : Item, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, UI_Upadte
{
    public GameObject selectImage;
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
            if(infoObj != null)
            {
                if (infoObj.masterItem != null)
                {
                    item_Info.transform.SetParent(infoObj.masterItem);
                }
                infoObj.gameObject.SetActive(false);
                //Destroy(infoObj_Pause.gameObject);
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
            item_Info.transform.SetParent(infoObj.masterItem);
            item_Info.gameObject.SetActive(false);
            //Destroy(infoObj_Pause.gameObject);
        }
    }
    public override void ShowItemInfo()
    {
        infoObj.Init(scriptable, transform.position);
        infoObj.masterItem = info;

        item_Info.SetActive(true);
        ForceRebuildLayouts(infoRect, infoObj.bgRect);

        // ������Ʈ�� UI �ȿ��� ���� �ִ��� �Ʒ��� �ִ��� üũ
        Vector3 worldPos = myRect.TransformPoint(myRect.anchoredPosition);
        Vector3 pauseLocalPos = PauseUI.instance.scrollsRect[(int)PauseUI.tabName.ItemTab].InverseTransformPoint(worldPos);

        // ĵ���� �� ��ǥ���� 0 ������ ���
        if (pauseLocalPos.y <= PauseUI.instance.scrollsRect[(int)PauseUI.tabName.ItemTab].anchoredPosition.y)
        {
            ForceRebuildLayouts(infoRect, infoObj.bgRect);
            infoRect.offsetMax = originInfo_OffsetMax;
            float calcY = infoObj.itemInfoUI_Rect.anchoredPosition.y - infoObj.originItemInfo_PosY;
            float top = -infoRect.offsetMax.y + calcY;
            infoRect.offsetMax = new Vector2(0, -top);
        }
        else
        {
            ForceRebuildLayouts(infoRect, infoObj.bgRect);
            float heightPos = infoObj.bgRect.rect.height;
            infoRect.offsetMax = new Vector2(0, -heightPos);
        }

        item_Info.transform.SetParent(StageManager.instance.itemInfoManager);
    }

    //public override void ShowItemInfo()
    //{
    //    infoObj.Init(scriptable, transform.position);
    //    infoObj.masterItem = info;

    //    item_Info.SetActive(true);
    //    LayoutRebuilder.ForceRebuildLayoutImmediate(infoRect);
    //    LayoutRebuilder.ForceRebuildLayoutImmediate(infoObj.bgRect);

    //    //������Ʈ�� UI�ȿ��� ���� �ִ��� �Ʒ��� �ִ��� üũ
    //    Vector3 worldPos = myRect.TransformPoint(myRect.anchoredPosition);
    //    Vector3 pauseLocalPos = PauseUI.instance.scrollsRect[(int)PauseUI.tabName.ItemTab].InverseTransformPoint(worldPos);

    //    // ĵ���� �� ��ǥ���� 0 ������ ���
    //    if (pauseLocalPos.y <= PauseUI.instance.scrollsRect[(int)PauseUI.tabName.ItemTab].anchoredPosition.y)
    //    {
    //        //y���� ������ ItemInfo�� �� �´� ��ġ�� ��ȯ�ǰ� ��
    //        LayoutRebuilder.ForceRebuildLayoutImmediate(infoRect);
    //        LayoutRebuilder.ForceRebuildLayoutImmediate(infoObj.bgRect);
    //        infoRect.offsetMax = originInfo_OffsetMax;
    //        float calcY = infoObj.itemInfoUI_Rect.anchoredPosition.y - infoObj.originItemInfo_PosY; //(0, -50)
    //        float top = -infoRect.offsetMax.y/*(0, -40)*/ + calcY;
    //        infoRect.offsetMax = new Vector2(0, -top);
    //    }
    //    else
    //    {
    //        //y���� ������ ItemInfo�� �� �´� ��ġ�� ��ȯ�ǰ� ��
    //        LayoutRebuilder.ForceRebuildLayoutImmediate(infoRect);
    //        LayoutRebuilder.ForceRebuildLayoutImmediate(infoObj.bgRect);
    //        float heightPos = infoObj.bgRect.rect.height;
    //        infoRect.offsetMax = new Vector2(0, -heightPos);
    //    }
    //    item_Info.transform.SetParent(StageManager.instance.itemInfoManager);
    //}
}
