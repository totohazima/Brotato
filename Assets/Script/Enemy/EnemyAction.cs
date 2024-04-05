using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : Enemy, ICustomUpdateMono, IDamageCalculate
{
    public Animator anim;
    public CapsuleCollider coll;
    public SpriteRenderer sprite;
    public Transform textPopUpPos;
    [HideInInspector] public Transform target;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public GameManager game;
    [HideInInspector] public float hitTimer;
    [HideInInspector] public Rigidbody rigid;

    void Awake()
    {
        game = GameManager.instance;
        rigid = GetComponent<Rigidbody>();
        target = game.mainPlayer.transform;
    }
    public virtual void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        StatSetting((int)name);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public virtual void CustomUpdate()
    {
        if(curHealth <= 0)
        {
            isDie = true;
            StartCoroutine(Died());
        }
        else
        {
            isDie = false;
        }

        if(isDie)
        {
            return;
        }

        Move();
        
        if(target.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

       
    }
 
    public virtual void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = speed / 2500;
        if (ugliyToothSlow >= 1)
        {
            moveSpeed = moveSpeed - ((moveSpeed / 100) * (ugliyToothSlow * 10));
        }

        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;
        rigid.MovePosition(rigid.position + nextVec); //-nextVec 반대로 감
        rigid.velocity = Vector3.zero;

        ///이동 제한
        float x = Mathf.Clamp(transform.position.x, game.xMin, game.xMax);
        float y = Mathf.Clamp(transform.position.y, game.yMin, game.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private IEnumerator KnockBack(Vector3 playerPos, float power)
    {
        Vector3 dir = transform.position - playerPos;
        rigid.AddForce(dir.normalized * power, ForceMode.Impulse);
        yield return 0;
    }
    public virtual void DamageCalculator(float damage, int per, float accuracy, bool isCritical, float criticalDamage, float knockBack, Vector3 bulletPos)
    {
        if (ItemEffect.instance.IsUglyTooth == true)
        {
            if (ugliyToothSlow < 3)
            {
                ugliyToothSlow++;
            }
        }
       

        float finalDamage = 0;
        string damageText = null;
        if (isCritical == true)
        {
            finalDamage = damage * criticalDamage;
            damageText = "<color=yellow>" + finalDamage.ToString("F0") + "</color>";
        }
        else
        {
            finalDamage = damage;
            damageText = finalDamage.ToString("F0");
        }

        Transform text = DamageTextManager.instance.TextCreate(0, damageText).transform;
        text.position = textPopUpPos.position;

        curHealth -= finalDamage;
        StartCoroutine(KnockBack(bulletPos, knockBack * 10));
    }

  
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (game.playerInfo.isHit == false)
            {
                game.HitCalculate(damage);
            }
        }
    }


}
