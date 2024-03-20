using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench_Weapon : Weapon_Action, ICustomUpdateMono
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
        ResetStat();
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
            Vector3 pos1 = ConvertAngleToVector(angle + 90, dis);
            Vector3 pos2 = ConvertAngleToVector(angle + 45, dis);
            Vector3 pos3 = ConvertAngleToVector(angle - 15, dis);
            Vector3 pos4 = ConvertAngleToVector(angle - 30, dis);
            Vector3 pos5 = ConvertAngleToVector(angle - 45, dis);
            Vector3 pos6 = ConvertAngleToVector(angle - 60, dis);
            Vector3 pos7 = ConvertAngleToVector(angle - 75, dis);
            Vector3 pos8 = ConvertAngleToVector(angle - 90, dis);
            curvePos[0].position = transform.position + pos1;
            curvePos[1].position = transform.position + pos2;
            curvePos[2].position = transform.position + pos3;
            curvePos[3].position = transform.position + pos4;
            curvePos[4].position = transform.position + pos5;
            curvePos[5].position = transform.position + pos6;
            curvePos[6].position = transform.position + pos7;
            curvePos[7].position = transform.position + pos8;

            isFire = true;
            coll.enabled = true;
            LeanTween.move(baseObj.gameObject, curvePos[0].position, 0.04f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.04f);
            LeanTween.move(baseObj.gameObject, curvePos[1].position, 0.04f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.04f);
            LeanTween.move(baseObj.gameObject, targetPos, 0.15f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.15f);
            LeanTween.move(baseObj.gameObject, curvePos[2].position, 0.02f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);
            LeanTween.move(baseObj.gameObject, curvePos[3].position, 0.02f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);
            LeanTween.move(baseObj.gameObject, curvePos[4].position, 0.02f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);
            LeanTween.move(baseObj.gameObject, curvePos[5].position, 0.02f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);
            LeanTween.move(baseObj.gameObject, curvePos[6].position, 0.02f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);
            LeanTween.move(baseObj.gameObject, curvePos[7].position, 0.02f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);
            LeanTween.move(baseObj.gameObject, transform.position, 0.02f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.02f);
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
