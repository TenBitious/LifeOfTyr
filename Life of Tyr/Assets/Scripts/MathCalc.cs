using UnityEngine;
using System.Collections;

public static class MathCalc {

    public static GameObject CheckDistance(GameObject[] targets, GameObject user, float minimalDistance)
    {
        foreach (GameObject target in targets)
        {
            if (Vector3.Distance(user.transform.position, target.transform.position) < minimalDistance)
            {
                return target;
            }
        }
        return null;
    }
}
