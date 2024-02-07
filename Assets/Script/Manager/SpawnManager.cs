using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public GameObject[] enemyPrefab;
    float timer;
    GameManager game;
    void Awake()
    {
        instance = this;
        game = GameManager.instance;
    }

    void FixedUpdate()
    {
        if(game.isEnd == true)
        {
            return;
        }

        timer += Time.deltaTime;

        if(timer >= 3f)
        {
            GameObject mark = PoolManager.instance.Get(0);
            mark.transform.position = Vector3.zero;
            timer = 0f;
        }
    }
}
