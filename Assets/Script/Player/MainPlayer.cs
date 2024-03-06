using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Player, ICustomUpdateMono
{
    private ForSettingPlayer CharacterImport;
    private MainSceneManager main;
    private GameManager game;
    private Rigidbody rigid;
    public Animator anim;
    public CapsuleCollider coll;
    public SpriteRenderer sprite;
    public SphereCollider magnet;

    private float magnetRanges; //자석 범위
    private JoyStick joy;
    private float moveSpeed; //캐릭터 이동속도
    void Start()
    {
        main = MainSceneManager.instance;
        game = GameManager.instance;
        rigid = GetComponent<Rigidbody>();
        CharacterImport = main.selectPlayer.GetComponent<ForSettingPlayer>();
        magnetRanges = magnet.radius;
        joy = JoyStick.instance;
        joy.moveTarget.SetParent(transform);

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
        if (game.isDie == true)
        {
            return;
        }
        StatApply();

        if (joy.isMove == true)
        {
            Move();
            //transform.position = Vector3.MoveTowards(transform.position, joy.moveTarget.position, moveSpeed);
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
    private void Move()
    {
        Vector3 dirVec = joy.moveTarget.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;
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
