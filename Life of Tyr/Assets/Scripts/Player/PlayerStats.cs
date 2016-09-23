using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    private static PlayerStats _instance;
    public static PlayerStats Instance { get { return _instance; } }

    public float movement_Speed, jump_Speed;
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
        Vector3 collider_Bounds = GetComponent<Collider>().bounds.extents;
        BoxCollider b = GetComponent<BoxCollider>();
        Dictionary<Vector3, bool> corners = new Dictionary<Vector3, bool>();
        Vector3 corner_1 = transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f);
        Vector3 corner_2 = transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f);
        Vector3 corner_3 = transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f);
        Vector3 corner_4 = transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f);
        corners.Add(corner_1, Physics.Raycast(corner_1, Vector3.down, collider_Bounds.y + 0.1f));
        corners.Add(corner_2, Physics.Raycast(corner_2, Vector3.down, collider_Bounds.y + 0.1f));
        corners.Add(corner_3, Physics.Raycast(corner_3, Vector3.down, collider_Bounds.y + 0.1f));
        corners.Add(corner_4, Physics.Raycast(corner_4, Vector3.down, collider_Bounds.y + 0.1f));



        //Midle of hitbox
        bool hitGround = Physics.Raycast(transform.position, Vector3.down, collider_Bounds.y + 0.1f);
        bool hitGroundCorner1 = Physics.Raycast(corner_1, Vector3.down, collider_Bounds.y + 0.1f);
        bool hitGroundCorner2= Physics.Raycast(corner_2, Vector3.down, collider_Bounds.y + 0.1f);
        bool hitGroundCorner3 = Physics.Raycast(corner_3, Vector3.down, collider_Bounds.y + 0.1f);
        bool hitGroundCorner4 = Physics.Raycast(corner_4, Vector3.down, collider_Bounds.y + 0.1f);

        foreach (KeyValuePair<Vector3, bool> corner in corners)
        {
            if (corner.Value)
            {
                Debug.DrawLine(corner.Key, new Vector3(corner.Key.x, corner.Key.y + collider_Bounds.y, corner.Key.z), Color.green, 1f);
            }
            else
            {
                Debug.DrawLine(corner.Key, new Vector3(corner.Key.x, corner.Key.y + collider_Bounds.y, corner.Key.z), Color.red, .5f);
            }
        }
        if (corners.ContainsValue(true))
        {
            return true;
        }
        else
        {
            Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + collider_Bounds.y, transform.position.z), Color.red, .5f);
            return false;
        }
    }

    void OnDrawGizmosSelected()
    {
        BoxCollider b = GetComponent<BoxCollider>();

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f),.1f);
        Gizmos.DrawSphere(transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f), .1f);
        Gizmos.DrawSphere(transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f), .1f);
        Gizmos.DrawSphere(transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f), .1f);
    }
}
