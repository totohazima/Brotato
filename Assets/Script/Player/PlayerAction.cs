using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : Player, ICustomUpdateMono
{
    private GameManager game;
    private StageManager stage;
    private Rigidbody rigid;
    private float magnetRanges; //�ڼ� ����
    private JoyStick joy;
    private float timer; //ü�� ��� Ÿ�̸�
    private float regenTime; //ü�� ��� �ð�
    private float invincibleTime = 0.5f; //�ǰ� �� ���� �ð�
    [SerializeField] private float moveSpeed; //ĳ���� �̵��ӵ�
    [SerializeField] private float hitTimer;
    public Animator anim;
    public CapsuleCollider coll;
    public SphereCollider magnet;
    public Transform animTrans;
    public Transform weaponMainPos;
    public bool isFullWeapon; //���Ⱑ ���� ���
    public bool isHit; //�ǰ� �� true�� �Ǹ� true�� ��� �ǰ� ������ �Ͼ�� �ʴ´�.
    public bool isStand; //ĳ���Ͱ� �����ִ��� Ȯ���ϴ� ����
    public JoyStick joyStick;
    public WhiteFlash whiteFlash;
    public PlayerSprite playerSprite;
    void Start()
    {
        game = GameManager.instance;
        stage = StageManager.instance;
        rigid = GetComponent<Rigidbody>();
        magnetRanges = magnet.radius;
        joy = JoyStick.instance;
        joyStick = StageManager.instance.joystick;
        playerSprite.SpriteSetting();
        StatSetting((int)game.character);
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
        if (weapons.Count >= game.maxWeaponCount)
        {
            isFullWeapon = true;
        }
        else
        {
            isFullWeapon = false;
        }

        if (game.isDie == true || stage.isEnd == true)
        {
            isStand = false;
            anim.SetBool("Move", false);
            return;
        }
        StatApply();

        if (joy.isMove == true)
        {
            Vector3 dir = new Vector3(joyStick.Horizontal, joyStick.Vertical, 0);
            // Vector�� ������ ���������� ũ�⸦ 1�� ���δ�. ���̰� ����ȭ ���� ������ 0���� ����.
            dir.Normalize();
            // ������Ʈ�� ��ġ�� dir �������� �̵���Ų��.
            Move(dir);

            if(dir.x < 0) //����
            {
                animTrans.rotation = Quaternion.Euler(0, 180, 0);
            }
            else //���� ��
            {
                animTrans.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (isStand == true)
            {
                isStand = false;
                //����: ������ ���� �� ����� +50%, ���� �ӵ� +50%
                if (game.character == Character.SOLDIER)
                {
                    persentDamage -= 50;
                    attackSpeed -= 50;
                }
            }
        }
        else
        {
            anim.SetBool("Move", false);
            if (isStand == false)
            {
                isStand = true;
                //����: ������ ���� �� ����� +50%, ���� �ӵ� +50%
                if (game.character == Character.SOLDIER)
                {
                    persentDamage += 50;
                    attackSpeed += 50;
                }
            }
        }
       
        for (int i = 0; i < weapons.Count; i++)
        {
            float deg = 360 * i / weapons.Count - 90;
            Vector3 pos = ConvertAngleToVector(deg);
            weapons[i].transform.position = new Vector3(weaponMainPos.position.x + pos.x, weaponMainPos.position.y + pos.y, weaponMainPos.position.z + pos.z);
        }

        if (isHit == true)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= invincibleTime)
            {
                isHit = false;
                hitTimer = 0;
            }
        }

        if(isHit == true || stage.isEnd == true)
        {
            coll.enabled = false;
        }
        else
        {
            coll.enabled = true;
        }

    }
    private void Move(Vector3 dir)
    {
        anim.SetBool("Move", true);
        //Vector3 dirVec = joy.moveTarget.position - rigid.position;
        Vector3 nextVec = dir.normalized * moveSpeed;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;

        ///�̵� ���� 
        float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
        float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }
    void StatApply()
    {
        float regenHp = (float)(0.09 * regeneration);
        if (regenHp > 1)
        {
            float i = 1;
            while (true)
            {
                if (regenHp * i < 1)
                {
                    i += 0.01f;
                    regenTime = i;
                    break;
                }
                i -= 0.01f;
            }
        }
        else if(regenHp < 1)
        {
            float i = 1;
            while (true)
            {
                if (regenHp * i < 1)
                {
                    regenTime = i;
                    break;
                }
                i += 0.01f;
            }
        }
        else
        {
            regenTime = 99f;
        }
        if (regenHp > 0) 
        {
            timer += Time.deltaTime;
            if (stage.curHp < stage.maxHp)
            {
                if (timer >= regenTime)
                {
                    stage.curHp += 1;
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
        return new Vector3(Mathf.Cos(rad) * 5f, Mathf.Sin(rad) * 5f, 0);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.CompareTag("Enemy") && isHit == false)
    //    {
    //        isHit = true;
    //    }
    //}
}
