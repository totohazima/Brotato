//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using UnityEngine;

//public class NewWrench_Weapon : Weapon_Action, ICustomUpdateMono
//{
//    [SerializeField] private Transform startPos;
//    [SerializeField] private Transform controlPos1;
//    [SerializeField] private Transform controlPos2;
//    [SerializeField] private Transform endPos;
//    [SerializeField] private Transform baseObj;
//    [SerializeField] private CapsuleCollider coll;
//    [SerializeField] private float timer;
//    private WeaponScanner scanner;
//    private StageManager stage;
//    private Melee_Bullet bullet;
//    private bool isFire;
//    private bool isLeft;
//    private bool isSpawnedTurret;
//    void Awake()
//    {
//        scanner = GetComponent<WeaponScanner>();
//        bullet = baseObj.GetComponent<Melee_Bullet>();
//        stage = StageManager.instance;
//    }
//    void OnEnable()
//    {
//        CustomUpdateManager.customUpdates.Add(this);
//        coll.enabled = false;
//        ResetStat();
//    }
//    void OnDisable()
//    {
//        CustomUpdateManager.customUpdates.Remove(this);
//    }

//    public void CustomUpdate()
//    {
//        ResetStat();
//        AfterStatSetting();
//        scanner.radius = realRange;

//        for (int i = 0; i < tierOutline.Length; i++)
//        {
//            if (i == weaponTier)
//            {
//                tierOutline[i].gameObject.SetActive(true);
//            }
//            else
//            {
//                tierOutline[i].gameObject.SetActive(false);
//            }
//        }

//        if (isFire == false)
//        {
//            StartCoroutine(MuzzleMove());
//        }

//        timer += Time.deltaTime;
//        //����: �̵� �� ���� �Ұ�
//        if (GameManager.instance.character == Player.Character.SOLDIER)
//        {
//            if (GameManager.instance.player_Info.isStand == true && scanner.target != null)
//            {
//                if (timer >= afterCoolTime && isFire == false)
//                {
//                    StartCoroutine(WheelAttack());
//                    timer = 0;
//                }
//            }
//        }
//        else
//        {
//            if (scanner.target != null)
//            {
//                if (timer >= afterCoolTime && isFire == false)
//                {
//                    StartCoroutine(WheelAttack());
//                    timer = 0;
//                }
//            }
//        }

//        if (GameManager.instance.isEnd == true)
//        {
//            isSpawnedTurret = false;
//        }
//        else if (GameManager.instance.isEnd == false && isSpawnedTurret == false)
//        {
//            isSpawnedTurret = true;
//            StartCoroutine(SpawnTurret());
//        }
//    }

//    public void ResetStat()
//    {
//        StatSetting((int)index, weaponTier);
//    }
//    private IEnumerator MuzzleMove() //���� ����� ������ ������ ������ ȸ���ϸ� �ȵ�
//    {
//        if (scanner.target == null)
//        {
//            if (GameManager.instance.player_Info != null && GameManager.instance.player_Info.isLeft == true)
//            {
//                WeaponSpinning(true);
//                LeanTween.rotate(gameObject, new Vector3(0, 0, 180), 0.01f).setEase(LeanTweenType.easeInOutQuad);
//            }
//            else
//            {
//                WeaponSpinning(false);
//                LeanTween.rotate(gameObject, new Vector3(0, 0, 0), 0.01f).setEase(LeanTweenType.easeInOutQuad);
//            }
//            //Vector3 target = Vector3.zero;
//            //Vector3 dir = target - transform.position;
//            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//            //LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.01f).setEase(LeanTweenType.easeInOutQuad);
//        }
//        else
//        {
//            Vector3 target = scanner.target.position;
//            if (GameManager.instance.player_Info != null && GameManager.instance.player_Info.isLeft == true)
//            {
//                WeaponSpinning(true);
//            }
//            else
//            {
//                WeaponSpinning(false);
//            }
//            Vector3 dir = target - transform.position;
//            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
//        }
//        yield return 0;
//    }

//    private IEnumerator WheelAttack()
//    {
//        bullet.Init(afterDamage, afterPenetrate, realRange, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

//        if (scanner.target != null)
//        {
//            StartCoroutine(MuzzleMove());
//            yield return new WaitForSeconds(0.1f);

//            Vector3 targetPos = scanner.target.position;
//            if (targetPos.x < transform.position.x)
//            {
//                isLeft = true;
//            }
//            else
//            {
//                isLeft = false;
//            }
//            float duration = 0;
//            float elapsedTime = 0;
//            float dis = Vector3.Distance(transform.position, targetPos);
//            float angle = GetAngle(transform.position, targetPos);

//            duration = 0.014f * dis;
//            coll.enabled = true;
//            isFire = true; 

//            ///���� ���ʿ� ���� ���
//            if (isLeft == true)
//            {
//                //Vector3 start = ConvertAngleToVector(angle + 90, dis / 2);
//                //Vector3 end = ConvertAngleToVector(angle - 90, dis / 2);
//                //Vector3 startVector = new Vector3(start.x, start.y, 0);
//                //Vector3 endVector = new Vector3(end.x, end.y, 0);

//                //Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
//                ////Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
//                //Vector3 controlVector_1 = new Vector3(start.x, start.y, 0);
//                //Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

//                //startPos.position = transform.position + endVector;
//                //controlPos1.position = transform.position + controlVector_2;
//                //controlPos2.position = transform.position + controlVector_1;
//                //endPos.position = transform.position + startVector;

//                Vector3 start = GetPositionAtAngle(targetPos, 134, dis / 2f);
//                Vector3 end = GetPositionAtAngle(targetPos, -90f, dis / 1.5f);
//                Vector3 startVector = start;
//                Vector3 endVector = end;

//                startPos.position = transform.position + startVector;
//                endPos.position = transform.position + endVector;

//                // ù ��° ����: startPos -> targetPos
//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPos1.position, startPos.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }

//                elapsedTime = 0f;

//                // �� ��° ����: targetPos -> endPos
//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, startPos.position, controlPos2.position, endPos.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 160), t);

//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }

//                // �� ��° ����: endPos -> transform.position
//                yield return new WaitForSeconds(0.05f);
//                elapsedTime = 0f;
//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, endPos.position, transform.position, transform.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);

//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }
//                isFire = false;
//                isLeft = false;
//                scanner.target = null;
//                dis = 0;
//                duration = 0;
//                elapsedTime = 0;
//            }
//            ///���� �����ʿ� ���� ���
//            else
//            {
//                //Vector3 start = ConvertAngleToVector(angle + 90, dis / 2);
//                //Vector3 end = ConvertAngleToVector(angle - 90, dis / 2);
//                //Vector3 startVector = new Vector3(start.x, start.y, 0);
//                //Vector3 endVector = new Vector3(end.x, end.y, 0);

//                //Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
//                ////Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
//                //Vector3 controlVector_1 = new Vector3(start.x, start.y, 0);
//                //Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

//                //startPos.position = transform.position + startVector;
//                //controlPos1.position = transform.position + controlVector_1;
//                //controlPos2.position = transform.position + controlVector_2;
//                //endPos.position = transform.position + endVector;
//                Vector3 start = GetPositionAtAngle(targetPos, 46.5f, dis / 2f);
//                Vector3 end = GetPositionAtAngle(targetPos, -90, dis / 1.5f);
//                Vector3 startVector = start;
//                Vector3 endVector = end;

//                startPos.position = transform.position + startVector;
//                endPos.position = transform.position + endVector;

//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPos1.position, startPos.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);
//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }

//                //yield return new WaitForSeconds(0.01f);
//                elapsedTime = 0f;

//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, startPos.position, controlPos2.position, endPos.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -160), t);
//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }
//            }

//            // �� ��° ����: endPos -> transform.position
//            yield return new WaitForSeconds(0.05f);
//            elapsedTime = 0f;
//            while (elapsedTime < duration)
//            {
//                float t = elapsedTime / duration;
//                Vector3 point = CalculateQuadraticBezierPoint(t, endPos.position, transform.position, transform.position);

//                baseObj.position = point;
//                baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);
//                elapsedTime += Time.deltaTime;
//                yield return null;
//            }

//            coll.enabled = false;
//            isFire = false;
//            isLeft = false;
//            scanner.target = null;
//            dis = 0;
//            duration = 0;
//            elapsedTime = 0;
//        }
//    }

//    private IEnumerator SpawnTurret()
//    {
//        int index = 0;
//        switch (weaponTier)
//        {
//            case (0):
//                index = (int)scrip.tier1_InfoStat[2];
//                break;
//            case (1):
//                index = (int)scrip.tier2_InfoStat[2];
//                break;
//            case (2):
//                index = (int)scrip.tier3_InfoStat[2];
//                break;
//            case (3):
//                index = (int)scrip.tier4_InfoStat[2];
//                break;
//        }
//        GameObject[] mark = new GameObject[index];
//        GameObject[] turret = new GameObject[mark.Length];
//        //�����Ͼ�: ���๰�� ���� ������ ������
//        if (GameManager.instance.character == Player.Character.ENGINEER)
//        {
//            for (int i = 0; i < mark.Length; i++)
//            {
//                mark[i] = PoolManager.instance.Get(7);
//                float distance = Random.Range(2f, 30f);
//                Vector2 randomDirection = Random.insideUnitCircle.normalized;
//                Vector2 pos = GameManager.instance.playerInfo.engineerBuildingPos + randomDirection * distance;
//                if (pos.x > stage.xMax)
//                {
//                    pos.x = stage.xMax;
//                }
//                else if (pos.x < stage.xMin)
//                {
//                    pos.x = stage.xMin;
//                }
//                if (pos.y > stage.yMax)
//                {
//                    pos.y = stage.yMax;
//                }
//                else if (pos.y < stage.yMin)
//                {
//                    pos.y = stage.yMin;
//                }
//                mark[i].transform.position = pos;
//            }
//        }
//        else
//        {
//            for (int i = 0; i < mark.Length; i++)
//            {
//                mark[i] = PoolManager.instance.Get(7);
//                Vector3 pos = SpawnManager.instance.FriendlySpawnPosition();
//                mark[i].transform.position = pos;
//            }
//        }
//        yield return new WaitForSeconds(0.6f);
//        for (int i = 0; i < mark.Length; i++)
//        {
//            turret[i] = PoolManager.instance.Get(8);
//            turret[i].transform.position = mark[i].transform.position;
//            switch (weaponTier)
//            {
//                case (0):
//                    turret[i].GetComponent<Turret>().Init(scrip.tier1_InfoStat[0], scrip.tier1_InfoStat[1]);
//                    break;
//                case (1):
//                    turret[i].GetComponent<Turret>().Init(scrip.tier2_InfoStat[0], scrip.tier2_InfoStat[1]);
//                    break;
//                case (2):
//                    turret[i].GetComponent<Turret>().Init(scrip.tier3_InfoStat[0], scrip.tier3_InfoStat[1]);
//                    break;
//                case (3):
//                    turret[i].GetComponent<Turret>().Init(scrip.tier4_InfoStat[0], scrip.tier4_InfoStat[1]);
//                    break;
//            }
//            SpawnManager.instance.turrets.Add(turret[i]);
//            mark[i].SetActive(false);
//        }
//    }
//    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
//    {
//        float u = 1 - t;
//        float tt = t * t;
//        float uu = u * u;

//        Vector3 p = uu * p0;
//        p += 2 * u * t * p1;
//        p += tt * p2;

//        return p;
//    }

//    public override void WeaponSpinning(bool isLeft)
//    {
//        if (isLeft == true)
//        {
//            for (int i = 0; i < tierOutline.Length; i++)
//            {
//                tierOutline[i].flipY = true;
//            }
//        }
//        else
//        {
//            for (int i = 0; i < tierOutline.Length; i++)
//            {
//                tierOutline[i].flipY = false;
//            }
//        }
//    }
//}
//using System.Collections;
//using UnityEngine;

//public class NewWrench_Weapon : Weapon_Action, ICustomUpdateMono
//{
//    [SerializeField] private Transform startPos;
//    [SerializeField] private Transform controlPos1;
//    [SerializeField] private Transform controlPos2;
//    [SerializeField] private Transform endPos;
//    [SerializeField] private Transform baseObj;
//    [SerializeField] private CapsuleCollider coll;
//    [SerializeField] private float timer;
//    private WeaponScanner scanner;
//    private StageManager stage;
//    private Melee_Bullet bullet;
//    private bool isFire;
//    private bool isLeft;
//    private bool isSpawnedTurret;

//    void Awake()
//    {
//        scanner = GetComponent<WeaponScanner>();
//        bullet = baseObj.GetComponent<Melee_Bullet>();
//        stage = StageManager.instance;
//    }

//    void OnEnable()
//    {
//        CustomUpdateManager.customUpdates.Add(this);
//        coll.enabled = false;
//        ResetStat();
//    }

//    void OnDisable()
//    {
//        CustomUpdateManager.customUpdates.Remove(this);
//        if (scanner.currentTarget != null)
//        {
//            StageManager.instance.trackedTargets.Remove(scanner.currentTarget);
//        }
//    }

//    public void CustomUpdate()
//    {
//        ResetStat();
//        AfterStatSetting();
//        scanner.radius = realRange;

//        for (int i = 0; i < tierOutline.Length; i++)
//        {
//            tierOutline[i].gameObject.SetActive(i == weaponTier);
//        }

//        if (!isFire && scanner.currentTarget != null && timer >= afterCoolTime)
//        {
//            StartCoroutine(WheelAttack());
//            timer = 0;
//        }

//        timer += Time.deltaTime;

//        if (GameManager.instance.isEnd)
//        {
//            isSpawnedTurret = false;
//        }
//        else if (!isSpawnedTurret)
//        {
//            isSpawnedTurret = true;
//           // StartCoroutine(SpawnTurret());
//        }
//    }

//    public void ResetStat()
//    {
//        StatSetting((int)index, weaponTier);
//    }

//    private IEnumerator MuzzleMove()
//    {
//        if (scanner.currentTarget == null)
//        {
//            if (GameManager.instance.player_Info != null && GameManager.instance.player_Info.isLeft)
//            {
//                WeaponSpinning(true);
//                LeanTween.rotate(gameObject, new Vector3(0, 0, 180), 0.01f).setEase(LeanTweenType.easeInOutQuad);
//            }
//            else
//            {
//                WeaponSpinning(false);
//                LeanTween.rotate(gameObject, new Vector3(0, 0, 0), 0.01f).setEase(LeanTweenType.easeInOutQuad);
//            }
//        }
//        else
//        {
//            Vector3 target = scanner.currentTarget.position;
//            WeaponSpinning(GameManager.instance.player_Info != null && GameManager.instance.player_Info.isLeft);
//            Vector3 dir = target - transform.position;
//            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
//        }
//        yield return null;
//    }
//    private IEnumerator WheelAttack()
//    {
//        bullet.Init(afterDamage, afterPenetrate, realRange, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

//        if (scanner.currentTarget != null)
//        {
//            StartCoroutine(MuzzleMove());
//            yield return new WaitForSeconds(0.1f);

//            Vector3 targetPos = scanner.currentTarget.position;
//            if (targetPos.x < transform.position.x)
//            {
//                isLeft = true;
//            }
//            else
//            {
//                isLeft = false;
//            }
//            float duration = 0;
//            float elapsedTime = 0;
//            float dis = Vector3.Distance(transform.position, targetPos);
//            float angle = GetAngle(transform.position, targetPos);

//            duration = 0.014f * dis;
//            coll.enabled = true;
//            isFire = true;

//            ///���� ���ʿ� ���� ���
//            if (isLeft == true)
//            {
//                //Vector3 start = ConvertAngleToVector(angle + 90, dis / 2);
//                //Vector3 end = ConvertAngleToVector(angle - 90, dis / 2);
//                //Vector3 startVector = new Vector3(start.x, start.y, 0);
//                //Vector3 endVector = new Vector3(end.x, end.y, 0);

//                //Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
//                ////Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
//                //Vector3 controlVector_1 = new Vector3(start.x, start.y, 0);
//                //Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

//                //startPos.position = transform.position + endVector;
//                //controlPos1.position = transform.position + controlVector_2;
//                //controlPos2.position = transform.position + controlVector_1;
//                //endPos.position = transform.position + startVector;

//                Vector3 start = GetPositionAtAngle(targetPos, 134, dis / 2f);
//                Vector3 end = GetPositionAtAngle(targetPos, -90f, dis / 1.5f);
//                Vector3 startVector = start;
//                Vector3 endVector = end;

//                startPos.position = transform.position + startVector;
//                endPos.position = transform.position + endVector;

//                // ù ��° ����: startPos -> targetPos
//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPos1.position, startPos.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }

//                elapsedTime = 0f;

//                // �� ��° ����: targetPos -> endPos
//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, startPos.position, controlPos2.position, endPos.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 160), t);

//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }

//                // �� ��° ����: endPos -> transform.position
//                yield return new WaitForSeconds(0.05f);
//                elapsedTime = 0f;
//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, endPos.position, transform.position, transform.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);

//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }

//                dis = 0;
//                duration = 0;
//                elapsedTime = 0;
//            }
//            ///���� �����ʿ� ���� ���
//            else
//            {
//                //Vector3 start = ConvertAngleToVector(angle + 90, dis / 2);
//                //Vector3 end = ConvertAngleToVector(angle - 90, dis / 2);
//                //Vector3 startVector = new Vector3(start.x, start.y, 0);
//                //Vector3 endVector = new Vector3(end.x, end.y, 0);

//                //Vector3 con1Pos = ConvertAngleToVector(angle + 45, dis);
//                ////Vector3 con2Pos = ConvertAngleToVector(returnAngle - 45, dis);
//                //Vector3 controlVector_1 = new Vector3(start.x, start.y, 0);
//                //Vector3 controlVector_2 = new Vector3(end.x, end.y, 0);

//                //startPos.position = transform.position + startVector;
//                //controlPos1.position = transform.position + controlVector_1;
//                //controlPos2.position = transform.position + controlVector_2;
//                //endPos.position = transform.position + endVector;
//                Vector3 start = GetPositionAtAngle(targetPos, 46.5f, dis / 2f);
//                Vector3 end = GetPositionAtAngle(targetPos, -90, dis / 1.5f);
//                Vector3 startVector = start;
//                Vector3 endVector = end;

//                startPos.position = transform.position + startVector;
//                endPos.position = transform.position + endVector;

//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPos1.position, startPos.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);
//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }

//                //yield return new WaitForSeconds(0.01f);
//                elapsedTime = 0f;

//                while (elapsedTime < duration)
//                {
//                    float t = elapsedTime / duration;
//                    Vector3 point = CalculateQuadraticBezierPoint(t, startPos.position, controlPos2.position, endPos.position);

//                    baseObj.position = point;
//                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -160), t);
//                    elapsedTime += Time.deltaTime;
//                    yield return null;
//                }
//            }

//            // �� ��° ����: endPos -> transform.position
//            yield return new WaitForSeconds(0.05f);
//            elapsedTime = 0f;
//            while (elapsedTime < duration)
//            {
//                float t = elapsedTime / duration;
//                Vector3 point = CalculateQuadraticBezierPoint(t, endPos.position, transform.position, transform.position);

//                baseObj.position = point;
//                baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);
//                elapsedTime += Time.deltaTime;
//                yield return null;
//            }

//            dis = 0;
//            duration = 0;
//            elapsedTime = 0;
//        }

//        StageManager.instance.trackedTargets.Remove(scanner.currentTarget);
//        scanner.currentTarget = null;
//        coll.enabled = false;
//        isFire = false;
//        isLeft = false;
//    }

//    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
//    {
//        float u = 1 - t;
//        float tt = t * t;
//        float uu = u * u;

//        Vector3 p = uu * p0;
//        p += 2 * u * t * p1;
//        p += tt * p2;

//        return p;
//    }
//}
using System.Collections;
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
    private StageManager stage;
    private Melee_Bullet bullet;
    [SerializeField] private bool isFire;
    [SerializeField] private bool isLeft;
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
        scanner.detectedRaius = realRange_Detected;
        scanner.attackRadius = realRange_Attack;
        StartCoroutine(MuzzleMove());

        for (int i = 0; i < tierOutline.Length; i++)
        {
            tierOutline[i].gameObject.SetActive(i == weaponTier);
        }

        if (!isFire && scanner.attackTarget != null && timer >= afterCoolTime)
        {
            StartCoroutine(WheelAttack());
            timer = 0;
        }

        timer += Time.deltaTime;

        if (GameManager.instance.isEnd)
        {
            isSpawnedTurret = false;
        }
        else if (!isSpawnedTurret)
        {
            isSpawnedTurret = true;
            StartCoroutine(SpawnTurret());
        }
    }

    public void ResetStat()
    {
        StatSetting((int)index, weaponTier);
    }

    private IEnumerator MuzzleMove()
    {
       if (scanner.detectedTarget == null && isFire == false)
        {
            Vector3 dir = new Vector3(GameManager.instance.playerAct.joyStick.Horizontal, GameManager.instance.playerAct.joyStick.Vertical, 0);
            dir.Normalize();
            float angle = GetAngle(Vector2.zero, dir);
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.01f).setEase(LeanTweenType.easeInOutQuad);

            if (GameManager.instance.playerAct != null && GameManager.instance.playerAct.isLeft)
            {

                WeaponSpinning(true);
            }
            else
            {
                WeaponSpinning(false);
            }
        }
        else if(scanner.detectedTarget != null && scanner.attackTarget == null && isFire == false)
        {
            Vector3 target = scanner.detectedTarget.position;
            if (target.x < transform.position.x)
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
        else if(scanner.attackTarget != null && isFire == false)
        {
            Vector3 target = scanner.attackTarget.position;
            if (target.x < transform.position.x)
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
        yield return null;
    }

    private IEnumerator WheelAttack()
    {
        bool isAttack = IsRangeInTarget(scanner.attackTarget, realRange_Attack);

        if (isAttack == true)
        {
            bullet.Init(afterDamage, afterPenetrate, realRange_Attack, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

            if (scanner.attackTarget != null)
            {
                Vector3 targetPos = scanner.attackTarget.position;
                StartCoroutine(MuzzleMove());
                yield return new WaitForSeconds(0.12f);

                isLeft = targetPos.x < transform.position.x;
                float dis = Vector3.Distance(transform.position, targetPos);
                float duration = 0.014f * dis;

                Vector3 startOffset = Vector3.zero;
                Vector3 endOffset = Vector3.zero;

                if (isLeft)
                {
                    startOffset = GetPositionAtAngle(targetPos, 134, dis / 2f); // Adjust angle and distance as needed
                    endOffset = GetPositionAtAngle(targetPos, -90f, dis / 1.5f); // Adjust angle and distance as needed
                }
                else
                {
                    startOffset = GetPositionAtAngle(targetPos, 46.5f, dis / 2f); // Adjust angle and distance as needed
                    endOffset = GetPositionAtAngle(targetPos, -90, dis / 1.5f); // Adjust angle and distance as needed
                }

                startPos.position = transform.position + startOffset;
                endPos.position = transform.position + endOffset;

                float elapsedTime = 0f;
                coll.enabled = true;
                isFire = true;
                // ù ��° ����: startPos -> targetPos
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPos1.position, startPos.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // �� ��° ����: targetPos -> endPos
                elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, startPos.position, controlPos2.position, endPos.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 160), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // �� ��° ����: endPos -> transform.position
                yield return new WaitForSeconds(0.05f);
                elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, endPos.position, transform.position, transform.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                dis = 0;
                duration = 0;
                elapsedTime = 0;
            }

            // Clear currentTarget and reset states
            StageManager.instance.trackedTargets.Remove(scanner.attackTarget);
            scanner.attackTarget = null;
            coll.enabled = false;
            isFire = false;
            isLeft = false;
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
        //�����Ͼ�: ���๰�� ���� ������ ������
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
}


