using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : Player, ICustomUpdateMono
{
    private GameManager game;
    private StageManager stage;
    private Rigidbody rigid;
    private float magnetRanges; //자석 범위
    private JoyStick joy;
    private float timer; //체력 재생 타이머
    private float regenTime; //체력 재생 시간
    private float invincibleTime = 0.5f; //피격 후 무적 시간
    [SerializeField] private float moveSpeed; //캐릭터 이동속도
    [SerializeField] private float hitTimer;
    public Animator anim;
    public CapsuleCollider coll;
    public SphereCollider magnet;
    public Transform animTrans;
    public Transform weaponMainPos;
    public bool isFullWeapon; //무기가 꽉찬 경우
    public bool isHit; //피격 시 true가 되며 true일 경우 피격 판정이 일어나지 않는다.
    public bool isStand; //캐릭터가 멈춰있는지 확인하는 변수
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
        game.playerInfo.StatImport(game.character);
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

        if (game.playerInfo.isDie == true || game.isEnd == true)
        {
            isStand = false;
            anim.SetBool("Move", false);
            return;
        }
        StatApply();

        if (joy.isMove == true)
        {
            Vector3 dir = new Vector3(joyStick.Horizontal, joyStick.Vertical, 0);
            // Vector의 방향은 유지하지만 크기를 1로 줄인다. 길이가 정규화 되지 않을시 0으로 설정.
            dir.Normalize();
            // 오브젝트의 위치를 dir 방향으로 이동시킨다.
            Move(dir);

            if(dir.x < 0) //왼쪽
            {
                animTrans.rotation = Quaternion.Euler(0, 180, 0);
            }
            else //오른 쪽
            {
                animTrans.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (isStand == true)
            {
                isStand = false;
                //군인: 가만히 있을 시 대미지 +50%, 공격 속도 +50%
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
                //군인: 가만히 있을 시 대미지 +50%, 공격 속도 +50%
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

        if(isHit == true || game.isEnd == true)
        {
            coll.enabled = false;
        }
        else
        {
            coll.enabled = true;
        }

    }
    //private void Move(Vector3 dir)
    //{
    //    anim.SetBool("Move", true);
    //    //Vector3 dirVec = joy.moveTarget.position - rigid.position;
    //    Vector3 nextVec = dir.normalized * moveSpeed;
    //    rigid.MovePosition(rigid.position + nextVec);
    //    rigid.velocity = Vector3.zero;

    //    ///이동 제한 
    //    float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
    //    float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
    //    transform.position = new Vector3(x, y, transform.position.z);
    //}
    private void Move(Vector3 dir)
    {
        anim.SetBool("Move", true);

        // 플레이어의 현재 위치
        Vector3 currentPosition = transform.position;

        // 새로운 위치를 계산하기 전에 현재 위치가 외곽에 있는지 확인합니다.
        float newX = currentPosition.x + dir.x * moveSpeed;
        float newY = currentPosition.y + dir.y * moveSpeed;

        // X 좌표가 외곽에 닿았을 때 이동을 제한합니다.
        if (newX <= stage.xMin || newX >= stage.xMax)
        {
            newX = currentPosition.x; // X 이동을 막습니다.
        }

        // Y 좌표가 외곽에 닿았을 때 이동을 제한합니다.
        if (newY <= stage.yMin || newY >= stage.yMax)
        {
            newY = currentPosition.y; // Y 이동을 막습니다.
        }

        // 새로운 위치를 설정합니다.
        Vector3 nextPosition = new Vector3(newX, newY, currentPosition.z);
        rigid.MovePosition(nextPosition);
        rigid.velocity = Vector3.zero;
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
            if (GameManager.instance.playerInfo.playerHealth < stage.maxHp)
            {
                if (timer >= regenTime)
                {
                    GameManager.instance.playerInfo.playerHealth += 1;
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
#if UNITY_EDITOR
    int segments = 100;
    Color gizmoColor = Color.yellow;
    bool drawWhenSelected = true;

    void OnDrawGizmosSelected()
    {
        if (game.isStart == true && drawWhenSelected)
        {
            Gizmos.color = gizmoColor;
            DrawHollowCircle(game.player_Info.weaponMainPos.position, game.playerInfo.doNotSpawnRange, segments);
        }
    }
    void DrawHollowCircle(Vector3 center, float radius, int segments)
    {
        float angle = 0f;
        Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);

        for (int i = 1; i <= segments; i++)
        {
            angle = i * Mathf.PI * 2f / segments;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            Gizmos.DrawLine(lastPoint, newPoint);
            lastPoint = newPoint;
        }
    }
#endif
}
