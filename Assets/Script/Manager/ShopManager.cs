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
    [SerializeField] private int rerollCount; //리롤을 누른 횟수 (매 상점마다 0으로 초기화)
    private float rerollPrice; //리롤 가격
    private float rerollPrice_Add; //리롤을 누를때마다 증가할 수치
    public List<GameObject> lockList; //잠긴 아이템(가격은 고정된채로 가장 앞 열로 이동)
    public List<GameObject> goodsList;
    List<GameObject>[] list;
    StageManager stage;
    public WeaponScrip[] weapon;

    [Header("# 무기 확률 체크")]
    [SerializeField] List<GameObject> sameWeapon;
    [SerializeField] List<GameObject> sameClassWeapon;
    [SerializeField] List<GameObject> allWeapon;

    enum ShopTab
    {
        WeaponTab,
        ItemTab,
        CardTab,
    }
    void Awake()
    {
        instance = this;
        stage = StageManager.instance;

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
            rerollPrice = Mathf.Ceil(rerollPrice); //올림
            rerollPrice_Add = Mathf.Ceil(rerollPrice / 3);
        }
        else
        {
            rerollPrice = wave + (wave / 2);
            rerollPrice = Mathf.Floor(rerollPrice); //내림
            rerollPrice_Add = Mathf.Floor(rerollPrice / 3);
        }
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
        titleWaveUI.text = "웨이브 " + (stage.waveLevel + 1) + "(총 10 물결)";
        moneyNumUI.text = stage.money.ToString();
        nextWaveUI.text = "이동(웨이브 " + (stage.waveLevel + 2) + ")";

        tabsText[(int)ShopTab.WeaponTab].text = "무기(" + tabsScroll[0].childCount + "/" + GameManager.instance.maxWeaponCount +")";
        tabsText[(int)ShopTab.ItemTab].text = "아이템(" + tabsScroll[1].childCount + ")";
        tabsText[(int)ShopTab.CardTab].text = "천부 카드";

        float price = rerollPrice + (rerollPrice_Add * rerollCount);
        rerollPrice_Text.text = price.ToString("F0");
    }
    public void ShopGoodsSetting()
    {
        int index;
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
                index = Random.Range(0, GameManager.instance.itemGroup_Scriptable.items.Length);
                indexes[i] = index;
            }

            for (int i = 0; i < indexes.Length; i++) //5개 번호 중에 최대 수량에 도달한 아이템이 있는지 체크
            {
                Item.ItemType type = GameManager.instance.itemGroup_Scriptable.items[indexes[i]].itemCode;
                for (int j = 0; j < ItemManager.instance.maxItemList.Count; j++)
                {
                    if (ItemManager.instance.maxItemList[j] == type)
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
            //잠긴 아이템 중 
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
        ///중복 무기와 같은 클래스의 무기를 찾는 코드
        List<Weapon.Weapons> weaponTypes = new List<Weapon.Weapons>();
        List<Weapon.SettType> weaponClasses = new List<Weapon.SettType>();

        for (int j = 0; j < stage.playerInfo.weapons.Count; j++)
        {
            Weapon_Action weapon = GameManager.instance.player_Info.weapons[j].GetComponent<Weapon_Action>();
            bool isSameType = false;
            bool isSameClass = false;
            //같은 무기 구분
            Weapon.Weapons type = (Weapon.Weapons)System.Enum.Parse(typeof(Weapon.Weapons), weapon.weaponCode);
            for (int z = 0; z < weaponTypes.Count; z++)
            {
                if (weaponTypes[z] == type)
                {
                    isSameType = true;
                }
                //근거리 무기는 제외하는 경우
                if(GameManager.instance.character == Player.Character.RANGER)
                {
                    Weapon.WeaponType attackType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), weapon.attackType);
                    if (attackType == Weapon.WeaponType.MELEE)
                    {
                        isSameType = true;
                    }
                }
                //원거리 무기는 제외하는 경우
                else if (GameManager.instance.character == Player.Character.GLADIATOR)
                {
                    Weapon.WeaponType attackType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), weapon.attackType);
                    if (attackType == Weapon.WeaponType.RANGE)
                    {
                        isSameType = true;
                    }
                }
            }
            if (isSameType == false)
            {
                weaponTypes.Add(type);
            }

            //동일 클래스 구분
            for (int k = 0; k < weapon.setTypes.Length; k++)
            {
                Weapon.SettType weaponClass = weapon.setTypes[k];
                for (int z = 0; z < weaponClasses.Count; z++)
                {
                    if (weaponClasses[z] == weaponClass)
                    {
                        isSameClass = true;
                    }

                    //근거리 무기는 제외하는 경우
                    if (GameManager.instance.character == Player.Character.RANGER)
                    {
                        Weapon.WeaponType attackType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), weapon.attackType);
                        if (attackType == Weapon.WeaponType.MELEE)
                        {
                            isSameClass = true;
                        }
                    }
                    //원거리 무기는 제외하는 경우
                    else if (GameManager.instance.character == Player.Character.GLADIATOR)
                    {
                        Weapon.WeaponType attackType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), weapon.attackType);
                        if (attackType == Weapon.WeaponType.RANGE)
                        {
                            isSameClass = true;
                        }
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
            //황소: 무기 탑재 불가
            if (GameManager.instance.character == Player.Character.BULL)
            {
                itemChance = 1;
                weaponChance = 0;
            }


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
                //무기가 없는 경우
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
                        Debug.Log("같은 무기 (20%) " + weapon[nums].weaponName.ToString());
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
                        Debug.Log("같은 클래스의 무기 (15%) " + weapon[nums].weaponName.ToString());
                    }
                    else
                    {
                        List<WeaponScrip> weapons = new List<WeaponScrip>();
                        for (int z = 0; z < weapon.Length; z++)
                        {
                            //근거리 무기는 제외하는 경우
                            if (GameManager.instance.character == Player.Character.RANGER)
                            {
                                Weapon.WeaponType attackType = weapon[z].attackType;
                                if (attackType != Weapon.WeaponType.MELEE)
                                {
                                    weapons.Add(weapon[z]);
                                }
                            }
                            //원거리 무기는 제외하는 경우
                            else if (GameManager.instance.character == Player.Character.GLADIATOR)
                            {
                                Weapon.WeaponType attackType = weapon[z].attackType;
                                if (attackType != Weapon.WeaponType.RANGE)
                                {
                                    weapons.Add(weapon[z]);
                                }
                            }
                            //둘 다 아닌 경우
                            else
                            {
                                weapons.Add(weapon[z]);
                            }
                        }
                        nums = Random.Range(0, weapons.Count);

                        GameObject product = Get(1);
                        WeaponGoods weaponGoods = product.GetComponent<WeaponGoods>();
                        weaponGoods.Init(weapons[nums]);
                        weaponGoods.transform.SetParent(goodsContent);
                        goodsList.Add(weaponGoods.gameObject);

                        allWeapon.Add(weaponGoods.gameObject);
                        Debug.Log("모든 무기 (65%) " + weapon[nums].weaponName.ToString());
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
        stage.nextWave();
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
