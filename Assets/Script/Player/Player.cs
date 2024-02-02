using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("STAT")]
    public int characterNum;
    public string characterName;
    public float maxHealth;
    public float regeneration;
    public float bloodSucking;
    public float persentDamage;
    public float meleeDamage;
    public float rangeDamage;
    public float attackSpeed;
    public float criticalChance;
    public float engine;
    public float range;
    public float armor;
    public float evasion;
    public float accuracy;
    public float speed;

    public enum Character
    {
        VERSATILE, //다재다능
    }

    public void StatSetting(int index)
    {
        PlayerStatImporter import = PlayerStatImporter.instance;

        characterNum = import.characterNum[index];
        characterName = import.characterName[index];
        maxHealth = import.maxHealth[index];
        regeneration = import.regeneration[index];
        bloodSucking = import.bloodSucking[index];
        persentDamage = import.persentDamage[index];
        meleeDamage = import.meleeDamage[index];
        rangeDamage = import.rangeDamage[index];
        attackSpeed = import.attackSpeed[index];
        criticalChance = import.criticalChance[index];
        engine = import.engine[index];
        range = import.range[index];
        armor = import.armor[index];
        evasion = import.evasion[index];
        accuracy = import.accuracy[index];
        speed = import.speed[index];
    }
}
