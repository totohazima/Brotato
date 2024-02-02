using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static JoyStick instance;

    public Transform moveTarget;
    public GameObject joyStick;
    public GameObject stick;
    public bool isMove;

    Transform joyTrans;
    Transform stickTrans;
    Vector3 startPos, endPos;
    GameManager game;
    void Awake()
    {
        instance = this;
        game = GameManager.instance;
        joyTrans = joyStick.transform;
        stickTrans = stick.transform;
        joyStick.SetActive(false);
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

        startPos = game.mainPlayer.transform.position;
        endPos = Camera.main.ScreenToWorldPoint(stickTrans.position);

        float a = GetAngle(startPos, endPos);
        Vector3 moveTargetPos = ConvertAngleToVector(a);
        moveTarget.position = new Vector3(moveTargetPos.x + startPos.x, moveTargetPos.y + startPos.y, 0f);

        ///<summary>
        ///특정 오브젝트가 좌표에서 원형으로 벗어나지 못하게 하는 코드
        ///</summary>
        float radius = 100f;
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

    float GetAngle(Vector2 start, Vector2 end)//각도구하기
    {
        Vector2 vectorPos = end - start;
        return Mathf.Atan2(vectorPos.y, vectorPos.x) * Mathf.Rad2Deg;
    }

    Vector3 ConvertAngleToVector(float _deg)//각도로 좌표 구하기
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * 100f, Mathf.Sin(rad) * 100f, 2);
    }
}
