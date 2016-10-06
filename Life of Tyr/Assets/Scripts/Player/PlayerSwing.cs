using UnityEngine;
using System.Collections;

public class PlayerSwing : MonoBehaviour {

    private ConfigurableJoint m_Joint;

    private bool canSwing;

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
        //Handle joint configuration
        HandleJointConfig();
        
	    //If not on ground && wall hit
        
	}

    void StartHookHit()
    {
        PlayerGlobal.Instance.Is_Swinging = false;
        canSwing = true;
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
        if (PlayerGlobal.Instance.Rigidbody.velocity.magnitude > PlayerStats.Instance.max_Swing_Speed)
        {
            //x
            float exceed = PlayerGlobal.Instance.Rigidbody.velocity.magnitude - PlayerStats.Instance.max_Swing_Speed;
            float reduction = exceed_Force / exceed;
            PlayerGlobal.Instance.Rigidbody.AddForce(-PlayerGlobal.Instance.Rigidbody.velocity.normalized * reduction, ForceMode.Force);
        }
    }

    void HandleJointConfig()
    {
        if (m_Joint != null)
        {
            if (!PlayerGlobal.Instance.Is_Grounded && PlayerGlobal.Instance.Hook_Connected && canSwing)
            {
                if (!PlayerGlobal.Instance.Is_Swinging)
                {
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
    }

    public void EndSwing()
    {
        canSwing = false;
        //Delete joint
        Debug.Log("end swing player swing");
        Destroy(m_Joint);
    }


}
