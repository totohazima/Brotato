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
    JoyStick joy;
    float moveSpeed; //캐릭터 이동속도
    void Start()
    {
        main = MainSceneManager.instance;
        game = GameManager.instance;
        CharacterImport = main.selectPlayer.GetComponent<ForSettingPlayer>();
        joy = JoyStick.instance;
        joy.moveTarget.SetParent(transform);

        StatSetting(CharacterImport.characterNum);
    }
    void Update()
    {
        moveSpeed = 0.1f * (1 + (speed / 100));
    }
    void FixedUpdate()
    {
        if(joy.isMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, joy.moveTarget.position, moveSpeed);
        }
    }
}
