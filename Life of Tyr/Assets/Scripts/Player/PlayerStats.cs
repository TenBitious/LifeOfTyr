using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    private static PlayerStats _instance;
    public static PlayerStats Instance { get { return _instance; } }

    public float movement_Speed, jump_Speed;
    public float jump_Cooldown;
    public float shoot_Speed, shoot_Distance;

    private bool is_Shooting_Hook = false;

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
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public bool Shooting_Hook
    {
        get { return is_Shooting_Hook; }
        set { is_Shooting_Hook = value; }
    }

    public bool Grounded()
    {
        CapsuleCollider m_CapsuleCollider = GetComponent<CapsuleCollider>();
        float distance_To_Ground = m_CapsuleCollider.bounds.extents.y + 0.1f;
        float capsule_Width = m_CapsuleCollider.bounds.extents.x;

        Ray cast_Direction = new Ray(transform.position,Vector3.down);

        Debug.DrawRay(transform.position,Vector3.down* distance_To_Ground, Color.red,.5f);

        if (Physics.SphereCast(cast_Direction, capsule_Width, distance_To_Ground))
        {
            return true;
        }
        return false;
    }
}
