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
        ELEMENTAL_DAMAGE, //���Ҵ����, ����
        ATTACK_SPEED,    //����, �ۼ�Ʈ
        CRITICAL_CHANCE, //ġȮ, �ۼ�Ʈ
        ENGINE,         //����, �ۼ�Ʈ
        RANGE,          //����, ����
        ARMOR,          //���, �ۼ�Ʈ
        EVASION,        //ȸ��, �ۼ�Ʈ
        ACCURACY,       //����, �ۼ�Ʈ
        SPEED,          //�̼�, �ۼ�Ʈ

        CONSUMABLE_HEAL,
        METERIAL_HEAL,
        EXP_GAIN,
        MAGNET_RANGE,
        PRICE_SALE,
        EXPLOSIVE_DAMAGE,
        EXPLOSIVE_SIZE,
        CHAIN,
        PENETRATE,
        PENETRTE_DAMAGE,
        BOSS_DAMAGE,
        KNOCK_BACK,
        DOUBLE_METERIAL,
        LOOT_IN_METERIAL,
        FREE_REROLL,
        TREE,
        ENEMY_AMOUNT,
        ENEMY_SPEED,
        INSTNAT_MAGNET,
    }
    
}
