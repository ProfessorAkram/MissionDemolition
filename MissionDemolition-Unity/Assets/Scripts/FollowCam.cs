/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 09, 2022
 * 
 * Last Edited by: NA
<<<<<<< Updated upstream
 * Last Edited: Feb 14, 2022
=======
 * Last Edited: Feb 16, 2022
>>>>>>> Stashed changes
 * 
 * Description: camera follow controlls
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /**** VARIABLES ****/
    static public GameObject POI; //the static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;//amount of ease
    public Vector2 minXY = Vector2.zero; 

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
        //if (POI == null) return; //if there is no POI exit method
        // Vector3 destination = POI.transform.position;//Get the positition of the POI

        Vector3 destination; //destination of POI
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position; 
            if(POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                }//end if (POI.GetComponent<Rigidbody>().IsSleeping())

            }//end if(POI.tag == "Projectile")

        }//end if (POI == null)



<<<<<<< Updated upstream
        
        Vector3 destination = POI.transform.position;//Get the positition of the POI
=======
>>>>>>> Stashed changes

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, easing); //interpolate from current camera postion towards destination
        
        destination.z = camZ; //reset the z of the destination to the camera z
        transform.position = destination; //Set position of the camera to the destination

        Camera.main.orthographicSize = destination.y + 10; 
        
    }//end FixedUpdate()
}
