using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon/weapon")]
public class WeaponScrip : ScriptableObject
{
    public string weaponName;
    public string setType;
    public Weapon.Weapons weaponNickNames;
    public Weapon.WeaponType attackType;
    public Weapon.SettType[] weaponSetType;
    public Sprite weaponImage;
    public GameObject weaponPrefab;

    public string[] tier1_Info;
    public float[] tier1_InfoStat;
    public string[] tier2_Info;
    public float[] tier2_InfoStat;
    public string[] tier3_Info;
    public float[] tier3_InfoStat;
    public string[] tier4_Info;
    public float[] tier4_InfoStat;
}
