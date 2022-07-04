using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpatula : MonoBehaviour
{
    public Transform spatula;
    public Vector3 offset;

    public bool work = true;
    // Update is called once per frame
    void Update()
    {
        if (work)
        {
            transform.position = spatula.transform.position + offset;
            transform.LookAt(spatula);
        }
        
    }
}
