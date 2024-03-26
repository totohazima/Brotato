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

    public List<GameObject> lockList; //��� ������(������ ������ä�� ���� �� ���� �̵�)
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
        titleWaveUI.text = "���̺� " + (game.waveLevel + 1) + "(�� 10 ����)";
        moneyNumUI.text = game.Money.ToString();
        nextWaveUI.text = "�̵�(���̺� " + (game.waveLevel + 2) + ")";

        tabsText[0].text = "����(" + tabsScroll[0].childCount + "/6)";
        tabsText[1].text = "������(" + tabsScroll[1].childCount + ")";
        tabsText[2].text = "õ�� ī��";
    }
    public void ShopGoodsSetting()
    {
        int index = 0;
        int[] indexes = new int[5];

        for (int i = 0; i < lockList.Count; i++) //��� ������ 
        {
            goodsList.Add(lockList[i]);
        }

        while (true) 
        {
            bool isNot = false;
            for (int i = 0; i < indexes.Length; i++) //��ȣ �̱�
            {
                index = Random.Range(0, item.items.Length);
                indexes[i] = index;
            }

            for (int i = 0; i < indexes.Length; i++) //5�� ��ȣ �߿� �ִ� ������ ������ �������� �ִ��� üũ
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
