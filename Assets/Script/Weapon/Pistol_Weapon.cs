using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Weapon : Weapon, ICustomUpdateMono
{
    public Weapons index;
    float timer;
    WeaponScanner scanner;
    GameManager game;

    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
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
        BulletSetting();
        scanner.radius = afterRange / 20;
        timer += Time.deltaTime;
        if(timer >= afterCoolTime)
        {
            if(scanner.target != null)
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

    private void Fire()
    {
        Vector3 dir = scanner.target.position - transform.position;
        dir = dir.normalized;
        Transform bullet = PoolManager.instance.Get(9).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dir);
        bullet.GetComponent<Bullet>().Init(afterDamage, afterPenetrate, afterRange / 20, 100, afterCriticalChance, afterCriticalDamage, afterKnockBack, dir * 200);
    }
}

