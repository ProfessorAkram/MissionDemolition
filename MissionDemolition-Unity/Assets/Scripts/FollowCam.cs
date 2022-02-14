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
        destination.z = camZ; //reset the z of the destination to the camera z
        transform.position = destination; //Set position of the camera to the destination
        
    }//end FixedUpdate()
}
