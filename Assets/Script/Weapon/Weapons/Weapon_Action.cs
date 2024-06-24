using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    /// <summary>
    /// 공격 시점에 타겟이 해당 무기 사거리내에 있는지 확인하는 함수
    /// </summary>
    /// <param name="target">타겟</param>
    /// <param name="range">무기의 사거리</param>
    /// <returns></returns>
    public virtual bool IsRangeInTarget(Transform target, float range)
    {
        bool isTrue = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, range, 1 << 6);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == null || colliders[i].gameObject.activeSelf == false)
            {
                continue;
            }

            if (colliders[i].transform == target)
            {
                isTrue = true;
            }
        }

        return isTrue;

    }
}
