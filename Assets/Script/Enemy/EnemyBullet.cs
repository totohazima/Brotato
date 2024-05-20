using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public override void FixedUpdate()
    {
        if (StageManager.instance.isEnd == true)
        {
            gameObject.SetActive(false);
        }

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
        if(damage < 0)
        {
            damage = 0;
        }
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
            PlayerAction player = StageManager.instance.playerInfo;

            if (player.whiteFlash != null)
                player.whiteFlash.PlayFlash();

            float evasion = player.evasion;
            if (player.evasion > 60)
            {
                evasion = 60;
            }
            
            float nonEvasion = 100 - evasion;
            float[] chanceLise = { evasion, nonEvasion };
            int index = StageManager.instance.Judgment(chanceLise);

            if(index == 0)
            {
                string txt = "<color=#4CFF52>회피</color>";
                Transform text = DamageTextManager.instance.TextCreate(0, txt).transform;
                text.position = player.transform.position;
            }
            else if(index == 1)
            {
                StageManager.instance.curHp -= damage;
            }
            //황소: 피격 시 폭발
            if (GameManager.instance.character == Player.Character.BULL)
            {
                GameObject booms = PoolManager.instance.Get(6);
                booms.transform.position = collision.transform.position;

                Bullet bullet = booms.GetComponent<Bullet>();
                float damage = (30 + (GameManager.instance.player_Info.meleeDamage * 3) + (GameManager.instance.player_Info.rangeDamage * 3) + (GameManager.instance.player_Info.elementalDamage * 3))
                    * (1 + (GameManager.instance.player_Info.persentDamage / 100) * (1 + (GameManager.instance.player_Info.explosiveDamage / 100)));
                bullet.Init(damage, 10000, -1000, 100, 0, 0, 0, 0, Vector3.zero);
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
