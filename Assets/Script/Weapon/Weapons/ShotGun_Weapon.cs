using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun_Weapon : Weapon_Action, ICustomUpdateMono
{
    private float timer;
    private WeaponScanner scanner;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform imageGroup;

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

        timer += Time.deltaTime;
        //군인: 이동 중 공격 불가
        if (GameManager.instance.character == Player.Character.SOLDIER)
        {
            if (GameManager.instance.player_Info.isStand == true && scanner.target != null)
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
            if (scanner.target != null)
            {
                if (timer >= afterCoolTime)
                {
                    StartCoroutine(Fire());
                    timer = 0;
                }
            }
        }
    }

    public void ResetStat()
    {
        StatSetting((int)index, weaponTier);
    }
    private IEnumerator MuzzleMove()
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
            //LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
        }
        else
        {
            Vector3 target = scanner.target.position;
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
        yield return 0;
    }
    private IEnumerator Fire()
    {
        if (scanner.target != null)
        {
            Vector3 targetPos = scanner.target.position;
            StartCoroutine(MuzzleMove());
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < bulletCount; i++)
            {
                float x = Random.Range(-7f, 7f);
                targetPos.x += x;

                Vector3 dir = targetPos - transform.position;
                dir = dir.normalized;

                Transform bullet = PoolManager.instance.Get(9).transform;
                bullet.position = muzzle.position;
                bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dir);
                bullet.GetComponent<Bullet>().Init(afterDamage, afterPenetrate, realRange, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, dir * 200);
            }
        }
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
