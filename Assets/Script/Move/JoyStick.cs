using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, ICustomUpdateMono
{
    public static JoyStick instance;

    private Vector3 DeathArea;
    [SerializeField] private RectTransform CenterReference;
    public Transform moveTarget;
    public GameObject joyStick;
    public GameObject stick;
    public bool isMove;

    [SerializeField] private float radio = 120;
    private Transform joyTrans;
    private Transform stickTrans;
    [SerializeField] private RectTransform stickRect;

    void Awake()
    {
        instance = this;
        joyTrans = joyStick.transform;
        stickTrans = stick.transform;
        joyStick.SetActive(false);
    }

    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }

    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
        stickTrans.position = joyTrans.position;
        joyStick.SetActive(false);
        isMove = false;
    }

    public void CustomUpdate()
    {
        if (GameManager.instance.isEnd)
        {
            stickTrans.position = joyTrans.position;
            joyStick.SetActive(false);
            isMove = false;
        }
        DeathArea = CenterReference.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joyStick.SetActive(true);
        joyTrans.position = eventData.pressPosition;
        stickTrans.position = joyTrans.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 dragPosition = new Vector3(eventData.position.x, eventData.position.y, 0);
        stickTrans.position = dragPosition;

        // 스틱이 원형 반경을 벗어나지 않도록 제한
        float radius = radio;
        Vector3 centerPosition = joyTrans.position;
        float distance = Vector3.Distance(stickTrans.position, centerPosition);
        if (distance > radius)
        {
            Vector3 fromorigintoobject = stickTrans.position - centerPosition;
            fromorigintoobject *= radius / distance;
            stickTrans.position = centerPosition + fromorigintoobject;
        }

        isMove = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stickTrans.position = joyTrans.position;
        joyStick.SetActive(false);
        isMove = false;
    }

    public float Horizontal
    {
        get
        {
            return Mathf.Clamp((stickRect.position.x - DeathArea.x) / radio, -1f, 1f);
        }
    }

    public float Vertical
    {
        get
        {
            return Mathf.Clamp((stickRect.position.y - DeathArea.y) / radio, -1f, 1f);
        }
    }
}

