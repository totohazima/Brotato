using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager instance;
    //프리펩 보관용 변수
    public Text[] prefabs;

    //풀 담당을하는 리스트들
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

        //선택한 풀의 놀고있는(비활성화 된) 게임오브젝트 접근

        foreach (Text item in pools[index])
        {
            if (!item.gameObject.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.gameObject.SetActive(true);
                select.text = text;
                break;
            }
        }
        //못 찾았으면
        if (!select)
        {
            //새롭게 생성하고 select에 할당
            select = Instantiate(prefabs[index], transform);
            select.text = text;
            pools[index].Add(select);
        }
        return select;
    }
}
