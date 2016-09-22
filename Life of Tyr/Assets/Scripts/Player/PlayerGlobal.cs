using UnityEngine;
using System.Collections;

public class PlayerGlobal : MonoBehaviour {

    private static PlayerGlobal _instance;
    public static PlayerGlobal Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private Rigidbody m_Rigidbody;
    private Vector3 m_POV;
    private Transform m_POV_Transform;

	// Use this for initialization
	void Start () 
    {
        m_Rigidbody = GetComponent<Rigidbody>();
	}

    void Update()
    {
        GetPOV();
    }
    public Rigidbody Rigidbody { get { return m_Rigidbody; } }
    public Vector3 PointOfView
    {

        get { return m_POV; }
    }
    public Transform POVTransform 
    { 
        get { return m_POV_Transform; } 
    }
    void GetPOV()
    {
        m_POV_Transform = Camera.main.transform;
        m_POV = m_POV_Transform.position;
    }
}
