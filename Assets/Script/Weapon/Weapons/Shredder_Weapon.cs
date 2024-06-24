using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder_Weapon : Weapon_Action, ICustomUpdateMono
{
    private float timer;
    private float targetLockTimer; // 타겟을 유지하는 시간 타이머
    private WeaponScanner scanner;
    private float boomChance;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform imageGroup;
    [SerializeField] private float targetLockTime = 0.6f; // 타겟 유지 시간
    [SerializeField] private Transform currentTarget;
    [SerializeField] private bool isAttacking;

    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
    }

    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
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
        StartCoroutine(MuzzleMove());
        UpdateTierOutline();

        timer += Time.deltaTime;
        targetLockTimer += Time.deltaTime;

        //타겟이 죽었을 경우 타겟 제거
        if (currentTarget != null)
        {
            if (currentTarget.parent.gameObject.activeSelf == false)
            {
                currentTarget = null;
                targetLockTimer = 0;
            }
        }
        // 타겟 재설정
        if (targetLockTimer >= targetLockTime && scanner.detectedTargets != null && scanner.detectedTargets.Count > 0)
        {
            if (currentTarget == null || !scanner.detectedTargets.Contains(currentTarget))
            {
                currentTarget = GetClosestTarget(scanner.detectedTargets);
                targetLockTimer = 0; // 타겟을 변경했으므로 타이머 초기화
            }
        }

        // 군인 캐릭터의 경우 이동 중에는 공격 불가능
        if (GameManager.instance.character == Player.Character.SOLDIER)
        {
            if (GameManager.instance.player_Info.isStand && currentTarget != null && IsInAttackRange(currentTarget))
            {
                TryFire();
            }
        }
        else
        {
            if (currentTarget != null && IsInAttackRange(currentTarget))
            {
                TryFire();
            }
        }
    }

    void TryFire()
    {
        if (timer >= afterCoolTime)
        {
            StartCoroutine(Fire());
            timer = 0;
        }
    }

    Transform GetClosestTarget(List<Transform> targets)
    {
        Transform closestTarget = null;
        float closestDistance = float.MaxValue;
        Vector3 currentPosition = transform.position;

        foreach (Transform target in targets)
        {
            if (IsInAttackRange(target))
            {
                float distance = Vector3.Distance(currentPosition, target.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }

        return closestTarget;
    }

    bool IsInAttackRange(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= realRange;
    }

    void ResetStat()
    {
        switch (weaponTier)
        {
            case 0:
                boomChance = scrip.tier1_InfoStat[0];
                break;
            case 1:
                boomChance = scrip.tier2_InfoStat[0];
                break;
            case 2:
                boomChance = scrip.tier3_InfoStat[0];
                break;
            case 3:
                boomChance = scrip.tier4_InfoStat[0];
                break;
        }
        StatSetting((int)index, weaponTier);
    }

    IEnumerator MuzzleMove()
    {
        if (currentTarget == null)
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
            Vector3 target = currentTarget.position;
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
        if (currentTarget != null)
        {
            isAttacking = true;

            Vector3 targetPos = currentTarget.position;
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
            yield return new WaitForSeconds(0.1f);

            Transform bullet = PoolManager.instance.Get(10).transform;
            bullet.position = muzzle.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dir);

            Shredder_Bullet bulletInit = bullet.GetComponent<Shredder_Bullet>();
            bulletInit.Init(afterDamage, afterPenetrate, realRange, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, dir * 200, boomChance);

            //yield return new WaitForSeconds(0.1f);

            isAttacking = false;
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
        for (int i = 0; i < tierOutline.Length; i++)
        {
            tierOutline[i].flipY = isLeft;
        }
    }
}


