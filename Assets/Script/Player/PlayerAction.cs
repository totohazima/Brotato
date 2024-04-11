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
    private float magnetRanges; //자석 범위
    private JoyStick joy;
    private float moveSpeed; //캐릭터 이동속도
    private float timer; //체력 재생 타이머
    private float regenTime; //체력 재생 시간
    public Transform weaponMainPos;
    public bool isFullWeapon; //무기가 꽉찬 경우
    public bool isHit; //피격 시 true가 되며 true일 경우 피격 판정이 일어나지 않는다.
    [SerializeField] private float hitTImer;
    private float invincibleTime = 0.5f; //피격 후 무적 시간
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
            float deg = 360 * i / weapons.Count - 90;
            Vector3 pos = ConvertAngleToVector(deg);
            weapons[i].transform.position = new Vector3(weaponMainPos.position.x + pos.x, weaponMainPos.position.y + pos.y, weaponMainPos.position.z + pos.z);
        }

        if (isHit == true)
        {
            hitTImer += Time.deltaTime;
            if (hitTImer >= invincibleTime)
            {
                isHit = false;
                hitTImer = 0;
            }
        }

        if(isHit == true)
        {
            coll.enabled = false;
        }
        else
        {
            coll.enabled = true;
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

    private Vector3 ConvertAngleToVector(float _deg)//각도로 좌표 구하기
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * 3f, Mathf.Sin(rad) * 3f, 0);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.CompareTag("Enemy") && isHit == false)
    //    {
    //        isHit = true;
    //    }
    //}
}
