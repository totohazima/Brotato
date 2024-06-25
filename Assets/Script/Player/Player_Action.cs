using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player_Action : Player, ICustomUpdateMono
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
    public bool isRight = true, isLeft; //���� �ٶ󺸰� �ִ� ����
    public bool isFullWeapon; //���Ⱑ ���� ���
    public bool isHit; //�ǰ� �� true�� �Ǹ� true�� ��� �ǰ� ������ �Ͼ�� �ʴ´�.
    public bool isStand; //ĳ���Ͱ� �����ִ��� Ȯ���ϴ� ����
    public JoyStick joyStick;
    public WhiteFlash whiteFlash;
    public PlayerSprite playerSprite;
    public WeaponPostion weaponPostion;

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
        isStand = false;
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
        WeaponAngleSetting();

        if (joy.isMove == true)
        {
            Vector3 dir = new Vector3(joyStick.Horizontal, joyStick.Vertical, 0);
            // Vector�� ������ ���������� ũ�⸦ 1�� ���δ�. ���̰� ����ȭ ���� ������ 0���� ����.
            dir.Normalize();
            // ������Ʈ�� ��ġ�� dir �������� �̵���Ų��.
            Move(dir);

            if (dir.x < 0) //����
            {
                animTrans.rotation = Quaternion.Euler(0, 180, 0);
                isRight = false;
                isLeft = true;
            }
            else //���� ��
            {
                animTrans.rotation = Quaternion.Euler(0, 0, 0);
                isRight = true;
                isLeft = false;
            }

            if (game.isStart == true && isStand == true)
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
            if (game.isStart == true && isStand == false)
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

        if (isHit == true)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= invincibleTime)
            {
                isHit = false;
                hitTimer = 0;
            }
        }

        if (isHit == true || game.isEnd == true)
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

        // �÷��̾��� ���� ��ġ
        Vector3 currentPosition = transform.position;

        // ���ο� ��ġ�� ����ϱ� ���� ���� ��ġ�� �ܰ��� �ִ��� Ȯ���մϴ�.
        float newX = currentPosition.x + dir.x * moveSpeed;
        float newY = currentPosition.y + dir.y * moveSpeed;

        // X ��ǥ�� �ܰ��� ����� �� �̵��� �����մϴ�.
        if (newX <= stage.xMin || newX >= stage.xMax)
        {
            newX = currentPosition.x; // X �̵��� �����ϴ�.
        }

        // Y ��ǥ�� �ܰ��� ����� �� �̵��� �����մϴ�.
        if (newY <= stage.yMin || newY >= stage.yMax)
        {
            newY = currentPosition.y; // Y �̵��� �����ϴ�.
        }

        // ���ο� ��ġ�� �����մϴ�.
        Vector3 nextPosition = new Vector3(newX, newY, currentPosition.z);
        rigid.MovePosition(nextPosition);
        rigid.velocity = Vector3.zero;
    }
    /// <summary>
    /// ���� ���ڿ� ���� ���� ����
    /// </summary>
    private void WeaponAngleSetting()
    {
        //float[] deg = new float[0];
        //float dis = 0;
        //switch (weapons.Count)
        //{
        //    case 1:
        //        deg = new float[] { 90 };
        //        dis = 0;
        //        break;
        //    case 2:
        //        deg = new float[] { 0, 180 };
        //        dis = 5;
        //        break;
        //    case 3:
        //        deg = new float[] { 45, 135, 270 };
        //        dis = 5;
        //        break;
        //    case 4:
        //        deg = new float[] { 45, 135, 225, 315 };
        //        dis = 5;
        //        break;
        //    case 5:
        //        deg = new float[] { 45, 135, 200, 270, 340 };
        //        dis = 5;
        //        break;
        //    case 6:
        //        deg = new float[] { 45, 135, 180, 225, 315, 360 };
        //        dis = 5;
        //        break;
        //    default:
        //        deg = new float[weapons.Count];
        //        for (int i = 0; i < weapons.Count; i++)
        //        {
        //            deg[i] = i * 360f / weapons.Count;
        //        }
        //        dis = 5;
        //        break;
        //}
        //for (int i = 0; i < deg.Length; i++)
        //{
        //    Vector3 pos = ConvertAngleToVector(-deg[i], dis);
        //    weapons[i].transform.position = new Vector3(weaponMainPos.position.x + pos.x, weaponMainPos.position.y + pos.y, weaponMainPos.position.z + pos.z);
        //}
        Vector3[] weaponPos = new Vector3[0];
        Vector3[] scannerPos = new Vector3[0];
        Vector3[] wPos = weaponPostion.weaponPos;
        Vector3[] sPos = weaponPostion.weaponScannerPos;
        switch (weapons.Count)
        {
            case 1:
                weaponPos = new Vector3[] { weaponPostion.firstWeaponPos };
                scannerPos = new Vector3[] { transform.position };
                break;
            case 2:
                weaponPos = new Vector3[] { wPos[0], wPos[8] };
                scannerPos = new Vector3[] { sPos[0], sPos[8] };
                break;
            case 3:
                weaponPos = new Vector3[] { wPos[2], wPos[6], wPos[12] };
                scannerPos = new Vector3[] { sPos[2], sPos[6], sPos[12] };
                break;
            case 4:
                weaponPos = new Vector3[] { wPos[14], wPos[2], wPos[6], wPos[10] };
                scannerPos = new Vector3[] { sPos[14], sPos[2], sPos[6], sPos[10] };
                break;
            case 5:
                weaponPos = new Vector3[] { wPos[2], wPos[6], wPos[9], wPos[12], wPos[15] };
                scannerPos = new Vector3[] { sPos[2], sPos[6], sPos[9], sPos[12], sPos[15] };
                break;
            case 6:
                weaponPos = new Vector3[] { wPos[14], wPos[0], wPos[2], wPos[6], wPos[8], wPos[10] };
                scannerPos = new Vector3[] { sPos[14], sPos[0], sPos[2], sPos[6], sPos[8], sPos[10] };
                break;
        }
        for(int i = 0; i < weaponPos.Length; i++)
        {
            Vector3 pos = weaponPos[i];
            weapons[i].transform.position = Vector3.Lerp(weapons[i].transform.position, new Vector3(pos.x, pos.y, pos.z), 5 * Time.deltaTime);
        }
        for (int i = 0; i < scannerPos.Length; i++)
        {
            Vector3 pos = scannerPos[i];
            weapons[i].scanner.scannerPos = new Vector3(pos.x, pos.y, pos.z);
        }
    }

    private void StatApply()
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
        else if (regenHp < 1)
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
        moveSpeed = 1300;
        moveSpeed = moveSpeed * (1 + (speed / 100));
        moveSpeed = moveSpeed / 2000;
        magnet.radius = magnetRanges * (1 + (magnetRange / 100));
    }


    public float GetAngle(Vector2 start, Vector2 end)//�������ϱ�
    {
        Vector2 vectorPos = end - start;
        return Mathf.Atan2(vectorPos.y, vectorPos.x) * Mathf.Rad2Deg;
    }
    private Vector3 ConvertAngleToVector(float _deg, float dis)//������ ��ǥ ���ϱ�
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * dis, Mathf.Sin(rad) * dis, 0);
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
            DrawHollowCircle(game.playerAct.weaponMainPos.position, game.playerInfo.doNotSpawnRange, segments);
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
