using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ShopManager : MonoBehaviour, ICustomUpdateMono
{
    public static ShopManager instance;

    public Text titleWaveUI;
    public Text moneyNumUI;
    public Text nextWaveUI;
    public Text rerollPrice_Text;
    public Transform selectTab;
    public Transform[] tabs;
    public Image[] tabsImage;
    public Text[] tabsText;
    public Transform[] tabsScroll;
    public Transform[] verticalTabsScroll;
    public Transform goodsContent;
    public GameObject[] goods;
    [SerializeField] private int rerollCount; //������ ���� Ƚ�� (�� �������� 0���� �ʱ�ȭ)
    private float rerollPrice; //���� ����
    private float rerollPrice_Add; //������ ���������� ������ ��ġ
    public List<GameObject> lockList; //��� ������(������ ������ä�� ���� �� ���� �̵�)
    public List<GameObject> goodsList;
    List<GameObject>[] list;
    StageManager stage;
    ItemManager item;
    public WeaponScrip[] weapon;

    [Header("# ���� Ȯ�� üũ")]
    [SerializeField] List<GameObject> sameWeapon;
    [SerializeField] List<GameObject> sameClassWeapon;
    [SerializeField] List<GameObject> allWeapon;
    void Awake()
    {
        instance = this;
        stage = StageManager.instance;
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
        ItemManager.instance.WeaponListUp();
        ItemManager.instance.ItemListUp();
        rerollCount = 0;

        int wave = stage.waveLevel + 1;
        if (wave < 2)
        {
            rerollPrice = wave + (wave / 2);
            rerollPrice = Mathf.Ceil(rerollPrice); //�ø�
            rerollPrice_Add = Mathf.Ceil(rerollPrice / 3);
        }
        else
        {
            rerollPrice = wave + (wave / 2);
            rerollPrice = Mathf.Floor(rerollPrice); //����
            rerollPrice_Add = Mathf.Floor(rerollPrice / 3);
        }

        //ItemManager.instance.WeaponListUp(tabsScroll[0], verticalTabsScroll[0], PauseUI_Manager.instance.scrollContents[0]);
        //ItemManager.instance.ItemListUp(tabsScroll[1], verticalTabsScroll[1], PauseUI_Manager.instance.scrollContents[1]);
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
        titleWaveUI.text = "���̺� " + (stage.waveLevel + 1) + "(�� 10 ����)";
        moneyNumUI.text = stage.money.ToString();
        nextWaveUI.text = "�̵�(���̺� " + (stage.waveLevel + 2) + ")";

        tabsText[0].text = "����(" + tabsScroll[0].childCount + "/6)";
        tabsText[1].text = "������(" + tabsScroll[1].childCount + ")";
        tabsText[2].text = "õ�� ī��";

        float price = rerollPrice + (rerollPrice_Add * rerollCount);
        rerollPrice_Text.text = price.ToString("F0");
    }
    public void ShopGoodsSetting()
    {
        int index;
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
                index = Random.Range(0, GameManager.instance.itemGroup_Scriptable.items.Length);
                indexes[i] = index;
            }

            for (int i = 0; i < indexes.Length; i++) //5�� ��ȣ �߿� �ִ� ������ ������ �������� �ִ��� üũ
            {
                //Item.ItemType type = ItemManager.instance.items[indexes[i]].itemCode;
                //for (int j = 0; j < ItemManager.instance.maxItemList.Count; j++)
                //{
                //    if (ItemManager.instance.maxItemList[j] == type)
                //    {
                //        isNot = true;
                //    }
                //}

                Item.ItemType type = GameManager.instance.itemGroup_Scriptable.items[indexes[i]].itemCode;
                for (int j = 0; j < ItemManager.instance.maxItemList.Count; j++)
                {
                    if (ItemManager.instance.maxItemList[j] == type)
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
            //��� ������ �� 
            int[] lockNum = new int[lockList.Count];
            for (int j = 0; j < lockList.Count; j++)
            {
                ItemGoods goods = lockList[j].GetComponent<ItemGoods>();
                for (int i = 0; i < GameManager.instance.itemGroup_Scriptable.items.Length; i++)
                {
                    if (goods != null)
                    {
                        if (goods.scriptable == GameManager.instance.itemGroup_Scriptable.items[i])
                        {
                            lockNum[j] = i;
                        }
                    }
                }
            }
            for (int i = 0; i < lockNum.Length; i++)
            {
                for (int j = 0; j < indexes.Length; j++)
                {
                    if(lockNum[i] == indexes[j])
                    {
                        isNot = true;
                    }
                }
            }

            if (isNot == false)
            {
                break;
            }
        }
        sameWeapon.Clear();
        sameClassWeapon.Clear();
        allWeapon.Clear();
        ///�ߺ� ����� ���� Ŭ������ ���⸦ ã�� �ڵ�
        List<Weapon.Weapons> weaponTypes = new List<Weapon.Weapons>();
        List<Weapon.SettType> weaponClasses = new List<Weapon.SettType>();

        for (int j = 0; j < stage.playerInfo.weapons.Count; j++)
        {
            Weapon_Action weapon = stage.playerInfo.weapons[j].GetComponent<Weapon_Action>();
            bool isSameType = false;
            bool isSameClass = false;
            ///���� ���� ����
            Weapon.Weapons type = (Weapon.Weapons)System.Enum.Parse(typeof(Weapon.Weapons), weapon.weaponCode);
            for (int z = 0; z < weaponTypes.Count; z++)
            {
                if (weaponTypes[z] == type)
                {
                    isSameType = true;
                }
            }
            if (isSameType == false)
            {
                weaponTypes.Add(type);
            }

            //���� Ŭ���� ����
            for (int k = 0; k < weapon.setTypes.Length; k++)
            {
                Weapon.SettType weaponClass = weapon.setTypes[k];
                for (int z = 0; z < weaponClasses.Count; z++)
                {
                    if (weaponClasses[z] == weaponClass)
                    {
                        isSameClass = true;
                    }
                }

                if (isSameClass == false)
                {
                    weaponClasses.Add(weaponClass);
                }
            } 
        }
        weaponTypes = weaponTypes.Distinct().ToList();
        weaponClasses = weaponClasses.Distinct().ToList();

        for (int i = 0 + lockList.Count; i < 5; i++)
        {
            float itemChance = 0.65f;
            float weaponChance = 0.35f;

            float[] chanceLise = { itemChance, weaponChance };
            int chanceIndex = stage.Judgment(chanceLise);

            if (chanceIndex == 0)
            {
                ItemScrip items = GameManager.instance.itemGroup_Scriptable.items[indexes[i]];
                GameObject product = Get(0);
                ItemGoods itemGoods = product.GetComponent<ItemGoods>();
                itemGoods.Init(items, indexes[i]);

                itemGoods.transform.SetParent(goodsContent);

                goodsList.Add(itemGoods.gameObject);
            }
            else
            {
                float sameWeaponPool, sameClassWeaponPool, allWeaponPool;
                int nums = 0;
                //���Ⱑ ���� ���
                if (stage.playerInfo.weapons.Count == 0)
                {
                    nums = Random.Range(0, weapon.Length);

                    GameObject product = Get(1);
                    WeaponGoods weaponGoods = product.GetComponent<WeaponGoods>();
                    weaponGoods.Init(weapon[nums]);
                    weaponGoods.transform.SetParent(goodsContent);
                    goodsList.Add(weaponGoods.gameObject);
                }
                else
                {
                    sameWeaponPool = 0.2f;
                    sameClassWeaponPool = 0.15f;
                    allWeaponPool = 0.65f;

                    float[] chanceLise2 = { sameWeaponPool, sameClassWeaponPool, allWeaponPool };
                    int weaponIndex = stage.Judgment(chanceLise2);

                    if (weaponIndex == 0)
                    {
                        int num = Random.Range(0, weaponTypes.Count);
                        nums = (int)weaponTypes[num];

                        GameObject product = Get(1);
                        WeaponGoods weaponGoods = product.GetComponent<WeaponGoods>();
                        weaponGoods.Init(weapon[nums]);
                        weaponGoods.transform.SetParent(goodsContent);
                        goodsList.Add(weaponGoods.gameObject);

                        sameWeapon.Add(weaponGoods.gameObject);
                        Debug.Log("���� ���� (20%) " + weapon[nums].weaponName.ToString());
                    }
                    else if (weaponIndex == 1)
                    {
                        List<WeaponScrip> weaponScrips = new List<WeaponScrip>();
                        for (int j = 0; j < weaponClasses.Count; j++)
                        {
                            for (int k = 0; k < weapon.Length; k++)
                            {
                                for (int l = 0; l < weapon[k].weaponSetType.Length; l++)
                                {
                                    if(weaponClasses[j] == weapon[k].weaponSetType[l])
                                    {
                                        weaponScrips.Add(weapon[k]);
                                    }
                                }
                            }
                        }
                        weaponScrips = weaponScrips.Distinct().ToList();
                        int num = Random.Range(0, weaponScrips.Count);
                        for (int j = 0; j < weapon.Length; j++)
                        {
                            if(weaponScrips[num] == weapon[j])
                            {
                                nums = j;
                            }
                        }
                        GameObject product = Get(1);
                        WeaponGoods weaponGoods = product.GetComponent<WeaponGoods>();
                        weaponGoods.Init(weapon[nums]);
                        weaponGoods.transform.SetParent(goodsContent);
                        goodsList.Add(weaponGoods.gameObject);

                        sameClassWeapon.Add(weaponGoods.gameObject);
                        Debug.Log("���� Ŭ������ ���� (15%) " + weapon[nums].weaponName.ToString());
                    }
                    else
                    {
                        
                        nums = Random.Range(0, weapon.Length);

                        GameObject product = Get(1);
                        WeaponGoods weaponGoods = product.GetComponent<WeaponGoods>();
                        weaponGoods.Init(weapon[nums]/*weapon[num].weaponName, weapon[num].setType, weapon[num].weaponNickNames, weapon[num].weaponImage, weapon[num].attackType*/);
                        weaponGoods.transform.SetParent(goodsContent);
                        goodsList.Add(weaponGoods.gameObject);

                        allWeapon.Add(weaponGoods.gameObject);
                        Debug.Log("��� ���� (65%) " + weapon[nums].weaponName.ToString());
                    }
                }

                //int num = Random.Range(0, weapon.Length);
                
            }
        }

        
    }
    public void RerollButton()
    {
        float price = rerollPrice + (rerollPrice_Add * rerollCount);
        if (price <= stage.money)
        {
            stage.money -= (int)price;
            rerollCount++;
            ShopReRoll();
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
        stage.nextWave();
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
