using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear_Weapon : Weapon_Action, ICustomUpdateMono
{
    private float timer;
    private float targetLockTimer; // 타겟을 유지하는 시간 타이머
    private Melee_Bullet bullet;
    private bool isFire;
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
        UpdateTierOutline();

        timer += Time.deltaTime;
        targetLockTimer += Time.deltaTime;

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
            if (GameManager.instance.playerAct.isStand && scanner.attackTarget != null )
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
            if (scanner.attackTarget != null )
            {
                if (timer >= afterCoolTime)
                {
                    StartCoroutine(Fire());
                    timer = 0;
                }
            }
        }
    }


    //Transform GetClosestTarget(List<Transform> targets)
    //{
    //    Transform closestTarget = null;
    //    float closestDistance = float.MaxValue;
    //    Vector3 currentPosition = transform.position;

    //    foreach (Transform target in targets)
    //    {
    //        if (IsInAttackRange(target))
    //        {
    //            float distance = Vector3.Distance(currentPosition, target.position);
    //            if (distance < closestDistance)
    //            {
    //                closestDistance = distance;
    //                closestTarget = target;
    //            }
    //        }
    //    }

    //    return closestTarget;
    //}

    //bool IsInAttackRange(Transform target)
    //{
    //    float distance = Vector3.Distance(transform.position, target.position);
    //    return distance <= realRange;
    //}

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

    IEnumerator Fire()
    {
        isFire = true;

        bullet.Init(afterDamage, afterPenetrate, realRange_Attack, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

        if (scanner.attackTarget != null)
        {
            Vector3 targetPos = scanner.attackTarget.position;
            Vector3 originalPos = transform.position;
            float realRanges = realRange_Attack - Vector3.Distance(baseObj.position, meleeMuzzle.position);
            StartCoroutine(MuzzleMove());
            yield return new WaitForSeconds(0.12f);
            
            Vector3 moveDir = (targetPos - originalPos).normalized;
            Vector3 destination = originalPos + moveDir * realRanges;
            float moveDuration = realRange_Attack / 160; // 속도 조정

            LeanTween.move(baseObj.gameObject, destination, moveDuration).setEase(LeanTweenType.easeInOutQuad);
            coll.enabled = true;

            yield return new WaitForSeconds(moveDuration);
            bullet.knockBack = 0;

            yield return new WaitForSeconds(moveDuration);

            coll.enabled = false;
            LeanTween.moveLocal(baseObj.gameObject, Vector3.zero, moveDuration).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(moveDuration);

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