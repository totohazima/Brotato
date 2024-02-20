using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    //public GameObject boom;

    public GameObject target;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target)
        {
            GameObject booms = PoolManager.instance.Get(6);
            booms.transform.position = transform.position;

            Bullet bullet = booms.GetComponent<Bullet>();
            float damage = 10 + (GameManager.instance.playerInfo.engine * 1);
            bullet.Init(damage, 10000, 100, 0f, 0f, Vector3.zero);

            //bullet.damage = 10 + (GameManager.instance.playerInfo.engine * 1);
            //bullet.per = 10000;
            //bullet.criticalChance = 0f;
            booms.SetActive(true);

            SpawnManager.instance.mines.Remove(gameObject);
            gameObject.SetActive(false);
        }
    }
}