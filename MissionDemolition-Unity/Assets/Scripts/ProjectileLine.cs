/***
 * Created by Garron Denney
 * Date Created: 2/14/21
 * Last Edited by: N/A
 * Last Date Edited 2/16/21
 * Description Creating the line for the projectile
 * 
 ***/








using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{


    static public ProjectileLine S;
    [Header("Set in Inspector")]
    public float minDist = .1f;


    private LineRenderer line;
    private GameObject poi;
    private List<Vector3> points;

    // Start is called before the first frame update



    private void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }

    public GameObject PoI
    {
        get { return (poi); }
        set
        {
            poi = value;
            if (poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoints();
            }
        }
    }

    public void Clear()
    {
        poi = null;
        line.enabled = false;
        points = new List<Vector3>();

    }

    void AddPoints()
    {
        Vector3 pt = poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return; // if the point is not far enough from the last point
        }

        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector3 lastPoint
    {
        get { 
            
            if (points == null) return (Vector3.zero);
            return (points[points.Count - 1]);

        }
        
    }

    private void FixedUpdate()
    {
        if(poi == null)
        {
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }
                else
                {
                    return;
                }
            }
            else return;
        }
        AddPoints();
        if(FollowCam.POI == null)
        {
            poi = null;
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
