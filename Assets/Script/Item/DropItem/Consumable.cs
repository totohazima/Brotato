using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : DropItem
{
 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float heal = (3f + StageManager.instance.playerInfo.consumableHeal);
            if (heal < 0)
                heal = 1;
            if (GameManager.instance.playerInfo.playerHealth < StageManager.instance.maxHp)
            {
                GameManager.instance.playerInfo.playerHealth += heal;
                string healTxt = "<color=#4CFF52>" + heal.ToString("F0") + "</color>";
                Transform text = DamageTextManager.instance.TextCreate(0, healTxt).transform;
                text.position = transform.position;
            }
            gameObject.SetActive(false);
        }
    }
}
