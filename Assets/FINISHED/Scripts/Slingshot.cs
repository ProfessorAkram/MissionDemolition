using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Akram Taghavi-Burris
 * Created: August 21, 2019
 * Description: Slingshot behavior for controling the slignshot
 */
public class Slingshot : MonoBehaviour
{
    /**** Public Variables ****/

    //Variables set in the Inspector
    [Header("Set in Inspector")]
    public GameObject prefabProjectile; //the prefab of the projectile object

    //Vairables set dynamically ( you can also use  [HideInInspector]  in front of each vairable you do not want seen in the Inspector) 
    [Header("Set Dynamically")]
    public GameObject launchPoint;    //launch point in the slignshot for projectile
    public Vector3 launchPos;      //Position of the launch
    public GameObject projectile;     //instance of the prefab projectile object
    public bool aimingMode;     //Checks if we are amining

    //When the sligshot initiatizes
    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint"); //find the launchPoint and retrun transform (position rotation scale) 
        launchPoint = launchPointTrans.gameObject;                  //set the launchPoint object to the transform object
        launchPoint.SetActive(false);                               //set the launchPoint to inactive
        launchPos = launchPointTrans.position;                      //set the launch position (vector 3 position only) 
    }//end Awake()


    // When mouse enters the slingshot
    void OnMouseEnter()
    {
        print("Slingshot:onMouseEnter()");

    }//end onMouseEnter()

    // When the mouse exits the slingshot
    void OnMouseExit()
    {
        print("Slingshot:onMouseExit()");

    }//end onMouseExit()

    //When the mouse is pressed down 
    void OnMouseDown()
    {
        aimingMode = true;      //the player is pressed on the slignshot
        projectile = Instantiate(prefabProjectile) as GameObject;   //create an instatnce of the prefab projectile
        projectile.transform.position = launchPos;  //set the position for the projecitle
        projectile.GetComponent<Rigidbody>().isKinematic = true;    //set the projectile to isKematic
    }//end OnMouseDown()

}//end class
