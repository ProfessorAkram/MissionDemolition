/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 16, 2022
 * 
 * Description: Indicates when goal is hit
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    /**** VARIABLES ****/
    static private bool goalMet = false; //has the goal been met

    private void OnTriggerEnter(Collider other)
    {
        //when the trigger is hit by something
        if(other.gameObject.tag == "Projectile") //if the other object is the projecitle
        {
            Goal.goalMet = true; //goal has been met
            Material mat = GetComponent<Renderer>().material; //reference to the object's matieral 
            Color c = mat.color; //reference to the material's color
            c.a = 1; //alpha value
            mat.color = c; //resets the color value
        }
    }
}
