using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Rigidbody m_Rigidbody;

    Vector3 _Move_Position;

    public float movement_Speed;
    private float speed_This_Frame;
	// Use this for initialization
	void Start ()
    {
        AssignDelegates();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        speed_This_Frame = movement_Speed * Time.deltaTime;
        Debug.Log("Move pos: " + _Move_Position);
        m_Rigidbody.MovePosition(transform.position + _Move_Position * speed_This_Frame);
        _Move_Position = Vector3.zero;
    }
    
    private void AssignDelegates()
    {
        PlayerEventManager.OnButtonForward += MoveForward;
        PlayerEventManager.OnButtonBack += MoveBackward;
        PlayerEventManager.OnButtonRight += StrifeRight;
        PlayerEventManager.OnButtonLeft += StrifeLeft;
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
