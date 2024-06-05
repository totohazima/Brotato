using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : EnemyAction
{
    public override void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        SpawnManager.instance.trees.Add(gameObject);
        StatSetting(name, enemyType);
    }
    public override void CustomUpdate()
    {
        if (curHealth <= 0)
        {
            isDie = true;
            StartCoroutine(Died(false));
        }
        else
        {
            isDie = false;
        }

        if (isDie)
        {
            return;
        }

    }

    public override void DamageCalculator(float damage, int per, float accuracy, float bloodSuck, bool isCritical, float criticalDamage, float knockBack, Vector3 bulletPos)
    {
        float finalDamage = 0;
        string damageText = null;
        if (isCritical == true)
        {
            finalDamage = damage * criticalDamage;
            damageText = "<color=yellow>" + finalDamage.ToString("F0") + "</color>";
        }
        else
        {
            finalDamage = damage;
            damageText = finalDamage.ToString("F0");
        }

        Transform text = DamageTextManager.instance.TextCreate(0, damageText).transform;
        text.position = textPopUpPos.position;
        ///임시로 황소캐릭터의 폭발은 나무를 한 방에 부숨
        if (GameManager.instance.playerInfo.isLumberJack == true || GameManager.instance.character == Player.Character.BULL)
        {
            curHealth = 0;
        }
        else
        {
            curHealth--;
        }
    }

    public override IEnumerator Died(bool isDeSpawned)
    {
        statusEffect.StatusReset();

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

        float consume = consumableDropRate / 100;
        float loot;
        if (enemyType == Stat.enemyType.NORMAL_ENEMY || enemyType == Stat.enemyType.NEUTRALITY_ENEMY)
        {
            loot = (lootDropRate * (1 + (StageManager.instance.playerInfo.lucky / 100))) / (1 + StageManager.instance.inWaveLoot_Amount);
            loot = loot / 100;
        }
        else
        {
            loot = lootDropRate / 100;
        }
        float notDrop = (100 - (consume + loot)) / 100;

        float[] chanceLise = { notDrop, consume, loot };
        int index = StageManager.instance.Judgment(chanceLise);

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
                StageManager.instance.inWaveLoot_Amount++;
                break;
        }
        SpawnManager.instance.trees.Remove(gameObject);
        gameObject.SetActive(false);
        yield return 0;
    }
}
