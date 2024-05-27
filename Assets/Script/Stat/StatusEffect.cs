using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour, ICustomUpdateMono
{
    public EnemyAction effecter;
    public ParticleSystem burningEffect;
    [Header("# ȭ�� ȿ��")]
    public bool isBurn;
    public bool isInfectedBurn; //ȭ�� ����
    public int infectedCount; //�ִ� ���� ��
    public float infecterRange = 7;
    public float burnDamage;
    public int burnCount;
    public float tickRate;
    [HideInInspector] public int maxBurnCount; //ȭ�� ������ ���� ����
    private float burnTimer;
    
    [Header("# ��ȭ(���ο�) ȿ��")]
    public bool isSlow;
    public float slowEffect;
    public enum EffectType
    {
        NONE = 0,
        BURN = 1,
        SLOW = 2,
    }
    
    void OnEnable()
    {
        effecter = gameObject.GetComponent<EnemyAction>();
        tickRate = 0.1f;
        CustomUpdateManager.customUpdates.Add(this);
    }

    void OnDisable()
    {
        effecter = null;
        CustomUpdateManager.customUpdates.Remove(this);
    }

    /// <summary>
    /// ���� ��� �� ��� �����̻� �ʱ�ȭ
    /// </summary>
    public void StatusReset()
    {
        IntReset(ref burnCount);
        IntReset(ref maxBurnCount);
        IntReset(ref infectedCount);
        FloatReset(ref burnDamage);
        FloatReset(ref burnTimer);
        FloatReset(ref slowEffect);
        IsBoolReset(ref isBurn);
        IsBoolReset(ref isInfectedBurn);
        IsBoolReset(ref isSlow);

        EffectOnOff();
    }
    public void CustomUpdate()
    {
        if(effecter == null)
        {
            return;
        }

        EffectOnOff();

        if (burnCount != 0)
        {
            isBurn = true;
            Burning();
            if(infectedCount != 0)
            {
                isInfectedBurn = true;
            }
        }
        else
        {
            //ȭ���� ������ �� burnTimer�� 0�� �ƴ� ��� �ʱ�ȭ
            if (burnTimer != 0)
            {
                FloatReset(ref burnTimer);
            }
        }

        if (slowEffect != 0)
        {
            isSlow = true;
        }
        else
        {
            isSlow = false;
        }
    }

    private void Burning()
    {
        burnTimer += Time.deltaTime;

        if (burnTimer >= tickRate)
        {
            effecter.curHealth -= burnDamage;
            burnCount--;

            if (effecter.whiteFlash != null)
                effecter.whiteFlash.PlayFlash();

            string txt = burnDamage.ToString("F0");
            Transform text = DamageTextManager.instance.TextCreate(0, txt).transform;
            text.position = effecter.textPopUpPos.position;

            burnTimer = 0;

            if(isInfectedBurn == true)
            {
                if(infectedCount > 0)
                {
                    List<EnemyAction> unBuringEnemy = new List<EnemyAction>();

                    for (int i = 0; i < SpawnManager.instance.enemys.Count; i++)
                    {
                        EnemyAction enemy = null;
                        //�ڱ� �ڽ� ����
                        if (SpawnManager.instance.enemys[i] != effecter)
                        {
                            enemy = SpawnManager.instance.enemys[i];
                        }

                        if (enemy != null)
                        {
                            float dis = Vector3.Distance(transform.position, enemy.transform.position);
                            //distance�� 1�����̰� ȭ�� �ɷ����� ���� ���
                            if (dis <= infecterRange && enemy.statusEffect.isBurn == false)
                            {
                                unBuringEnemy.Add(enemy);
                            }
                        }
                    }

                    InfectedBurning(unBuringEnemy);
                }
            }
        }

        if(burnCount == 0)
        {
            IntReset(ref burnCount);
            IntReset(ref maxBurnCount);
            IntReset(ref infectedCount);
            FloatReset(ref burnDamage);
            IsBoolReset(ref isInfectedBurn);
            isBurn = false;
        }
    }
    /// <summary>
    /// ȭ�� ������� ���� ������ infectedCount��ŭ �ֺ� ������ ȭ���� ������Ŵ
    /// ������Ų ��ŭ infectedCount�� �����ϰ� ȭ���� ������ �ش� �Լ� ����
    /// </summary>
    private void InfectedBurning(List<EnemyAction> unBurningEnemys)
    {
        List<EnemyAction> selectInfected = new List<EnemyAction>();

        for (int i = 0; i < infectedCount; i++)
        {
            EnemyAction shortEnemy = null;
            float shortDis = Mathf.Infinity;

            for (int j = 0; j < unBurningEnemys.Count; j++)
            {
                float dis = Vector3.Distance(transform.position, unBurningEnemys[j].transform.position);

                if(dis < shortDis)
                {
                    shortDis = dis;
                    shortEnemy = unBurningEnemys[i];
                }
            }

            if(shortEnemy != null)
            {
                selectInfected.Add(shortEnemy);
                unBurningEnemys.Remove(shortEnemy);
            }
        }

        for (int i = 0; i < selectInfected.Count; i++)
        {
            selectInfected[i].statusEffect.burnDamage = burnDamage;
            selectInfected[i].statusEffect.burnCount = maxBurnCount;
            selectInfected[i].statusEffect.maxBurnCount = maxBurnCount;
        }
        infectedCount -= selectInfected.Count;
    }

    /// <summary>
    /// ����Ʈ �¿���
    /// </summary>
    private void EffectOnOff()
    {
        switch(isBurn)
        {
            case true:
                if (burningEffect.isPlaying == false)
                    burningEffect.Play();
                break;
            case false:
                if (burningEffect.isStopped == false)
                        burningEffect.Stop();
                break;
        }
    }
    /// <summary>
    /// int �� �ʱ�ȭ
    /// </summary>
    private void IntReset(ref int counts)
    {
        counts = 0;
    }
    /// <summary>
    /// bool �� �ʱ�ȭ
    /// </summary>
    private void IsBoolReset(ref bool isBools)
    {

        isBools = false;
        
    }
    /// <summary>
    /// float �� �ʱ�ȭ
    /// </summary>
    private void FloatReset(ref float effects)
    {
        effects = 0;
    }


    /// <summary>
    /// ȭ�� ���� ��Ÿ� ǥ��
    /// ������ �󿡼��� �� �� ����
    /// </summary>
#if UNITY_EDITOR
    int segments = 100;
    Color gizmoColor = Color.yellow;
    bool drawWhenSelected = true;

    void OnDrawGizmosSelected()
    {
        if (drawWhenSelected)
        {
            Gizmos.color = gizmoColor;
            DrawHollowCircle(transform.position, infecterRange, segments);
        }
    }
    void DrawHollowCircle(Vector3 center, float radius, int segments)
    {
        float angle = 0f;
        Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);

        for (int i = 1; i <= segments; i++)
        {
            angle = i * Mathf.PI * 2f / segments;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            Gizmos.DrawLine(lastPoint, newPoint);
            lastPoint = newPoint;
        }
    }
#endif
}
