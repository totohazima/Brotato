using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : DropItem
{
    void FixedUpdate()
    {
        if (GameManager.instance.isEnd == true) //웨이브 끝날 시 자동으로 획득
        {
            GameManager.instance.lootChance++;
            gameObject.SetActive(false);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.lootChance++;
            GameManager.instance.curHp += 3f;
            gameObject.SetActive(false);
        }
    }
}
