using UnityEngine;
using System.Collections;

public enum HookState { Shooting, Retracting, Pulling_Player, Pulling_Object, Waiting_Pull_Player_Order, Waiting_Pull_Object};
public class Hook : MonoBehaviour {

    private HookState m_HookState;

    private float  max_Shoot_Distance;
    private Vector3 shoot_Direction, retract_Direction, pull_Direction;
    private float shoot_Speed;

    private Wall wall_Hooked_On;

    private Rigidbody m_Rigidbody;

    void Awake()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), PlayerGlobal.Instance.GetComponent<Collider>());
    }
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
        if (m_HookState == HookState.Shooting)
        {
            HandleShooting();
        }
        //Handle retracting
        if (m_HookState == HookState.Retracting)
        {
            HandleRetracting();
        }

        if (m_HookState == HookState.Pulling_Player)
        {
            HandlePullingPlayer();
        }
        
        //Destroy
	}

    void OnCollisionEnter(Collision col)
    {
        if (m_HookState == HookState.Shooting)
        {
            if (col.gameObject.GetComponent<Wall>() != null)
            {
                wall_Hooked_On = col.gameObject.GetComponent<Wall>();
                HitWall();
            }
            else
            {
                Debug.Log("Hit something, startretract");
                StartRetracting();
            }
        }
    }
    public void StartShoot(float t_Shoot_Distance, Vector3 t_shoot_Direction, Vector3 t_Start_Position, Quaternion t_Rotation)
    {
        //Set shoot position, direction, rotation and timer;
        transform.position = t_Start_Position;
        shoot_Direction = t_shoot_Direction;
        transform.rotation = t_Rotation;
        max_Shoot_Distance = t_Shoot_Distance;
        
        m_HookState = HookState.Shooting;
        PlayerStats.Instance.Shooting_Hook = true;
    }

    void HandleShooting()
    {
        m_Rigidbody.MovePosition(transform.position + shoot_Direction * shoot_Speed);

        //If distance reached
        if (Vector3.Distance(Camera.main.transform.position,transform.position) > max_Shoot_Distance)//Reached distance
        {
            StartRetracting();
        }
    }

    void StartRetracting()
    {
        if(wall_Hooked_On != null)
        {
            wall_Hooked_On.HookRelease();
        }

        UnregisterDelegates();
        m_Rigidbody.velocity = Vector3.zero;
        m_HookState = HookState.Retracting;
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

    void HitWall()
    {
        wall_Hooked_On.HookHit();

        Debug.Log("Hit wall");
        PlayerEventManager.OnMouseLeft += StartPullPlayer;
        PlayerEventManager.OnMouseRight += StartRetracting;
        
        m_HookState = HookState.Waiting_Pull_Player_Order;
    }

    void StartPullPlayer()
    {
        Debug.Log("Start pull player");
        UnregisterDelegates();
        m_HookState = HookState.Pulling_Player;
    }
    void HandlePullingPlayer()
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
            wall_Hooked_On.HookRelease();
            EndHook();
        }
    }

    void HandleWaitingPullAction()
    {
        //Accept pull
        //Decline pull
    }

    void EndHook()
    {
        PlayerStats.Instance.Shooting_Hook = false;
        //Destroy
        Destroy(this.gameObject);
    }

    void UnregisterDelegates()
    {
        PlayerEventManager.OnMouseLeft -= StartPullPlayer;
        PlayerEventManager.OnMouseRight -= StartRetracting;
    }
}
