using UnityEngine;
using System.Collections;

public enum HookState { Shooting, Retracting, Pulling_Player, Pulling_Object, Waiting_Pull_Player_Order, Waiting_Pull_Object};
public class Hook : MonoBehaviour {

    private HookState m_HookState;

    private float  max_Shoot_Distance;
    private Vector3 shoot_Direction, retract_Direction, pull_Direction;
    private float shoot_Speed;

    private HookableObject object_Hooked_On;

    private Rigidbody m_Rigidbody;
    public Rigidbody joint_Rigidbody;

    private ConfigurableJoint m_Joint, player_Joint;
    private HookJoint m_HookJoint;

    private PlayerSwing m_Player_Swing;

    void Awake()
    {
        PlayerEventManager.OnRespawn += OnPlayerRespawn;
        Physics.IgnoreCollision(GetComponent<Collider>(), PlayerGlobal.Instance.GetComponent<Collider>());
    }
	// Use this for initialization
	void Start () 
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_HookJoint = GetComponentInChildren<HookJoint>();
        m_Player_Swing = PlayerGlobal.Instance.GetComponent<PlayerSwing>();
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
            if (col.gameObject.GetComponent<HookableObject>() != null)
            {
                m_Rigidbody.velocity = Vector3.zero;
                object_Hooked_On = col.gameObject.GetComponent<HookableObject>();
                HitHookableObject();
            }
            else
            {
                Debug.Log("Hit something, startretract");
                StartRetracting();
            }
        }
    }
    #region General methods
    
    void UnregisterMouseDelegates()
    {
        PlayerEventManager.OnMouseLeft -= StartPullPlayer;
        PlayerEventManager.OnMouseRight -= StartRetracting;
    }

    void OnPlayerRespawn()
    {
        EndHook();
    }
    void CreateJoints()
    {
        //Add hook joint on joint location
        m_HookJoint.CreateJoint();

        //Add player joint
        player_Joint = PlayerGlobal.Instance.gameObject.AddComponent<ConfigurableJoint>();
        
        player_Joint.connectedBody = joint_Rigidbody;
    }
    void DestroyJoints()
    {
        PlayerGlobal.Instance.Is_Swinging = false;

        Destroy(m_Joint);
        m_Player_Swing.EndSwing();
    }
    void EndHook()
    {
        PlayerGlobal.Instance.Rigidbody.useGravity = true;
        PlayerEventManager.OnRespawn -= OnPlayerRespawn;
        PlayerGlobal.Instance.Is_Shooting_Hook = false;
        PlayerGlobal.Instance.Hook_Connected = false;
        //Destroy
        Destroy(this.gameObject);
    }

    #endregion
    public void StartShoot(float t_Shoot_Distance, Vector3 t_shoot_Direction, Vector3 t_Start_Position, Quaternion t_Rotation)
    {
        //Set shoot position, direction, rotation and timer;
        transform.position = t_Start_Position;
        shoot_Direction = t_shoot_Direction;
        transform.rotation = t_Rotation;
        max_Shoot_Distance = t_Shoot_Distance;
        
        m_HookState = HookState.Shooting;
        PlayerGlobal.Instance.Is_Shooting_Hook = true;
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
        DestroyJoints();
        PlayerGlobal.Instance.Is_Swinging = false;
        PlayerGlobal.Instance.Hook_Connected = false;
        if (object_Hooked_On != null)
        {
            object_Hooked_On.HookRelease();
        }

        UnregisterMouseDelegates();
        m_Rigidbody.velocity = Vector3.zero;
        m_HookState = HookState.Retracting;
    }

    void HandleRetracting()
    {
        Debug.Log("Retracting");
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

    void HitHookableObject()
    {
        PlayerGlobal.Instance.Hook_Connected = true;
        object_Hooked_On.HookHit();
        CreateJoints();
        PlayerEventManager.Instance.HookHit();

        Debug.Log("Hit wall");
        PlayerEventManager.OnMouseLeft += StartPullPlayer;
        PlayerEventManager.OnMouseRight += StartRetracting;
        
        m_HookState = HookState.Waiting_Pull_Player_Order;
    }

    
    #region //Region pull player
    void StartPullPlayer()
    {
        DestroyJoints();
        PlayerGlobal.Instance.Is_Swinging = false;
        Debug.Log("Start pull player");
        UnregisterMouseDelegates();
        m_HookState = HookState.Pulling_Player;
    }
    void HandlePullingPlayer()
    {
        PlayerGlobal.Instance.Rigidbody.useGravity = false;
        //Get pull direction
        pull_Direction = transform.position - PlayerGlobal.Instance.transform.position;
        pull_Direction.Normalize();

        //Pull player towards hook
        PlayerGlobal.Instance.Rigidbody.MovePosition(PlayerGlobal.Instance.transform.position + pull_Direction * shoot_Speed);

        //Check if back at player
        if (Vector3.Distance(PlayerGlobal.Instance.transform.position, transform.position) < 2f)
        {
            PlayerGlobal.Instance.Rigidbody.velocity = Vector3.zero;
            object_Hooked_On.HookRelease();
            EndHook();
        }
    }
    #endregion

    
    void HandleWaitingPullAction()
    {
        //Accept pull
        //Decline pull
    }

    

   

    
    
}
