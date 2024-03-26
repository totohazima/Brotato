using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour, ICustomUpdateMono
{
    public static ShopManager instance;

    public Text titleWaveUI;
    public Text moneyNumUI;
    public Text nextWaveUI;
    public Transform selectTab;
    public Transform[] tabs;
    public Image[] tabsImage;
    public Text[] tabsText;
    public Transform[] tabsScroll;
    public Transform[] verticalTabsScroll;
    public Transform goodsContent;
    public GameObject[] goods;

    public List<GameObject> lockList; //잠긴 아이템(가격은 고정된채로 가장 앞 열로 이동)
    public List<GameObject> goodsList;
    public List<Item.ItemType> maxItemList;
    List<GameObject>[] list;
    GameManager game;
    ItemManager item;
    public WeaponScrip[] weapon;
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

    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        ItemManager.instance.WeaponListUp(tabsScroll[0], verticalTabsScroll[0]);
        ItemManager.instance.ItemListUp(tabsScroll[1], verticalTabsScroll[1]);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        UiVisualize();
    }


    void UiVisualize()
    {
        titleWaveUI.text = "웨이브 " + (game.waveLevel + 1) + "(총 10 물결)";
        moneyNumUI.text = game.Money.ToString();
        nextWaveUI.text = "이동(웨이브 " + (game.waveLevel + 2) + ")";

        tabsText[0].text = "무기(" + tabsScroll[0].childCount + "/6)";
        tabsText[1].text = "아이템(" + tabsScroll[1].childCount + ")";
        tabsText[2].text = "천부 카드";
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
            for (int i = 0; i < indexes.Length; i++) //번호 뽑기
            {
                index = Random.Range(0, item.items.Length);
                indexes[i] = index;
            }

            for (int i = 0; i < indexes.Length; i++) //5개 번호 중에 최대 수량에 도달한 아이템이 있는지 체크
            {
                Item.ItemType type = ItemManager.instance.items[indexes[i]].itemCode;
                for (int j = 0; j < maxItemList.Count; j++)
                {
                    if (maxItemList[j] == type)
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

            float itemChance = 0.65f;
            float weaponChance = 0.35f;

            float[] chanceLise = { itemChance, weaponChance };
            int chanceIndex = game.Judgment(chanceLise);

            if (chanceIndex == 0)
            {
                ItemScrip items = item.items[indexes[i]];
                GameObject product = Get(0);
                ItemGoods itemGoods = product.GetComponent<ItemGoods>();
                itemGoods.Init(items, indexes[i]);

                itemGoods.transform.SetParent(goodsContent);

                goodsList.Add(itemGoods.gameObject);
            }
            else
            {
                int num = Random.Range(0, weapon.Length);
                GameObject product = Get(1);
                WeaponGoods weaponGoods = product.GetComponent<WeaponGoods>();

                weaponGoods.Init(weapon[num].weaponName, weapon[num].setType, weapon[num].weaponNickNames, weapon[num].weaponImage);
                weaponGoods.transform.SetParent(goodsContent);

                goodsList.Add(weaponGoods.gameObject);
            }
        }

        
    }

    public void ShopReRoll()
    {
        List<GameObject> locks = new List<GameObject>();
        List<GameObject> unLocks = new List<GameObject>();

        for (int i = 0; i < lockList.Count; i++) //잠금 아이템 위치 조정(맨 왼쪽으로)
        {
            lockList[i].transform.SetSiblingIndex(i);
            locks.Add(lockList[i]);
        }
        for (int i = 0; i < goodsList.Count; i++)
        {
            bool isLock = false;
            for (int j = 0; j < lockList.Count; j++)
            {
                if (goodsList[i].gameObject == lockList[j].gameObject)
                {
                    isLock = true;
                }
            }
            if (isLock == false)
            {
                unLocks.Add(goodsList[i]);
            }
        }
        goodsList.Clear();

        //잠긴 아이템과 아닌 아이템 분류 후 순서대로 넣어주기
        for (int i = 0; i < locks.Count; i++)
        {
            goodsList.Add(locks[i]);
        }
        for (int i = 0; i < unLocks.Count; i++)
        {
            goodsList.Add(unLocks[i]);
        }

        for (int i = locks.Count; i < goodsList.Count; i++)//잠긴 아이템이 아니면 오브젝트 꺼주기
        {
            goodsList[i].SetActive(false);
        }
        goodsList.Clear();
        ShopGoodsSetting();

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
