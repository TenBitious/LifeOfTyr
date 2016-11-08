using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform startLocation, endLocation;

    private Vector3 originalStartPosition, originalEndPosition;

    private Transform destination;
    private Vector3 direction;

    [Range(0,5)]
    public float speed;
    private Rigidbody m_Rigidbody;

    private GameObject m_Player, m_Hook;

    private bool is_Player_On_Me, is_Hook_On_Me;

	// Use this for initialization
	void Start ()
    {
        IgnoreObjects();
        m_Player = GameObject.FindGameObjectWithTag("Player");

        originalStartPosition = startLocation.position;
        originalEndPosition = endLocation.position;

        m_Rigidbody = GetComponent<Rigidbody>();
        destination = endLocation;
	}

    void IgnoreObjects()
    {

    }
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Get Direction
        UpdateDirection();
        //Move platform
        MovePlatform();
        //Update player position
        UpdatePlayer();
        //Update hook if has hook
        UpdateHook();
        //Set location childeren
        UpdateDestinaions();
        

        CheckDestinationReached();
	}

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject == m_Player)
        {
            Debug.Log("Player on me: " + is_Player_On_Me);
            is_Player_On_Me = true;
        }
        if(col.gameObject.GetComponent<Hook>() != null)
        {
            m_Hook = col.gameObject;
            is_Hook_On_Me = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject == m_Player)
        {
            is_Player_On_Me = false;
        }
        if (col.gameObject.GetComponent<Hook>() != null)
        {
            m_Hook = null;
            is_Hook_On_Me = false;
        }
    }

    void UpdateDirection()
    {
        direction = destination.position - transform.position;
        direction.Normalize();
    }

    void MovePlatform()
    {
        transform.position += direction * speed * Time.deltaTime;
        //m_Rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    void UpdatePlayer()
    {
        if (is_Player_On_Me)
        {
            PlayerGlobal.Instance.transform.position += direction * speed * Time.deltaTime;
        }
    }
    
    void UpdateHook()
    {
        if (is_Hook_On_Me)
        {
            m_Hook.transform.position += direction * speed * Time.deltaTime;
        }
    }
    void UpdateDestinaions()
    {
        startLocation.position = originalStartPosition;
        endLocation.position = originalEndPosition;
    }

    void CheckDestinationReached()
    {
        if(Vector3.Distance(transform.position,destination.position)< 0.1f)
        {
            DestinationReached();
        }
    }

    void DestinationReached()
    {
        if (destination == startLocation)
        {
            destination = endLocation;
        }
        else if (destination == endLocation)
        {
            destination = startLocation;
        }
    }
}
