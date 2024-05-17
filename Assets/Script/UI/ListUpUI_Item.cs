using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUpUI_Item : MonoBehaviour
{
    public static ListUpUI_Item instance;

    [SerializeField] private Item poolObject;
    public List<Item> poolingObject;
    Queue<Item> poolingObjectQueue = new Queue<Item>();


    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// 상점 등에서 쓸 오브젝트 풀링
    /// </summary>
    private Item CreateNewObject()
    {
        var newObj = Instantiate(poolObject);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        poolingObject.Add(newObj);
        return newObj;
    }

    public static Item GetItemObj()
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

    public static void ReturnObject(Item obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolingObjectQueue.Enqueue(obj);
    }

    /// <summary>
    /// 일시정지 등에서 쓸 오브젝트 풀링
    /// </summary>

    [SerializeField] private Item_Object_Pause poolObject_Pause;
    public List<Item_Object_Pause> poolingObject_Pause;
    Queue<Item_Object_Pause> poolingObjectQueue_Pause = new Queue<Item_Object_Pause>();

    private Item_Object_Pause CreateNewObject_Pause()
    {
        var newObj = Instantiate(poolObject_Pause);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        poolingObject_Pause.Add(newObj);
        return newObj;
    }

    public static Item_Object_Pause GetItemObj_Pause()
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

    public static void ReturnObject_Pause(Item_Object_Pause obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolingObjectQueue_Pause.Enqueue(obj);
    }
}
