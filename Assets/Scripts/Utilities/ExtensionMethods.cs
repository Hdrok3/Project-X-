using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    private const float DOT_TRESHOLD = 0.5f;
    public static bool IsFacingTarget(this Transform transform, Transform target)
    {
        // Direction to target vector
        Vector3 vecDirToTarget = (target.position - transform.position).normalized;

        var dot = Vector3.Dot(transform.forward, vecDirToTarget);
        return dot >= DOT_TRESHOLD;
    }
}
