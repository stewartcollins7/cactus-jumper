using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * This class handles the measurement of the game timer and the updating
 * of the canvas element that displays the game timer.
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class GameTimer : MonoBehaviour {
    //The total time limit for the level
    public float TIMELIMIT = 100;
    //The speed the timer decreases at once the goal has been reached
    public float countdownSpeed=20;
    //The amount of points for each point of the timer remaining
    public int timerPoints=50;
    
    //The remaining time on the timer
    private float timeRemaining;
    //The text that displays the timer
    private Text timerText;
    //The gameManager that controls game execution
    private GameManager gameManager;

    //Returns the amount of time the level was completed in
    //Only meant to be called once goal has been reached
    public int getCompletionTime()
    {
        return ((int)TIMELIMIT - (int)timeRemaining);
    }

    //Set timer to time limit and get component variables
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameObject timerObject = GameObject.Find("TimerText");
        timerText = timerObject.GetComponent<Text>();
        timeRemaining = TIMELIMIT;
    }

    //When the timer is reached by a certain player start the timer decreasing quickly on the canvas
    public void TimerCountdown(int playerNumber)
    {
        StartCoroutine(countdown(playerNumber));
    }

    /**Decrease the timer quickly while updating the canvas
     * Meant to be called when goal has been reached
     * When countdown finished signal to game manager that goal has been reached
     */
    private IEnumerator countdown(int playerNumber)
    {
        for(int i = (int)timeRemaining; i >= 0; i--)
        {
            timerText.text = "Time Remaining: " + i;
            yield return new WaitForSeconds(1.0f/countdownSpeed);
        }gameManager.addToScore((int)timeRemaining * timerPoints, playerNumber);
        gameManager.GoalReached(playerNumber);
    }

    // Update the timer and update the timer display to the user
    void Update () {
        if(gameManager.gameState == GameManager.gameStateEnum.PLAYING)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                gameManager.TimeOver();
                timerText.text = "Time Remaining: 0";
            }
            else
            {
                timerText.text = "Time Remaining: " + timeRemaining.ToString("F0");
            }
        }
        
	}
}
