using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun_Weapon : Weapon_Action, ICustomUpdateMono
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
        targetLockTimer += Time.deltaTime;

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

            for (int i = 0; i < bulletCount; i++)
            {
                float x = Random.Range(-7f, 7f);
                targetPos.x += x;

                Vector3 dirs = targetPos - transform.position;
                dirs = dirs.normalized;

                Transform bullet = PoolManager.instance.Get(9).transform;
                bullet.position = muzzle.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dirs);
                bullet.GetComponent<Bullet>().Init(afterDamage, afterPenetrate, realRange_Attack, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, dirs);
            }

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

