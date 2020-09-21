using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public static GameObject FindClosestGameObject(this Collider[] allColliders, Transform transform)
    {
        GameObject closestTarget = null;
        if (allColliders.Length != 0)
        {
            var ranges = allColliders.Select((x) => (transform.position - x.transform.position).sqrMagnitude).ToList();
            var indexOfMin = ranges.IndexOf(ranges.Min());
                
            closestTarget = allColliders[indexOfMin].gameObject;
            //foreach (var collider in allColliders)
            //{
            //    if (closestTarget != null)
            //    {
            //        if ((transform.position - closestTarget.transform.position).sqrMagnitude > (transform.position - collider.gameObject.transform.position).sqrMagnitude)
            //        {
            //            closestTarget = collider.gameObject;
            //        }
            //    }
            //    else closestTarget = collider.gameObject;
            //}
        }
        return closestTarget;
    }
}
