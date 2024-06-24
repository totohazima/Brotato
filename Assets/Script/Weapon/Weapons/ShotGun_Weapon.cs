using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun_Weapon : Weapon_Action, ICustomUpdateMono
{
    private float timer;
    private float targetLockTimer; // Ÿ���� �����ϴ� �ð� Ÿ�̸�
    private WeaponScanner scanner;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform imageGroup;
    [SerializeField] private float targetLockTime = 0.6f; // Ÿ�� ���� �ð�
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

        ////Ÿ���� �׾��� ��� Ÿ�� ����
        //if (currentTarget != null)
        //{
        //    if (currentTarget.parent.gameObject.activeSelf == false)
        //    {
        //        currentTarget = null;
        //        targetLockTimer = 0;
        //    }
        //}
        //// Ÿ�� �缳��
        //if (targetLockTimer >= targetLockTime && scanner.detectedTargets != null && scanner.detectedTargets.Count > 0)
        //{
        //    if (currentTarget == null || !scanner.detectedTargets.Contains(currentTarget))
        //    {
        //        currentTarget = GetClosestTarget(scanner.detectedTargets);
        //        targetLockTimer = 0; // Ÿ���� ���������Ƿ� Ÿ�̸� �ʱ�ȭ
        //    }
        //}

        // ���� ĳ������ ��� �̵� �߿��� ���� �Ұ���
        if (GameManager.instance.character == Player.Character.SOLDIER)
        {
            if (GameManager.instance.player_Info.isStand && scanner.currentTarget != null )
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
            if (scanner.currentTarget != null )
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
        if (scanner.currentTarget == null)
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
        if (scanner.currentTarget != null)
        {
            isAttacking = true;

            Vector3 targetPos = scanner.currentTarget.position;
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

            for (int i = 0; i < bulletCount; i++)
            {
                float x = Random.Range(-7f, 7f);
                targetPos.x += x;

                Vector3 dirs = targetPos - transform.position;
                dirs = dirs.normalized;

                Transform bullet = PoolManager.instance.Get(9).transform;
                bullet.position = muzzle.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dirs);
                bullet.GetComponent<Bullet>().Init(afterDamage, afterPenetrate, realRange, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, dirs * 200);
            }


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

