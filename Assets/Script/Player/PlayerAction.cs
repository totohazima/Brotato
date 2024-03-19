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
    public Transform animTrans;
    private float magnetRanges; //�ڼ� ����
    private JoyStick joy;
    private float moveSpeed; //ĳ���� �̵��ӵ�
    private float timer; //ü�� ��� Ÿ�̸�
    private float regenTime; //ü�� ��� �ð�
    public Transform weaponMainPos;
    public bool isFullWeapon; //���Ⱑ ���� ���
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
        if (weapons.Count >= 6)
        {
            isFullWeapon = true;
        }
        else
        {
            isFullWeapon = false;
        }

        if (game.isDie == true || game.isEnd == true)
        {
            anim.SetBool("Move", false);
            return;
        }
        StatApply();

        if (joy.isMove == true)
        {
            Move();
            if (joy.moveTarget.position.x < transform.position.x)
            {
                animTrans.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                animTrans.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            anim.SetBool("Move", false);
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            float deg = 360 * i / weapons.Count;
            Vector3 pos = ConvertAngleToVector(deg);
            weapons[i].transform.position = new Vector3(weaponMainPos.position.x + pos.x, weaponMainPos.position.y + pos.y, weaponMainPos.position.z + pos.z);
        }

        

    }
    private void Move()
    {
        anim.SetBool("Move", true);
        Vector3 dirVec = joy.moveTarget.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;

        ///�̵� ���� 
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

    private Vector3 ConvertAngleToVector(float _deg)//������ ��ǥ ���ϱ�
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * 2.5f, Mathf.Sin(rad) * 2.5f, 0);
    }

}
