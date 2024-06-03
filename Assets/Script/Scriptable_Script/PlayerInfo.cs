using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "GameDataBase/playerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("Settings")]
    public bool isDie; //플레이어 사망
    [Header("Stats")]
    public float playerHealth; //현재 체력
    [Header("Level")]
    public int playerLevel;
    public int levelUpChance; //웨이브 종료 후 레벨 업 할 횟수
    public int lootChance; //상자깡 찬스
    public float curExp;  //현재 경험치
    public float maxExp;  //최대 경험치
    [HideInInspector] public float overExp; //레벨업 후 남은 경험치
    [Header("Money")]
    public int money; //돈
    public int interest; //이자
    [Header("#Item_Info")]
    public ItemGroup_Scriptable itemGroup_Scriptable;
    public bool isUglyTooth; //못생긴 이빨 구매 시 적 타격 시마다 스피드 -10% (3회 중첩)
    public bool isLumberJack; //럼버 잭 셔츠 구매 시 나무가 한 방에 파괴됨
    public bool isWeirdGhost; // 이상한 유령 구매 시 true가 되며 웨이브 시작 시 체력이 1이됨 
    public int minesCount; //지뢰 아이템 갯수
    public int turretCount; //터렛 아이템 갯수
    public int snakeCount; //뱀 아이템 갯수 (하나 당 화상 적용 시 전염되는 몬스터 수 +1)
    public bool isScaredSausage;
    public float scaredSausageChance; //겁먹은 소시지의 화상 확률 (하나당 25%)
    public float scaredSausageDamage; //겁먹은 소시지의 틱 당 화상 대미지 (하나당 1)
    public int scaredSausageDamageCount; //겁먹은 소시지의 틱 횟수 


    /// <summary>
    /// 플레이어 피격 시 대미지 계산
    /// </summary>
    /// <param name="damage">적이 나한테 입힌 대미지</param>
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
            //방어력이 0 초과
            if (GameManager.instance.player_Info.armor > 0)
            {
                float enduce = 1 / (1 + (GameManager.instance.player_Info.armor / 15));
                enduce = 1 - enduce;
                damages -= damages * enduce;
                GameManager.instance.playerInfo.playerHealth -= damages;
            }
            //방어력이 0 미만
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
            string dodgeText = "<color=#4CFF52>회피</color>";
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
    /// PlayerInfo를 초기화하는 함수
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
