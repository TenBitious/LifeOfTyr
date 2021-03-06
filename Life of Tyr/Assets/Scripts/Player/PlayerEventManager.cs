﻿using UnityEngine;
using System.Collections;

public class PlayerEventManager : MonoBehaviour {
    private static PlayerEventManager _instance;
    public static PlayerEventManager Instance { get { return _instance; } }

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
    public delegate void ButtonForwardAction();
    public static event ButtonForwardAction OnButtonForward;
    public delegate void ButtonForwardReleaseAction();
    public static event ButtonForwardReleaseAction OnButtonForwardRelease;

    public delegate void ButtonRightAction();
    public static event ButtonRightAction OnButtonRight;
    public delegate void ButtonRightReleaseAction();
    public static event ButtonRightReleaseAction OnButtonRightRelease;

    public delegate void ButtonLeftAction();
    public static event ButtonLeftAction OnButtonLeft;
    public delegate void ButtonLeftReleaseAction();
    public static event ButtonLeftReleaseAction OnButtonLeftRelease;

    public delegate void ButtonBackAction();
    public static event ButtonBackAction OnButtonBack;
    public delegate void ButtonBackReleaseAction();
    public static event ButtonBackReleaseAction OnButtonBackRelease;

    public delegate void MouseLeftAction();
    public static event MouseLeftAction OnMouseLeft;
    public delegate void MouseRightAction();
    public static event MouseRightAction OnMouseRight;

    public delegate void ButtonJumpAction();
    public static event ButtonJumpAction OnButtonJump;
    public delegate void ButtonJumpReleaseAction();
    public static event ButtonJumpReleaseAction OnButtonJumpRelease;

    public delegate void PlayerRespawn();
    public static event PlayerRespawn OnRespawn;

    public delegate void PlayerHookHit();
    public static event PlayerHookHit OnHookHit;

    public void Forward()
    {
        if (OnButtonForward != null)
        {
            OnButtonForward();
        }
    }

    public void ForwardRelease()
    {
        if (OnButtonForwardRelease != null)
        {
            OnButtonForwardRelease();
        }
    }

    public void Right()
    {
        if (OnButtonRight != null)
        {
            OnButtonRight();
        }
    }

    public void RightRelease()
    {
        if (OnButtonRightRelease != null)
        {
            OnButtonRightRelease();
        }
    }

    public void Left()
    {
        if (OnButtonLeft != null)
        {
            OnButtonLeft();
        }
    }

    public void LeftRelease()
    {
        if (OnButtonLeftRelease != null)
        {
            OnButtonLeftRelease();
        }
    }

    public void Back()
    {
        if (OnButtonBack != null)
        {
            OnButtonBack();
        }
    }

    public void BackRelease()
    {
        if (OnButtonBackRelease != null)
        {
            OnButtonBackRelease();
        }
    }

    public void Jump()
    {
        if (OnButtonJump != null)
        {
            OnButtonJump();
        }
    }
    public void JumpRelease()
    {
        if (OnButtonJumpRelease != null)
        {
            OnButtonJumpRelease();
        }
    }
    public void MouseLeft()
    {
        if (OnMouseLeft != null)
        {
            OnMouseLeft();
        }
    }

    public void MouseRight()
    {
        if(OnMouseRight != null)
        {
            OnMouseRight();
        }
    }
   
    public void Respawn()
    {
        if(OnRespawn != null)
        {
            OnRespawn();
        }
    }

    public void HookHit()
    {
        if(OnHookHit != null)
        {
            OnHookHit();
        }
    }
}
