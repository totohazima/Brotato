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
}
