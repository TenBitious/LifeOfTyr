using UnityEngine;
using System.Collections;

public class PlayerEventManager : MonoBehaviour {

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
   
}
