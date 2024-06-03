using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "GameDataBase/playerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("Settings")]
    public bool isDie; //�÷��̾� ���
    [Header("Stats")]
    public float playerHealth; //���� ü��
    [Header("Level")]
    public int playerLevel;
    public int levelUpChance; //���̺� ���� �� ���� �� �� Ƚ��
    public int lootChance; //���ڱ� ����
    public float curExp;  //���� ����ġ
    public float maxExp;  //�ִ� ����ġ
    [HideInInspector] public float overExp; //������ �� ���� ����ġ
    [Header("Money")]
    public int money; //��
    public int interest; //����
    [Header("#Item_Info")]
    public ItemGroup_Scriptable itemGroup_Scriptable;
    public bool isUglyTooth; //������ �̻� ���� �� �� Ÿ�� �ø��� ���ǵ� -10% (3ȸ ��ø)
    public bool isLumberJack; //���� �� ���� ���� �� ������ �� �濡 �ı���
    public bool isWeirdGhost; // �̻��� ���� ���� �� true�� �Ǹ� ���̺� ���� �� ü���� 1�̵� 
    public int minesCount; //���� ������ ����
    public int turretCount; //�ͷ� ������ ����
    public int snakeCount; //�� ������ ���� (�ϳ� �� ȭ�� ���� �� �����Ǵ� ���� �� +1)
    public bool isScaredSausage;
    public float scaredSausageChance; //�̸��� �ҽ����� ȭ�� Ȯ�� (�ϳ��� 25%)
    public float scaredSausageDamage; //�̸��� �ҽ����� ƽ �� ȭ�� ����� (�ϳ��� 1)
    public int scaredSausageDamageCount; //�̸��� �ҽ����� ƽ Ƚ�� 


    /// <summary>
    /// �÷��̾� �ǰ� �� ����� ���
    /// </summary>
    /// <param name="damage">���� ������ ���� �����</param>
    public void HitCalculate(float damage)
    {
        float damages = Mathf.Round(damage);
        float hit, dodge;
        if (GameManager.instance.player_Info.evasion >= 60)
        {
            dodge = 60;
            hit = 100 - dodge;
        }
        else
        {
            dodge = GameManager.instance.player_Info.evasion;
            hit = 100 - dodge;
        }
        float[] chance = { hit, dodge };
        int index = Judgment(chance);
        if (index == 0)
        {
            //������ 0 �ʰ�
            if (GameManager.instance.player_Info.armor > 0)
            {
                float enduce = 1 / (1 + (GameManager.instance.player_Info.armor / 15));
                enduce = 1 - enduce;
                damages -= damages * enduce;
                GameManager.instance.playerInfo.playerHealth -= damages;
            }
            //������ 0 �̸�
            else if (GameManager.instance.player_Info.armor < 0)
            {
                float armor = Mathf.Abs(GameManager.instance.player_Info.armor);
                float enduce = 1 / (1 + (armor / 15));
                enduce = 1 + (1 - enduce);
                damages = (damages * enduce);
                GameManager.instance.playerInfo.playerHealth -= damages;
            }
            else
            {
                GameManager.instance.playerInfo.playerHealth -= damages;
            }
        }
        else
        {
            string dodgeText = "<color=#4CFF52>ȸ��</color>";
            Transform text = DamageTextManager.instance.TextCreate(0, dodgeText).transform;
            text.position = GameManager.instance.player_Info.transform.position;
        }
    }

    public void ItemSearch()
    {
        List<Item> item = GameManager.instance.player_Info.itemInventory;

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
                case Item.ItemType.SCARED_SAUSAGE:
                    isScaredSausage = true;
                    scaredSausageChance = item[i].curCount * 25f;
                    scaredSausageDamage = item[i].curCount * 1;
                    scaredSausageDamageCount = item[i].curCount * 3;
                    break;
            }

        }
    }
    /// <summary>
    /// PlayerInfo�� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    public void Reset_PlayerInfo()
    {
        isDie = false;
        playerLevel = 0;
        levelUpChance = 0;
        curExp = 0;
        maxExp = 0;
        money = 0;
        interest = 0;
    }

    private int Judgment(float[] rando)
    {
        int count = rando.Length;
        float max = 0;
        for (int i = 0; i < count; i++)
            max += rando[i];

        float range = UnityEngine.Random.Range(0f, (float)max);
        //0.1, 0.2, 30, 40
        double chance = 0;
        for (int i = 0; i < count; i++)
        {
            chance += rando[i];
            if (range > chance)
                continue;

            return i;
        }

        return -1;
    }
}
