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

public class FollowCam : MonoBehaviour
{
    /**** VARIABLES ****/
    static public GameObject POI; //the static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f; //ease of the camera motion
    public Vector2 miniumXY = Vector2.zero; 

    [Header("Set Dynamically")]
    public float camZ; //Desired Z posiiton of the camera

    //Awake is called the start of the game
    private void Awake()
    {
        camZ = this.transform.position.z; //record the inital z position for the camera
    }//end Awake()

    // FixedUpdate will be a a constant frame rate
    void FixedUpdate()
    {
        if (POI == null) return; //if there is no POI exit method

        Vector3 destination = POI.transform.position;//Get the positition of the POI

        //Limit the camera's minium XY values
        destination.x = Mathf.Max(miniumXY.x, destination.x); 
        destination.y = Mathf.Max(miniumXY.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, easing); //interpolate from the current camera position towards the destination
        destination.z = camZ; //reset the z of the destination to the camera z
        transform.position = destination; //Set position of the camera to the destination

        Camera.main.orthographicSize = destination.y + 10; //Sets the orthographicSize of the camera to keep the ground in view
        
    }//end FixedUpdate()
}
