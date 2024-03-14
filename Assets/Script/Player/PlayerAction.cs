using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : Player, ICustomUpdateMono
{
    private ForSettingPlayer CharacterImport;
    private MainSceneManager main;
    private GameManager game;
    private Rigidbody rigid;
    public Animator anim;
    public CapsuleCollider coll;
    public SphereCollider magnet;

    private float magnetRanges; //자석 범위
    private JoyStick joy;
    private float moveSpeed; //캐릭터 이동속도
    private float timer; //체력 재생 타이머
    private float regenTime; //체력 재생 시간
    void Start()
    {
        main = MainSceneManager.instance;
        game = GameManager.instance;
        rigid = GetComponent<Rigidbody>();
        CharacterImport = main.selectPlayer.GetComponent<ForSettingPlayer>();
        magnetRanges = magnet.radius;
        joy = JoyStick.instance;
        //joy.moveTarget.SetParent(transform);

        StatSetting(CharacterImport.characterNum);
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
        if (game.isDie == true || game.isEnd == true)
        {
            anim.SetBool("Move", false);
            return;
        }
        StatApply();

        if (joy.isMove == true)
        {
            Move();
            //transform.position = Vector3.MoveTowards(transform.position, joy.moveTarget.position, moveSpeed);
            if (joy.moveTarget.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            anim.SetBool("Move", false);
        }

    }
    private void Move()
    {
        anim.SetBool("Move", true);
        Vector3 dirVec = joy.moveTarget.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;

        ///이동 제한 
        float x = Mathf.Clamp(transform.position.x, game.xMin, game.xMax);
        float y = Mathf.Clamp(transform.position.y, game.yMin, game.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }
    void StatApply()
    {
        float regenHp = (float)(0.09 * regeneration);
        regenTime = 1 / regenHp;
        if (regenHp > 0)
        {
            timer += Time.deltaTime;
            if (game.curHp < game.maxHp)
            {
                if (timer >= regenTime)
                {
                    game.curHp += 1;
                    timer = 0;
                    string healTxt = "<color=#4CFF52>1</color>";
                    Transform text = DamageTextManager.instance.TextCreate(0, healTxt).transform;
                    text.position = transform.position;
                }
            }
        }
        moveSpeed = 1 * (1 + (speed / 100));
        moveSpeed = moveSpeed / 2;
        magnet.radius = magnetRanges * (1 + (magnetRange / 100));
    }



}
