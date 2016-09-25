using UnityEngine;
using System.Collections;
using System;

public class FollowAI : BasicAI {

    // Update is called once per frame
    public override void UpdateBehaviour()
    {
        base.UpdateBehaviour();
        RotateToTarget();
        MoveToTarget();
    }

    void MoveToTarget()
    {
        mob.Rigidbody.velocity = new Vector3(transform.forward.x * (mob.EnemyInfo.runSpeed * Time.deltaTime), mob.Rigidbody.velocity.y, transform.forward.z * (mob.EnemyInfo.runSpeed * Time.deltaTime));
    }

    void RotateToTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(mob.Target.transform.position - transform.position);

        // Disable rotation on the xz axis
        targetRotation.x = 0.0f;
        targetRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 8);
    }
}
