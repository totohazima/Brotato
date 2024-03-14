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
            if (game.isEnd == false)
            {
                game.Money += moneyValue;
                game.curExp += (expValue * (1 + (game.playerInfo.expGain / 100)));
                if (game.interest > 0)
                {
                    if (game.interest < moneyValue)
                    {
                        game.Money += game.interest;
                        game.interest = 0;
                    }
                    else if (game.interest >= moneyValue)
                    {
                        game.Money += moneyValue;
                        game.interest -= moneyValue;
                    }
                }

                
            }
            else if(game.isEnd == true)
            {
                game.interest += moneyValue;
                game.curExp += (expValue * (1 + (game.playerInfo.expGain / 100)));
            }

            if (game.curHp < game.maxHp)
            {
                float monkeyChance = ItemEffect.instance.CuteMonkey();
                monkeyChance /= 100;
                float failure = 1 - monkeyChance;
                float[] chanceLise = { monkeyChance, failure };
                int index = game.Judgment(chanceLise);
                if (index == 0)
                {
                    game.curHp++;
                    string healTxt = "<color=#4CFF52>1</color>";
                    Transform text = DamageTextManager.instance.TextCreate(0, healTxt).transform;
                    text.position = transform.position;
                }
            }
            gameObject.SetActive(false);
        }
    }
}
