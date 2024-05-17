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
    public int unArmed_Set;
    public int tool_Set;
    public int gun_Set;
    public int explosive_Set;
    public int precision_Set;
    public int native_Set;
    [Header("#Item_Info")]
    public ItemGroup_Scriptable itemGroup_Scriptable;
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
    public void GameStart() //Title���� Main����
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
        WeaponSetSearch();
        for (int i = 0; i < StageManager.instance.playerInfo.weapons.Count; i++)
        {
            if (StageManager.instance.playerInfo.weapons[i].GetComponent<Weapon_Action>().index == Weapon.Weapons.WRENCH)
            {
                StartCoroutine(StageManager.instance.playerInfo.weapons[i].GetComponent<Wrench_Weapon>().SpawnTurret());
            }
        }

        //���� ���� �߰�
        GameObject startWeapon = Instantiate(weaponPrefab);
        startWeapon.transform.SetParent(player_Info.weaponMainPos);
        player_Info.weapons.Add(startWeapon);

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
        LoadingSceneManager.CloseScene("MainScene");
    }

    public void WeaponSetSearch()
    {
        unArmed_Set = 0;
        tool_Set = 0;
        gun_Set = 0;
        explosive_Set = 0;
        precision_Set = 0;
        native_Set = 0;

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
                }
            }
        }
    }
}
