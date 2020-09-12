﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Definition", menuName = "Combat/AttackDefinition")]
public class AttackDefinition : ScriptableObject
{
    [SerializeField] private List<AttackEffect> attackEffects = new List<AttackEffect>();

    [SerializeField] protected float coolDown;
    [SerializeField] protected float range;
    [SerializeField] protected float minDamage;
    [SerializeField] protected float maxDamage;
    [SerializeField] protected float critMultiplier;
    [SerializeField] protected float critChance;

    // TODO:
    // Kullanici ve target statlarina ihtiyac var. (Fonksiyonun almasi gerekir.)
    public Attack CreateAttack(GameObject attacker, GameObject defender)
    {
        // float coreDamage = attacker.GetDamage();
        float coreDamage = 0;
        coreDamage += Random.Range(minDamage, maxDamage);

        bool isCritical = Random.value < critChance;
        if (isCritical)
            coreDamage *= critMultiplier;

        // coreDamage -= defender.GetResistance();

        return new Attack((int)coreDamage, isCritical);
    }

    public void ExecuteAttackEffecs(GameObject attacker, GameObject defender, Attack attack)
    {
        foreach (var effect in attackEffects)
        {
            effect.OnAttack(attacker, defender, this, attack);
        }
    }
}
