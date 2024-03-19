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
        if(GameManager.instance.isEnd == true)
        {
            target = GameManager.instance.mainPlayer.transform;
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
        if(other.CompareTag("Magnet")) //�÷��̾� �ڼ� ������ ���� ���
        {
            if(type == ItemType.CONSUMABLE || type == ItemType.LOOT) //�Ҹ�ǰ�̳� ���ڴ� ü���� �ִ��� �� �������� ����
            {
                if(GameManager.instance.curHp < GameManager.instance.maxHp)
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
