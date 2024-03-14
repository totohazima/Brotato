using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon/weapon")]
public class WeaponScrip : ScriptableObject
{
    public string weaponName;
    public string setType;
    public Weapon.Weapons weaponNickNames;
    public Sprite weaponImage;

}
