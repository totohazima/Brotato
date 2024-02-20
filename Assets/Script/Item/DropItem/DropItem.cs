using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public ItemType type;
    public Transform target;

    public enum ItemType
    {
        METERIAL,
        CONSUMABLE,
        LOOT,
    }
    void OnEnable()
    {
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
    void Update()
    {
        if (target != null)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, 20f * Time.deltaTime);
            transform.position = pos;
        }
    }
    void OnDisable()
    {
        target = null;
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
