/***
 * Created by Garron Denney
 * Date Created: 2/16/21
 * Last Edited by: N/A
 * Last Date Edited 2/16/21
 * Description: Sleep for castle walls
 * 
 ***/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class RigidBodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null) rb.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
