using UnityEngine;
using System.Collections;

public static class MathCalc {

    public static GameObject CheckDistance(GameObject[] targets, GameObject user, float minimalDistance)
    {
        foreach (GameObject target in targets)
        {
            if (IsInRange(target.transform.position, user.transform.position, minimalDistance))
            {
                return target;
            }
        }
        return null;
    }

    static public bool IsInRange(Vector3 target, Vector3 user, float criteria)
    {
        if (Vector3.Distance(target, user) < criteria) return true;
        return false;
    }

    static public bool IsOutOfRange(Vector3 target, Vector3 user, float criteria)
    {
        if (Vector3.Distance(target, user) > criteria) return true;
        return false;
    }
}
