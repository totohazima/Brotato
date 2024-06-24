using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver_Weapon : Weapon_Action, ICustomUpdateMono
{
    private float timer;
    private float mineTimer;
    private float targetLockTimer; // 타겟을 유지하는 시간 타이머
    private WeaponScanner scanner;
    private Melee_Bullet bullet;
    private bool isFire;
    private bool isTimerReset;
    [SerializeField] private Transform baseObj;
    [SerializeField] private Transform meleeMuzzle;
    [SerializeField] private CapsuleCollider coll;
    [SerializeField] private Transform imageGroup;
    [SerializeField] private float targetLockTime = 0.8f; // 타겟 유지 시간

    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
        bullet = baseObj.GetComponent<Melee_Bullet>();
    }

    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        mineTimer = 100;
        coll.enabled = false;
        ResetStat();
    }

    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {

        if (GameManager.instance.isEnd == true && isTimerReset == false)
        {
            mineTimer = 100;
            isTimerReset = true;
        }
        else if (GameManager.instance.isEnd == false)
        {
            isTimerReset = false;
        }

        ResetStat();
        AfterStatSetting();
        scanner.radius = realRange;
        StartCoroutine(MuzzleMove());
        UpdateTierOutline();

        timer += Time.deltaTime;
        //targetLockTimer += Time.deltaTime;

        //// 타겟이 죽었을 경우 타겟 제거
        //if (currentTarget != null)
        //{
        //    if (currentTarget.parent.gameObject.activeSelf == false)
        //    {
        //        currentTarget = null;
        //        targetLockTimer = 0;
        //    }
        //}

        //// 타겟 재설정
        //if (targetLockTimer >= targetLockTime && scanner.detectedTargets != null && scanner.detectedTargets.Count > 0)
        //{
        //    if (currentTarget == null || !scanner.detectedTargets.Contains(currentTarget))
        //    {
        //        currentTarget = GetClosestTarget(scanner.detectedTargets);
        //        targetLockTimer = 0; // 타겟을 변경했으므로 타이머 초기화
        //    }
        //}

        // 군인 캐릭터의 경우 이동 중에는 공격 불가능
        if (GameManager.instance.character == Player.Character.SOLDIER)
        {
            if (GameManager.instance.player_Info.isStand && scanner.currentTarget != null)
            {
                if (timer >= afterCoolTime)
                {
                    StartCoroutine(Fire());
                    timer = 0;
                }
            }
        }
        
        else
        {
            if (timer >= afterCoolTime)
            {
                StartCoroutine(Fire());
                timer = 0;
            }
        }


        if (GameManager.instance.isEnd == false)
        {
            mineTimer += Time.deltaTime;
            switch (weaponTier)
            {
                case (0):
                    if (mineTimer >= scrip.tier1_InfoStat[0])
                    {
                        StartCoroutine(SpawnManager.instance.MineSpawn(1));
                        mineTimer = 0;
                    }
                    break;
                case (1):
                    if (mineTimer >= scrip.tier2_InfoStat[0])
                    {
                        StartCoroutine(SpawnManager.instance.MineSpawn(1));
                        mineTimer = 0;
                    }
                    break;
                case (2):
                    if (mineTimer >= scrip.tier3_InfoStat[0])
                    {
                        StartCoroutine(SpawnManager.instance.MineSpawn(1));
                        mineTimer = 0;
                    }
                    break;
                case (3):
                    if (mineTimer >= scrip.tier4_InfoStat[0])
                    {
                        StartCoroutine(SpawnManager.instance.MineSpawn(1));
                        mineTimer = 0;
                    }
                    break;
            }
        }
    }
 

    void ResetStat()
    {
        StatSetting((int)index, weaponTier);
    }

    IEnumerator MuzzleMove()
    {
        if (scanner.currentTarget == null && isFire == false)
        {
            if (GameManager.instance.player_Info != null && GameManager.instance.player_Info.isLeft)
            {
                LeanTween.rotate(gameObject, new Vector3(0, 0, 180), 0.1f).setEase(LeanTweenType.easeInOutQuad);
                WeaponSpinning(true);
            }
            else
            {
                LeanTween.rotate(gameObject, new Vector3(0, 0, 0), 0.1f).setEase(LeanTweenType.easeInOutQuad);
                WeaponSpinning(false);
            }
        }
        else
        {
            Vector3 target = scanner.currentTarget.position;
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

    IEnumerator Fire()
    {
        isFire = true;

        bullet.Init(afterDamage, afterPenetrate, realRange, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

        if (scanner.currentTarget != null)
        {
            Vector3 targetPos = scanner.currentTarget.position;
            Vector3 originalPos = transform.position;
            float realRanges = realRange - Vector3.Distance(baseObj.position, meleeMuzzle.position);
            StartCoroutine(MuzzleMove());
            if (targetPos.x < transform.position.x)
            {
                WeaponSpinning(true);
            }
            else
            {
                WeaponSpinning(false);
            }
            Vector3 dir = (targetPos - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);

            Vector3 moveDir = (targetPos - originalPos).normalized;
            Vector3 destination = originalPos + moveDir * realRanges;

            float moveDuration = realRange / 160; // 속도 조정

            yield return new WaitForSeconds(0.1f);

            LeanTween.move(baseObj.gameObject, destination, moveDuration).setEase(LeanTweenType.easeInOutQuad);
            coll.enabled = true;

            yield return new WaitForSeconds(moveDuration);
            bullet.knockBack = 0;

            yield return new WaitForSeconds(realRange / 160);

            LeanTween.moveLocal(baseObj.gameObject, Vector3.zero, moveDuration).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(moveDuration);

            coll.enabled = false;
            isFire = false;
        }
    }

    void UpdateTierOutline()
    {
        for (int i = 0; i < tierOutline.Length; i++)
        {
            tierOutline[i].gameObject.SetActive(i == weaponTier);
        }
    }

    public override void WeaponSpinning(bool isLeft)
    {
        foreach (var outline in tierOutline)
        {
            outline.flipY = isLeft;
        }
    }
}