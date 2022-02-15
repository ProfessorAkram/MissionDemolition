/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 14, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 14, 2022
 * 
 * Description: generate multiple clouds
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    /**** VARIABLES ****/
    [Header("Set in Inspector")]

    public int numberOfClouds = 40; //total number of clouds
    public GameObject cloudPrefab; //cloud prefab
    public Vector3 cloudPositionMinium = new Vector3(-50, -5, 10); //minimum position of cloud
    public Vector3 cloudPositionMax = new Vector3(150, 100, 10); //max position of cloud
    public float cloudScaleMin = 1; //min scale of cloud
    public float cloudScaleMax = 3; //max scale of cloud
    public float cloudSpeedMultipler = 0.5f; //the multipler of speed

    private GameObject[] cloudInstances; //array of clouds


    private void Awake()
    {
        cloudInstances = new GameObject[numberOfClouds]; //create cloud array
        GameObject anchor = GameObject.Find("CloudAnchor");// find the cloud anchor object

        GameObject cloud; //create a new game object for cloud

        //for each cloud
        for (int i = 0; i < numberOfClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab); //instantiate cloud

            //position cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPositionMinium.x, cloudPositionMax.x);
            cPos.y = Random.Range(cloudPositionMinium.y, cloudPositionMax.y);

            //Scale clouds
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(cloudPositionMinium.y, cPos.y, scaleU); //smaller clouds should be closer to ground
            cPos.z = 100 - 90 * scaleU; //smaller clouds be further away

            //apply transform to cloud
            cloud.transform.localPosition = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;

            //make cloud chid of cloud anchor
            cloud.transform.SetParent(anchor.transform);

            //add the cloud to the array
            cloudInstances[i] = cloud;
        }//end for

    }//end Awake



    // Update is called once per frame
    void Update()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            //Get cloud scale and position
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;

            //Move larger clouds faster
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMultipler;

            //wrap clouds
            if (cPos.x <= cloudPositionMinium.x)
            {
                cPos.x = cloudPositionMax.x;
            }

            //Apply transforms
            cloud.transform.position = cPos;


        }//end foreach

    }//end Update()

}