using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private Rigidbody m_Rigidbody;

    public float speedBoost;

    private float speedBoostCooldown = 0f;

	// Use this for initialization
	void Start ()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Ball").GetComponent<Collider>());
        m_Rigidbody = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        speedBoostCooldown += Time.deltaTime;

	    if(m_Rigidbody.velocity.magnitude < 5f && speedBoostCooldown >= 4f)
        {
            SpeedBoost();
        }
	}

    void SpeedBoost()
    {
        speedBoostCooldown = 0f;
        Vector3 speedDir = m_Rigidbody.velocity;
        speedDir.Normalize();
        m_Rigidbody.velocity = speedDir * speedBoost;
    }
}
