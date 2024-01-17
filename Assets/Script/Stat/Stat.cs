using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Stat
{
    public enum PlayerStat
    {
        MAXHEALTH,      //체력, 정수
        REGENERATION,   //재생, 퍼센트
        BLOODSUCKING,   //흡혈, 퍼센트
        PERSENTDAMAGE,  //퍼뎀, 퍼센트
        MELEEDAMAGE,    //근뎀, 정수
        RANGEDAMAGE,    //원뎀, 정수
        ATTACKSPEED,    //공속, 퍼센트
        CRITICALCHANCE, //치확, 퍼센트
        ENGINE,         //엔진, 퍼센트
        RANGE,          //범위, 정수
        ARMOR,          //방어, 퍼센트
        EVASION,        //회피, 퍼센트
        ACCURACY,       //명중, 퍼센트
        SPEED,          //이속, 퍼센트
    }

    public enum EnemyStat
    {
        MAXHEALTH,      //체력, 정수  
        DAMAGE,         //데미지, 정수    
        COOLTIME,       //쿨타임, 정수
        ARMOR,          //방어, 퍼센트
        RANGE,          //범위, 퍼센트
        EVASION,        //회피, 퍼센트
        ACCURACY,       //명중, 퍼센트
        SPEED,          //이속, 퍼센트
    }

    public enum WeaponStat
    {
        DAMAGE,         //데미지, 정수  
        CRITICALCHANCE, //치확, 정수  
        COOLTIME,       //쿨타임, 정수  
        KNOCKBACK,      //넉백, 정수  
        RANGE,          //범위, 정수  
        PENETRATE,      //관통, 정수  
    }
}
