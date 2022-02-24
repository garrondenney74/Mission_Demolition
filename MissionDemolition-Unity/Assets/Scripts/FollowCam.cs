/***
 * Created by Garron Denney
 * Date Created: 2/9/21
 * Last Edited by: N/A
 * Last Date Edited 2/9/21
 * Description Follow Camera code
 * 
 ***/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /***Variables***/
    static public GameObject POI; //static point of interest
    [Header("Set in Inspector")]
    public float easing = 0.05f;//Amount of ease
    public Vector2 minXY = Vector2.zero;




    public float camZ; //the desired Z pos of the camera


    private void Awake()
    {
        camZ = this.transform.position.z;
    }//end Awake()
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        //if no point of interest exit update

        Vector3 destination = POI.transform.position;
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;
            if(POI.tag == "Projectile")
            {
                if(POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                }
            }//end if(POI.tag == "Projectile")

        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.x, destination.y);


        destination = Vector3.Lerp(transform.position, destination, easing); //interpolate from current camera position towards destination

        destination.z = camZ;
        transform.position = destination;//Set position of the camera to destination

        Camera.main.orthographicSize = destination.y + 10;

    }//end FixedUpdate()
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
