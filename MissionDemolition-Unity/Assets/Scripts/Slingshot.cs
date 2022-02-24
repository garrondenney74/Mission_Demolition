using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;

    /***Variables***/
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 LaunchPos;
    public GameObject projectile; //instance of projectile
    public bool aimingMode;//is player aiming
    public Rigidbody projectileRB;

    static public Vector3 LAUNCH_POS
    {
        get
        {
            if(S == null)
            {
                return Vector3.zero;
            }
            return S.LaunchPos;
        }

    }

    public GameObject launchpoint;

    private void Awake()
    {
        S = this;
        Transform LaunchPointTrans = transform.Find("LaunchPoint");
        launchpoint = LaunchPointTrans.gameObject;

        launchpoint.SetActive(false); //Disable Launchpoint
        LaunchPos = LaunchPointTrans.position; //position of launchpoint
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return;//if not aiming exit update

        //get mouse position from 2D coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - LaunchPos;

        //limit the mouseDelta to slingshot collider radius
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize(); //sets the vector to the same direction but a length of 1
            mouseDelta *= maxMagnitude;
        }//end if (mouseDelta > maxMagnitude)


        //Move projectile to new position
        Vector3 projectilePos = LaunchPos + mouseDelta;
        projectile.transform.position = projectilePos;

        if (Input.GetMouseButtonUp(0))
        {
            //mouse button has been released
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMultiplier;
            FollowCam.POI = projectile;
            projectile = null; //empties reference to instance projectile
            
            
        }
    }

    private void OnMouseEnter()
    {
        launchpoint.SetActive(true);
        print("Slingshot: OnMouseEnter");
    }//end OnMouseEnter()
    private void OnMouseExit()
    {
        launchpoint.SetActive(false);
        print("Slingshot: OnMouseExit");
        
    }//end OnMouseExit()

    private void OnMouseDown()
    {
        aimingMode = true;//player is aiming
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = LaunchPos;
        projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.isKinematic = true;
    }//end OnMouseDown
}
