﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Rigidbody m_Rigidbody;

    Vector3 _Move_Position;

    private bool can_Move, can_Jump;

    private float movement_Speed;
    private float speed_This_Frame;

    private float reduction_Air, reduction_Shooting;

    private float jump_Timer;

	// Use this for initialization
	void Start ()
    {
        AssignDelegates();
        movement_Speed = PlayerStats.Instance.movement_Speed;
        reduction_Air = PlayerStats.Instance.reduction_Air;
        reduction_Shooting = PlayerStats.Instance.reduction_Shooting;

        m_Rigidbody = GetComponent<Rigidbody>();
        can_Move = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        GetSpeedThisFrame();
        HandleReduction();
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


    void HandleReduction()
    {//If player is shooting the player wont be able to move
        //if shooting
        if (PlayerGlobal.Instance.Is_Shooting_Hook)
        {
            speed_This_Frame -= speed_This_Frame * reduction_Shooting / 100;
        }
        //if in air
        if (!PlayerGlobal.Instance.Is_Grounded)
        {
            speed_This_Frame -= speed_This_Frame * reduction_Air / 100;
        }

    }
    
    void HandleMovePosition()
    {
        if (can_Move)
        {
            Debug.Log("Move_Position: " + _Move_Position);
            _Move_Position.Normalize();
            _Move_Position *= speed_This_Frame;

            m_Rigidbody.MovePosition(transform.position + _Move_Position * speed_This_Frame);
            //transform.position += _Move_Position * speed_This_Frame;
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
        transform.Translate(transform.forward * speed_This_Frame, Space.World);
    }
    void MoveBackward()
    {
        transform.Translate(-transform.forward * speed_This_Frame, Space.World);
    }
    void StrifeLeft()
    {
        transform.Translate(-transform.right * speed_This_Frame, Space.World);
    }
    void StrifeRight()
    {
        transform.Translate(transform.right * speed_This_Frame, Space.World);
    }

    void Jump()
    {
        //Jump
        if (PlayerGlobal.Instance.Is_Grounded && can_Jump)
        {//Check if on ground
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
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
