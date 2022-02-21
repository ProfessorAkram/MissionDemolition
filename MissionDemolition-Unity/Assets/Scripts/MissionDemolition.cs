/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Feb 16, 2022
 * 
 * Description: Game Manager for the Mission Demolition Game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //import UI librairies


//Enumrator Game Modes
public enum GameMode
{                                                  
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition GM; // a private Singleton of Game Manager (GM)

    /**** VARIABLES ****/
    [Header("Set in Inspector")]
    public Text uitLevel;  // The level text
    public Text uitShots;  // The shots taken Text
    public Text uitButton; // The Text on view button
    public Vector3 castlePos; // The place to put castles
    public GameObject[] castles;   // An array of the castles

    [Header("Set Dynamically")]
    public int level;     // The current level
    public int levelMax;  // The number of levels
    public int shotsTaken; //number of shots taken
    public GameObject castle;    // The current castle
    public GameMode mode = GameMode.idle; //game mode
    public string showing = "Show Slingshot"; // FollowCam mode


    void Start()
    {
        GM = this; //refrence to self

        level = 0; //set the level
        levelMax = castles.Length; //the max number of levels is equal to the amount of castles in the array
        StartLevel(); //start the level
    }

    //Method to start the level
    void StartLevel()
    {
        // Get rid of the old castle if one exists
        if (castle != null)
        {
            Destroy(castle);
        }

        // Destroy old projectiles if they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        // Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        // Reset the camera
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        // Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        // Show the data in the GUITexts
        uitLevel.text = "Level: " + (level + 1) + " /" + levelMax;
        uitShots.text = "Shots: " + shotsTaken;
    }

    void Update()
    {
        UpdateGUI();

        // Check for level end
        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            // Zoom out
            SwitchView("Show Both");
            // Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    //Swith between camera views
    public void SwitchView(string eView = "")
    {                                    
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = GM.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;

        }
    }

    // Static method that allows code anywhere to increment shotsTaken
    public static void ShotFired()
    {                                            
        GM.shotsTaken++;
    }

}