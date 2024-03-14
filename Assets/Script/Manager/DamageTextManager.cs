using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager instance;
    //������ ������ ����
    public Text[] prefabs;

    //Ǯ ������ϴ� ����Ʈ��
    List<Text>[] pools;

    void Awake()
    {
        instance = this;
        pools = new List<Text>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<Text>();
        }
    }

    public Text TextCreate(int index, string text)
    {
        Text select = null;

        //������ Ǯ�� ����ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����

        foreach (Text item in pools[index])
        {
            if (!item.gameObject.activeSelf)
            {
                //�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.gameObject.SetActive(true);
                select.text = text;
                break;
            }
        }
        //�� ã������
        if (!select)
        {
            //���Ӱ� �����ϰ� select�� �Ҵ�
            select = Instantiate(prefabs[index], transform);
            select.text = text;
            pools[index].Add(select);
        }
        return select;
    }
}
