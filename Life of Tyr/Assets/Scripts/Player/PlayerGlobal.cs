﻿using UnityEngine;
using System.Collections;

public class PlayerGlobal : MonoBehaviour {

    private static PlayerGlobal _instance;
    public static PlayerGlobal Instance { get { return _instance; } }
    private static Vector3 start_Position;
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
        start_Position = transform.position;
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
    public Vector3 StartPosition
    {
        get { return start_Position; }
    }
    public bool Grounded()
    {
        CapsuleCollider m_CapsuleCollider = GetComponent<CapsuleCollider>();
        float cast_To_Ground = m_CapsuleCollider.bounds.extents.y/4 + 0.2f;
        float capsule_Width = m_CapsuleCollider.bounds.extents.x * .9f;
        Debug.Log("capsuel width: " + capsule_Width);
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
}