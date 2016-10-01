using UnityEngine;
using System.Collections;

public class PlayerGlobal : MonoBehaviour {

    private static PlayerGlobal _instance;
    public static PlayerGlobal Instance { get { return _instance; } }
    private static Vector3 start_Position;

    private Rigidbody m_Rigidbody;
    private CapsuleCollider m_CapsuleCollider;
    private Vector3 m_POV;
    private Transform m_POV_Transform;

    private bool is_Shooting_Hook = false;
    private bool hook_Connected = false;
    private bool is_Grounded;
    private bool is_Swinging;

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

	// Use this for initialization
	void Start () 
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
        start_Position = transform.position;
	}

    void Update()
    {
        GetPOV();
        is_Grounded = Grounded();
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
    public Vector3 StartPosition
    {
        get { return start_Position; }
    }
    public bool Grounded()
    {
        float cast_To_Ground = m_CapsuleCollider.bounds.extents.y/4 + 0.1f;
        float capsule_Width = m_CapsuleCollider.bounds.extents.x * .9f;

        Ray cast_Direction = new Ray(transform.position, Vector3.down);
        

        if (Physics.SphereCast(cast_Direction,capsule_Width, cast_To_Ground))
        {
            Debug.DrawRay(transform.position, Vector3.down * cast_To_Ground, Color.green, 2f);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * cast_To_Ground, Color.red, 2f);
        }
        return false;
    }

    public bool Is_Shooting_Hook
    {
        get { return is_Shooting_Hook; }
        set { is_Shooting_Hook = value; }
    }

    public bool Hook_Connected
    {
        get { return hook_Connected; }
        set { hook_Connected = value; }
    }

    public bool Is_Grounded
    {
        get { return is_Grounded; }
    }

    public bool Is_Swinging
    {
        get { return is_Swinging; }
        set { is_Swinging = value; }
    }
}
