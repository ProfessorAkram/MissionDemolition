/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 14, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 14 2022
 * 
 * Description: Camera to follow projectile
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /**** VARIABLES ****/
    static public GameObject POI; //The static point of interest
    [Header("Set in Inespector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero; 

    [Header("Set Dynamically")]
    public float camZ; //desired Z pos of the camera


    private void Awake()
    {
        camZ = this.transform.position.z; //sets z
    }//end Awake()


    void FixedUpdate()
    {
        if (POI == null) return; //do nothing if no poi

        Vector3 destination = POI.transform.position;

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        //interpolate from current position to destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        destination.z = camZ;

        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10; 

    }//end FixedUpdate()
}
