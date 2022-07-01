using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpatulaMovement : MonoBehaviour
{
    //private values
    private Rigidbody spatulaRb;
    private bool spatulaDown = false;

    //public values
    public Vector3 velocitySpatula = new Vector3(0,0,2);
    
    //Serialized values
    [SerializeField] private  float spatulaMovementSpeed;

    private void Start()
    {
        spatulaRb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        spatulaRb.velocity = velocitySpatula;
    }

    private void MoveSpatulaDown()
    {
        if (!spatulaDown)
        {
            this.transform.DOMove(this.transform.position + Vector3.down, spatulaMovementSpeed);
            spatulaDown = true;
        }
    }
    private void MoveSpatulaUp()
    {
        if (spatulaDown)
        {
            this.transform.DOMove(this.transform.position + Vector3.up , spatulaMovementSpeed);
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
