using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stat")]
    public EnemyName name;
    public int level;
    public float maxHealth;
    public float curHealth;
    [HideInInspector] public float healthPerWave; //���̺� �� ü�� ����ġ
    public float damage;
    [HideInInspector] public float damagePerWave;//���̺� �� ������ ����ġ 
    public float coolTime;
    public float armor;
    public float range;
    public float evasion;
    public float accuracy;
    public float minSpeed;
    public float maxSpeed;
    [HideInInspector] public float moneyDropRate; //��ȭ ��� ����
    [HideInInspector] public int moneyValue;
    [HideInInspector] public int expValue;
    [HideInInspector] public float consumableDropRate; //�Ҹ�ǰ ��� Ȯ��;
    [HideInInspector] public float lootDropRate; //����ǰ ��� Ȯ��;
    public bool isDie;

    [Header("Debuff")]
    public int ugliyToothSlow; //������ �̻� ȿ�� ���ο� �ִ� 30%

    public enum EnemyName
    {
        BASIC,
        RANGER,
        CHARGER,
        TANKER,
        BOSS,
        TREE,
        ELITE,
        SPECIAL,
    }

    public virtual IEnumerator Died()
    {
        SpawnManager.instance.enemys.Remove(gameObject);
        ugliyToothSlow = 0;
        float randomX, randomY;
        for (int i = 0; i < moneyDropRate; i++)
        {
            GameObject meterial = PoolManager.instance.Get(2);
            randomX = Random.Range(-2f, 2f);
            randomY = Random.Range(-2f, 2f);
            meterial.transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY);

            Meterial meterialScript = meterial.GetComponent<Meterial>();
            meterialScript.moneyValue = moneyValue;
            meterialScript.expValue = expValue;
        }

        float consum = consumableDropRate / 100;
        float loot = lootDropRate / 100;
        float notDrop = (100 - (consum + loot)) / 100;

        float[] chanceLise = { notDrop, consum, loot };
        int index = GameManager.instance.Judgment(chanceLise);

        switch (index)
        {
            case 0:
                break;
            case 1:
                GameObject consumable = PoolManager.instance.Get(3);
                randomX = Random.Range(-3f, 3f);
                randomY = Random.Range(-3f, 3f);
                consumable.transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY);
                break;
            case 2:
                GameObject lootCrate = PoolManager.instance.Get(4);
                randomX = Random.Range(-3f, 3f);
                randomY = Random.Range(-3f, 3f);
                lootCrate.transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY);
                break;
        }
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0f);
    }
    public void StatSetting(int index, EnemyName type)
    {
        StatReset();
        void StatReset()
        {
            EnemyStatImporter enemy = EnemyStatImporter.instance;

            maxHealth = enemy.health[index];
            healthPerWave = enemy.healthWave[index];
            damage = enemy.damage[index];
            damagePerWave = enemy.damageWave[index];
            coolTime = enemy.coolTime[index];
            armor = enemy.armor[index];
            range = enemy.range[index];
            evasion = enemy.evasion[index];
            accuracy = enemy.accuracy[index];
            minSpeed = enemy.minSpeed[index];
            maxSpeed = enemy.maxSpeed[index];
            moneyDropRate = enemy.moneyDropNum[index];
            moneyValue = enemy.moneyValue[index];
            expValue = enemy.expValue[index];
            consumableDropRate = enemy.consumDropRate[index];
            lootDropRate = enemy.LootDropRate[index];
        }

        WaveStat();
        void WaveStat()
        {
            int wave = GameManager.instance.waveLevel;

            maxHealth += healthPerWave * wave;
            damage += damagePerWave * wave;

            maxHealth += maxHealth * (GameManager.instance.enemyRiseHealth / 100);
            damage += damage * (GameManager.instance.enemyRiseDamage / 100);

            //���� 2���� ��ȯ �� ü�� 25% ����
            if(type == EnemyName.BOSS && GameManager.instance.doubleBoss == true)
            {
                maxHealth = maxHealth * 0.75f;
            }
        }

        curHealth = maxHealth;
    }
}
