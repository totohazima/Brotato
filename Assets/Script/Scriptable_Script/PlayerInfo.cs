using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "GameDataBase/playerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("Settings")]
    public bool isDie; //플레이어 사망
    [Header("Level")]
    public int playerLevel;
    public float curExp;  //현재 경험치
    public float maxExp;  //최대 경험치
    [Header("Money")]
    public int money; //돈
    public int interest; //이자

    /// <summary>
    /// PlayerInfo를 초기화하는 함수
    /// </summary>
    public void Reset_PlayerInfo()
    {
        isDie = false;
        playerLevel = 0;
        curExp = 0;
        maxExp = 0;
        money = 0;
        interest = 0;
    }
}
