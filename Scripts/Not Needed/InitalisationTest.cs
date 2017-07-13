using UnityEngine;
using System.Collections;

/**
 * Class for making a test initialisation info object
 * To test game initialisation
 */
public class InitalisationTest : MonoBehaviour {

    //The info object
    private InitialisationInfo info;

    //Return the info object
    public InitialisationInfo getInfo()
    {
        return info;
    }
	
    /**Input the test data on awake (before start)
     * 
     */
	void Awake () {
        info = new InitialisationInfo();
        info.isMultiplayer = false;
        info.level = 11;
        info.isTest = true;

        info.player1Character = "Satyr";
        info.player1Score = 1000;
        
        info.player2Character = "Pink";
        info.player2Score = 2000;
        
    }
	
}
