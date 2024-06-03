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

    float radio = 120;
    Transform joyTrans;
    Transform stickTrans;
    [SerializeField] RectTransform stickRect;
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
        if(GameManager.instance.isEnd == true )
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
        ///<summary>
        ///특정 오브젝트가 좌표에서 원형으로 벗어나지 못하게 하는 코드
        ///</summary>
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
            return (stickRect.position.x - DeathArea.x) / radio;
        }
    }

    /// <summary>
    /// Value Vertical of the Joystick
    /// Get this for get the vertical value of joystick
    /// </summary>
    public float Vertical
    {
        get
        {
            return (stickRect.position.y - DeathArea.y) / radio;
        }
    }

}
