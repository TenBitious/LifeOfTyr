using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Rigidbody m_Rigidbody;

    Vector3 _Move_Position;

    private bool can_Move, can_Jump;

    private float movement_Speed;
    private float speed_This_Frame;

    private float jump_Timer;

	// Use this for initialization
	void Start ()
    {
        AssignDelegates();
        movement_Speed = PlayerStats.Instance.movement_Speed;
        m_Rigidbody = GetComponent<Rigidbody>();
        can_Move = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CheckCanMove();
        HandleJumpCooldown();
        HandleMovePosition();
    }
    
    private void AssignDelegates()
    {
        PlayerEventManager.OnButtonForward += MoveForward;
        PlayerEventManager.OnButtonBack += MoveBackward;
        PlayerEventManager.OnButtonRight += StrifeRight;
        PlayerEventManager.OnButtonLeft += StrifeLeft;
        PlayerEventManager.OnButtonJump += Jump;
    }

    
    void CheckCanMove()
    {
        if (PlayerStats.Instance.Shooting_Hook) can_Move = false;
        else can_Move = true;
    }
    
    void HandleMovePosition()
    {
        GetSpeedThisFrame();//Get speed

        if (can_Move)
        {
            m_Rigidbody.MovePosition(transform.position + _Move_Position * speed_This_Frame);
        }

        //Reset temporary move position
        _Move_Position = Vector3.zero;
    }
    void GetSpeedThisFrame()
    {
        speed_This_Frame = movement_Speed * Time.deltaTime;
    }

    void MoveForward()
    {
        _Move_Position += transform.forward * speed_This_Frame;
    }
    void MoveBackward()
    {
        _Move_Position += -transform.forward * speed_This_Frame;
    }
    void StrifeLeft()
    {
        _Move_Position += -transform.right * speed_This_Frame;
    }
    void StrifeRight()
    {
        _Move_Position += transform.right * speed_This_Frame;
    }

    void Jump()
    {
        //Jump
        if (PlayerStats.Instance.Grounded() && can_Jump)
        {//Check if on ground
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
