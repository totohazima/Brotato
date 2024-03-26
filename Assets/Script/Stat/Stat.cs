using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stat
{
    [System.Serializable]
    public enum PlayerStat
    {
        NONE,
        MAXHEALTH,      //체력, 정수
        REGENERATION,   //재생, 퍼센트
        BLOOD_SUCKING,   //흡혈, 퍼센트
        PERSENT_DAMAGE,  //퍼뎀, 퍼센트
        MELEE_DAMAGE,    //근뎀, 정수
        RANGE_DAMAGE,    //원뎀, 정수
        ATTACK_SPEED,    //공속, 퍼센트
        CRITICAL_CHANCE, //치확, 퍼센트
        ENGINE,         //엔진, 퍼센트
        RANGE,          //범위, 정수
        ARMOR,          //방어, 퍼센트
        EVASION,        //회피, 퍼센트
        ACCURACY,       //명중, 퍼센트
        SPEED,          //이속, 퍼센트

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
