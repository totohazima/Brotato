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
            StageManager game = StageManager.instance;

            float instantMagnet = game.playerInfo.instantMagnet / 100;
            float notInstant = 1 - instantMagnet;
            float[] chanceLise = { instantMagnet, notInstant };
            int index = game.Judgment(chanceLise);

            if (index == 0)
            {
                target = game.mainPlayer.transform;
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
        if(StageManager.instance.isEnd == true)
        {
            target = StageManager.instance.mainPlayer.transform;
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
                if(StageManager.instance.curHp < StageManager.instance.maxHp)
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
}
