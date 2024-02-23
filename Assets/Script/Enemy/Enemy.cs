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
    public float healthPerWave; //���̺� �� ü�� ����ġ
    public float damage;
    public float damagePerWave;//���̺� �� ������ ����ġ 
    public float coolTime;
    public float armor;
    public float range;
    public float evasion;
    public float accuracy;
    public float minSpeed;
    public float maxSpeed;
    public float moneyDropRate; //��ȭ ��� ����
    public int moneyValue;
    public int expValue; 
    public float consumableDropRate; //�Ҹ�ǰ ��� Ȯ��;
    public float lootDropRate; //����ǰ ��� Ȯ��;
    public bool isDie;
    public bool isHit; //�÷��̾�� �΋H�� ��� 1�� ���� �÷��̾�� �ǰ������� ���� ����

    [Header("Debuff")]
    public int ugliyToothSlow; //������ �̻� ȿ�� ���ο� �ִ� 30%
    public enum EnemyName
    {
        BASIC,
    }
    public IEnumerator Died()
    {
        SpawnManager.instance.enemys.Remove(gameObject);
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
    public void StatSetting(int index)
    {
        EnemyStatImporter enemy = EnemyStatImporter.instance;

        maxHealth = enemy.health[index];
        curHealth = maxHealth;
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
}
