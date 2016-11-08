using UnityEngine;
using System.Collections;

public class PlayerSwing : MonoBehaviour {

    private ConfigurableJoint m_Joint;

    private bool canSwing;
    public float exceed_Force;

    private bool swinging_Up;
    private float previousY;
    private float swingMuliplier = 1;
    [Range(0, 1)]
    public float multiplierReduction, multiplierIncrease;
	// Use this for initialization
	void Start () 
    {
        PlayerEventManager.OnHookHit += StartHookHit;
	}

	// Update is called once per frame
	void Update () 
    {
        //Check if swinging
        CheckSwinging();
        CheckSwingUpDown();
        ApplySwingMultiplier();
        //Handle swing speed
        if (PlayerGlobal.Instance.Is_Swinging)
        {
            //HandleSwingSpeed();
        }
        //Handle joint configuration
        HandleJointConfig();

        //If not on ground && wall hit


        previousY = transform.position.y;
        
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

    void CheckSwinging()
    {
        if (!PlayerGlobal.Instance.Is_Grounded && PlayerGlobal.Instance.Hook_Connected && canSwing)
        {//Player is swinging
            PlayerGlobal.Instance.Is_Swinging = true;
        }
        else
        {
            PlayerGlobal.Instance.Is_Swinging = false;
        }
    }

    
    void CheckSwingUpDown()
    {
        
        if (transform.position.y > previousY)
        {
            swinging_Up = true;
        }
        else
        {
            swinging_Up = false;
        }
    }

    void ApplySwingMultiplier()
    {
        if (swinging_Up)
        {
            //Reduce 
            swingMuliplier -= multiplierReduction;
        }
        else
        {
            swingMuliplier += multiplierIncrease;
        }

        if (swingMuliplier < 0)
        {
            swingMuliplier = 0;
        }
        if (swingMuliplier > 1)
        {
            swingMuliplier = 1;
        }

        PlayerGlobal.Instance.Swing_Multiplier = swingMuliplier;
        Debug.Log("Swing multiplier: " + swingMuliplier);
    }

    void HandleJointConfig()
    {
        if (m_Joint != null)
        {
            if (PlayerGlobal.Instance.Is_Swinging)
            {
                m_Joint.xMotion = ConfigurableJointMotion.Locked;
                m_Joint.yMotion = ConfigurableJointMotion.Locked;
                m_Joint.zMotion = ConfigurableJointMotion.Locked;
            }
            else
            {
                m_Joint.xMotion = ConfigurableJointMotion.Free;
                m_Joint.yMotion = ConfigurableJointMotion.Free;
                m_Joint.zMotion = ConfigurableJointMotion.Free;

                m_Joint.anchor = Vector3.zero;
                m_Joint.axis = Vector3.zero;
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
