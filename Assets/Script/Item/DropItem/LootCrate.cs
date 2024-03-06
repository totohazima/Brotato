using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : DropItem
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.lootChance++;
            GameManager.instance.curHp += (3f + GameManager.instance.playerInfo.consumableHeal);
            GameManager.instance.Money += ItemEffect.instance.Bag();

            gameObject.SetActive(false);
        }
    }
}
