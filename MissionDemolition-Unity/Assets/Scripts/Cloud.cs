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

public class Cloud : MonoBehaviour
{
    /**** VARIABLES ****/
    [Header("Set in Inspector")]
    public GameObject cloudSphere; //prefab of cloud sphere
    public int numSpheresMin = 6; //minium number of spheres
    public int numSpheresMax = 10; //maximum number of spheres
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1); //the offset scale of the spheres
    public Vector2 sphereScaleRangeX = new Vector2(4, 8); //the scale range for X
    public Vector2 sphereScaleRangeY = new Vector2(3, 4); //the scale range for X
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4); //the scale range for X
    public float scaleYMin = 2f; //minimum y scale

    private List<GameObject> spheres; //list of spheres in cloud

    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>();

        int num = Random.Range(numSpheresMin, numSpheresMax); //find a random number of spheres to generate

        //for total number of random spheres
        for(int i =0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere); //instantiate the cloudsphere
            spheres.Add(sp); //add spheres to list
            Transform spTrans = sp.transform; //get the transform of sphere
            spTrans.SetParent(this.transform); //sets the object as a child of this object

            //Randomly assign position
            Vector3 offset = Random.insideUnitSphere; //Random.InsideUnitSphere generates a random vector3 within a sphere with the radius of 1
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset; //set the local position (inside parent) to the offset


            //Randomly assign scale
            Vector3 scale = Vector3.one; //reset the scale to 1
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            //Adjust the y scale by x distance from the core
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x); //altered based on how far the cloudsphere is offset from cloud in X direction, the further out x the smaller the y scale.
            scale.y = Mathf.Max(scale.y, scaleYMin);

            spTrans.localScale = scale; //set the scale

        }//end for

    }//end Start()

    // Update is called once per frame
    void Update()
    {
        //if spacebar is pressed restart clouds
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }//end if (Input.GetKeyDown(KeyCode.Space))
    }//end Update()

    private void Restart()
    {
        //delete all cloudspheres game objects
        foreach(GameObject sp in spheres)
        {
            Destroy(sp);
        }
        
        Start(); //run start again
    }//end Restart()
}
