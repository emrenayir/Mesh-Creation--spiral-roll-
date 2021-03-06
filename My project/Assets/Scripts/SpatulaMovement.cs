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
    public Transform spatula;
    public GameObject spiralPrefab;
    private GameObject spiralGenerator;
    

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

            spiralGenerator = Instantiate(spiralPrefab, spatula.position, Quaternion.identity);
            spiralGenerator.GetComponent<FollowSpatula>().spatula = spatula;
        }
    }
    private void MoveSpatulaUp()
    {
        if (spatulaDown)
        {
            this.transform.DOMove(this.transform.position + Vector3.up , spatulaMovementSpeed);
            spatulaDown = false;
            spiralGenerator.GetComponent<FollowSpatula>().work = false;
            spiralGenerator.GetComponent<MeshGenerator>().StopScraping();
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
