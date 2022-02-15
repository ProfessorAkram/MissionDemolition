/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 14, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 14, 2022
 * 
 * Description: randomly generate a cloud
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    /**** VARIABLES ****/
    [Header("Set in Inspector")]

    public GameObject cloudSphere; //prefab for cloud shape
    public int numberOfSpheresMin = 6; //minimum number of spheres
    public int numberOfSpheresMax = 10; //max number of speheres
    public Vector2 sphereScaleRangeX = new Vector2(4, 8); //range for the scale X
    public Vector2 sphereScaleRangeY = new Vector2(3, 4); //range for the scale Y
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4); //range for the scale Z
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1); //offset for the scale 
    public float scaleYmin = 2f; //minimum Y scale

    private List<GameObject> spheres; //list of spheres


    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>(); //create new list

        int num = Random.Range(numberOfSpheresMin, numberOfSpheresMax);
        //set the max number of cloud spehres, with a random number within range

        //For each cloudsphere
        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere); //create the sphere
            spheres.Add(sp); //add it to the list

            Transform spTrans = sp.transform; //reference the sphere's transform
            spTrans.SetParent(this.transform); //set the cloud as the parent object

            //Randomly assign a position
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            //Randomly assign scale
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            //Adjust y scale by x distance from core
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYmin);

            spTrans.localScale = scale;

        }//end for


    }//end Start()

    // Update is called once per frame
    void Update()
    {
        //keypress spacebar input
        //if (Input.GetKeyDown(KeyCode.Space))
        //{  Restart();  }

    }//end Update()


    void Restart()
    {
        foreach (GameObject sp in spheres)
        {
            Destroy(sp);
        }//end foreach

        Start();

    }//end Restart()




}