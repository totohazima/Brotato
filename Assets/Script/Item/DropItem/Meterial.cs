using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meterial : DropItem
{

    public int moneyValue;
    public float expValue;


    void FixedUpdate()
    {
        if (GameManager.instance.isEnd == true) //웨이브 끝날 시 자동으로 획득
        {
            GameManager.instance.interest += moneyValue;
            GameManager.instance.curExp += expValue;
            gameObject.SetActive(false);
            
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.Money += moneyValue;
            GameManager.instance.curExp += (expValue * (1 + (GameManager.instance.playerInfo.expGain / 100)));

            float monkeyChance = ItemEffect.instance.CuteMonkey();
            monkeyChance /= 100;
            float failure = 1 - monkeyChance;
            float[] chanceLise = { monkeyChance, failure };
            int index = GameManager.instance.Judgment(chanceLise);
            if(index == 0)
            {
                GameManager.instance.curHp++;
            }
            gameObject.SetActive(false);
        }
    }
}
