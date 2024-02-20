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

        //������ Ǯ�� ����ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����

        foreach (GameObject item in poolItems[index])
        {
            if (!item.gameObject.activeSelf)
            {
                //�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //�� ã������
        if (!select)
        {
            //���Ӱ� �����ϰ� select�� �Ҵ�
            select = Instantiate(items[index].gameObject, transform);
            poolItems[index].Add(select);
        }
        return select;
    }
}

