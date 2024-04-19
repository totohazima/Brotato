using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, ICustomUpdateMono
{
    public static WeaponManager instance;
    public int unArmed_Set;
    public int tool_Set;
    public int gun_Set;
    public int explosive_Set;
    public int precision_Set;
    public int native_Set;
    Weapon_Action[] weapon;

    void Awake()
    {
        instance = this;
        WeaponSetSearch();
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }

    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        
    }

    public void WeaponSetSearch()
    {
        unArmed_Set = 0;
        tool_Set = 0;
        gun_Set = 0;
        explosive_Set = 0;
        precision_Set = 0;
        native_Set = 0;

        weapon = new Weapon_Action[StageManager.instance.playerInfo.weapons.Count];
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i] = StageManager.instance.playerInfo.weapons[i].GetComponent<Weapon_Action>();
        }

        for (int i = 0; i < weapon.Length; i++)
        {
            for (int j = 0; j < weapon[i].setTypes.Length; j++)
            {
                switch (weapon[i].setTypes[j])
                {
                    case Weapon.SettType.UNARMED:
                        unArmed_Set++;
                        break;
                    case Weapon.SettType.TOOL:
                        tool_Set++;
                        break;
                    case Weapon.SettType.GUN:
                        gun_Set++;
                        break;
                    case Weapon.SettType.EXPLOSIVE:
                        explosive_Set++;
                        break;
                    case Weapon.SettType.PRECISION:
                        precision_Set++;
                        break;
                    case Weapon.SettType.NATIVE:
                        native_Set++;
                        break;
                }
            }
        }
    }
}
