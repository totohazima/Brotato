using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// 스크롤에 들어있는 아이템들이 RayCastTarget을 통해 스크롤 작동을 막는 것을 해결하는 스크립트
/// 버튼이 받은 event를 스크롤에게 넘긴다.
/// </summary>
public class ScrollLinkage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    ScrollRect parentSR;
    public void OnBeginDrag(PointerEventData e)
    {
        parentSR = transform.parent.parent.parent.GetComponent<ScrollRect>();
        parentSR.OnBeginDrag(e);
    }
    public void OnDrag(PointerEventData e)
    {
        parentSR.OnDrag(e);
    }
    public void OnEndDrag(PointerEventData e)
    {
        parentSR.OnEndDrag(e);
    }
}
