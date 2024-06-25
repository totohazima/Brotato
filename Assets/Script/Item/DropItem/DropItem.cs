using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour, ICustomUpdateMono
{
    public ItemType type;
    public Transform target;
    float moveSpeed;
    public enum ItemType
    {
        METERIAL,
        CONSUMABLE,
        LOOT,
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        moveSpeed = 100f;
        if (type == ItemType.METERIAL)
        {
            GameManager game = GameManager.instance;

            float instantMagnet = game.playerAct.instantMagnet / 100;
            float notInstant = 1 - instantMagnet;
            float[] chanceLise = { instantMagnet, notInstant };
            int index = Judgment(chanceLise);

            if (index == 0)
            {
                target = game.playerTrans;
            }
        }
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
        target = null;
    }

    public void CustomUpdate()
    {
        if(GameManager.instance.isEnd == true)
        {
            target = GameManager.instance.playerTrans;
        }

        if (target != null)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            transform.position = pos;
            moveSpeed += (moveSpeed / 50);
        }
    }
   
   
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Magnet")) //플레이어 자석 범위에 닿을 경우
        {
            if(type == ItemType.CONSUMABLE || type == ItemType.LOOT) //소모품이나 상자는 체력이 최대일 때 끌려오지 않음
            {
                if(GameManager.instance.playerInfo.playerHealth < StageManager.instance.maxHp)
                {
                    target = other.transform;
                }
            }
            else
            {
                target = other.transform;
            }
            
        }
    }

    public int Judgment(float[] rando)
    {
        int count = rando.Length;
        float max = 0;
        for (int i = 0; i < count; i++)
            max += rando[i];

        float range = UnityEngine.Random.Range(0f, (float)max);
        //0.1, 0.2, 30, 40
        double chance = 0;
        for (int i = 0; i < count; i++)
        {
            chance += rando[i];
            if (range > chance)
                continue;

            return i;
        }

        return -1;
    }
}
