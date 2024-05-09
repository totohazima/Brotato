using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class GameManager : MonoBehaviour, UI_Upadte
{
    public static GameManager instance;
    public GameDataBase gameDataBase;
    public Wave_Scriptable[] wave_Scriptables = new Wave_Scriptable[10];
    public bool isStart;

    [Header("Player_Info")]
    public Player.Character character;
    [Header("Item_Info")]
    public ItemGroup_Scriptable itemGroup_Scriptable;
    [Header("Test")]
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
    }

    public void UI_Update()
    {

    }

    public IEnumerator StageStart()
    {
        yield return 0;
        //playerInfo.StatCalculate();
        isStart = true;
        StageManager.instance.curHp = StageManager.instance.playerInfo.maxHealth_Origin;
        for (int i = 0; i < StageManager.instance.playerInfo.weapons.Count; i++)
        {
            if (StageManager.instance.playerInfo.weapons[i].GetComponent<Weapon_Action>().index == Weapon.Weapons.WRENCH)
            {
                StartCoroutine(StageManager.instance.playerInfo.weapons[i].GetComponent<Wrench_Weapon>().SpawnTurret());
            }
        }
        
    }
    

}
