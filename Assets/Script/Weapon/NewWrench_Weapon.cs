using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NewWrench_Weapon : Weapon_Action, ICustomUpdateMono
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform controlPos1;
    [SerializeField] private Transform controlPos2;
    [SerializeField] private Transform endPos;
    [SerializeField] private Transform baseObj;
    [SerializeField] private CapsuleCollider coll;
    [SerializeField] private float timer;
    private WeaponScanner scanner;
    private StageManager stage;
    private Melee_Bullet bullet;
    private bool isFire;
    private bool isLeft;
    private bool isSpawnedTurret;
    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
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
                if (timer >= afterCoolTime && isFire == false)
                {
                    StartCoroutine(WheelAttack());
                    timer = 0;
                }
            }
        }
        else
        {
            if (scanner.target != null)
            {
                if (timer >= afterCoolTime && isFire == false)
                {
                    StartCoroutine(WheelAttack());
                    timer = 0;
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
            if (GameManager.instance.player_Info != null && GameManager.instance.player_Info.isLeft == true)
            {
                WeaponSpinning(true);
                LeanTween.rotate(gameObject, new Vector3(0, 0, 180), 0.01f).setEase(LeanTweenType.easeInOutQuad);
            }
            else
            {
                WeaponSpinning(false);
                LeanTween.rotate(gameObject, new Vector3(0, 0, 0), 0.01f).setEase(LeanTweenType.easeInOutQuad);
            }
            //Vector3 target = Vector3.zero;
            //Vector3 dir = target - transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.01f).setEase(LeanTweenType.easeInOutQuad);
        }
        else
        {
            Vector3 target = scanner.target.position;
            if (GameManager.instance.player_Info != null && GameManager.instance.player_Info.isLeft == true)
            {
                WeaponSpinning(true);
            }
            else
            {
                WeaponSpinning(false);
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
            StartCoroutine(MuzzleMove());
            yield return new WaitForSeconds(0.1f);

            Vector3 targetPos = scanner.target.position;
            if (targetPos.x < transform.position.x)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }
            float duration = 0;
            float elapsedTime = 0;
            float dis = Vector3.Distance(transform.position, targetPos);
            float angle = GetAngle(transform.position, targetPos);
            float returnAngle = GetAngle(GameManager.instance.player_Info.weaponMainPos.position, targetPos);

            duration = 0.014f * dis;
            coll.enabled = true;
            isFire = true; 

            ///적이 왼쪽에 있을 경우
            if (isLeft == true)
            {
                Vector3 start = ConvertAngleToVector(angle + 90, dis / 3);
                Vector3 end = ConvertAngleToVector(angle - 90, dis / 1.5f);
                Vector3 startVector = new Vector3(start.x, start.y, 0);
                Vector3 endVector = new Vector3(end.x, end.y, 0);

                Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
                //Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
                Vector3 controlVector_1 = new Vector3(con1Pos.x, start.y, 0);
                Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

                startPos.position = transform.position + endVector;
                controlPos1.position = transform.position + controlVector_2;
                controlPos2.position = transform.position + controlVector_1;
                endPos.position = transform.position + startVector;
                // 공격 시작 시 초기 회전 설정
                //baseObj.localRotation = Quaternion.Euler(0, 0, -160);
                //yield return new WaitForSeconds(duration);
                // 첫 번째 구간: startPos -> targetPos
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, startPos.position, controlPos1.position, targetPos);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                //휘두른 무기를 수거하기 직전
                yield return new WaitForSeconds(0.01f);
                elapsedTime = 0f;

                // 두 번째 구간: targetPos -> endPos
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, targetPos, controlPos2.position, endPos.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 160), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }    
            }
            ///적이 오른쪽에 있을 경우
            else
            {
                Vector3 start = ConvertAngleToVector(angle + 90, dis / 1.5f);
                Vector3 end = ConvertAngleToVector(angle - 90, dis / 3);
                Vector3 startVector = new Vector3(start.x, start.y, 0);
                Vector3 endVector = new Vector3(end.x, end.y, 0);

                Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
                //Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
                Vector3 controlVector_1 = new Vector3(con1Pos.x, start.y, 0);
                Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

                startPos.position = transform.position + startVector;
                controlPos1.position = transform.position + controlVector_1;
                controlPos2.position = transform.position + controlVector_2;
                endPos.position = transform.position + endVector;
                // 공격 시작 시 초기 회전 설정
                //baseObj.localRotation = Quaternion.Euler(0, 0, 160);
                yield return new WaitForSeconds(duration);
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, startPos.position, controlPos1.position, targetPos);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                //yield return new WaitForSeconds(0.01f);
                elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, targetPos, controlPos2.position, endPos.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -160), t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }

            // 세 번째 구간: endPos -> transform.position
            LeanTween.moveLocal(baseObj.gameObject, Vector3.zero, 0.05f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.rotateLocal(baseObj.gameObject, Vector3.zero, 0.05f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);

            coll.enabled = false;
            isFire = false;
            isLeft = false;
            scanner.target = null;
            dis = 0;
            duration = 0;
            elapsedTime = 0;
        }
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
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }

    public override void WeaponSpinning(bool isLeft)
    {
        if (isLeft == true)
        {
            for (int i = 0; i < tierOutline.Length; i++)
            {
                tierOutline[i].flipY = true;
            }
        }
        else
        {
            for (int i = 0; i < tierOutline.Length; i++)
            {
                tierOutline[i].flipY = false;
            }
        }
    }
}
