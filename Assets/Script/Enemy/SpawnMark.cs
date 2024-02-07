using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMark : MonoBehaviour
{
    float E1, E2, E3, E4, E5;
    GameObject spawnEnemy;
    void OnEnable()
    {
        StartCoroutine(SpawnSetting());
    }

    IEnumerator SpawnSetting()
    {
        //float[] chanceLise = {};
        //int index = GameManager.instance.Judgment(chanceLise);
        StartCoroutine(Spawn());
        yield return new WaitForSeconds(0f);
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject enemy = PoolManager.instance.Get(1);
        enemy.transform.position = transform.position;
    }
}
