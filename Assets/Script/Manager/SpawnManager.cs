using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public GameObject[] enemyPrefab;
    public List<GameObject> enemys;
    int enemyCount; //소환되어있는 몬스터 수
    public int enemyLimit; //몬스터 소환 수 제한
    public int spawnCount; //한 번에 소환되는 몬스터 수
    float timer;
    public float spawnTime; //스폰 시간
    GameManager game;
    void Awake()
    {
        instance = this;
        game = GameManager.instance;
        Wave1();
    }

    void FixedUpdate()
    {
        if(game.isEnd == true)
        {
            return;
        }
        enemyCount = enemys.Count;
        timer += Time.deltaTime;
        if (enemyCount < enemyLimit)
        {
            if (timer >= spawnTime)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    GameObject mark = PoolManager.instance.Get(0);
                    Vector3 pos = SpawnPosition();
                    mark.transform.position = pos;
                }
                timer = 0f;
            }
        }
    }
    public void WaveSelect(int waveLevel)
    {
        switch(waveLevel)
        {
            case 1:
                Wave1();
                break;
            case 3:
                Wave2();
                break;
            case 5:
                Wave3();
                break;
            case 7:
                Wave4();
                break;
            case 9:
                Wave5();
                break;
            case 10:
                Wave6();
                break;
            default:
                break;
        }
    }
    Vector3 SpawnPosition()
    {
        Vector3 spawnPoint;
        while (true)
        { 
            float randomX = Random.Range(-25f, 25f);
            float randomY = Random.Range(-25f, 25f);

            Vector3 playerPos = game.mainPlayer.transform.position;
            Vector3 point = new Vector3(randomX, randomY);

            float distance = Vector3.Distance(playerPos, point);
            if (distance > 5)
            {
                spawnPoint = point;
                break;
            }
        }
        
        return spawnPoint;
    }
    void Wave1()
    {
        spawnTime = 2f;
        enemyLimit = 50;
        spawnCount = 1;
    }
    void Wave2()
    {
        spawnTime = 1.8f;
        enemyLimit = 50;
        spawnCount = 2;
    }
    void Wave3()
    {
        spawnTime = 1.7f;
        enemyLimit = 70;
        spawnCount = 2;
    }
    void Wave4()
    {
        spawnTime = 1.6f;
        enemyLimit = 70;
        spawnCount = 3;
    }
    void Wave5()
    {
        spawnTime = 1.5f;
        enemyLimit = 100;
        spawnCount = 3;
    }
    void Wave6()
    {
        spawnTime = 1.4f;
        enemyLimit = 100;
        spawnCount = 4;
    }
}
