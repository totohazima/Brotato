using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear_Weapon : Weapon_Action, ICustomUpdateMono
{
    SpriteRenderer sprite;
    float timer;
    WeaponScanner scanner;
    GameManager game;
    [SerializeField]
    private Transform baseObj;
    Melee_Bullet bullet;
    bool isFire;
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
        ResetStat();
        AfterStatSetting();
        scanner.radius = afterRange;

        for (int i = 0; i < tierOutline.Length; i++)
        {
            if (i == weaponTier)
            {
                tierOutline[i].gameObject.SetActive(true);
            }
            else
            {
                tierOutline[i].gameObject.SetActive(false);
            }
        }

        if (isFire == false)
        {
            StartCoroutine(MuzzleMove());
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
    private IEnumerator MuzzleMove() //근접 무기는 공격이 끝나기 전까지 회전하면 안됨
    {
        if (scanner.target == null)
        {
            Vector3 target = JoyStick.instance.moveTarget.position;
            if (target.x < transform.position.x)
            {
                sprite.flipY = true;
                for (int i = 1; i < tierOutline.Length; i++)
                {
                    tierOutline[i].flipX = true;
                }
            }
            else
            {
                sprite.flipY = false;
                for (int i = 1; i < tierOutline.Length; i++)
                {
                    tierOutline[i].flipX = false;
                }
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
                sprite.flipY = true;
                for (int i = 1; i < tierOutline.Length; i++)
                {
                    tierOutline[i].flipX = true;
                }
            }
            else
            {
                sprite.flipY = false;
                for (int i = 1; i < tierOutline.Length; i++)
                {
                    tierOutline[i].flipX = false;
                }
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

            if(backX > 0f)
            {
                backX = -3f;
            }
            else if(backX < 0f)
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
