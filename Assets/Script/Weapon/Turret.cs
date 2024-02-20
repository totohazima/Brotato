using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float damage;
    public int penetrate;
    public float coolTime;
    float timer;

    FriendlyScanner scan;
    GameManager game;
    void Awake()
    {
        scan = GetComponent<FriendlyScanner>();
        game = GameManager.instance;
    }
    void Update()
    {
        damage = 10 + (game.playerInfo.engine * 0.8f);
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
        bullet.GetComponent<Bullet>().Init(damage, penetrate, 100, 0, 0, dir * 50);
    }
}
