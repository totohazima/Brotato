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
