using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench_Weapon : Weapon, ICustomUpdateMono
{
    public Weapons index;
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
    public Transform[] curvePos;

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
        AfterStatSetting();
        scanner.radius = afterRange;

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
            Vector3 targetPos = scanner.target.position;
            float dis = Vector3.Distance(transform.position, targetPos);
            float angle = GetAngle(transform.position, targetPos);
            Vector3 firstPos = ConvertAngleToVector(angle + 90, dis);
            Vector3 secondPos = ConvertAngleToVector(angle + 45, dis);
            Vector3 thirdPos = ConvertAngleToVector(angle - 25, dis);
            Vector3 fourPos = ConvertAngleToVector(angle - 50, dis);
            Vector3 fivePos = ConvertAngleToVector(angle - 75, dis);
            Vector3 endPos = ConvertAngleToVector(angle - 90, dis);
            curvePos[0].position = firstPos;
            curvePos[1].position = secondPos;
            curvePos[2].position = thirdPos;
            curvePos[3].position = fourPos;
            curvePos[4].position = fivePos;
            curvePos[5].position = endPos;

            isFire = true;
            coll.enabled = true;
            LeanTween.move(baseObj.gameObject, curvePos[0].position, 0.04f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.04f);
            LeanTween.move(baseObj.gameObject, curvePos[1].position, 0.04f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.04f);
            LeanTween.move(baseObj.gameObject, targetPos, 0.15f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.15f);
            LeanTween.move(baseObj.gameObject, curvePos[2].position, 0.07f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.07f);
            LeanTween.move(baseObj.gameObject, curvePos[3].position, 0.07f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.07f);
            LeanTween.move(baseObj.gameObject, curvePos[4].position, 0.07f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.07f);
            LeanTween.move(baseObj.gameObject, curvePos[5].position, 0.07f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.07f);
            LeanTween.move(baseObj.gameObject, transform.position, 0.03f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.03f);
            coll.enabled = false;
            isFire = false;
        }
        else
        {
            StopCoroutine(Fire());
        }
    }
}
