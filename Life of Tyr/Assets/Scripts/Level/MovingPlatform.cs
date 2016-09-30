using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform startLocation, endLocation;
    private Vector3 originalStartPosition, originalEndPosition;

    private Transform destination;
    [Range(0.1f,5)]
    public float speed;
    private Rigidbody m_Rigidbody;

    private GameObject m_Player;

	// Use this for initialization
	void Start ()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");

        originalStartPosition = startLocation.position;
        originalEndPosition = endLocation.position;

        m_Rigidbody = GetComponent<Rigidbody>();
        destination = endLocation;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 dir =  destination.position - transform.position;
        dir.Normalize();
        m_Rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime);

        //transform.position += dir * speed * Time.deltaTime;
        //Set location childeren
        startLocation.position = originalStartPosition;
        endLocation.position = originalEndPosition;

        CheckDestinationReached();
	}

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject == m_Player)
        {
            //transform.SetParent(m_Player.transform);
            m_Player.transform.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject == m_Player)
        {
            m_Player.transform.parent = null;
        }
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
