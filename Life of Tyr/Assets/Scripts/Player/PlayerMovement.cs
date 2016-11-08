using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Rigidbody m_Rigidbody;

    private bool can_Move, can_Jump;

    private float movement_Speed, swing_Speed;
    private float max_Ground_Speed;

    private float walk_Speed_This_Frame;

    private float reduction_Air, reduction_Shooting;

    private float jump_Timer;

	// Use this for initialization
	void Start ()
    {

        AssignDelegates();
        movement_Speed = PlayerStats.Instance.movement_Speed;
        swing_Speed = PlayerStats.Instance.swing_Speed;
        reduction_Air = PlayerStats.Instance.reduction_Air;
        reduction_Shooting = PlayerStats.Instance.reduction_Shooting;
        max_Ground_Speed = PlayerStats.Instance.max_Movement_Speed;
        m_Rigidbody = GetComponent<Rigidbody>();
        can_Move = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        GetSpeedThisFrame();
        //HandleReduction();
        HandleJumpCooldown();
        HandleMaxSpeed();
        //Handle speed
        //Reduction
        //MAx speed
    }
    
    private void AssignDelegates()
    {
        PlayerEventManager.OnButtonForward += MoveForward;
        PlayerEventManager.OnButtonBack += MoveBackward;
        PlayerEventManager.OnButtonRight += StrifeRight;
        PlayerEventManager.OnButtonLeft += StrifeLeft;
        PlayerEventManager.OnButtonJump += Jump;
    }

    void HandleReduction()
    {//If player is shooting the player wont be able to move
        //if shooting
        if (PlayerGlobal.Instance.Is_Shooting_Hook)
        {
            walk_Speed_This_Frame -= walk_Speed_This_Frame * reduction_Shooting / 100;
        }
        //if in air
        if (!PlayerGlobal.Instance.Is_Grounded)
        {
            walk_Speed_This_Frame -= walk_Speed_This_Frame * reduction_Air / 100;
        }

    }

    void HandleMaxSpeed()
    {
        //TO DO: CHECK VELOCITY 
        //TO DO: CHECK IN WALKING
        //Walk speed
            if (PlayerGlobal.Instance.Rigidbody.velocity.magnitude > max_Ground_Speed)
            {
                Vector3 maxSpeed = PlayerGlobal.Instance.Rigidbody.velocity.normalized * max_Ground_Speed;
                maxSpeed.y = PlayerGlobal.Instance.Rigidbody.velocity.y;
                PlayerGlobal.Instance.Rigidbody.velocity = maxSpeed;


            }
    }

    void GetSpeedThisFrame()
    {
        walk_Speed_This_Frame = movement_Speed * Time.deltaTime;
    }

    void MoveForward()
    {
        if (PlayerGlobal.Instance.Is_Swinging)
        {
            m_Rigidbody.AddForce(transform.forward * swing_Speed * PlayerGlobal.Instance.Swing_Multiplier, ForceMode.Force);
        }
        else
        {
            Vector3 forwardSpeed = transform.forward * movement_Speed;
            m_Rigidbody.velocity += forwardSpeed;
        }
    }
    void MoveBackward()
    {
        if (PlayerGlobal.Instance.Is_Swinging)
        {
            
            m_Rigidbody.AddForce(-transform.forward * swing_Speed * PlayerGlobal.Instance.Swing_Multiplier, ForceMode.Force);
        }
        else
        {
            m_Rigidbody.velocity += -transform.forward * movement_Speed;
        }
    }
    void StrifeLeft()
    {
        m_Rigidbody.velocity += -transform.right * movement_Speed;
    }
    void StrifeRight()
    {
        m_Rigidbody.velocity += transform.right * movement_Speed;
    }

    void Jump()
    {
        //Jump
        if (PlayerGlobal.Instance.Is_Grounded && can_Jump)
        {//Check if on ground
            //m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
            m_Rigidbody.AddForce(Vector3.up * PlayerStats.Instance.jump_Speed, ForceMode.Force);
            can_Jump = false;
            jump_Timer = 0f;
        }
        
    }

    void HandleJumpCooldown()
    {
        jump_Timer += Time.deltaTime;

        if(jump_Timer >= PlayerStats.Instance.jump_Cooldown)
        {
            can_Jump = true;
        }
    }
}
