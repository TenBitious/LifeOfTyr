using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour {
    Dictionary<string, KeyCode> directions = new Dictionary<string, KeyCode>();
    public KeyCode button_Forward = KeyCode.W, button_Right = KeyCode.D, button_Left = KeyCode.A, button_Back = KeyCode.S, button_Jump = KeyCode.Space;
    

    PlayerEventManager m_EventManager;
    
	// Use this for initialization
	void Start () 
    {
        m_EventManager = GetComponent<PlayerEventManager>();

        directions.Add("Forward", button_Forward);
        directions.Add("Right", button_Right);
        directions.Add("Left", button_Left);
        directions.Add("Back", button_Back);
        directions.Add("Jump", button_Jump);
	}
	

	// Update is called once per frame
	void FixedUpdate () 
    {
        GetKeyInput();
        GetMouseRotation();
        GetMouseInput();
	}

    private void GetKeyInput()
    {
        foreach (KeyValuePair<string, KeyCode> kc in directions)
        {
            if (Input.GetKey(kc.Value))
            {
                EventButton(kc.Key);
            }
            if (Input.GetKeyUp(kc.Value))
            {
                EventButton(kc.Key + "Release");
            }
        }
    }

    private void GetMouseRotation()
    {

    }

    private void GetMouseInput()
    {
        if(Input.GetMouseButton(0))
        {
            m_EventManager.MouseLeft();
        }

        if (Input.GetMouseButton(1))
        {
            m_EventManager.MouseRight();
        }
    }
    void LateUpdate()
    {
        foreach (KeyValuePair<string, KeyCode> kc in directions)
        {
            if (Input.GetKeyUp(kc.Value))
            {
                EventButton(kc.Key + "Release");
            }
        }
    }


    void EventButton(string c_Button)
    {
        m_EventManager.Invoke(c_Button, 0f);
    }
}
