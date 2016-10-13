using UnityEngine;
using System.Collections;

public class AttackAI : BasicAI {

    // Use this for initialization
    public override void StartBehaviour()
    {
        animationName = "Attack";
        Debug.Log(animationName);
        base.StartBehaviour();
    }

    public override void UpdateBehaviour()
    {
        //if (!mob.Anim.IsPlaying("Attack"))
        //    StartBehaviour();

        base.UpdateBehaviour();
        if (MathCalc.IsOutOfRange(mob.Target.transform.position, this.gameObject.transform.position, mob.EnemyInfo.attackRange))
        {
            ChangeState("FollowAI");
        }
    }
}
