using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StatInfo_OnOff : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public Transform stat_Info;
    public Image image;

    public void OnPointerDown(PointerEventData eventData)
    {
        StatInfo_UI.instance.isShowInfo = true;
        StatInfo_UI.instance.basic_Status_Info = stat_Info.gameObject;
        StatInfo_UI.instance.basic_Status_InfoImage = image;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 40 / 255f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StatInfo_UI.instance.isShowInfo = false;
        StatInfo_UI.instance.basic_Status_Info.SetActive(false);
        StatInfo_UI.instance.basic_Status_InfoImage.color = new Color(image.color.r, image.color.g, image.color.b, 0 / 255f);
        StatInfo_UI.instance.basic_Status_Info = null;
        StatInfo_UI.instance.basic_Status_InfoImage = null;
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (StatInfo_UI.instance.basic_Status_Info != null)
        {
            StatInfo_UI.instance.basic_Status_Info.SetActive(false);
        }
        StatInfo_UI.instance.basic_Status_Info = stat_Info.gameObject;
        StatInfo_UI.instance.basic_Status_InfoImage = image;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 40 / 255f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        stat_Info.gameObject.SetActive(false);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0 / 255f);
    }

    
}
