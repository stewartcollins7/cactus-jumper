using UnityEngine;
using System.Collections;

/**This signleton class contains all the information required
 * by the game manager to initialise the level
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class InitialisationInfo {

    //Indicates whether it is a multiplayer game
    public bool isMultiplayer;
    //Indicates whether it is a saved game
    public bool isSavedGame;
    //The current level
    public int level;
    //The current difficulty
    public string difficulty;
    
    //Player 1 attributes
    public string player1ID;
    public string player1Name;
    public string player1Character;
    public int player1Lives;
    public int player1Score;

    //Player 2 attributes
    public string player2ID;
    public string player2Name;
    public string player2Character;
    public int player2Lives;
    public int player2Score;

    //Only used in tested to allow a different player controller to be initialised
    //for player 2 that uses the same game instance and different keyboard keys
    public bool isTest = false;

    //The instance of the signleton class
    public static InitialisationInfo info;

    //Returns the instance of the singleton class
    //Creates a new object with default values if instance does not yet exist
    public static InitialisationInfo getInitialisationInfo()
    {
        if (info == null)
        {
            //Initialise all variables with default data for testing
            info = new InitialisationInfo();
            info.isMultiplayer = false;
            info.isSavedGame = false;
            info.level = 1;
            info.difficulty = "Easy";
            info.player1ID = "DefaultID";
            info.player1Name = "DefaultName";
            info.player1Character = "NinjaGirl";
            info.player1Lives = 3;
            info.player1Score = 0;
        }
        return info;
    }


    //Updates the singleton instance with newInfo
    public static void setInitialisationInfo(InitialisationInfo newInfo)
    {
        info = newInfo;
    }
}
