using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meterial : DropItem
{
    public int moneyValue;
    public float expValue;

    void OnTriggerEnter(Collider other)
    {
        GameManager game = GameManager.instance;
        if(other.CompareTag("Player"))
        {
            if (GameManager.instance.isEnd == false)
            {
                game.playerInfo.money += moneyValue;
                game.playerInfo.curExp += (expValue * (1 + (game.player_Info.expGain / 100)));
                if (game.playerInfo.interest > 0)
                {
                    if (game.playerInfo.interest < moneyValue)
                    {
                        game.playerInfo.money += game.playerInfo.interest;
                        game.playerInfo.interest = 0;
                    }
                    else if (game.playerInfo.interest >= moneyValue)
                    {
                        game.playerInfo.money += moneyValue;
                        game.playerInfo.interest -= moneyValue;
                    }
                }

                
            }
            else if(GameManager.instance.isEnd == true)
            {
                game.playerInfo.interest += moneyValue;
                game.playerInfo.curExp += (expValue * (1 + (game.player_Info.expGain / 100)));
            }

            if (StageManager.instance.curHp < StageManager.instance.maxHp)
            {
                float monkeyChance = (game.player_Info.meterialHeal / 100);
                float failure = 1 - monkeyChance;
                float[] chanceLise = { monkeyChance, failure };
                int index = StageManager.instance.Judgment(chanceLise);
                if (index == 0)
                {
                    StageManager.instance.curHp++;
                    string healTxt = "<color=#4CFF52>1</color>";
                    Transform text = DamageTextManager.instance.TextCreate(0, healTxt).transform;
                    text.position = transform.position;
                }
            }
            gameObject.SetActive(false);
        }
    }
}
