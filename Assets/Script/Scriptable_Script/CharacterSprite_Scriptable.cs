using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChracterSprite", menuName = "Sprite/Chracter_Sprite")]
public class CharacterSprite_Scriptable : ScriptableObject
{
    public Player.Character code;
    [Header("Pevis")]
    public Sprite pevis;
    [Header("Head")]
    public Sprite head_1;
    public Sprite head_2;
    public Sprite head_3;
    public Sprite halfCloseEye;
    public Sprite closeEye;
    [Header("Body")]
    public Sprite body;
    public Sprite hood_1;
    public Sprite hood_2;
    public Sprite hood_3;
    [Header("RightArm")]
    public Sprite rightArm;
    public Sprite rightHand;
    [Header("LeftArm")]
    public Sprite leftArm;
    public Sprite leftHand;
    [Header("Belt(Leg)")]
    public Sprite belt;
    public Sprite rightLeg;
    public Sprite leftLeg;
    [Header("Foot")]
    public Sprite rightFoot;
    public Sprite leftFoot;
}
