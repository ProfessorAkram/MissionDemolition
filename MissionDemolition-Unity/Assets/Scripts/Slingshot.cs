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
    public float velocityMult = 8f; //velocieity multipler


    [Header("Set in Dynamically")]
    public GameObject launchPoint; //launchPoint object
    public Vector3 launchPos; //launch position
    public GameObject projectile; //projectile instance
    public bool aimingMode; //is the player aiming



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

        //Set it to isKinematic for now
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }


}
