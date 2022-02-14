/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 09, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 09, 2022
 * 
 * Description: Slingshot controller
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    /**** VARIABLES ****/
    [Header("Set in Inspector")]
    public GameObject prefabProjectile; //projectile prefab
    public float velocityMultipler = 8f; //velocieity multipler


    [Header("Set in Dynamically")]
    public GameObject launchPoint; //launchPoint object
    public Vector3 launchPos; //launch position
    public GameObject projectile; //projectile instance
    public bool aimingMode; //is the player aiming
    public Rigidbody projectileRB; //ridigbody of projectile


    //Awake is called the start of the game
    private void Awake()
    {
        /*** IS THIS REALLY NECESSARY ***/
        Transform launchPointTrans = transform.Find("LaunchPoint"); //find child object
        /*NOTE: GameObject.Find will search for a gameobject in the scene. To search a gameobject from a parent, use Transform.*/
        launchPoint = launchPointTrans.gameObject; //Get the gameobject of child object and set it

        launchPoint.SetActive(false); //disable the launchPoint
        launchPos = launchPointTrans.position; //set the launch poisiton 
    }//end Awake()

    //Update is called every frame of the game
    private void Update()
    {
        //If we are not aiming exit the method
        if (!aimingMode) return;

        //get the current mouse position in 2d screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos; //pixel amount of change between the mouse3d and launchPos

        //limit mouseDelta to slingshot collider radius
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize(); //sets the vector to the same direction, but length is 1.0
            mouseDelta *= maxMagnitude;
        }//end  if(mouseDelta.magnitude > maxMagnitude)

        //move projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        //If we release the mouse button
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false; //we are no longer aiming
            projectileRB.isKinematic = false; //the ball is no longer kinematic
            projectileRB.velocity = -mouseDelta * velocityMultipler; //veloscity is multipled to the mousedelta
            FollowCam.POI = projectile; //set the POI for the follow cam
            projectile = null; //forget the last instance (The instance still exsists but we do not have a reference to it)
        }


    }//end Update()

    //When mouse enters
    private void OnMouseEnter()
    {
        launchPoint.SetActive(true); //disable the launchPoint
        print("Slingshot: OnMouseEnter");
    }//end OnMouseEnter()


    //When mouse exits
    private void OnMouseExit()
    {
        launchPoint.SetActive(false); //disable the launchPoint
        print("Slingshot: OnMouseExit");
    }//end OnMouseExit()

    //When mouse is pressed down
    private void OnMouseDown()
    {
        //the player has pressed the mouse button down over the slingshot
        aimingMode = true;

        //Instantiate a projectile
        projectile = Instantiate(prefabProjectile) as GameObject;

        //Start it at the launchpoint
        projectile.transform.position = launchPos;
        
        //Get reference to projectile rigidbody component
        projectileRB = projectile.GetComponent<Rigidbody>();
        
        //Set it to isKinematic for now
        projectileRB.isKinematic = true;
    }


}
