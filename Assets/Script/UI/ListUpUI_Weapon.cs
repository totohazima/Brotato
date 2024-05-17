using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUpUI_Weapon : MonoBehaviour
{
    public static ListUpUI_Weapon instance;

    [SerializeField] private Weapon_Object poolObject;
    public List<Weapon_Object> poolingObject;
    Queue<Weapon_Object> poolingObjectQueue = new Queue<Weapon_Object>();


    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// 상점 등에서 쓸 오브젝트 풀링
    /// </summary>
    private Weapon_Object CreateNewObject()
    {
        var newObj = Instantiate(poolObject);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        poolingObject.Add(newObj);
        return newObj;
    }

    public static Weapon_Object GetWeaponObj()
    {
        if (instance.poolingObjectQueue.Count > 0)
        {
            var obj = instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Weapon_Object obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolingObjectQueue.Enqueue(obj);
    }

    /// <summary>
    /// 일시정지 등에서 쓸 오브젝트 풀링
    /// </summary>

    [SerializeField] private Weapon_Object_Pause poolObject_Pause;
    public List<Weapon_Object_Pause> poolingObject_Pause;
    Queue<Weapon_Object_Pause> poolingObjectQueue_Pause = new Queue<Weapon_Object_Pause>();

    private Weapon_Object_Pause CreateNewObject_Pause()
    {
        var newObj = Instantiate(poolObject_Pause);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        poolingObject_Pause.Add(newObj);
        return newObj;
    }

    public static Weapon_Object_Pause GetWeaponObj_Pause()
    {
        if (instance.poolingObjectQueue_Pause.Count > 0)
        {
            var obj = instance.poolingObjectQueue_Pause.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreateNewObject_Pause();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject_Pause(Weapon_Object_Pause obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolingObjectQueue_Pause.Enqueue(obj);
    }
}

