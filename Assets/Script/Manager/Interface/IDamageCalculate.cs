using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageCalculate
{
    //�⺻ ����� ���
    void DamageCalculator(float damage, int per, float accuracy, float bloodSuck, bool isCritical,float criticalDamage, float knockBack, Vector3 bulletPos);
    //����� ���� ����
    void StatusEffectCalculator(StatusEffect.EffectType[] effectType, Bullet bullet);
    //ȭ�� ����� ����� ���
    void ApplyBurnEffect(int infectedCount, float burnDamage, int burnCount);
    //���ο�(��ȭ) ����� ȿ�� ���
    void ApplySlowEffect(float slowEffect);

    
}

