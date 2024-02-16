using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meterial : DropItem
{

    public int moneyValue;
    public int expValue;


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
            GameManager.instance.curExp += expValue;
            gameObject.SetActive(false);
        }
    }
}
