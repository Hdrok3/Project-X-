using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Fire Attack", menuName = "Combat/Effects/Fire Effect")]
public class FireAttak : AttackEffect
{
    public ParticleSystem burningEfffect;
    public override void OnAttack(GameObject attacker, GameObject defender, AttackDefinition attackDefinition, Attack attack)
    {
        //var test = Instantiate(burningEfffect, defender.transform);
        // test.initialize();

        Debug.Log("Fire Effect");
    }

}
