using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    private static PlayerStats _instance;
    public static PlayerStats Instance { get { return _instance; } }

    public float movement_Speed, jump_Speed;
    public float swing_Speed, max_Swing_Speed;
    public float jump_Cooldown;

    [Range(0,100)]
    public float reduction_Air, reduction_Shooting;

    public float shoot_Speed, shoot_Distance;

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

 
}
