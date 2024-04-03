using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun_Weapon : Weapon_Action, ICustomUpdateMono
{
    SpriteRenderer sprite;
    float timer;
    public Transform muzzle;
    WeaponScanner scanner;
    GameManager game;

    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
        sprite = GetComponent<SpriteRenderer>();
        game = GameManager.instance;
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
        scanner.radius = afterRange;
        StartCoroutine(MuzzleMove());

        if (scanner.target != null)
        {
            
            timer += Time.deltaTime;
            if (timer >= afterCoolTime)
            {
                Fire();
                timer = 0;
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
            Vector3 target = JoyStick.instance.moveTarget.position;
            if (target.x < transform.position.x)
            {
                sprite.flipY = true;
            }
            else
            {
                sprite.flipY = false;
            }
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
        }
        else
        {
            Vector3 target = scanner.target.position;
            if (target.x < transform.position.x)
            {
                sprite.flipY = true;
            }
            else
            {
                sprite.flipY = false;
            }
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
        }
        yield return 0;
    }
    private void Fire()
    {
        Vector3 targetPos = scanner.target.position;
        for (int i = 0; i < 4; i++)
        {
            float x = Random.Range(-7f, 7f);
            targetPos.x += x;

            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = PoolManager.instance.Get(9).transform;
            bullet.position = muzzle.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dir);
            bullet.GetComponent<Bullet>().Init(afterDamage, afterPenetrate, afterRange, 100, afterCriticalChance, afterCriticalDamage, afterKnockBack,afterPenetrateDamage, dir * 200);
            scanner.target = null;
        }
    }
}