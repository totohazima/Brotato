using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder_Weapon : Weapon, ICustomUpdateMono
{
    public Weapons index;
    SpriteRenderer sprite;
    float timer;
    public Transform muzzle;
    WeaponScanner scanner;
    GameManager game;
    float boomChance;
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
        switch(weaponTier)
        {
            case 0:
                boomChance = 50;
                break;
            case 1:
                boomChance = 65;
                break;
            case 2:
                boomChance = 80;
                break;
            case 3:
                boomChance = 100;
                break;
        }
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
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = PoolManager.instance.Get(10).transform;
        bullet.position = muzzle.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dir);
        bullet.GetComponent<Shredder_Bullet>().Init(afterDamage, afterPenetrate, afterRange, 100, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, dir * 200, boomChance);

    }
}
