using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Weapon : Weapon_Action, ICustomUpdateMono
{
    private float timer;
    private float targetLockTimer; // 타겟을 유지하는 시간 타이머
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform imageGroup;
    [SerializeField] private float targetLockTime = 0.6f; // 타겟 유지 시간
    [SerializeField] private bool isFire;

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
        scanner.detectedRaius = realRange_Detected;
        scanner.attackRadius = realRange_Attack;
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

        // 타겟이 공격 범위에 들어오면 즉시 공격 시도
        if (scanner.attackTarget != null)
        {
            if (timer >= afterCoolTime)
            {
                StartCoroutine(Fire());
                timer = 0;
            }
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
        return distance <= realRange_Attack;
    }

    void ResetStat()
    {
        StatSetting((int)index, weaponTier);
    }

    IEnumerator MuzzleMove()
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
        else if (scanner.detectedTarget != null && scanner.attackTarget == null && isFire == false)
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
        else if (scanner.attackTarget != null && isFire == false)
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

    IEnumerator Fire()
    {
        if (scanner.attackTarget != null)
        {
            isFire = true;

            Vector3 targetPos = scanner.attackTarget.position;
            StartCoroutine(MuzzleMove());
            yield return new WaitForSeconds(0.12f);

            Vector3 dirs = targetPos - transform.position;
            dirs = dirs.normalized;

            Transform bullet = PoolManager.instance.Get(9).transform;
            bullet.position = muzzle.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dirs);

            Bullet bulletInit = bullet.GetComponent<Bullet>();
            bulletInit.Init(afterDamage, afterPenetrate, realRange_Attack, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, dirs);

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
        for (int i = 0; i < tierOutline.Length; i++)
        {
            tierOutline[i].flipY = isLeft;
        }
    }
}

