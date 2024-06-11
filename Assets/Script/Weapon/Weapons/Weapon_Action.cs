using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Action : Weapon
{
    public Weapons index;
    public SettType[] setTypes;
    public WeaponScrip scrip;

    public SpriteRenderer[] tierOutline;
    public virtual void ReturnWeapon(Transform baseObject) //근접 전용
    {
        baseObject.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 무기 회전
    /// </summary>
    /// <param name="isLeft"></param>
    public virtual void WeaponSpinning(bool isLeft)
    {
        return;
    }
}
