using UnityEngine;
using System.Collections;
using System;

public class FollowAI : BasicAI {

    public override void StartBehaviour()
    {
        animationName = "Walk";
        base.StartBehaviour();
    }
    // Update is called once per frame
    public override void UpdateBehaviour()
    {
        base.UpdateBehaviour();
        FindDestination();
        RotateToTarget(); 
        MoveToTarget();
    }

    private void FindDestination()
    {
        GameObject target = MathCalc.CheckDistance(mob.Players, this.gameObject, mob.EnemyInfo.sightDistance);
        if (target != null)
        {
            mob.Target = target;
            mob.Destination = mob.Target.transform.position;
            if(MathCalc.IsInRange(mob.Target.transform.position, this.gameObject.transform.position, mob.EnemyInfo.attackRange))
            {
                mob.Rigidbody.velocity = Vector3.zero;
                ChangeState("AttackAI");
            }
        }
        else
        {
            ChangeState("DefaultAI");
        }
    }

    void MoveToTarget()
    {
        mob.Rigidbody.velocity = new Vector3(transform.forward.x * (mob.EnemyInfo.runSpeed * Time.deltaTime), mob.Rigidbody.velocity.y, transform.forward.z * (mob.EnemyInfo.runSpeed * Time.deltaTime));
    }

    void RotateToTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(mob.Destination - transform.position);

        // Disable rotation on the xz axis
        targetRotation.x = 0.0f;
        targetRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 8);
    }
}
