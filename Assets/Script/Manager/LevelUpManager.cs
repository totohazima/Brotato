using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour, ICustomUpdateMono
{
    public static LevelUpManager instance;

    public Transform[] stats;
    public Text[] statName;
    public Text[] statNum;

    public Transform upgrade;
    public GameObject[] upgrades;

    private Player player;
    public bool isSetting;
    void Awake()
    {
        instance = this;
        //for (int i = 0; i < stats.Length; i++)
        //{
        //    statName[i] = stats[i].GetChild(0).GetComponent<Text>();
        //    statNum[i] = stats[i].GetChild(1).GetComponent<Text>();
        //}
        upgrades = new GameObject[upgrade.childCount];
        for (int i = 0; i < upgrade.childCount; i++)
        {
            upgrades[i] = upgrade.GetChild(i).gameObject;
        }
        player = StageManager.instance.mainPlayer.GetComponent<Player>();
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        StageManager.instance.StatUI_On();
        StartCoroutine(UpgradeSetting());
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        //statNum[0].text = StageManager.instance.playerLevel.ToString("F0");
        //statNum[1].text = player.maxHealth.ToString("F0");
        //statNum[2].text = player.regeneration.ToString("F0");
        //statNum[3].text = player.bloodSucking.ToString("F0");
        //statNum[4].text = player.persentDamage.ToString("F0");
        //statNum[5].text = player.meleeDamage.ToString("F0");
        //statNum[6].text = player.rangeDamage.ToString("F0");
        //statNum[7].text = player.attackSpeed.ToString("F0");
        //statNum[8].text = player.criticalChance.ToString("F0");
        //statNum[9].text = player.engine.ToString("F0");
        //statNum[10].text = player.range.ToString("F0");
        //statNum[11].text = player.armor.ToString("F0");
        //statNum[12].text = player.evasion.ToString("F0");
        //statNum[13].text = player.accuracy.ToString("F0");
        //statNum[14].text = player.speed.ToString("F0");

        //for (int i = 1; i < statName.Length; i++) //0인 레벨 스탯은 제외
        //{
        //    if(float.Parse(statNum[i].text) > 0) //0 보다 큰 경우 초록 글씨
        //    {
        //        statName[i].color = Color.green;
        //        statNum[i].color = Color.green;
        //    }
        //    else if(float.Parse(statNum[i].text) < 0) //0 보다 작은 경우 빨간 글씨
        //    {
        //        statName[i].color = Color.red;
        //        statNum[i].color = Color.red;
        //    }
        //    else if(float.Parse(statNum[i].text) == 0) //0 인 경우 흰 글씨
        //    {
        //        statName[i].color = Color.white;
        //        statNum[i].color = Color.white;
        //    }
        //}
    }
    public void NextSelect() //남은 레벨업 기회 소진용
    {
        StartCoroutine(UpgradeSetting());
    }
    public void ReRoll() //골드로 초기화
    {
        if(isSetting == true)
        {
            return;
        }

        if (StageManager.instance.money >= 1)
        {
            StageManager.instance.money -= 1;
            StartCoroutine(UpgradeSetting());
        }
        else
        {
            Debug.Log("골드 부족");
        }
    }
    public IEnumerator UpgradeSetting()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].SetActive(false);
        }

        GameObject[] chooseUpgrades = new GameObject[4];
        float[] percent = new float[upgrades.Length];

        for (int i = 0; i < upgrades.Length; i++)
        {
            percent[i] = 1;
        }

        for (int i = 0; i < 4; i++)
        {
            float[] chanceLise = percent;
            int index = StageManager.instance.Judgment(chanceLise);

            chooseUpgrades[i] = upgrades[index];
            percent[index] = 0f;
        }

        for(int i = 0; i < 4; i++)
        {
            isSetting = true;
            yield return new WaitForSeconds(0.1f);
            chooseUpgrades[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.05f);
        isSetting = false;
    }
}
