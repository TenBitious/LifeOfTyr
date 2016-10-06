using UnityEngine;
using System.Collections;

public class HookJoint : MonoBehaviour {

    private Rigidbody m_Rigidbody;

    private ConfigurableJoint m_Joint;

    public Quaternion rotateDown;

	// Use this for initialization
	void Start () 
    {
        m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    //Create joint
    public void CreateJoint()
    {
        m_Rigidbody.isKinematic = false;
        m_Joint = gameObject.AddComponent<ConfigurableJoint>();
        m_Joint.axis = Vector3.back;
        m_Joint.anchor = Vector3.zero;

        m_Joint.xMotion = ConfigurableJointMotion.Locked;
        m_Joint.yMotion = ConfigurableJointMotion.Locked;
        m_Joint.zMotion = ConfigurableJointMotion.Locked;

        m_Joint.angularXMotion = ConfigurableJointMotion.Free;
        m_Joint.angularYMotion = ConfigurableJointMotion.Free;
        m_Joint.angularZMotion = ConfigurableJointMotion.Free;

        SoftJointLimit SJLXLow = m_Joint.lowAngularXLimit;
        SJLXLow.limit = -90;
        m_Joint.lowAngularXLimit = SJLXLow;
        SoftJointLimit SJLXHigh = m_Joint.highAngularXLimit;
        SJLXHigh.limit = 90;
        m_Joint.highAngularXLimit = SJLXHigh;

        m_Joint.rotationDriveMode = RotationDriveMode.Slerp;
        JointDrive SD = m_Joint.slerpDrive;
        SetTargetRotation();
        //SD.positionSpring = 10000;
        //SD.positionDamper = 10;
        m_Joint.slerpDrive = SD;

    }

    
    //Set target rotation down
    private void SetTargetRotation()
    {
        m_Rigidbody.mass = 200;
        m_Rigidbody.angularDrag = 300;
    }

    void OnInspectorGUI()
    {
        if (GUILayout.Button("Update target rotation"))
        {
            
        }
    }
}
