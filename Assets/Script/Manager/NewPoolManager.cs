using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPoolManager : MonoBehaviour
{
    public static NewPoolManager instance;

    //[HideInInspector] public GameObject poolObject;
    public List<GameObject> poolingObject;
    [SerializeField] private Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();


    void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// 오브젝트 풀링
    /// </summary>
    private GameObject CreateNewObject(GameObject pool)
    {
        var newObj = Instantiate(pool);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        poolingObject.Add(newObj);
        return newObj;
    }

    public static GameObject GetObject(GameObject pool)
    {
        if (instance.poolingObjectQueue.Count > 0)
        {
            var obj = instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(instance.transform);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreateNewObject(pool);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(instance.transform);
            return newObj;
        }
    }

    public static void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.poolingObjectQueue.Enqueue(obj);
    }
}
