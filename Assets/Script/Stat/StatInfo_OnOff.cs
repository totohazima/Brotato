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
        Stat_Manager.instance.isShowInfo = true;
        Stat_Manager.instance.basic_Status_Info = stat_Info.gameObject;
        Stat_Manager.instance.basic_Status_InfoImage = image;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 40 / 255f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Stat_Manager.instance.isShowInfo = false;
        Stat_Manager.instance.basic_Status_Info.SetActive(false);
        Stat_Manager.instance.basic_Status_InfoImage.color = new Color(image.color.r, image.color.g, image.color.b, 0 / 255f);
        Stat_Manager.instance.basic_Status_Info = null;
        Stat_Manager.instance.basic_Status_InfoImage = null;
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Stat_Manager.instance.basic_Status_Info != null)
        {
            Stat_Manager.instance.basic_Status_Info.SetActive(false);
        }
        Stat_Manager.instance.basic_Status_Info = stat_Info.gameObject;
        Stat_Manager.instance.basic_Status_InfoImage = image;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 40 / 255f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        stat_Info.gameObject.SetActive(false);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0 / 255f);
    }

    
}
