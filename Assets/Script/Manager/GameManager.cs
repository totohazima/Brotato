using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, UI_Upadte
{
    public static GameManager instance;
    public bool isStart;
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
