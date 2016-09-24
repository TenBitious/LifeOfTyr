using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {

    private bool is_Shooting, is_Retracting, is_Pulling;

    private float  max_Shoot_Distance;
    private Vector3 shoot_Direction, retract_Direction, pull_Direction;
    private float shoot_Speed;

    private Rigidbody m_Rigidbody;

	// Use this for initialization
	void Start () 
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        shoot_Speed = PlayerStats.Instance.shoot_Speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	    //Handle shooting
        if (is_Shooting)
        {
            HandleShooting();
        }
        //Handle retracting
        if (is_Retracting)
        {
            HandleRetracting();
        }

        if (is_Pulling)
        {
            HandlePulling();
        }
        //Destroy
	}

    void OnCollisionEnter(Collision col)
    {
        if (is_Shooting)
        {
            if (col.gameObject.name == "Wall")
            {
                Debug.Log("Hit wall");
                StartPull();
            }
            else
            {
                Debug.Log("Hit something, startretract");
                StartRetracting();
            }
        }
    }
    public void StartShoot(float t_Shoot_Distance, Vector3 t_shoot_Direction, Vector3 t_Start_Position)
    {
        //Set shoot position, direction, rotation and timer;
        transform.position = t_Start_Position;
        shoot_Direction = t_shoot_Direction;
        transform.rotation = Quaternion.LookRotation(shoot_Direction) * transform.rotation;
        max_Shoot_Distance = t_Shoot_Distance;

        is_Shooting = true;
        PlayerStats.Instance.Shooting_Hook = true;
    }

    void HandleShooting()
    {
        m_Rigidbody.MovePosition(transform.position + shoot_Direction * shoot_Speed);
        //If distance reached or hit object start retract
        if (Vector3.Distance(Camera.main.transform.position,transform.position) > max_Shoot_Distance)//Reached distance
        {
            StartRetracting();
        }
    }

    void StartRetracting()
    {
        m_Rigidbody.velocity = Vector3.zero;
        
        is_Shooting = false;
        is_Retracting = true;
    }

    void HandleRetracting()
    {
        //Get direction back to player
        shoot_Direction = Camera.main.transform.position - transform.position;
        shoot_Direction.Normalize();

        //Move hook to player
        m_Rigidbody.MovePosition(transform.position + shoot_Direction * shoot_Speed);

        //Check if back at player
        if (Vector3.Distance(Camera.main.transform.position, transform.position) < 0.5f)
        {
            EndHook();
        }
    }

    void StartPull()
    {
        is_Pulling = true;
        is_Shooting = false;

        pull_Direction = transform.position - PlayerGlobal.Instance.transform.position;
        pull_Direction.Normalize();
    }
    void HandlePulling()
    {
        //Get pull direction
        pull_Direction = transform.position - PlayerGlobal.Instance.transform.position;
        pull_Direction.Normalize();

        //Pull player towards hook
        PlayerGlobal.Instance.Rigidbody.MovePosition(PlayerGlobal.Instance.transform.position + pull_Direction * shoot_Speed);

        //Check if back at player
        if (Vector3.Distance(PlayerGlobal.Instance.transform.position, transform.position) < 2f)
        {
            PlayerGlobal.Instance.Rigidbody.velocity = Vector3.zero;
            EndHook();
        }
    }

    void EndHook()
    {
        PlayerStats.Instance.Shooting_Hook = false;
        //Destroy
        Destroy(this.gameObject);
    }
}
