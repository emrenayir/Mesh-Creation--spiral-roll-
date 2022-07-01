using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpatulaMovement : MonoBehaviour
{
    [SerializeField] private  Transform posSpatulaUp;
    [SerializeField] private  Transform posSpatulaDown;
    [SerializeField] private  float spatulaMovementSpeed;
    
    private bool spatulaDown = false;

    private void MoveSpatulaDown()
    {
        if (!spatulaDown)
        {
            this.transform.DOMove(posSpatulaDown.position, spatulaMovementSpeed);
            spatulaDown = true;
        }
    }
    private void MoveSpatulaUp()
    {
        if (spatulaDown)
        {
            this.transform.DOMove(posSpatulaUp.position , spatulaMovementSpeed);
            spatulaDown = false;
        }
    }


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
}
