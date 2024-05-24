using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class GameManager : MonoBehaviour, UI_Upadte
{
    public static GameManager instance;
    public GameDataBase gameDataBase;
    public CharacterSprite_Scriptable[] characterSprite;
    public Wave_Scriptable[] wave_Scriptables = new Wave_Scriptable[10];
    public bool isStart;
    [Header("#Player_Info")]
    [HideInInspector] public ForSettingPlayer player;
    [HideInInspector] public ForSettingWeapon weapon;
    [HideInInspector] public Difficult diffiicult;
    [HideInInspector] public GameObject weapon_Obj;
    [HideInInspector] public GameObject player_Obj;
    [HideInInspector] public GameObject difficult_Obj;
    [HideInInspector] public int maxWeaponCount; //�ִ� ���� ����
    public PlayerAction player_Info;
    public Player.Character character;
    public GameObject weaponPrefab;
    public GameObject optionUI;
    public bool isDie; //�÷��̾� ���
    public bool isPause; //�Ͻ�����
    [Header("#Weapon_Info")]
    public Vector2 engineerBuildingPos;
    public int unArmed_Set;
    public int tool_Set;
    public int gun_Set;
    public int explosive_Set;
    public int precision_Set;
    public int native_Set;
    public int elemental_Set;
    [Header("#Item_Info")]
    public ItemGroup_Scriptable itemGroup_Scriptable;
    public bool isUglyTooth; //������ �̻� ���� �� �� Ÿ�� �ø��� ���ǵ� -10% (3ȸ ��ø)
    public bool isLumberJack; //���� �� ���� ���� �� ������ �� �濡 �ı���
    public bool isWeirdGhost; // �̻��� ���� ���� �� true�� �Ǹ� ���̺� ���� �� ü���� 1�̵� 
    public int minesCount; //���� ������ ����
    public int turretCount; //�ͷ� ������ ����
    public int snakeCount; //�� ������ ���� (�ϳ� �� ȭ�� ���� �� �����Ǵ� ���� �� +1)
    [Header("#Difficult_Info")]
    public int difficult_Level; //���̵�
    public bool isSpecialEnemySpawn; //���ο� ���� ����
    public bool isEliteSpawn; //����Ʈ�� ������ ����
    public int eliteEnemyWave; //����Ʈ�� ������ �����ϴ� ���̺� ��
    public float enemyRiseDamage; //�� ������ ����ġ %
    public float enemyRiseHealth; //�� ü�� ����ġ %
    public bool doubleBoss; //������ 2���� (�� ������ ü���� 25% ����)
    [Header("#Prefab")]
    public GameObject dontDestoryOBJ;
    public GameObject option;
    [Header("#Test")] //QA�� ���� ���� public ����
    public float harvestVariance_Amount; //��Ȯ ����ġ(���̺긶�� �ش��ġ�� ���ϸ� ��Ȯ���� ���Ŀ��� �������� %�� ������)
    public float riseHarvest_Num; //���̺긶�� ��Ȯ ����ġ 5 -> 5%
    public float decreaseHarvest_Num; //10���̺� ���� ��Ȯ ����ġ 20 -> 20%
    public float[] weaponTierChance = new float[4];
    public float[] upgradeChance = new float[4];
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        UIUpdateManager.uiUpdates.Add(this);
        GameObject obj = Instantiate(dontDestoryOBJ);
        option = obj.transform.GetChild(1).gameObject;
        optionUI = option.gameObject;

        GameStart();
    }

    public void UI_Update()
    {
        if (player != null)
        {
            character = player.index;
        }
        if(weapon != null)
        {
            weaponPrefab = weapon.weaponPrefabs;
        }
        if(diffiicult != null)
        {
            difficult_Level = diffiicult.difficultLevel;
            DifficultInfoTable.Data import = gameDataBase.difficultInfoTable.table[difficult_Level];

            isSpecialEnemySpawn = import.specialEnemySpawn;
            isEliteSpawn = import.eliteEnemySpawn;
            eliteEnemyWave = import.eliteEnemySpawnCount;
            enemyRiseDamage = import.enemyDamageRise;
            enemyRiseHealth = import.enemyHpRise;
            doubleBoss = import.twinBoss;
        }
    }
    private void GameStart() //Title���� Main����
    {
        LoadingSceneManager.LoadScene("MainScene");
    }
    public IEnumerator WaveStart()
    {
        yield return 0;
        isStart = true;
        if(character == Player.Character.MULTITASKER)
        {
            maxWeaponCount = 12;
        }
        else
        {
            maxWeaponCount = 6;
        }
        StageManager.instance.curHp = StageManager.instance.playerInfo.maxHealth_Origin;
        StageManager.instance.money = 30;
        
        //���� ���� �߰�
        if (weaponPrefab != null)
        {
            GameObject startWeapon = Instantiate(weaponPrefab);
            startWeapon.transform.SetParent(player_Info.weaponMainPos);
            player_Info.weapons.Add(startWeapon);
        }

        //������ ���� �߰�
        if (character == Player.Character.RANGER)
        {
            GameObject plusWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Pistol_Weapon");
            if (plusWeapon != null)
            {
                GameObject weapon = Instantiate(plusWeapon);
                weapon.transform.SetParent(player_Info.weaponMainPos);
                player_Info.weapons.Add(weapon);
            }
        }
        //�����Ͼ� ��ġ �߰�
        else if (character == Player.Character.ENGINEER)
        {
            GameObject plusWeapon = Resources.Load<GameObject>("Prefabs/Weapon/Wrench_Weapon");
            if (plusWeapon != null)
            {
                GameObject weapon = Instantiate(plusWeapon);
                weapon.transform.SetParent(player_Info.weaponMainPos);
                player_Info.weapons.Add(weapon);
            }
        }
        //��ȭ������ ������ ���� �߰�
        else if(character == Player.Character.PACIFIST)
        {
            ItemScrip getItem = null;
            for (int i = 0; i < itemGroup_Scriptable.items.Length; i++)
            {
                if(itemGroup_Scriptable.items[i].itemCode == Item.ItemType.LUMBERJACK_SHIRT)
                {
                    getItem = itemGroup_Scriptable.items[i];
                    break;
                }
            }

            GameObject obj = Resources.Load<GameObject>("Prefabs/Item/Item_Object");
            GameObject itemObj = Instantiate(obj);
            itemObj.transform.SetParent(transform);
            Item item = itemObj.GetComponent<Item>();
            item.Init(getItem);
            item.curCount++;
            player_Info.itemInventory.Add(item);
        }

        float randomX = Random.Range(StageManager.instance.xMin, StageManager.instance.xMax);
        float randomY = Random.Range(StageManager.instance.yMin, StageManager.instance.yMax);
        Vector3 point = new Vector3(randomX, randomY);
        engineerBuildingPos = point;

        WeaponSetSearch();
        player_Info.StatCalculate();
        LoadingSceneManager.CloseScene("MainScene");
    }
    public void ItemSearch()
    {
        List<Item> item = player_Info.itemInventory;

        for (int i = 0; i < item.Count; i++)
        {
            switch (item[i].itemType)
            {
                case Item.ItemType.UGLY_TOOTH:
                    isUglyTooth = true;
                    break;
                case Item.ItemType.LUMBERJACK_SHIRT:
                    isLumberJack = true;
                    break;
                case Item.ItemType.WEIRD_GHOST:
                    isWeirdGhost = true;
                    break;
                case Item.ItemType.LAND_MINES:
                    minesCount = item[i].curCount;
                    break;
                case Item.ItemType.TURRET:
                    turretCount = item[i].curCount;
                    break;
                case Item.ItemType.SNAKE:
                    snakeCount = item[i].curCount;
                    break;
            }

        }
    }
    public void WeaponSetSearch()
    {
        unArmed_Set = 0;
        tool_Set = 0;
        gun_Set = 0;
        explosive_Set = 0;
        precision_Set = 0;
        native_Set = 0;
        elemental_Set = 0;

        if (player_Info != null)
        {
            Weapon_Action[] weapon = new Weapon_Action[player_Info.weapons.Count];
            for (int i = 0; i < weapon.Length; i++)
            {
                weapon[i] = StageManager.instance.playerInfo.weapons[i].GetComponent<Weapon_Action>();
            }

            for (int i = 0; i < weapon.Length; i++)
            {
                for (int j = 0; j < weapon[i].setTypes.Length; j++)
                {
                    switch (weapon[i].setTypes[j])
                    {
                        case Weapon.SettType.UNARMED:
                            unArmed_Set++;
                            break;
                        case Weapon.SettType.TOOL:
                            tool_Set++;
                            break;
                        case Weapon.SettType.GUN:
                            gun_Set++;
                            break;
                        case Weapon.SettType.EXPLOSIVE:
                            explosive_Set++;
                            break;
                        case Weapon.SettType.PRECISION:
                            precision_Set++;
                            break;
                        case Weapon.SettType.NATIVE:
                            native_Set++;
                            break;
                        case Weapon.SettType.ELEMENTALS:
                            elemental_Set++;
                            break;
                    }
                }
            }
        }
    }
}
