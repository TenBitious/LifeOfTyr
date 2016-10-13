using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {

    protected EnemyBehaviour mob;
    protected string animationName = "Idle";
    protected Vector3 destination;

	// Use this for initialization
	public virtual void StartBehaviour()
    {
        mob = gameObject.GetComponent<EnemyBehaviour>();
        //mob.Anim.CrossFade(animationName);
    }

    // Update is called once per frame
    public virtual void UpdateBehaviour () {
	
	}

    public virtual void EndBehaviour()
    {
        Destroy(this);
    }

    protected void ChangeState(string state)
    {
        mob.ChangeState(state);
    }


}
