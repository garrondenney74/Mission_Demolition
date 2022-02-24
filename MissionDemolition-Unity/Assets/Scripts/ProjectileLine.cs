/***
 * Created by Garron Denney
 * Date Created: 2/24/21
 * Last Edited by: N/A
 * Last Date Edited 2/24/21
 * Code for constructing the follow path of the projectile
 * 
 ***/
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S;
    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    [Header("Set in Inspector")]
    public LineRenderer line;
    private GameObject _poi;
    public List<Vector3> points;

    void Awake()
    {
        S = this; // Set the singleton
        // Get a reference to the LineRenderer
        line = GetComponent<LineRenderer>();
        // Disable the LineRenderer until it's needed
        line.enabled = false;
        // Initialize the points List
        points = new List<Vector3>();
    }

    public GameObject poi 
    { 
        get
        {
            return(_poi);
        }
        set
        {
            _poi = value;
            if(_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points =new List<Vector3>();
    }

    public void AddPoint()
    {
        Vector3 pt = _poi.transform.position;
        if(points.Count > 0 && (pt-lastPoint).magnitude < minDist)
        {
            return;
        }
        if (points.Count == 0)
        {
            // If this is the launch point...
            Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
            Vector3 launchPosDiff = pt - launchPos;

            // ...it adds an extra bit of line to aid aiming later
            points.Add( pt + launchPosDiff );
            points.Add(pt);
            line.SetVertexCount(2);

            // Sets the first two points
            line.SetPosition(0, points[0] );
            line.SetPosition(1, points[1] );

            // Enables the LineRenderer
            line.enabled = true;    
        }
        else
        {
            // Normal behavior of adding a point
            points.Add( pt );
            line.SetVertexCount( points.Count );
            line.SetPosition( points.Count-1, lastPoint );
            line.enabled = true;
        }
    }

        public Vector3 lastPoint
        {
            get
            {
                if (points == null) 
                {
                    // If there are no points, returns Vector3.zero
                    return(Vector3.zero);
                }
                return(points[points.Count-1]);
            }
        }


    void FixedUpdate()
    {
        if ( poi == null ) 
        {
            // If there is no poi, search for one
            if (FollowCam.poi != null) 
            {
                if (FollowCam.poi.tag == "Projectile") 
                {
                    poi = FollowCam.poi;
                } 
                else 
                {
                    return; // Return if we didn't find a poi
                }
            } 
            else 
            {
                return; // Return if we didn't find a poi
            }
        }
        // If there is a poi, it's loc is added every FixedUpdate
        AddPoint();
        if ( poi.GetComponent<Rigidbody>().IsSleeping() ) 
        {
        // Once the poi is sleeping, it is cleared
        poi = null;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

