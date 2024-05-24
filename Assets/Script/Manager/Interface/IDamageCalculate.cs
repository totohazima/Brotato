using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageCalculate
{
    //기본 대미지 계산
    void DamageCalculator(float damage, int per, float accuracy, float bloodSuck, bool isCritical,float criticalDamage, float knockBack, Vector3 bulletPos);
    //디버프 종류 측정
    void StatusEffectCalculator(StatusEffect.EffectType[] effectType, Bullet bullet);
    //화상 디버프 대미지 계산
    void ApplyBurnEffect(int infectedCount, float burnDamage, int burnCount);
    //슬로우(둔화) 디버프 효과 계산
    void ApplySlowEffect(float slowEffect);

    
}

