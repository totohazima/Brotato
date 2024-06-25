using System.Collections;
using UnityEngine;

public class NewTorch_Weapon : Weapon_Action, ICustomUpdateMono
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform controlPos1;
    [SerializeField] private Transform controlPos2;
    [SerializeField] private Transform endPos;
    [SerializeField] private Transform baseObj;
    [SerializeField] private CapsuleCollider coll;
    [SerializeField] private float timer;
    private StageManager stage;
    private Melee_Bullet bullet;
    [SerializeField] private bool isFire;
    [SerializeField] private bool isLeft;

    void Awake()
    {
        scanner = GetComponent<WeaponScanner>();
        bullet = baseObj.GetComponent<Melee_Bullet>();
        stage = StageManager.instance;
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
        scanner.detectedRaius = realRange_Detected;
        scanner.attackRadius = realRange_Attack;
        StartCoroutine(MuzzleMove());

        for (int i = 0; i < tierOutline.Length; i++)
        {
            tierOutline[i].gameObject.SetActive(i == weaponTier);
        }

        if (!isFire && scanner.attackTarget != null && timer >= afterCoolTime)
        {
            StartCoroutine(TorchAttack());
            timer = 0;
        }

        timer += Time.deltaTime;
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

    private IEnumerator TorchAttack()
    {
        bool isAttack = IsRangeInTarget(scanner.attackTarget, realRange_Attack);

        if (isAttack == true)
        {
            // Initialize bullet with stats and burn effect
            bullet.Init(afterDamage, afterPenetrate, realRange_Attack, 100, afterBloodSucking, afterCriticalChance, afterCriticalDamage, afterKnockBack, afterPenetrateDamage, Vector3.zero);
            bullet.StatusEffecInit(StatusEffect.EffectType.BURN);
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
            bullet.BurnInit(GameManager.instance.playerInfo.snakeCount, damage, count);

            if (scanner.attackTarget != null)
            {
                Vector3 targetPos = scanner.attackTarget.position;
                StartCoroutine(MuzzleMove());
                yield return new WaitForSeconds(0.12f);

                isLeft = targetPos.x < transform.position.x;
                float dis = Vector3.Distance(transform.position, targetPos);
                float duration = 0.014f * dis;

                Vector3 startOffset = Vector3.zero;
                Vector3 endOffset = Vector3.zero;

                if (isLeft)
                {
                    startOffset = GetPositionAtAngle(targetPos, 134, dis / 2f); // Adjust angle and distance as needed
                    endOffset = GetPositionAtAngle(targetPos, -90f, dis / 1.5f); // Adjust angle and distance as needed
                }
                else
                {
                    startOffset = GetPositionAtAngle(targetPos, 46.5f, dis / 2f); // Adjust angle and distance as needed
                    endOffset = GetPositionAtAngle(targetPos, -90, dis / 1.5f); // Adjust angle and distance as needed
                }

                startPos.position = transform.position + startOffset;
                endPos.position = transform.position + endOffset;

                float elapsedTime = 0f;
                coll.enabled = true;
                isFire = true;

                // 첫 번째 구간: startPos -> targetPos
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, transform.position, controlPos1.position, startPos.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -160), Quaternion.Euler(0, 0, 0), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // 두 번째 구간: targetPos -> endPos
                elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, startPos.position, controlPos2.position, endPos.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 160), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // 세 번째 구간: endPos -> transform.position
                yield return new WaitForSeconds(0.05f);
                elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    Vector3 point = CalculateQuadraticBezierPoint(t, endPos.position, transform.position, transform.position);

                    baseObj.position = point;
                    baseObj.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 160), Quaternion.Euler(0, 0, 0), t);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                dis = 0;
                duration = 0;
                elapsedTime = 0;
            }

            // Clear currentTarget and reset states
            StageManager.instance.trackedTargets.Remove(scanner.attackTarget);
            scanner.attackTarget = null;
            coll.enabled = false;
            isFire = false;
            isLeft = false;
        }
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    } 
}

