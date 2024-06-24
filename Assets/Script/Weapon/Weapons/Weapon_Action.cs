using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Action : Weapon
{
    public Weapons index;
    public SettType[] setTypes;
    public WeaponScrip scrip;

    public SpriteRenderer[] tierOutline;
    public virtual void ReturnWeapon(Transform baseObject) //���� ����
    {
        baseObject.localPosition = Vector3.zero;
    }

    /// <summary>
    /// ���� ȸ��
    /// </summary>
    /// <param name="isLeft"></param>
    public virtual void WeaponSpinning(bool isLeft)
    {
        return;
    }

    /// <summary>
    /// ���� ������ Ÿ���� �ش� ���� ��Ÿ����� �ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="target">Ÿ��</param>
    /// <param name="range">������ ��Ÿ�</param>
    /// <returns></returns>
    public virtual bool IsRangeInTarget(Transform target, float range)
    {
        float trueRange = Vector3.Distance(transform.position, target.position);
        if(trueRange <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
