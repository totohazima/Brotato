using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, ICustomUpdateMono
{
    public float damage;
    public int penetrate;
    public float coolTime;
    public float timer;
    public float basicDamage;
    public float plusDamage;
    FriendlyScanner scan;
    GameManager game;
    void Awake()
    {
        scan = GetComponent<FriendlyScanner>();
        game = GameManager.instance;
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        basicDamage = 10;
        plusDamage = 0.8f;
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }
    public void Init(float basicDamage, float plusDamage)
    {
        this.basicDamage = basicDamage;
        this.plusDamage = plusDamage;
    }
    public void CustomUpdate()
    {
        damage = basicDamage + (game.playerInfo.engine * plusDamage);
        penetrate = 0 + (game.playerInfo.penetrate);
        coolTime = 0.73f;// - ((0.73f / 100) * game.playerInfo.attackSpeed);

        timer += Time.deltaTime;
        if(timer >= coolTime)
        {
            if (scan.target != null)
            {
                Fire();
                timer = 0f;
            }
        }
    }

    void Fire()
    {
        Vector3 dir = scan.target.position - transform.position;
        dir = dir.normalized;
        Transform bullet = PoolManager.instance.Get(9).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dir);
        float penDamage = -50 + game.playerInfo.penetrateDamage;
        bullet.GetComponent<Bullet>().Init(damage, penetrate, scan.radius, 100, 0, 0, 0, penDamage,  dir * 200);
    }
}
