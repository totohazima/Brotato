using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Player
{
    ForSettingPlayer CharacterImport;
    MainSceneManager main;
    GameManager game;
    public Animator anim;
    public CapsuleCollider coll;
    public SpriteRenderer sprite;
    public SphereCollider magnet;

    float magnetRanges; //자석 범위
    JoyStick joy;
    float moveSpeed; //캐릭터 이동속도
    void Start()
    {
        main = MainSceneManager.instance;
        game = GameManager.instance;
        CharacterImport = main.selectPlayer.GetComponent<ForSettingPlayer>();
        magnetRanges = magnet.radius;
        joy = JoyStick.instance;
        joy.moveTarget.SetParent(transform);

        StatSetting(CharacterImport.characterNum);
    }
    void Update()
    {
        if (game.isDie == true)
        {
            return;
        }
        StatApply();

    }

    void FixedUpdate()
    {
        if (game.isDie == true)
        {
            return;
        }

        if (joy.isMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, joy.moveTarget.position, moveSpeed);
            if (joy.moveTarget.position.x < transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
    }

    void StatApply()
    {
        if (regeneration >= 0)
        {
            double regenHp = (0.09 * regeneration) * Time.deltaTime;
            if (regenHp > 0)
            {
                regenHp = 0;
            }
            game.curHp += (float)regenHp;
        }
        moveSpeed = 0.2f * (1 + (speed / 100));

        magnet.radius = magnetRanges * (1 + (magnetRange / 100));
    }

}
