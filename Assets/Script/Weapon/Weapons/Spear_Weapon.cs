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

        timer += Time.deltaTime;
        if (scanner.target != null)
        {
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

    private IEnumerator MuzzleMove()
    {
        if (scanner.target == null)
        {
            Vector3 target = JoyStick.instance.moveTarget.position;
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.01f).setEase(LeanTweenType.easeInOutQuad);
        }
        else
        {
            Vector3 target = scanner.target.position;
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
        }
        yield return null;
    }

    private IEnumerator Fire()
    {
        bullet.Init(afterDamage, afterPenetrate, afterRange, 100, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

        if (scanner.target != null)
        {
            Vector3 targetPos = scanner.target.position;
            Vector3 originalPos = transform.position;

            Vector3 moveDir = (targetPos - originalPos).normalized;
            Vector3 destination = originalPos + moveDir * afterRange;

            float moveSpeed = 100f; // �̵� �ӵ�
            float moveDuration = afterRange / moveSpeed;

            // Ÿ�� �ݴ� �������� ���� �Ÿ� �̵�
            float backDistance = 20f; // Ÿ�� �ݴ� �������� �̵��� �Ÿ�
            Vector3 backDestination = targetPos + (-moveDir) * backDistance;

            isFire = true;

            //Ÿ�� �������� ȸ��
            Vector3 target = scanner.target.position;
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(0.1f);

            // Ÿ�� �ݴ� �������� �̵�
            //LeanTween.move(baseObj.gameObject, backDestination, 0.02f).setEase(LeanTweenType.easeInOutQuad);
            //yield return new WaitForSeconds(0.02f);

            LeanTween.move(baseObj.gameObject, destination, moveDuration).setEase(LeanTweenType.easeInOutQuad);
            coll.enabled = true;

            // ��ǥ �������� �̵�
            yield return new WaitForSeconds(moveDuration);

            // ���� ���� �ð� (�� �κ��� �ʿ信 �°� �����ϼ���)
            yield return new WaitForSeconds(0.03f);

            // ���� ��ġ�� ���ƿ���
            LeanTween.moveLocal(baseObj.gameObject, Vector3.zero, moveDuration).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(moveDuration);

            // ���� ��ġ�� ���ƿ� �� ó��
            coll.enabled = false;
            isFire = false;
        }
        else
        {
            StopCoroutine(Fire());
        }
    }

}
