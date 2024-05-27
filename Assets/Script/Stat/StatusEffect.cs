using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour, ICustomUpdateMono
{
    public EnemyAction effecter;
    public ParticleSystem burningEffect;
    [Header("# 화상 효과")]
    public bool isBurn;
    public bool isInfectedBurn; //화상 전염
    public int infectedCount; //최대 전염 수
    public float infecterRange = 7;
    public float burnDamage;
    public int burnCount;
    public float tickRate;
    [HideInInspector] public int maxBurnCount; //화상 전염을 위한 변수
    private float burnTimer;
    
    [Header("# 둔화(슬로우) 효과")]
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
    /// 몬스터 사망 시 모든 상태이상 초기화
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
            //화상이 끝났을 때 burnTimer가 0이 아닌 경우 초기화
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
                        //자기 자신 제외
                        if (SpawnManager.instance.enemys[i] != effecter)
                        {
                            enemy = SpawnManager.instance.enemys[i];
                        }

                        if (enemy != null)
                        {
                            float dis = Vector3.Distance(transform.position, enemy.transform.position);
                            //distance가 1이하이고 화상에 걸려있지 않은 대상
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
    /// 화상 대미지를 입을 때마다 infectedCount만큼 주변 적에게 화상을 전염시킴
    /// 전염시킨 만큼 infectedCount가 감소하고 화상이 끝나면 해당 함수 정지
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
    /// 이펙트 온오프
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
    /// int 값 초기화
    /// </summary>
    private void IntReset(ref int counts)
    {
        counts = 0;
    }
    /// <summary>
    /// bool 값 초기화
    /// </summary>
    private void IsBoolReset(ref bool isBools)
    {

        isBools = false;
        
    }
    /// <summary>
    /// float 값 초기화
    /// </summary>
    private void FloatReset(ref float effects)
    {
        effects = 0;
    }


    /// <summary>
    /// 화상 전염 사거리 표시
    /// 에디터 상에서만 볼 수 있음
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
