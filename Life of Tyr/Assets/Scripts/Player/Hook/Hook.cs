﻿using UnityEngine;
using System.Collections;

public enum HookState { Shooting, Retracting, Pulling_Player, Pulling_Object, Waiting_Pull_Player_Order, Waiting_Pull_Object};
public class Hook : MonoBehaviour {

    private HookState m_HookState;

    private float  max_Shoot_Distance;
    private Vector3 shoot_Direction, retract_Direction, pull_Direction;
    private float shoot_Speed;

    private Wall wall_Hooked_On;

    private Rigidbody m_Rigidbody;
    public Rigidbody joint_Rigidbody;

    private ConfigurableJoint m_Joint, m_Joint_Location, player_Joint;
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
        PlayerGlobal.Instance.Hook_Connected = false;
        if (wall_Hooked_On != null)
        {
            wall_Hooked_On.HookRelease();
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

    void HitWall()
    {
       
        PlayerGlobal.Instance.Hook_Connected = true;
        wall_Hooked_On.HookHit();

        CreateJoints();
        PlayerEventManager.Instance.HookHit();

        Debug.Log("Hit wall");
        PlayerEventManager.OnMouseLeft += StartPullPlayer;
        PlayerEventManager.OnMouseRight += StartRetracting;
        
        m_HookState = HookState.Waiting_Pull_Player_Order;
    }

    void CreateJoints()
    {
        //Anchor point = hook/JointLocation
        m_Joint_Location = gameObject.AddComponent<ConfigurableJoint>();
        m_Joint_Location.axis = Vector3.back;
        m_Joint_Location.anchor = Vector3.zero;

        m_Joint_Location.xMotion = ConfigurableJointMotion.Locked;
        m_Joint_Location.yMotion = ConfigurableJointMotion.Locked;
        m_Joint_Location.zMotion = ConfigurableJointMotion.Locked;
        //Add hook joint on joint location
        joint_Rigidbody.isKinematic = false;
        m_Joint = joint_Rigidbody.gameObject.AddComponent<ConfigurableJoint>();
        m_Joint.axis = Vector3.back;
        m_Joint.anchor = Vector3.zero;
        m_Joint.connectedBody = m_Rigidbody;

        m_Joint.xMotion = ConfigurableJointMotion.Locked;
        m_Joint.yMotion = ConfigurableJointMotion.Locked;
        m_Joint.zMotion = ConfigurableJointMotion.Locked;
        //Add player joint
        player_Joint = PlayerGlobal.Instance.gameObject.AddComponent<ConfigurableJoint>();
        player_Joint.axis = Vector3.back;
        player_Joint.anchor = Vector3.zero;
        player_Joint.connectedBody = joint_Rigidbody;
    }

    void StartPullPlayer()
    {
        DestroyJoints();
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
        PlayerGlobal.Instance.Rigidbody.useGravity = true;
        PlayerEventManager.OnRespawn -= OnPlayerRespawn;
        PlayerGlobal.Instance.Is_Shooting_Hook = false;
        PlayerGlobal.Instance.Hook_Connected = false;
        //Destroy
        Destroy(this.gameObject);
    }

    void UnregisterMouseDelegates()
    {
        PlayerEventManager.OnMouseLeft -= StartPullPlayer;
        PlayerEventManager.OnMouseRight -= StartRetracting;
    }

    void OnPlayerRespawn()
    {
        EndHook();
    }

    void DestroyJoints()
    {
        PlayerGlobal.Instance.Is_Swinging = false;

        Destroy(m_Joint);
        Destroy(m_Joint_Location);
        m_Player_Swing.EndSwing();
    }
}
