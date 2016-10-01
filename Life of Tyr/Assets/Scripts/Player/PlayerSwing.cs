using UnityEngine;
using System.Collections;

public class PlayerSwing : MonoBehaviour {

    private ConfigurableJoint m_Joint;
	// Use this for initialization
	void Start () 
    {
        PlayerEventManager.OnHookHit += StartHookHit;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
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
                m_Joint.xMotion = ConfigurableJointMotion.Locked;
                m_Joint.yMotion = ConfigurableJointMotion.Locked;
                m_Joint.zMotion = ConfigurableJointMotion.Locked;
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

    public void EndSwing()
    {
        //Delete joint
        Debug.Log("end swing player swing");
        Destroy(m_Joint);
    }


}
