using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand_Weapon : Weapon_Action, ICustomUpdateMono
{
    float timer;
    [SerializeField] private bool isFire;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform imageGroup;

    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
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
        scanner.detectedRaius = realRange_Detected;
        scanner.attackRadius = realRange_Attack;
        StartCoroutine(MuzzleMove());
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

        timer += Time.deltaTime;
        //군인: 이동 중 공격 불가
        if (GameManager.instance.character == Player.Character.SOLDIER)
        {
            if (GameManager.instance.playerAct.isStand == true && scanner.attackTarget != null)
            {
                if (timer >= afterCoolTime)
                {
                    StartCoroutine(Fire());
                    timer = 0;
                }
            }
        }
        else
        {
            if (scanner.attackTarget != null)
            {
                if (timer >= afterCoolTime)
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
        if (scanner.detectedTarget == null && isFire == false)
        {
            Vector3 dir = new Vector3(GameManager.instance.playerAct.joyStick.Horizontal, GameManager.instance.playerAct.joyStick.Vertical, 0);
            dir.Normalize();
            float angle = GetAngle(Vector2.zero, dir);
            LeanTween.rotate(gameObject, new Vector3(0, 0, angle), 0.01f).setEase(LeanTweenType.easeInOutQuad);

            if (GameManager.instance.playerAct != null && GameManager.instance.playerAct.isLeft)
            {

                WeaponSpinning(true);
            }
            else
            {
                WeaponSpinning(false);
            }
        }
        else if (scanner.detectedTarget != null && scanner.attackTarget == null && isFire == false)
        {
            Vector3 target = scanner.detectedTarget.position;
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
        else if (scanner.attackTarget != null && isFire == false)
        {
            Vector3 target = scanner.attackTarget.position;
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
        if (scanner.attackTarget != null)
        {
            isFire = true;

            Vector3 targetPos = scanner.attackTarget.position;
            StartCoroutine(MuzzleMove());
            yield return new WaitForSeconds(0.12f);

            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;
            Transform bullet = PoolManager.instance.Get(9).transform;
            bullet.position = muzzle.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.zero, dir);

            Bullet bulletInit = bullet.GetComponent<Bullet>();
            bulletInit.Init(afterDamage, afterPenetrate, realRange_Attack, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, dir);
            bulletInit.StatusEffecInit(StatusEffect.EffectType.BURN);

            float damage = 0;
            int count = 0;
            Player_Action player = GameManager.instance.playerAct;
            switch (weaponTier)
            {
                case 0:
                    damage = (scrip.tier1_InfoStat[0] + (player.elementalDamage * (scrip.tier1_InfoStat[2] / 100))) * (1 + (player.persentDamage / 100));
                    count = (int)scrip.tier1_InfoStat[1];
                    break;
                case 1:
                    damage = (scrip.tier2_InfoStat[0] + (player.elementalDamage * (scrip.tier2_InfoStat[2] / 100))) * (1 + (player.persentDamage / 100));
                    count = (int)scrip.tier2_InfoStat[1];
                    break;
                case 2:
                    damage = (scrip.tier3_InfoStat[0] + (player.elementalDamage * (scrip.tier3_InfoStat[2] / 100))) * (1 + (player.persentDamage / 100));
                    count = (int)scrip.tier3_InfoStat[1];
                    break;
                case 3:
                    damage = (scrip.tier4_InfoStat[0] + (player.elementalDamage * (scrip.tier4_InfoStat[2] / 100))) * (1 + (player.persentDamage / 100));
                    count = (int)scrip.tier4_InfoStat[1];
                    break;
            }
            bulletInit.BurnInit(GameManager.instance.playerInfo.snakeCount, damage, count);

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
