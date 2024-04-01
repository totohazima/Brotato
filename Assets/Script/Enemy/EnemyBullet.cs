using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public override void FixedUpdate()
    {
        if (GameManager.instance.isPause == true)
        {
            if (rigid != null)
            {
                rigid.velocity = Vector3.zero;
            }
        }
        else if (GameManager.instance.isPause == false)
        {
            if (rigid != null)
            {
                rigid.velocity = direction;
            }
        }
    }
    public void Init(float damage, int per, float range, float accuracy, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        this.range = range;
        this.accuracy = accuracy;

        if (per >= 0)
        {
            direction = dir;
            rigid.velocity = dir;
        }
    }
    public override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player"))
        {
            PlayerAction player = GameManager.instance.playerInfo;

            float evasion = player.evasion;
            if (player.evasion > 60)
            {
                evasion = 60;
            }
            
            float nonEvasion = 100 - evasion;
            float[] chanceLise = { evasion, nonEvasion };
            int index = GameManager.instance.Judgment(chanceLise);

            if(index == 0)
            {
                string txt = "<color=#4CFF52>È¸ÇÇ</color>";
                Transform text = DamageTextManager.instance.TextCreate(0, txt).transform;
                text.position = player.transform.position;
            }
            else if(index == 1)
            {
                GameManager.instance.curHp -= damage;
            }

            per--;
            if (per < 0)
            {
                if (rigid != null)
                {
                    rigid.velocity = Vector3.zero;
                }
                gameObject.SetActive(false);
            }
            

        }
    }
}
