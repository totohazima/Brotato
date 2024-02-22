using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public Text titleWaveUI;
    public Text moneyNumUI;
    public Text nextWaveUI;
    public Transform[] tabs;
    public Image[] tabsImage;
    public Text[] tabsText;
    public Transform[] tabsScroll;
    public Transform goodsContent;
    public GameObject[] goods;

    public List<GameObject> lockList; //잠긴 아이템(가격은 고정된채로 가장 앞 열로 이동)
    public List<GameObject> goodsList;
    public List<int> maxItemList;
    List<GameObject>[] list;
    GameManager game;
    ItemManager item;
    bool isReRoll;
    void Awake()
    {
        instance = this;
        game = GameManager.instance;
        item = ItemManager.instance;

        list = new List<GameObject>[goods.Length];

        for (int i = 0; i < list.Length; i++)
        {
            list[i] = new List<GameObject>();
        }
    }

    void Update()
    {
        UiVisualize();
    }


    void UiVisualize()
    {
        titleWaveUI.text = "웨이브 " + (game.waveLevel + 1).ToString();
        moneyNumUI.text = game.Money.ToString();
        nextWaveUI.text = "이동(웨이브 " + game.waveLevel + 2 + ")";

        tabsText[0].text = "무기(" + tabsScroll[0].childCount + "/6)";
        tabsText[1].text = "아이템(" + tabsScroll[1].childCount + ")";
        tabsText[2].text = "적(" + tabsScroll[2].childCount + ")";
        tabsText[3].text = "천부 카드";
    }
    public void ShopGoodsSetting()
    {
        int index = 0;
        int[] indexes = new int[5];
        for (int i = 0; i < lockList.Count; i++) //잠금 아이템 
        {
            goodsList.Add(lockList[i]);
        }

        while (true) 
        {
            bool isNot = false;
            for (int i = 0; i < indexes.Length; i++) //번호를 뽑고 해당 번호 아이템이 리스트에 들어간 경우 다시 뽑는다.(아이템 최대치 이기때문)
            {
                index = Random.Range(0, item.items.Length);
                indexes[i] = index;
                for (int j = 0; j < maxItemList.Count; j++)
                {
                    if (maxItemList[j] == index)
                    {
                        isNot = true;
                    }
                }
            }

            for (int i = 0; i < indexes.Length; i++) //5개 번호가 서로 중복 되는지 체크
            {
                for (int j = 0; j < indexes.Length; j++)
                {
                    if (i != j) //같지 않을 때
                    {
                        if (indexes[i] == indexes[j])
                        {
                            isNot = true;
                        }
                    }
                }
            }

            if (isNot == false)
            {
                break;
            }
        }



        for (int i = 0 + lockList.Count; i < 5; i++)
        {
            Item items = item.items[indexes[i]];

            GameObject product = Get(0);
            ItemGoods itemGoods = product.GetComponent<ItemGoods>();
            itemGoods.Init(items.itemType.ToString(), items.itemSprite.sprite, indexes[i]);

            itemGoods.transform.SetParent(goodsContent);
            
            goodsList.Add(itemGoods.gameObject);
        }

        isReRoll = false;
    }

    public void ShopReRoll()
    {
        if (isReRoll == false)
        {
            isReRoll = true;
            for (int i = 0 + lockList.Count; i < 5 ; i++)
            {
                goodsList[i].SetActive(false);
            }
            goodsList.Clear();
            ShopGoodsSetting();
        }
    }
    public void ShopClose()
    {
        game.nextWave();
        gameObject.SetActive(false);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //선택한 풀의 놀고있는(비활성화 된) 게임오브젝트 접근

        foreach (GameObject item in list[index])
        {
            if (!item.activeSelf)
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
            select = Instantiate(goods[index], transform);
            list[index].Add(select);
        }
        return select;
    }

}
