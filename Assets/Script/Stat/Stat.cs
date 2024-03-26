using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stat
{
    [System.Serializable]
    public enum PlayerStat
    {
        NONE,
        MAXHEALTH,      //ü��, ����
        REGENERATION,   //���, �ۼ�Ʈ
        BLOOD_SUCKING,   //����, �ۼ�Ʈ
        PERSENT_DAMAGE,  //�۵�, �ۼ�Ʈ
        MELEE_DAMAGE,    //�ٵ�, ����
        RANGE_DAMAGE,    //����, ����
        ATTACK_SPEED,    //����, �ۼ�Ʈ
        CRITICAL_CHANCE, //ġȮ, �ۼ�Ʈ
        ENGINE,         //����, �ۼ�Ʈ
        RANGE,          //����, ����
        ARMOR,          //���, �ۼ�Ʈ
        EVASION,        //ȸ��, �ۼ�Ʈ
        ACCURACY,       //����, �ۼ�Ʈ
        SPEED,          //�̼�, �ۼ�Ʈ

        CONSUMABLE_HEAL,
        MAGNET_RANGE,
        EXP_GAIN,
        PENETRATE,
        INSTNAT_MAGNET,
        KNOCK_BACK,
        EXPLOSIVE_DAMAGE,
        PENETRTE_DAMAGE,
        EXPLOSIVE_SIZE,
        CHAIN,
        BOSS_DAMAGE,
    }
    
}
