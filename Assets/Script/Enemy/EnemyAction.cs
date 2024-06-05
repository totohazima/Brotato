using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : Enemy, ICustomUpdateMono, IDamageCalculate
{
    public Animator anim;
    public CapsuleCollider coll;
    public SpriteRenderer sprite;
    public Transform textPopUpPos;
    public WhiteFlash whiteFlash;
    public float moveSpeed;
    public bool isDontPush; //true일 경우 넉백 미작동
    public StatusEffect statusEffect;
    [HideInInspector] public Transform target;
    [HideInInspector] public StageManager stage;
    [HideInInspector] public float hitTimer;
    [HideInInspector] public Rigidbody rigid;

    void Awake()
    {
        stage = StageManager.instance;
        rigid = GetComponent<Rigidbody>();
        target = stage.mainPlayer.transform;
    }
    public virtual void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        SpawnManager.instance.enemys.Add(this);
        StatSetting(name, enemyType);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public virtual void CustomUpdate()
    {
        if(curHealth <= 0)
        {
            isDie = true;
            StartCoroutine(Died());
        }
        else
        {
            isDie = false;
        }

        if(isDie)
        {
            return;
        }

        Move();
        
        if(target.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

       
    }
 
    public virtual void Move()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = speed / 2500;
        if (ugliyToothSlow >= 1)
        {
            moveSpeed = moveSpeed - ((moveSpeed / 100) * (ugliyToothSlow * 10));
        }

        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * moveSpeed;
        rigid.MovePosition(rigid.position + nextVec); //-nextVec 반대로 감
        rigid.velocity = Vector3.zero;

        ///이동 제한
        float x = Mathf.Clamp(transform.position.x, stage.xMin, stage.xMax);
        float y = Mathf.Clamp(transform.position.y, stage.yMin, stage.yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private IEnumerator KnockBack(Vector3 playerPos, float power)
    {
        Vector3 dir = transform.position - playerPos;
        rigid.AddForce(dir.normalized * (power), ForceMode.Impulse); //power가 양수일시 플레이어 방향으로 넉백됨
        yield return 0;
    }
    public virtual void DamageCalculator(float damage, int per, float accuracy, float bloodSuck, bool isCritical, float criticalDamage, float knockBack, Vector3 bulletPos)
    {
        if (GameManager.instance.playerInfo.isUglyTooth == true)
        {
            if (ugliyToothSlow < 3)
            {
                ugliyToothSlow++;
            }
        }

        float nonBloodSucking = 100 - bloodSuck;
        float[] chanceLise = { nonBloodSucking, bloodSuck };
        int index = Judgment(chanceLise);
        if(index == 0)
        { }
        else
        {
            GameManager.instance.playerInfo.playerHealth++;
            string txt = "<color=#4CFF52>1</color>";
            Transform texts = DamageTextManager.instance.TextCreate(0, txt).transform;
            texts.position = GameManager.instance.player_Info.transform.position;
        }

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

        curHealth -= finalDamage;

        if (whiteFlash != null)
            whiteFlash.PlayFlash();
        

        if (isDontPush == false)
            StartCoroutine(KnockBack(stage.playerInfo.transform.position, knockBack * 10));
        
    }
    public void StatusEffectCalculator(StatusEffect.EffectType[] effect, Bullet bullet)
    {
        if (bullet.isSausage == true)
        {
            GameManager game = GameManager.instance;

            float damage = (game.playerInfo.scaredSausageDamage + game.player_Info.elementalDamage) * (1 + (game.player_Info.persentDamage / 100));
            ScaredSausage(game.playerInfo.snakeCount, damage, game.playerInfo.scaredSausageDamageCount);
        }

        for (int i = 0; i < effect.Length; i++)
        {
            switch (effect[i])
            {
                case StatusEffect.EffectType.NONE:
                    break;
                case StatusEffect.EffectType.BURN:
                    ApplyBurnEffect(bullet.infectedCount ,bullet.burnDamage, bullet.burnCount);
                    break;
                case StatusEffect.EffectType.SLOW:
                    ApplySlowEffect(bullet.slowEffect);
                    break;
            }
        }
    }

    /// <summary>
    /// 겁 먹은 소시지를 가지고 있을 경우 사용하는 함수
    /// 소시지 갯수에 비례한 확률로 화상에 걸릴지 체크한다.
    /// 모든 무기에 전부 적용되는 상태이상 함수
    /// </summary>
    public void ScaredSausage(int infectedCount, float burnDamage, int burnCount)
    {
        //가장 높은 대미지를 가진 화상효과만 적용됨
        if (statusEffect.burnDamage <= burnDamage)
        {
            statusEffect.infectedCount = infectedCount;
            statusEffect.burnDamage = burnDamage;
            statusEffect.burnCount = burnCount;
            statusEffect.maxBurnCount = burnCount;
        }

    }
    public void ApplyBurnEffect(int infectedCount, float burnDamage, int burnCount)
    {
        //가장 높은 대미지를 가진 화상효과만 적용됨
        if (statusEffect.burnDamage <= burnDamage)
        {
            statusEffect.infectedCount = infectedCount;
            statusEffect.burnDamage = burnDamage;
            statusEffect.burnCount = burnCount;
            statusEffect.maxBurnCount = burnCount;
        }
    }
    public void ApplySlowEffect(float slowEffect)
    {
        statusEffect.slowEffect = slowEffect;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (stage.playerInfo.isHit == false)
            {
                stage.playerInfo.isHit = true;
                if (stage.playerInfo.whiteFlash != null && name != EnemyName.TREE)
                {
                    stage.playerInfo.whiteFlash.PlayFlash();
                    GameManager.instance.playerInfo.HitCalculate(damage);
                    //황소: 피격 시 폭발
                    if(GameManager.instance.character == Player.Character.BULL)
                    {
                        GameObject booms = PoolManager.instance.Get(6);
                        booms.transform.position = other.transform.position;

                        Bullet bullet = booms.GetComponent<Bullet>();
                        float damage = (30 + (GameManager.instance.player_Info.meleeDamage * 3) + (GameManager.instance.player_Info.rangeDamage * 3) + (GameManager.instance.player_Info.elementalDamage * 3))
                            * (1 + (GameManager.instance.player_Info.persentDamage / 100) * (1 + (GameManager.instance.player_Info.explosiveDamage / 100)));
                        bullet.Init(damage, 10000, -1000, 100, 0, 0, 0, 0, 0, Vector3.zero);
                    }
                }
                
            }
        }
    }

    public virtual IEnumerator Died()
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

        SpawnManager.instance.replaceEnemyCount++;
        SpawnManager.instance.enemys.Remove(this);
        gameObject.SetActive(false);
        yield return 0;
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
