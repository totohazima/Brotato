using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : DropItem
{
    void FixedUpdate()
    {
        if (GameManager.instance.isEnd == true) //���̺� ���� �� �ڵ����� ȹ��
        {
            gameObject.SetActive(false);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.curHp += (3f + GameManager.instance.playerInfo.consumableHeal);
            gameObject.SetActive(false);
        }
    }
}
