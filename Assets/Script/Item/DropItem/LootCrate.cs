using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : DropItem
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StageManager.instance.lootChance++;

            float heal = (3f + StageManager.instance.playerInfo.consumableHeal);
            if (StageManager.instance.curHp < StageManager.instance.maxHp)
            {
                StageManager.instance.curHp += heal;
                string healTxt = "<color=#4CFF52>" + heal.ToString("F0") + "</color>";
                Transform text = DamageTextManager.instance.TextCreate(0, healTxt).transform;
                text.position = transform.position;
            }
            StageManager.instance.money += (int)StageManager.instance.playerInfo.lootInMeterial;

            gameObject.SetActive(false);
        }
    }
}
