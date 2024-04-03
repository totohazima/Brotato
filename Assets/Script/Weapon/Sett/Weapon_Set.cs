using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weapon_Set : MonoBehaviour, ICustomUpdateMono
{
    public Text[] texts;
    [HideInInspector] public WeaponManager weaponManager;
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        weaponManager = WeaponManager.instance;
    }

    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public virtual void CustomUpdate()
    {
        return;
    }
}
