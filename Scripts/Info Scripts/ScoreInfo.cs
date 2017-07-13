using UnityEngine;
using System.Collections;


/**Singleton class which contains the information
 * required by the level complete screen to display
 * and update high scores
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class ScoreInfo {

    //Indicates whether it was a multiplayer game
    public bool isMultiplayer;
    //Indicates what level the score is for
    public int level;
    //The amount of time the level was completed in
    public int completionTime;

    //Player 1 details
    public string player1ID;
    public string player1Name;
    public int player1Score;

    //Player 2 details
    public string player2ID;
    public string player2Name;
    public int player2Score;

    public bool isTest = false;

    //The instance of the singleton class
    public static ScoreInfo info;

    //Returns the instance of the singleton class
    //If none exist then initialise one with default values
    public static ScoreInfo getScoreInfo()
    {
        if (info == null)
        {
            info = new ScoreInfo();
            info.isMultiplayer = false;
            info.level = 1;
            info.player1ID = "DefaultID";
            info.player1Name = "DefaultName";
            info.player1Score = 1000;
            info.completionTime = 50;
        }
        return info;
    }

    //Updates the instance with newInfo
    public static void setScoreInfo(ScoreInfo newInfo)
    {
        info = newInfo;
    }
}
