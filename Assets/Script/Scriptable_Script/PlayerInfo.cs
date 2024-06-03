using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "GameDataBase/playerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("Settings")]
    public bool isDie; //�÷��̾� ���
    [Header("Level")]
    public int playerLevel;
    public float curExp;  //���� ����ġ
    public float maxExp;  //�ִ� ����ġ
    [Header("Money")]
    public int money; //��
    public int interest; //����

    /// <summary>
    /// PlayerInfo�� �ʱ�ȭ�ϴ� �Լ�
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
