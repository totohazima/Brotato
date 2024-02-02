using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    //������ ������ ����
    public GameObject[] prefabs;

    //Ǯ ������ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        instance = this;
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //������ Ǯ�� ����ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
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
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}
