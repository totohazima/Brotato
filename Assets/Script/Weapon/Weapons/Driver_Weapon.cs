using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver_Weapon : Weapon_Action, ICustomUpdateMono
{
    SpriteRenderer sprite;
    float timer;
    float mineTimer;
    WeaponScanner scanner;
    GameManager game;
    [SerializeField]
    private Transform baseObj;
    Melee_Bullet bullet;
    bool isFire;
    bool isTimerReset;
    [SerializeField]
    private CapsuleCollider coll;
    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
        sprite = baseObj.GetComponent<SpriteRenderer>();
        bullet = baseObj.GetComponent<Melee_Bullet>();
        game = GameManager.instance;
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        coll.enabled = false;
        ResetStat();
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        if(game.isEnd == true && isTimerReset == false)
        {
            mineTimer = 100;
            isTimerReset = true;
        }
        else if(game.isEnd == false)
        {
            isTimerReset = false;
        }

        ResetStat();
        AfterStatSetting();
        scanner.radius = afterRange;

        if (isFire == false)
        {
            StartCoroutine(MuzzleMove());
        }

        mineTimer += Time.deltaTime;
        switch (weaponTier)
        {
            case (0):
                if(mineTimer >= scrip.tier1_InfoStat[0])
                {
                    StartCoroutine(SpawnManager.instance.MineSpawn(1));
                    mineTimer = 0;
                }
                break;
            case (1):
                if (mineTimer >= scrip.tier2_InfoStat[0])
                {
                    StartCoroutine(SpawnManager.instance.MineSpawn(1));
                    mineTimer = 0;
                }
                break;
            case (2):
                if (mineTimer >= scrip.tier3_InfoStat[0])
                {
                    StartCoroutine(SpawnManager.instance.MineSpawn(1));
                    mineTimer = 0;
                }
                break;
            case (3):
                if (mineTimer >= scrip.tier4_InfoStat[0])
                {
                    StartCoroutine(SpawnManager.instance.MineSpawn(1));
                    mineTimer = 0;
                }
                break;
        }
        if (scanner.target != null)
        {

            timer += Time.deltaTime;
            if (timer >= afterCoolTime)
            {
                if (isFire == false)
                {
                    StartCoroutine(Fire());
                    timer = 0;
                }
            }
        }
    }

    public void ResetStat()
    {
        StatSetting((int)index, weaponTier);
    }
    private IEnumerator MuzzleMove() //���� ����� ������ ������ ������ ȸ���ϸ� �ȵ�
    {
        if (scanner.target == null)
        {
            Vector3 target = JoyStick.instance.moveTarget.position;
            if (target.x < transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.01f).setEase(LeanTweenType.easeInOutQuad);
        }
        else
        {
            Vector3 target = scanner.target.position;
            if (target.x < transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
        }
        yield return 0;
    }
    private IEnumerator Fire()
    {
        bullet.Init(afterDamage, afterPenetrate, afterRange, 100, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

        if (scanner.target != null)
        {
            float backX = scanner.target.position.x;
            float backY = scanner.target.position.y;

            if (backX > 0f)
            {
                backX = -3f;
            }
            else if (backX < 0f)
            {
                backX = 3f;
            }
            if (backY > 0f)
            {
                backY = -3f;
            }
            else if (backY < 0f)
            {
                backY = 3f;
            }
            backX = baseObj.position.x + backX;
            backY = baseObj.position.y + backY;
            Vector3 backPos = new Vector3(backX, backY, 0);

            Vector3 targetPos = Vector3.zero;
            float i = 0;
            while (true)
            {
                float radius = afterRange;
                Vector3 centerPosition = transform.position;

                float x = 0;
                float y = 0;
                if (scanner.target.position.x > transform.position.x)
                {
                    x = scanner.target.position.x + i;
                }
                else
                {
                    x = scanner.target.position.x - i;
                }
                if (scanner.target.position.y > transform.position.y)
                {
                    y = scanner.target.position.y + i;
                }
                else
                {
                    y = scanner.target.position.y - i;
                }

                Vector3 meleePos = new Vector3(x, y, 0);
                float distance = Vector3.Distance(meleePos, centerPosition);
                if (distance < radius)
                {
                    i += 0.5f;
                }
                else if (distance >= radius)
                {
                    targetPos = meleePos;
                    break;
                }
            }

            isFire = true;
            LeanTween.move(baseObj.gameObject, backPos, 0.04f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.04f);
            coll.enabled = true;
            LeanTween.move(baseObj.gameObject, targetPos, 0.17f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.2f);
            LeanTween.move(baseObj.gameObject, transform.position, 0.17f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.18f);
            ReturnWeapon(baseObj);
            scanner.target = null;
            coll.enabled = false;
            isFire = false;
        }
        else
        {
            StopCoroutine(Fire());
        }
    }
}