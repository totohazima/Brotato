using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager instance;

    public Transform[] stats;
    public Text[] statName;
    public Text[] statNum;

    public Transform upgrade;
    public GameObject[] upgrades;

    Player player;
    public bool isSetting;
    void Awake()
    {
        instance = this;
        for (int i = 0; i < stats.Length; i++)
        {
            statName[i] = stats[i].GetChild(0).GetComponent<Text>();
            statNum[i] = stats[i].GetChild(1).GetComponent<Text>();
        }
        upgrades = new GameObject[upgrade.childCount];
        for (int i = 0; i < upgrade.childCount; i++)
        {
            upgrades[i] = upgrade.GetChild(i).gameObject;
        }
        player = GameManager.instance.mainPlayer.GetComponent<Player>();
    }

    void Update()
    {
        statNum[0].text = player.maxHealth.ToString("F0");
        statNum[1].text = player.regeneration.ToString("F0");
        statNum[2].text = player.bloodSucking.ToString("F0");
        statNum[3].text = player.persentDamage.ToString("F0");
        statNum[4].text = player.meleeDamage.ToString("F0");
        statNum[5].text = player.rangeDamage.ToString("F0");
        statNum[6].text = player.attackSpeed.ToString("F0");
        statNum[7].text = player.criticalChance.ToString("F0");
        statNum[8].text = player.engine.ToString("F0");
        statNum[9].text = player.range.ToString("F0");
        statNum[10].text = player.armor.ToString("F0");
        statNum[11].text = player.evasion.ToString("F0");
        statNum[12].text = player.speed.ToString("F0");

        for (int i = 0; i < statName.Length; i++)
        {
            if(float.Parse(statNum[i].text) > 0) //0 ���� ū ��� �ʷ� �۾�
            {
                statName[i].color = Color.green;
                statNum[i].color = Color.green;
            }
            else if(float.Parse(statNum[i].text) < 0) //0 ���� ���� ��� ���� �۾�
            {
                statName[i].color = Color.red;
                statNum[i].color = Color.red;
            }
            else if(float.Parse(statNum[i].text) == 0) //0 �� ��� �� �۾�
            {
                statName[i].color = Color.white;
                statNum[i].color = Color.white;
            }
        }
    }
    public void NextSelect() //���� ������ ��ȸ ������
    {
        StartCoroutine(UpgradeSetting());
    }
    public void ReRoll() //���� �ʱ�ȭ
    {
        if(isSetting == true)
        {
            return;
        }

        if (GameManager.instance.Money >= 1)
        {
            GameManager.instance.Money -= 1;
            StartCoroutine(UpgradeSetting());
        }
        else
        {
            Debug.Log("��� ����");
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
            int index = GameManager.instance.Judgment(chanceLise);

            chooseUpgrades[i] = upgrades[index];
            percent[index] = 0f;
        }

        for(int i = 0; i < 4; i++)
        {
            isSetting = true;
            yield return new WaitForSeconds(0.1f);
            chooseUpgrades[i].SetActive(true);
        }
        isSetting = false;
    }
}