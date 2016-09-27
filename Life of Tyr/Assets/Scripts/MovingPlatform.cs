using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform startLocation, endLocation;
    private Transform destination;
    public float speed;
    private Rigidbody m_Rigidbody;

	// Use this for initialization
	void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        destination = endLocation;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 dir =  destination.transform.position - transform.position;
        dir.Normalize();
        m_Rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime);

        CheckDestinationReached();
	}

    void CheckDestinationReached()
    {
        if(Vector3.Distance(transform.position,destination.transform.position)< 0.1f)
        {
            DestinationReached();
        }
    }

    void DestinationReached()
    {
        if (destination = startLocation) destination = endLocation;
        else if (destination = endLocation) destination = startLocation;
    }
}
