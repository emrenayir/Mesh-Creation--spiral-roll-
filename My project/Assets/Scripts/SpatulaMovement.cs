using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatulaMovement : MonoBehaviour
{
    private bool spatulaDown = false;


    private void OnEnable()
    {
        EventManager.OnMouseDownClick += MoveSpatulaDown;
        EventManager.OnMouseUpClick += MoveSpatulaUp;
    }

    private void OnDisable()
    {
        EventManager.OnMouseDownClick -= MoveSpatulaDown;
        EventManager.OnMouseUpClick -= MoveSpatulaUp;
    }
    
    private void MoveSpatulaDown()
    {
        Debug.Log("down");
    }
    
    private void MoveSpatulaUp()
    {
        Debug.Log("up");
    }
   
}
