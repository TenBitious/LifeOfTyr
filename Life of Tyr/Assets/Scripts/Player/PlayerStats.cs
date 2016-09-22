using UnityEngine;
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
        Debug.Log("Check grounded");
        Vector3 position1 = GetComponent<Collider>().bounds.min;
        position1.y -= 0.1f; 
        Debug.DrawRay(position1, Vector3.down*5);
        //Check 4 corners
        RaycastHit hit;
        if (Physics.Raycast(position1, Vector3.down*2f, out hit))
        {
            Debug.Log("Grounden object name: " + hit.transform.gameObject.name);
            return true;
        }
        return false;
    }
}
