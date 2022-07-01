using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpatula : MonoBehaviour
{
    public Transform spatula;
    public Vector3 offset;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = spatula.transform.position + offset;
        transform.LookAt(spatula);
    }
}
