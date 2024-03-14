using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// ��ũ�ѿ� ����ִ� �����۵��� RayCastTarget�� ���� ��ũ�� �۵��� ���� ���� �ذ��ϴ� ��ũ��Ʈ
/// ��ư�� ���� event�� ��ũ�ѿ��� �ѱ��.
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
