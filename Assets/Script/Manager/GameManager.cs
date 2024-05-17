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
    [HideInInspector] public int maxWeaponCount; //최대 무기 갯수
    public PlayerAction player_Info;
    public Player.Character character;
    public GameObject weaponPrefab;
    public GameObject optionUI;
    public bool isDie; //플레이어 사망
    public bool isPause; //일시정지
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
    public int difficult_Level; //난이도
    public bool isSpecialEnemySpawn; //새로운 적의 출현
    public bool isEliteSpawn; //엘리트와 무리가 등장
    public int eliteEnemyWave; //엘리트와 무리가 등장하는 웨이브 수
    public float enemyRiseDamage; //적 데미지 증가치 %
    public float enemyRiseHealth; //적 체력 증가치 %
    public bool doubleBoss; //보스가 2마리 (한 마리는 체력이 25% 감소)
    [Header("#Prefab")]
    public GameObject dontDestoryOBJ;
    public GameObject option;
    [Header("#Test")] //QA를 위해 만든 public 변수
    public float harvestVariance_Amount; //수확 변동치(웨이브마다 해당수치가 변하며 수확스탯 계산식에서 마지막에 %로 곱해줌)
    public float riseHarvest_Num; //웨이브마다 수확 증가치 5 -> 5%
    public float decreaseHarvest_Num; //10웨이브 이후 수확 감소치 20 -> 20%
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
    public void GameStart() //Title에서 Main으로
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

        //시작 무기 추가
        GameObject startWeapon = Instantiate(weaponPrefab);
        startWeapon.transform.SetParent(player_Info.weaponMainPos);
        player_Info.weapons.Add(startWeapon);

        //레인저 권총 추가
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
