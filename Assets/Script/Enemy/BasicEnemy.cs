using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public Animator anim;
    public CapsuleCollider coll;

    Transform target;
    float moveSpeed;
    GameManager game;
    void Awake()
    {
        game = GameManager.instance;
        target = game.mainPlayer.transform;
    }

    void Update()
    {
        moveSpeed = 0.05f * (1 + (speed / 100));
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
    }
}
