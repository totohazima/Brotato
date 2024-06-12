using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWrench_Weapon : Weapon_Action, ICustomUpdateMono
{
    public Transform startPos;
    public Transform endPos;
    [SerializeField] private Transform baseObj;
    [SerializeField] private CapsuleCollider coll;
    private SpriteRenderer sprite;
    [SerializeField] private float timer;
    [SerializeField] private float duration;
    [SerializeField] private float elapsedTime;
    private WeaponScanner scanner;
    private StageManager stage;
    private Melee_Bullet bullet;
    private bool isFire;
    private bool isSpawnedTurret;
    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
        sprite = baseObj.GetComponent<SpriteRenderer>();
        bullet = baseObj.GetComponent<Melee_Bullet>();
        stage = StageManager.instance;
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        coll.enabled = false;
        ResetStat();
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        ResetStat();
        AfterStatSetting();
        scanner.radius = realRange;

        for (int i = 0; i < tierOutline.Length; i++)
        {
            if (i == weaponTier)
            {
                tierOutline[i].gameObject.SetActive(true);
            }
            else
            {
                tierOutline[i].gameObject.SetActive(false);
            }
        }

        if (isFire == false)
        {
            StartCoroutine(MuzzleMove());
        }

        timer += Time.deltaTime;
        //군인: 이동 중 공격 불가
        if (GameManager.instance.character == Player.Character.SOLDIER)
        {
            if (GameManager.instance.player_Info.isStand == true && scanner.target != null)
            {
                if (timer >= afterCoolTime)
                {
                    StartCoroutine(WheelAttack());
                }
            }
        }
        else
        {
            if (scanner.target != null)
            {
                if (timer >= afterCoolTime)
                {
                    StartCoroutine(WheelAttack());                  
                }
            }
        }

        if (GameManager.instance.isEnd == true)
        {
            isSpawnedTurret = false;
        }
        else if (GameManager.instance.isEnd == false && isSpawnedTurret == false)
        {
            isSpawnedTurret = true;
            StartCoroutine(SpawnTurret());
        }
    }

    public void ResetStat()
    {
        StatSetting((int)index, weaponTier);
    }
    private IEnumerator MuzzleMove() //근접 무기는 공격이 끝나기 전까지 회전하면 안됨
    {
        if (scanner.target == null)
        {
            Vector3 target = Vector3.zero;
            if (target.x < transform.position.x)
            {
                sprite.flipX = true;
                for (int i = 1; i < tierOutline.Length; i++)
                {
                    tierOutline[i].flipX = true;
                }
            }
            else
            {
                sprite.flipX = false;
                for (int i = 1; i < tierOutline.Length; i++)
                {
                    tierOutline[i].flipX = false;
                }
            }
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.01f).setEase(LeanTweenType.easeInOutQuad);
        }
        else
        {
            Vector3 target = scanner.target.position;
            if (target.x < transform.position.x)
            {
                sprite.flipX = true;
                for (int i = 1; i < tierOutline.Length; i++)
                {
                    tierOutline[i].flipX = true;
                }
            }
            else
            {
                sprite.flipX = false;
                for (int i = 1; i < tierOutline.Length; i++)
                {
                    tierOutline[i].flipX = false;
                }
            }
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
        }
        yield return 0;
    }
    private IEnumerator WheelAttack()
    {
        bullet.Init(afterDamage, afterPenetrate, realRange, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

        if (scanner.target != null)
        {
            Vector3 target = scanner.target.position;
            
            Vector3 dirs = target - transform.position;
            float angles = Mathf.Atan2(dirs.y, dirs.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angles), 0.1f).setEase(LeanTweenType.easeInOutQuad);

            if (scanner.target != null)
            {
                Vector3 targetPos = scanner.target.position;
                float dis = Vector3.Distance(transform.position, targetPos);

                if (isFire == false)
                {
                    duration = 0.1f * dis;
                }
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                if (t > 1.0f)
                {
                    t = 1.0f;
                }
                Vector3 newPosition = CalculateBezierPoint(t, startPos.position, targetPos, endPos.position);
                transform.position = newPosition;

                coll.enabled = true;
                isFire = true;
                if (transform.position == endPos.position)
                {
                    ReturnWeapon(baseObj);
                    scanner.target = null;
                    coll.enabled = false;
                    timer = 0;
                    duration = 0;
                    elapsedTime = 0;
                }
            }
        }
        yield return 0;
    }

    private IEnumerator SpawnTurret()
    {
        int index = 0;
        switch (weaponTier)
        {
            case (0):
                index = (int)scrip.tier1_InfoStat[2];
                break;
            case (1):
                index = (int)scrip.tier2_InfoStat[2];
                break;
            case (2):
                index = (int)scrip.tier3_InfoStat[2];
                break;
            case (3):
                index = (int)scrip.tier4_InfoStat[2];
                break;
        }
        GameObject[] mark = new GameObject[index];
        GameObject[] turret = new GameObject[mark.Length];
        //엔지니어: 건축물이 서로 가깝게 생성됨
        if (GameManager.instance.character == Player.Character.ENGINEER)
        {
            for (int i = 0; i < mark.Length; i++)
            {
                mark[i] = PoolManager.instance.Get(7);
                float distance = Random.Range(2f, 30f);
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 pos = GameManager.instance.playerInfo.engineerBuildingPos + randomDirection * distance;
                if (pos.x > stage.xMax)
                {
                    pos.x = stage.xMax;
                }
                else if (pos.x < stage.xMin)
                {
                    pos.x = stage.xMin;
                }
                if (pos.y > stage.yMax)
                {
                    pos.y = stage.yMax;
                }
                else if (pos.y < stage.yMin)
                {
                    pos.y = stage.yMin;
                }
                mark[i].transform.position = pos;
            }
        }
        else
        {
            for (int i = 0; i < mark.Length; i++)
            {
                mark[i] = PoolManager.instance.Get(7);
                Vector3 pos = SpawnManager.instance.FriendlySpawnPosition();
                mark[i].transform.position = pos;
            }
        }
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < mark.Length; i++)
        {
            turret[i] = PoolManager.instance.Get(8);
            turret[i].transform.position = mark[i].transform.position;
            switch (weaponTier)
            {
                case (0):
                    turret[i].GetComponent<Turret>().Init(scrip.tier1_InfoStat[0], scrip.tier1_InfoStat[1]);
                    break;
                case (1):
                    turret[i].GetComponent<Turret>().Init(scrip.tier2_InfoStat[0], scrip.tier2_InfoStat[1]);
                    break;
                case (2):
                    turret[i].GetComponent<Turret>().Init(scrip.tier3_InfoStat[0], scrip.tier3_InfoStat[1]);
                    break;
                case (3):
                    turret[i].GetComponent<Turret>().Init(scrip.tier4_InfoStat[0], scrip.tier4_InfoStat[1]);
                    break;
            }
            SpawnManager.instance.turrets.Add(turret[i]);
            mark[i].SetActive(false);
        }
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)^2 * P0
        p += 2 * u * t * p1; // 2(1-t)t * P1
        p += tt * p2; // t^2 * P2

        return p;
    }
}
