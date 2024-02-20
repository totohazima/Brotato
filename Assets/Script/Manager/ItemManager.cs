using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public Item[] items;

    List<GameObject>[] poolItems;
    void Awake()
    {
        instance = this;
        poolItems = new List<GameObject>[items.Length];

        for (int i = 0; i < poolItems.Length; i++)
        {
            poolItems[i] = new List<GameObject>();
        }
        Invoke("ItemObtain", 2f);
    }

    void ItemObtain()
    {
        ///test
        for (int i = 0; i < 5; i++)
        {
            GameObject item = Get(33);
            item.transform.position = Vector3.zero;
            GameManager.instance.playerInfo.items.Add(item.GetComponent<Item>());
            GameManager.instance.playerInfo.StatCalculate();
        }
        ///
    }

    

    public GameObject Get(int index)
    {
        GameObject select = null;

        //선택한 풀의 놀고있는(비활성화 된) 게임오브젝트 접근

        foreach (GameObject item in poolItems[index])
        {
            if (!item.gameObject.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았으면
        if (!select)
        {
            //새롭게 생성하고 select에 할당
            select = Instantiate(items[index].gameObject, transform);
            poolItems[index].Add(select);
        }
        return select;
    }
}

