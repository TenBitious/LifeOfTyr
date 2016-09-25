using UnityEngine;
using System.Collections;

public class DefaultAI : BasicAI {

    public override void StartBehaviour()
    {
        animationName = "Idle";
        Debug.Log(animationName);
        base.StartBehaviour();
    }

    public override void UpdateBehaviour()
    {
        Debug.Log(mob.name);
        GameObject target = MathCalc.CheckDistance(mob.Players, this.gameObject, mob.EnemyInfo.sightDistance);
        if (target != null)
        {
            mob.Target = target;
            ChangeState("FollowAI");
        }
	}
}
