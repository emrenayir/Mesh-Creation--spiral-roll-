using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void MouseDownClickDelegate();
    public static MouseDownClickDelegate OnMouseDownClick;

    public delegate void MouseUpClickDelegate();
    public static MouseUpClickDelegate OnMouseUpClick;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (OnMouseDownClick != null)
            {
                OnMouseDownClick();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (OnMouseUpClick != null)
            {
                OnMouseUpClick();
            }
        }
    }
}
