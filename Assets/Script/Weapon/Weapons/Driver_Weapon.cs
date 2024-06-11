using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver_Weapon : Weapon_Action, ICustomUpdateMono
{
    private float timer;
    private float mineTimer;
    private WeaponScanner scanner;
    private Melee_Bullet bullet;
    private bool isFire;
    private bool isTimerReset;
    [SerializeField] private Transform baseObj;
    [SerializeField] private Transform meleeMuzzle;
    [SerializeField] private CapsuleCollider coll;
    [SerializeField] private Transform imageGroup;
    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
        bullet = baseObj.GetComponent<Melee_Bullet>();
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
        if(GameManager.instance.isEnd == true && isTimerReset == false)
        {
            mineTimer = 100;
            isTimerReset = true;
        }
        else if(GameManager.instance.isEnd == false)
        {
            isTimerReset = false;
        }

        ResetStat();
        AfterStatSetting();
        scanner.radius = realRange;
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

        if (GameManager.instance.isEnd == false)
        {
            mineTimer += Time.deltaTime;
            switch (weaponTier)
            {
                case (0):
                    if (mineTimer >= scrip.tier1_InfoStat[0])
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
        }
        timer += Time.deltaTime;
        //����: �̵� �� ���� �Ұ�
        if (GameManager.instance.character == Player.Character.SOLDIER)
        {
            if (GameManager.instance.player_Info.isStand == true && scanner.target != null)
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
        else
        {
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
    }

    public void ResetStat()
    {
        StatSetting((int)index, weaponTier);
    }
    private IEnumerator MuzzleMove() //���� ����� ������ ������ ������ ȸ���ϸ� �ȵ�
    {
        if (scanner.target == null)
        {
            if (GameManager.instance.player_Info != null && GameManager.instance.player_Info.isLeft == true)
            {
                WeaponSpinning(true);
                LeanTween.rotate(gameObject, new Vector3(0, 0, 180), 0.01f).setEase(LeanTweenType.easeInOutQuad);
            }
            else
            {
                WeaponSpinning(false);
                LeanTween.rotate(gameObject, new Vector3(0, 0, 0), 0.01f).setEase(LeanTweenType.easeInOutQuad);
            }
            
        }
        else
        {
            Vector3 target = scanner.target.position;
            if (target.x < transform.position.x)
            {
                WeaponSpinning(true);
            }
            else
            {
                WeaponSpinning(false);
            }
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);
        }
        yield return null;
    }
    private IEnumerator Fire()
    {
        bullet.Init(afterDamage, afterPenetrate, realRange, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);

        if (scanner.target != null)
        {
            Vector3 targetPos = scanner.target.position;
            Vector3 originalPos = transform.position;

            float realRanges = realRange - (Vector3.Distance(baseObj.position, meleeMuzzle.position));
            StartCoroutine(MuzzleMove());
            //if (targetPos.x < transform.position.x)
            //{
            //    WeaponSpinning(true);
            //}
            //else
            //{
            //    WeaponSpinning(false);
            //}
            ////Ÿ�� �������� ȸ��
            //Vector3 dir = targetPos - transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.1f).setEase(LeanTweenType.easeInOutQuad);

            Vector3 moveDir = (targetPos - originalPos).normalized;
            Vector3 destination = originalPos + moveDir * realRanges;

            float moveDuration = (realRange / 100) / 2;
            isFire = true;

            yield return new WaitForSeconds(0.1f);

            LeanTween.move(baseObj.gameObject, destination, moveDuration).setEase(LeanTweenType.easeInOutQuad);
            coll.enabled = true;

            // ��ǥ �������� �̵�
            yield return new WaitForSeconds(moveDuration);
            bullet.knockBack = 0;

            // ���� ���� �ð� (�� �κ��� �ʿ信 �°� �����ϼ���)
            yield return new WaitForSeconds(realRange / 100);

            // ���� ��ġ�� ���ƿ���
            LeanTween.moveLocal(baseObj.gameObject, Vector3.zero, moveDuration).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(moveDuration);

            // ���� ��ġ�� ���ƿ� �� ó��
            coll.enabled = false;
            isFire = false;
        }
    }

    public override void WeaponSpinning(bool isLeft)
    {
        if (isLeft == true)
        {
            for (int i = 0; i < tierOutline.Length; i++)
            {
                tierOutline[i].flipY = true;
            }
        }
        else
        {
            for (int i = 0; i < tierOutline.Length; i++)
            {
                tierOutline[i].flipY = false;
            }
        }
    }
}
