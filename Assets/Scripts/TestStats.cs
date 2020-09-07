using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/New Stats")]
public class TestStats : ScriptableObject
{
    public int attack;
    public int defense;

    public void UpgradeAttack(int num) => attack += num;
    public void UpgradeDefense(int num) => defense += num;
}
