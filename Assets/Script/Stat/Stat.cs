using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Stat
{
    public enum PlayerStat
    {
        MAXHEALTH,      //ü��, ����
        REGENERATION,   //���, �ۼ�Ʈ
        BLOODSUCKING,   //����, �ۼ�Ʈ
        PERSENTDAMAGE,  //�۵�, �ۼ�Ʈ
        MELEEDAMAGE,    //�ٵ�, ����
        RANGEDAMAGE,    //����, ����
        ATTACKSPEED,    //����, �ۼ�Ʈ
        CRITICALCHANCE, //ġȮ, �ۼ�Ʈ
        ENGINE,         //����, �ۼ�Ʈ
        RANGE,          //����, ����
        ARMOR,          //���, �ۼ�Ʈ
        EVASION,        //ȸ��, �ۼ�Ʈ
        ACCURACY,       //����, �ۼ�Ʈ
        SPEED,          //�̼�, �ۼ�Ʈ
    }

    public enum EnemyStat
    {
        MAXHEALTH,      //ü��, ����  
        DAMAGE,         //������, ����    
        COOLTIME,       //��Ÿ��, ����
        ARMOR,          //���, �ۼ�Ʈ
        RANGE,          //����, �ۼ�Ʈ
        EVASION,        //ȸ��, �ۼ�Ʈ
        ACCURACY,       //����, �ۼ�Ʈ
        SPEED,          //�̼�, �ۼ�Ʈ
    }

    public enum WeaponStat
    {
        DAMAGE,         //������, ����  
        CRITICALCHANCE, //ġȮ, ����  
        COOLTIME,       //��Ÿ��, ����  
        KNOCKBACK,      //�˹�, ����  
        RANGE,          //����, ����  
        PENETRATE,      //����, ����  
    }
}
