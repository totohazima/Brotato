using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ICustomUpdateMono
{
    public static SpawnManager instance;
    public GameObject[] enemyPrefab;
    public Wave_Scriptable[] scrip;
    int enemyCount; //소환되어있는 몬스터 수
    public int enemyLimit; //한 웨이브에 소환될 몬스터 수
    public int spawnCount; //한 번에 소환되는 몬스터 수
    float timer;
    float treeTimer;
    public float spawnTime; //스폰 시간
    GameManager game;
    float mineTimer;
    float minesCoolTime; //지뢰 생성 쿨타임

    public List<GameObject> enemys;
    public List<GameObject> mines;
    public List<GameObject> turrets;
    public List<GameObject> trees;
    private List<GameObject>[] pools;
    void Awake()
    {
        instance = this;
        game = GameManager.instance;
        timer = 100f;
        mineTimer = 100f;
        WaveSelect(0);
        spawnTime = game.waveTime[game.waveLevel] / enemyLimit;

        scrip = new Wave_Scriptable[WaveStatImporter.instance.wave_Scriptables.Length];
        for (int i = 0; i < scrip.Length; i++)
        {
            scrip[i] = WaveStatImporter.instance.wave_Scriptables[i];
        }

        pools = new List<GameObject>[enemyPrefab.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
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
            treeTimer = 0;
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

        treeTimer += Time.deltaTime;
        if(treeTimer >= 10)
        {
            StartCoroutine(TreeSpawn());
            treeTimer = 0;
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
            mark[i].transform.position = EnemySpawnPosition();
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < spawnCount; i++)
        {
            float[] enemyChance = new float[scrip[game.waveLevel].spawnEnemys.Length];
            for (int j = 0; j < enemyChance.Length; j++)
            {
                enemyChance[j] = scrip[game.waveLevel].enemyPersentage[j];
            }
            int index = Judgment(enemyChance);
            for (int k = 0; k < enemyChance.Length; k++)
            {
                if(enemyPrefab[k].gameObject == enemyPrefab[index].gameObject)
                {
                    enemy[i] = Spawn(k);
                }
            }
            enemy[i].transform.position = mark[i].transform.position;
            enemys.Add(enemy[i]);
            mark[i].SetActive(false);
        }
    }

    public IEnumerator BossSpawn(int index)
    {
        GameObject[] mark = new GameObject[index];
        GameObject[] enemy = new GameObject[mark.Length];

        for (int i = 0; i < mark.Length; i++)
        {
            mark[i] = PoolManager.instance.Get(0);
            mark[i].transform.position = EnemySpawnPosition();
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < mark.Length; i++)
        {
            enemy[i] = Spawn(4);
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
    public IEnumerator MineSpawn(int index)
    {
        GameObject[] mark = new GameObject[index];
        GameObject[] mine = new GameObject[mark.Length];

        for (int i = 0; i < mark.Length; i++)
        {
            mark[i] = PoolManager.instance.Get(7);
            Vector3 pos = FriendlySpawnPosition();
            mark[i].transform.position = pos;
        }
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < mark.Length; i++)
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
    public IEnumerator TreeSpawn()
    {
        float treeNum = WaveStatImporter.instance.treeCount[game.waveLevel];
        float a = (treeNum + ItemEffect.instance.Tree()) * 0.33f;
        float b = (float)System.Math.Truncate(a);
        float c = (a - b) * 100;

        float someTree = 0;
        float noneTree = 0;
        if(c > 50f)
        {
            someTree = c;
            noneTree = 100 - someTree;
        }
        else if(c <= 50f)
        {
            someTree = 100 - c;
            noneTree = someTree;
        }
        float[] chanceLise = { someTree, noneTree };
        int index = GameManager.instance.Judgment(chanceLise);
        int treeCount;
        if (index == 0)
        {
            treeCount = (int)(b + 1);
        }
        else
        {
            treeCount = (int)(b);
        }

        GameObject[] mark = new GameObject[treeCount];
        GameObject[] tree = new GameObject[mark.Length];

        for (int i = 0; i < mark.Length; i++)
        {
            mark[i] = PoolManager.instance.Get(12);
            Vector3 pos = FriendlySpawnPosition();
            mark[i].transform.position = pos;
        }
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < mark.Length; i++)
        {
            tree[i] = PoolManager.instance.Get(13);
            tree[i].transform.position = mark[i].transform.position;
            trees.Add(tree[i]);
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
            if (distance > 25)
            {
                spawnPoint = point;
                break;
            }
        }
        
        return spawnPoint;
    }
    public Vector3 FriendlySpawnPosition() //아군 유닛 소환 위치
    {
        Vector3 spawnPoint;

        float randomX = Random.Range(game.xMin, game.xMax);
        float randomY = Random.Range(game.yMin, game.yMax);

        Vector3 point = new Vector3(randomX, randomY);

        spawnPoint = point;

        return spawnPoint;
    }
    private GameObject Spawn(int index)
    {
        GameObject select = null;

        //선택한 풀의 놀고있는(비활성화 된) 게임오브젝트 접근

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못 찾았으면
        if (!select)
        {
            //새롭게 생성하고 select에 할당
            select = Instantiate(enemyPrefab[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
    private int Judgment(float[] rando)
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
