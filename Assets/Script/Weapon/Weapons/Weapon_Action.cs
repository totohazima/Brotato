using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Action : Weapon
{
    public Weapons index;
    public SettType[] setTypes;
    public WeaponScrip scrip;
    public virtual void ReturnWeapon(Transform baseObject) //���� ����
    {
        baseObject.localPosition = Vector3.zero;
    }
}
