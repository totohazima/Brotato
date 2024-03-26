using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ICustomUpdateMono
{
    public static SpawnManager instance;
    public GameObject[] enemyPrefab;
    int enemyCount; //소환되어있는 몬스터 수
    public int enemyLimit; //한 웨이브에 소환될 몬스터 수
    public int spawnCount; //한 번에 소환되는 몬스터 수
    float timer;
    public float spawnTime; //스폰 시간
    GameManager game;
    float mineTimer;
    float minesCoolTime; //지뢰 생성 쿨타임

    public List<GameObject> enemys;
    public List<GameObject> mines;
    public List<GameObject> turrets;
    void Awake()
    {
        instance = this;
        game = GameManager.instance;
        timer = 100f;
        mineTimer = 100f;
        WaveSelect(0);
        spawnTime = game.waveTime[game.waveLevel] / enemyLimit;
    }
    void OnEnable() //생성시 티어를 정한다 (현재 1티어만 존재)
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }
    public void CustomUpdate()
    {
        if(game.isEnd == true)
        {
            enemyClear();
            return;
        }
        enemyCount = enemys.Count;
        timer += Time.deltaTime;
        if (enemyCount < enemyLimit)
        {
            if (timer >= spawnTime)
            {
                StartCoroutine(EnemySpawn());
                
                timer = 0f;
            }
        }

        //지뢰
        mineTimer += Time.deltaTime;
        minesCoolTime = 12f;
        if (mineTimer >= minesCoolTime)
        {
            StartCoroutine(MineSetting());
            mineTimer = 0f;
        }
    }
    void enemyClear()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].SetActive(false);
        }
        enemys.Clear();
    }
    IEnumerator EnemySpawn()
    {
        GameObject[] mark = new GameObject[spawnCount];
        GameObject[] enemy = new GameObject[mark.Length];

        for (int i = 0; i < spawnCount; i++)
        {
            mark[i] = PoolManager.instance.Get(0);
            Vector3 pos = EnemySpawnPosition();
            mark[i].transform.position = pos;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < spawnCount; i++)
        {
            enemy[i] = PoolManager.instance.Get(1);
            enemy[i].transform.position = mark[i].transform.position;
            enemys.Add(enemy[i]);
            mark[i].SetActive(false);
        }
    }

    public IEnumerator MineSetting()
    {
        GameObject[] mark = new GameObject[ItemEffect.instance.LandMines()];
        GameObject[] mine = new GameObject[mark.Length];

        for (int i = 0; i < mark.Length; i++)
        {
            mark[i] = PoolManager.instance.Get(7);
            Vector3 pos = FriendlySpawnPosition();
            mark[i].transform.position = pos;
        }
        yield return new WaitForSeconds(0.6f);
        for(int i = 0; i < mark.Length; i++)
        {
            mine[i] = PoolManager.instance.Get(5);
            mine[i].transform.position = mark[i].transform.position;
            mines.Add(mine[i]);
            mark[i].SetActive(false);
        }
    }

    public IEnumerator TurretSetting()
    {
        GameObject[] mark = new GameObject[ItemEffect.instance.Turret()];
        GameObject[] turret = new GameObject[mark.Length];

        for (int i = 0; i < mark.Length; i++)
        {
            mark[i] = PoolManager.instance.Get(7);
            Vector3 pos = FriendlySpawnPosition();
            mark[i].transform.position = pos;
        }
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < mark.Length; i++)
        {
            turret[i] = PoolManager.instance.Get(8);
            turret[i].transform.position = mark[i].transform.position;
            turrets.Add(turret[i]);
            mark[i].SetActive(false);
        }
    }
    public void WaveSelect(int waveLevel)
    {
        WaveStatImporter import = WaveStatImporter.instance;

        enemyLimit = import.enemyCount[waveLevel];
        spawnCount = import.enemySpawnCount[waveLevel];
    }

    Vector3 EnemySpawnPosition() //적대적 유닛 소환 위치
    {
        Vector3 spawnPoint;
        while (true)
        { 
            float randomX = Random.Range(game.xMin, game.xMax);
            float randomY = Random.Range(game.yMin, game.yMax);

            Vector3 playerPos = game.mainPlayer.transform.position;
            Vector3 point = new Vector3(randomX, randomY);

            float distance = Vector3.Distance(playerPos, point);
            if (distance > 2)
            {
                spawnPoint = point;
                break;
            }
        }
        
        return spawnPoint;
    }
    Vector3 FriendlySpawnPosition() //아군 유닛 소환 위치
    {
        Vector3 spawnPoint;

        float randomX = Random.Range(game.xMin, game.xMax);
        float randomY = Random.Range(game.yMin, game.yMax);

        Vector3 point = new Vector3(randomX, randomY);

        spawnPoint = point;

        return spawnPoint;
    }
    
}
