using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Author: Akram Taghavi-Burris
 * Created: August 20, 2019
 * Description: Game Manager script that controls default game settings
 */

public class GameManager : MonoBehaviour
{

    //Make game manager public static for access from other scripts
    public static GameManager gm;

    /**** Public Variables ****/

    public GameObject player;   //player object
    public int score = 0;       //score value
    public bool canBeatLevel = false;   //if player can beat level by a certain score
    public int beatLevelScore = 0;      //score value player can beat level at
    public int playerLives = 0;         // number of lives for player
    public float startTime = 5.0f;      //time for level (if level is timed)
    public Text scoreDisplay;  //text display for score
    public Text timerDisplay;  //text dipslay for level time
    public GameObject gameOverScoreOutline; //object displaying gameover
    public AudioSource backgroundMusicAudio;    //background music audio source
    public bool gameIsOver = false;         //test if game is over
    public GameObject startButton;      //start button
    public GameObject playAgainButton;  //play again button
    public string playAgainLevelToLoad; //the level to load on play again
    public GameObject nextLevelButton;  //next level button
    public string nextLevelToLoad;  //the next level to load
    public GameObject targetSpawner;    //object from which others are spawned

    /**** Private Variables ****/
    private float currentTime; //sets current time for timer
    private float poweredUpTime;    //sets the alotted time for powerups
    private bool poweredUp = false; //test if powerups are allowed
    private bool gameStarted = false; //test if games has started
    private static bool levelTwo = false; //test for multiple levels





    // Setup for the game
    void Start()
    {
        // set the current time to the startTime specified
        currentTime = startTime;

        // get a reference to the GameManager component for use by other scripts
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();



        // activate the Start Button gameObject, if it is set
        if (startButton)
            startButton.SetActive(true);


        // inactivate the targetSpwaners gameObject, if it is set
        if (targetSpawner)
            targetSpawner.SetActive(false);

        // inactivate the gameOverScoreOutline gameObject, if it is set
        if (gameOverScoreOutline)
            gameOverScoreOutline.SetActive(false);

        // inactivate the playAgainButtons gameObject, if it is set
        if (playAgainButton)
            playAgainButton.SetActive(false);

        // inactivate the nextLevelButtons gameObject, if it is set
        if (nextLevelButton)
            nextLevelButton.SetActive(false);


        //Check for level two in console
        if (levelTwo)
        {
            Debug.Log("Level 2");
            StartGame();

        }
    }//end Start()


    // Start Gameplay 
    public void StartGame()
    {
        // set the current time to the startTime specified
        currentTime = startTime;

        // init scoreboard to 0
        scoreDisplay.text = "0";

        // inactivate the Start Button gameObject, if it is set
        if (startButton)
            startButton.SetActive(false);

        // activate the targetSpwaners gameObject, if it is set
        if (targetSpawner)
            targetSpawner.SetActive(true);

        //Sets the Game Started value
        gameStarted = true;

    }//end StartGame()



    // Main game loop
    void Update()
    {
        if (!gameIsOver)
        {
            if (canBeatLevel && (score >= beatLevelScore))
            {  // check to see if player beat level
                BeatLevel();
            }
            else if (currentTime < 0)
            { // check to see if timer has run out
                EndGame();
            }
            else if (playerLives < 0)
            { // check to see if player runs out of lives
                EndGame();
            }
            else if (gameStarted)
            { // update timer during gameplay
                currentTime -= Time.deltaTime;
                timerDisplay.text = currentTime.ToString("0.00");
            }
        }

        //Check for PowerUP
        if (poweredUp)
        {
            PowerDown();

        }//end if(poweredUp)


    }//end Update()

    void EndGame()
    {
        // game is over
        gameIsOver = true;

        // repurpose the timer to display a message to the player
        timerDisplay.text = "GAME OVER";

        // activate the gameOverScoreOutline gameObject, if it is set 
        if (gameOverScoreOutline)
            gameOverScoreOutline.SetActive(true);

        // activate the playAgainButtons gameObject, if it is set 
        if (playAgainButton)
            playAgainButton.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (backgroundMusicAudio)
            backgroundMusicAudio.pitch = 0.5f; // slow down the music
    }//end EndGame()

    void BeatLevel()
    {
        // game is over
        gameIsOver = true;

        // repurpose the timer to display a message to the player
        timerDisplay.text = "LEVEL COMPLETE";

        // activate the gameOverScoreOutline gameObject, if it is set 
        if (gameOverScoreOutline)
            gameOverScoreOutline.SetActive(true);

        // activate the nextLevelButtons gameObject, if it is set 
        if (nextLevelButton)
            nextLevelButton.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (backgroundMusicAudio)
            backgroundMusicAudio.pitch = 0.5f; // slow down the music
    }//end BeatLevel()

    //Power Up - What happens on power up 
    void PowerUp()
    {
        Debug.Log("powerUp");
        //what happens on power up goes here

    }//end PowerUp()


    //Power down - What happens on power down
    void PowerDown()
    {
        Debug.Log(poweredUpTime);
        //Set timer for powerup
        if (poweredUpTime > 0)
        {
            poweredUpTime -= Time.deltaTime;
        }
        else
        //when powerup timer ends
        {
            Debug.Log("powerDown");
            // what happnes on power down goes here
            

            poweredUp = false; //stops Update() from subtractig poweredUpTime
        }//end if (poweredTime <0)


    }//end PowerDown()



    // public function that can be called to update the score or time
    public void targetHit(int scoreAmount, float timeAmount, bool powerUp, float powerUpTime)
    {
        // increase the score by the scoreAmount and update the text UI
        score += scoreAmount;
        scoreDisplay.text = score.ToString();

        // increase the time by the timeAmount
        currentTime += timeAmount;

        // don't let it go negative
        if (currentTime < 0)
            currentTime = 0.0f;

        // update the text UI
        timerDisplay.text = currentTime.ToString("0.00");

        poweredUpTime = powerUpTime;
        poweredUp = powerUp;

        //if Power Up target, call powerUp function
        if (powerUp)
            PowerUp();

    }//end TargetHit()

    // public function that can be called to restart the game
    public void RestartGame()
    {
        //Restart level by loading a scene (or reloading this scene)
        SceneManager.LoadScene(playAgainLevelToLoad);
    }//end RestartGame()

    // public function that can be called to go to the next level of the game
    public void NextLevel()
    {
        // Load the specified next level (scene)
        SceneManager.LoadScene(nextLevelToLoad);
        levelTwo = true;
    }//end NextLevel()

}//end class