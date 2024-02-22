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

    public List<GameObject> lockList; //��� ������(������ ������ä�� ���� �� ���� �̵�)
    public List<GameObject> goodsList;
    public List<int> maxItemList;
    List<GameObject>[] list;
    GameManager game;
    ItemManager item;
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
        titleWaveUI.text = "���̺� " + (game.waveLevel + 1).ToString();
        moneyNumUI.text = game.Money.ToString();
        nextWaveUI.text = "�̵�(���̺� " + game.waveLevel + 2 + ")";

        tabsText[0].text = "����(" + tabsScroll[0].childCount + "/6)";
        tabsText[1].text = "������(" + tabsScroll[1].childCount + ")";
        tabsText[2].text = "��(" + tabsScroll[2].childCount + ")";
        tabsText[3].text = "õ�� ī��";
    }
    public void ShopGoodsSetting()
    {
        int index = 0;
        int[] indexes = new int[5];
        int[] lockIndexed = new int[lockList.Count];
        ItemGoods[] itemProduct = new ItemGoods[lockList.Count];

        for (int i = 0; i < lockList.Count; i++) //��� ������ 
        {
            goodsList.Add(lockList[i]);
            itemProduct[i] = lockList[i].GetComponent<ItemGoods>();
            lockIndexed[i] = itemProduct[i].itemNum;
        }

        while (true) 
        {
            bool isNot = false;
            for (int i = 0; i < indexes.Length; i++) //��ȣ�� �̰� �ش� ��ȣ �������� ����Ʈ�� �� ��� �ٽ� �̴´�.(������ �ִ�ġ �̱⶧��)
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

            for(int i = 0; i < lockList.Count; i++) //��� ������ ��ȣ �ֱ�
            {
                indexes[i] = lockIndexed[i];
            }


            for (int i = 0; i < indexes.Length; i++) //5�� ��ȣ�� ���� �ߺ� �Ǵ��� üũ
            {
                for (int j = 0; j < indexes.Length; j++)
                {
                    if (i != j) //���� ���� ��
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

        
    }

    public void ShopReRoll()
    {
        
        List<GameObject> locks = new List<GameObject>();
        List<GameObject> unLocks = new List<GameObject>();

        for (int i = 0; i < lockList.Count; i++) //��� ������ ��ġ ����(�� ��������)
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

        //��� �����۰� �ƴ� ������ �з� �� ������� �־��ֱ�
        for (int i = 0; i < locks.Count; i++)
        {
            goodsList.Add(locks[i]);
        }
        for (int i = 0; i < unLocks.Count; i++)
        {
            goodsList.Add(unLocks[i]);
        }

        for (int i = locks.Count; i < goodsList.Count; i++)//��� �������� �ƴϸ� ������Ʈ ���ֱ�
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

        //������ Ǯ�� ����ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����

        foreach (GameObject item in list[index])
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
            select = Instantiate(goods[index], transform);
            list[index].Add(select);
        }
        return select;
    }

}
