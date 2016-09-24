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
}
