using UnityEngine;
using System.Collections;

public class PlayerSwing : MonoBehaviour {

    private ConfigurableJoint m_Joint;

    public float exceed_Force;
	// Use this for initialization
	void Start () 
    {
        PlayerEventManager.OnHookHit += StartHookHit;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        //Handle max speed
        HandleSwingSpeed();
        if (m_Joint != null)
        {
            // Distance to hook is max xyzMotion = locked;
            m_Joint.anchor = Vector3.zero;
            if (!PlayerGlobal.Instance.Is_Grounded && PlayerGlobal.Instance.Hook_Connected)
            {
                if (!PlayerGlobal.Instance.Is_Swinging)
                {
                    m_Joint.axis = Vector3.back;
                    m_Joint.anchor = Vector3.zero;

                    PlayerGlobal.Instance.Is_Swinging = true;
                    m_Joint.autoConfigureConnectedAnchor = false;
                }
                m_Joint.xMotion = ConfigurableJointMotion.Locked;
                m_Joint.yMotion = ConfigurableJointMotion.Locked;
                m_Joint.zMotion = ConfigurableJointMotion.Locked;

            }
            else
            {
                PlayerGlobal.Instance.Is_Swinging = false;
                m_Joint.xMotion = ConfigurableJointMotion.Free;
                m_Joint.yMotion = ConfigurableJointMotion.Free;
                m_Joint.zMotion = ConfigurableJointMotion.Free;
                m_Joint.axis = Vector3.back;
                    m_Joint.anchor = Vector3.zero;
            }
        }
	    //If not on ground && wall hit
        
	}

    void StartHookHit()
    {
        PlayerGlobal.Instance.Is_Swinging = false;

        m_Joint = GetComponent<ConfigurableJoint>();

        m_Joint.xMotion = ConfigurableJointMotion.Free;
        m_Joint.yMotion = ConfigurableJointMotion.Free;
        m_Joint.zMotion = ConfigurableJointMotion.Free;
    }

    void SwingForward()
    {

    }

    void HandleSwingSpeed()
    {
        Debug.Log("Sing speed : " + PlayerGlobal.Instance.Rigidbody.velocity.magnitude);
        if (PlayerGlobal.Instance.Rigidbody.velocity.magnitude > PlayerStats.Instance.max_Swing_Speed)
        {
            //x
            float exceed = PlayerGlobal.Instance.Rigidbody.velocity.magnitude - PlayerStats.Instance.max_Swing_Speed;
            float reduction = exceed_Force / exceed;
            PlayerGlobal.Instance.Rigidbody.AddForce(-PlayerGlobal.Instance.Rigidbody.velocity.normalized * reduction, ForceMode.Impulse);
        }
    }

    public void EndSwing()
    {
        //Delete joint
        Debug.Log("end swing player swing");
        Destroy(m_Joint);
    }


}
