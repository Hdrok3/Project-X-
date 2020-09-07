using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Combat/Weapon")]
public abstract class WeaponAttack : AttackDefinition
{
    [SerializeField] private GameObject weaponPrefab;

    public virtual void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (defender == null) return;
        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > range) return;
        if (!attacker.transform.IsFacingTarget(defender.transform)) return;

        // TODO:
        // get attacker and defender stats

        var attack = CreateAttack(attacker, defender);
        ExecuteAttackEffecs(attacker, defender, attack);
    }
}
