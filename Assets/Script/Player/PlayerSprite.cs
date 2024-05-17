using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [Header("Pevis")]
    public SpriteRenderer pevis;
    [Header("Head")]
    public SpriteRenderer head_1;
    public SpriteRenderer head_2;
    public SpriteRenderer head_3;
    public SpriteRenderer halfCloseEye;
    public SpriteRenderer closeEye;
    [Header("Body")]
    public SpriteRenderer body;
    public SpriteRenderer hood_1;
    public SpriteRenderer hood_2;
    public SpriteRenderer hood_3;
    [Header("RightArm")]
    public SpriteRenderer rightArm;
    public SpriteRenderer rightHand;
    [Header("LeftArm")]
    public SpriteRenderer leftArm;
    public SpriteRenderer leftHand;
    [Header("Belt(Leg)")]
    public SpriteRenderer belt;
    public SpriteRenderer rightLeg;
    public SpriteRenderer leftLeg;
    [Header("Foot")]
    public SpriteRenderer rightFoot;
    public SpriteRenderer leftFoot;

    public void SpriteSetting()
    {
        CharacterSprite_Scriptable scriptable = null;
        for (int i = 0; i < GameManager.instance.characterSprite.Length; i++)
        {
            if(GameManager.instance.character == GameManager.instance.characterSprite[i].code)
            {
                scriptable = GameManager.instance.characterSprite[i];
                break;
            }
        }

        if (scriptable != null)
        {
            pevis.sprite = scriptable.pevis;
            head_1.sprite = scriptable.head_1;
            head_2.sprite = scriptable.head_2;
            head_3.sprite = scriptable.head_3;
            halfCloseEye.sprite = scriptable.halfCloseEye;
            closeEye.sprite = scriptable.closeEye;
            body.sprite = scriptable.body;
            hood_1.sprite = scriptable.hood_1;
            hood_2.sprite = scriptable.hood_2;
            hood_3.sprite = scriptable.hood_3;
            rightArm.sprite = scriptable.rightArm;
            rightHand.sprite = scriptable.rightHand;
            leftArm.sprite = scriptable.leftArm;
            leftHand.sprite = scriptable.leftHand;
            belt.sprite = scriptable.belt;
            rightLeg.sprite = scriptable.rightLeg;
            leftLeg.sprite = scriptable.leftLeg;
            rightFoot.sprite = scriptable.rightFoot;
            leftFoot.sprite = scriptable.leftFoot;
        }
    }
}
