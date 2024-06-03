using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ICustomUpdateMono
{
    public static SpawnManager instance;
    public GameObject[] enemyPrefab;
    public Wave_Scriptable[] scrip;
    int enemyCount; //��ȯ�Ǿ��ִ� ���� ��
    public int enemyLimit; //�� ���̺꿡 ��ȯ�� ���� ��
    public int spawnCount; //�� ���� ��ȯ�Ǵ� ���� ��
    float timer;
    float treeTimer;
    public float spawnTime; //���� �ð�
    StageManager stage;
    float mineTimer;
    float minesCoolTime; //���� ���� ��Ÿ��

    public List<EnemyAction> enemys;
    public List<GameObject> mines;
    public List<GameObject> turrets;
    public List<GameObject> trees;
    private List<GameObject>[] pools;
    void Awake()
    {
        instance = this;
        stage = StageManager.instance;
        timer = 100f;
        mineTimer = 100f;
        WaveSelect(0);

        scrip = new Wave_Scriptable[GameManager.instance.wave_Scriptables.Length];
        for (int i = 0; i < scrip.Length; i++)
        {
            scrip[i] = GameManager.instance.wave_Scriptables[i];
        }

        pools = new List<GameObject>[enemyPrefab.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    void OnEnable() //������ Ƽ� ���Ѵ� (���� 1Ƽ� ����)
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }
    public void CustomUpdate()
    {
        if (GameManager.instance.isEnd == true)
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

        //����
        mineTimer += Time.deltaTime;
        minesCoolTime = 12f;
        if (mineTimer >= minesCoolTime)
        {
            StartCoroutine(MineSetting());
            mineTimer = 0f;
        }

        treeTimer += Time.deltaTime;
        if (treeTimer >= 10)
        {
            StartCoroutine(TreeSpawn());
            treeTimer = 0;
        }
    }
    void enemyClear()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].gameObject.SetActive(false);
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
            float[] enemyChance = new float[scrip[stage.waveLevel].spawnEnemys.Length];
            for (int j = 0; j < enemyChance.Length; j++)
            {
                enemyChance[j] = scrip[stage.waveLevel].enemyPersentage[j];
            }
            int index = Judgment(enemyChance);
            for (int k = 0; k < enemyChance.Length; k++)
            {
                if (enemyPrefab[k].gameObject == enemyPrefab[index].gameObject)
                {
                    enemy[i] = Spawn(k);
                }
            }
            enemy[i].transform.position = mark[i].transform.position;
            //enemys.Add(enemy[i]);
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
            //enemys.Add(enemy[i]);
            mark[i].SetActive(false);
        }
    }
    public IEnumerator MineSetting()
    {
        GameObject[] mark = new GameObject[GameManager.instance.minesCount];
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
        GameObject[] mark = new GameObject[GameManager.instance.turretCount];
        GameObject[] turret = new GameObject[mark.Length];

        //�����Ͼ�: ���๰�� ���� ������ ������
        if (GameManager.instance.character == Player.Character.ENGINEER)
        {
            for (int i = 0; i < mark.Length; i++)
            {
                mark[i] = PoolManager.instance.Get(7);
                float distance = Random.Range(2f, 30f);
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 pos = GameManager.instance.engineerBuildingPos + randomDirection * distance;
                if (pos.x > stage.xMax)
                {
                    pos.x = stage.xMax;
                }
                else if (pos.x < stage.xMin)
                {
                    pos.x = stage.xMin;
                }
                if (pos.y > stage.yMax)
                {
                    pos.y = stage.yMax;
                }
                else if (pos.y < stage.yMin)
                {
                    pos.y = stage.yMin;
                }
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
        else
        {
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
    }
    public IEnumerator TreeSpawn()
    {
        float treeNum = stage.waveStat.table[stage.waveLevel].waveTreeStat;
        float a = (treeNum + stage.playerInfo.tree) * 0.33f;
        float b = (float)System.Math.Truncate(a);
        float c = (a - b) * 100;

        float someTree = 0;
        float noneTree = 0;
        if (c > 50f)
        {
            someTree = c;
            noneTree = 100 - someTree;
        }
        else if (c <= 50f)
        {
            someTree = 100 - c;
            noneTree = someTree;
        }
        float[] chanceLise = { someTree, noneTree };
        int index = StageManager.instance.Judgment(chanceLise);
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
        WaveStatInfoTable.Data import = GameManager.instance.gameDataBase.waveStatInfoTable.table[waveLevel];

        enemyLimit = (int)import.maxEnemySpawn;
        spawnTime = import.enemySpawnTime;
        spawnCount = (int)import.enemySpawnCount;
    }

    Vector3 EnemySpawnPosition() //������ ���� ��ȯ ��ġ
    {
        Vector3 spawnPoint;
        while (true)
        {
            float randomX = Random.Range(stage.xMin, stage.xMax);
            float randomY = Random.Range(stage.yMin, stage.yMax);

            Vector3 playerPos = stage.mainPlayer.transform.position;
            Vector3 point = new Vector3(randomX, randomY);

            float distance = Vector3.Distance(playerPos, point);
            if (distance > 25)
            {
                spawnPoint = point;
                break;
            }
            InfiniteLoopDetector.Run();
        }

        return spawnPoint;
    }
    public Vector3 FriendlySpawnPosition() //�Ʊ� ���� ��ȯ ��ġ
    {
        Vector3 spawnPoint;

        float randomX = Random.Range(stage.xMin, stage.xMax);
        float randomY = Random.Range(stage.yMin, stage.yMax);

        Vector3 point = new Vector3(randomX, randomY);

        spawnPoint = point;

        return spawnPoint;
    }
    private GameObject Spawn(int index)
    {
        GameObject select = null;

        //������ Ǯ�� ����ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //�� ã������
        if (!select)
        {
            //���Ӱ� �����ϰ� select�� �Ҵ�
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
