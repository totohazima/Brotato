using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : DropItem
{
 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float heal = (3f + GameManager.instance.playerInfo.consumableHeal);
            if (GameManager.instance.curHp < GameManager.instance.maxHp)
            {
                GameManager.instance.curHp += heal;
                string healTxt = "<color=#4CFF52>" + heal.ToString("F0") + "</color>";
                Transform text = DamageTextManager.instance.TextCreate(0, healTxt).transform;
                text.position = transform.position;
            }
            gameObject.SetActive(false);
        }
    }
}
