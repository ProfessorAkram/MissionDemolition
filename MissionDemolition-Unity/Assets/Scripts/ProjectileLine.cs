/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 16, 2022
 * 
 * Description: Render lines of the projectile
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{

    static public ProjectileLine S; //set class as singleton
    /*NOTE: a singleton in Unity is a globally accessible class that exists in the scene, but only once.*/

    /**** VARIABLES ****/

    [Header("Set in Inspector")]
    public float minDist = 0.1f; //minimum distance of the line
    public Vector2 lineWidth = new Vector2(0.0f, 0.1f); //width of the line (start and end) to render [NOT IN BOOK: Makes it easier to set the width, instead of using the graph]

    private LineRenderer line; //reference to the line render
    private GameObject _poi; //private reference to the poi gameobject
    private List<Vector3> points; //list of points in the trail

    private void Awake()
    {
        S = this; //reference to self
        line = GetComponent<LineRenderer>();//reference to linerender
        line.SetWidth(lineWidth.x , lineWidth.y);//set the width of the lineRender
        line.enabled = false; //disable the line render 
        /*NOTE: "setActive" is used to enable/disable objects, while "enabled" is used to enable/disable components.*/
        points = new List<Vector3>();//create a new list

    }//end Awake()

    //GET/SET are methods set when decalring a variable
    public GameObject poi
    {
        get { return (_poi); } //returns the value of _poi
        set
        {
            _poi = value; //sets the value of the _poi object

            //If _poi already exsists
            if (_poi != null)
            {
                line.enabled = false; //disable the line render
                points = new List<Vector3>(); //create a new list of points
                AddPoint(); //run the AddPoints() method
            }//end if(_poi != null)
        }
    }

    //Clear and reset poi, lines and point list
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }//end Clear()


    //Adds points to line render
    public void AddPoint()
    {
        Vector3 pt = _poi.transform.position; //get the point from the _poi position
        
        //Check how far away the point is from the last point
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return; //if the point is not far enough from the last point
        }

        //If there are no points, this is the first point
        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS; //get the difference from the launch point
            points.Add(pt + launchPosDiff); //add this point to the list
            points.Add(pt);//add the pt of the poi to the list
            line.positionCount = 2; // set the amount of points to render
            line.SetPosition(0, points[0]); //set point 1 from the list of points
            line.SetPosition(1, points[1]); //set point 2 from the list of points
            line.enabled = true; //enable the line render
        }
        else //if this is not the first set of points
        {
            points.Add(pt); //add the point to the list
            line.positionCount = points.Count; //update the number of points to render
            line.SetPosition(points.Count - 1, lastPoint); //cycle through the points in the list
            line.enabled = true; //enable the line render

        }//end  if(points.Count == 0)
    }//end AddPoints()


    public Vector3 lastPoint //variable for last point
    {
        get
        {//if the the points list is null
            if (points == null) 
            {
                return (Vector3.zero); //return 0,0,0
            }//end if (points == null)

            return (points[points.Count - 1]); //otherwise find the last point the last point
        }//end get
    }//end lastPoint

    private void FixedUpdate()
    {
        //if the poi is null
        if (poi == null)
        {
            //Check if the FollowCam poi is not null
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile") //check to see is if the FollowCam poi is the Projectile
                {
                    poi = FollowCam.POI; //then set the POI
                }
                else 
                {
                    return; //exit if FollowCam POI is not Projectile
                }//end  if(FollowCam.POI.tag == "Projectile")
            }
            else
            {
                return; //exit if FollowCam is null
            }//end if (FollowCam.POI != null)

        }//end if(poi == null)

        AddPoint(); //Run the AddPoints() method

        //If the FollowCam POI is null
        if (FollowCam.POI == null)
        {
            poi = null; //set the poi to null 

        }//end if(FollowCam.POI == null)

    }//end FixedUpdate()



}