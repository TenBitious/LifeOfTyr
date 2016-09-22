using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Rigidbody m_Rigidbody;

    Vector3 _Move_Position;

    private bool can_Move;

    private float movement_Speed;
    private float speed_This_Frame;
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
        HandleMovePosition();
    }
    
    private void AssignDelegates()
    {
        PlayerEventManager.OnButtonForward += MoveForward;
        PlayerEventManager.OnButtonBack += MoveBackward;
        PlayerEventManager.OnButtonRight += StrifeRight;
        PlayerEventManager.OnButtonLeft += StrifeLeft;
    }

    
    void CheckCanMove()
    {
        if (PlayerStats.Instance.Shooting_Hook) can_Move = false;
        else can_Move = true;
    }
    void HandleMovePosition()
    {
        GetSpeedThisFrame();//Get speed

        if (can_Move) m_Rigidbody.MovePosition(transform.position + _Move_Position * speed_This_Frame);
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

}
