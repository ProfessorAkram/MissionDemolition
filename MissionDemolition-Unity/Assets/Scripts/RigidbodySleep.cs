/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 16, 2022
 * 
 * Description: Put the rigidbody to sleep
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))] 
//Forces a Rigidbody component on the object
public class RigidbodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>(); //refernces the rigidbody

        if (rb != null) rb.Sleep();  //if there is a rigidbody, put it to sleep (forces a rigidbody to sleep at least one frame).


    }//end Start()


}
