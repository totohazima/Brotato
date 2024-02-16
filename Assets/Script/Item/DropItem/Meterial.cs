using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meterial : DropItem
{

    public int moneyValue;
    public int expValue;


    void FixedUpdate()
    {
        if (GameManager.instance.isEnd == true) //���̺� ���� �� �ڵ����� ȹ��
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
