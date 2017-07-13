using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/***
 * The game manager class controls the execution of the game
 * It interacts with the UI components to provide feedback to the player
 * It is a singleton class that is used by the units to updates scores,
 * and indicate what state the game is currently in
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class GameManager : MonoBehaviour {

    //Displays messages to the player about the status of the game
    private Text gameStatusText;

    //Indicates whether it is singleplayer or multiplayer
    private bool isMultiplayer;

    //Displays the game score
    private Text scoreP1Text;
    private Text scoreP2Text;

    //Indicates the current state of the game
    public enum gameStateEnum { START, PLAYING, GAMEOVER};
    [HideInInspector]public gameStateEnum gameState;

    //A timer for displaying messages
    private float messageTimer = 3f;

    //Indicates whether the game has been initialised
    private bool initialised;

    //Player information
    string player1ID;
    string player2ID;
    string player1Name;
    string player2Name;

    //The player's score
    private int player1Score;
    private int player2Score;

    //The current level
    private int level;

    

    //Adds points to the player's score
    public void addToScore(int points, int player)
    {
        if(player == 1)
        {
            player1Score += points;
            scoreP1Text.text = "Score: " + player1Score;
        }
        else
        {
            player2Score += points;
            scoreP2Text.text = "Score: " + player2Score;
        }
        
        
    }

    /**Initialises the game settings based on
     * info from the initialisation info object that is passed to the level
     * when it is loaded
     */
    private void InitialiseGame()
    {

        if (!initialised)
        {
            initialised = true;
            Debug.Log("initialising");

            //Get the initialisation info from the singleton class
            InitialisationInfo info = InitialisationInfo.getInitialisationInfo();

            //Set level attributes
            isMultiplayer = info.isMultiplayer;
            level = info.level;

            //Set player1 attributes
            player1Score = info.player1Score;
            player1ID = info.player1ID;
            player1Name = info.player1Name;

            //Instantiate player object at start spawn
            GameObject p1StartPosition = GameObject.FindWithTag("P1 Start");
            GameObject player = (GameObject)Instantiate(Resources.Load(info.player1Character), p1StartPosition.transform.position, Quaternion.identity);
            //Set the player number on the player object
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.setPlayerNumber(1);

            //If multiplayer set player2 attributes
            if (isMultiplayer)
            {
                player2Score = info.player2Score;
                GameObject p2StartPosition = GameObject.FindWithTag("P2 Start");
                player = (GameObject)Instantiate(Resources.Load(info.player2Character), p2StartPosition.transform.position, Quaternion.identity);
                playerController = player.GetComponent<PlayerController>();
                player2ID = info.player2ID;
                player2Name = info.player2Name;

                //Below only for testing multiplayer in one game instance with different keyboard inputs assigned
                if (info.isTest)
                {
                    Destroy(playerController);
                    player.AddComponent<OtherPlayerController>();
                    playerController = player.GetComponent<PlayerController>();
                }

                //Set the player number on the instantiated object
                playerController.setPlayerNumber(2);
            }
            else
            {
                //Otherwise hide the player 2 score and health info on the canvas
                GameObject p2Info = GameObject.FindGameObjectWithTag("P2Info");
                p2Info.SetActive(false);
                
            }
        }
        
    }

    /**Method called when a player reaches the level goal
     * Creates the high score information to be passed to the level complete screen
     * Plays a message and loads the level complete screen
     */
    public void GoalReached(int playerNumber)
    {
        //Set the information required by the level complete screen
        ScoreInfo info = ScoreInfo.getScoreInfo();
        info.isMultiplayer = this.isMultiplayer;
        info.level = this.level;
        GameTimer timer = FindObjectOfType<GameTimer>();
        info.completionTime = timer.getCompletionTime();

        info.player1ID = this.player1ID;
        info.player1Name = this.player1Name;
        info.player1Score = this.player1Score;

        //Add information for other player if needed
        if (isMultiplayer)
        {
            info.player2ID = this.player2ID;
            info.player2Name = this.player2Name;
            info.player2Score = this.player2Score;
        }
        
        //Update the score info in the singleton class
        ScoreInfo.setScoreInfo(info);

        //Play the win message
        StartCoroutine(PlayWinMessage(playerNumber));

    }

    /**Method called when the timer runs out
     * 
     */
    public void TimeOver()
    {
        gameStatusText.text = "You ran out of time";
        gameState = gameStateEnum.GAMEOVER;
        initialised = false;
        StartCoroutine(reloadIn3Seconds());
    }

    /**Method called when the player dies
     * Sends a message to the player then reloads the level
     */
    public void GameOver()
    {
        gameStatusText.text = "You died";
        gameState = gameStateEnum.GAMEOVER;
        initialised = false;
        StartCoroutine(reloadIn3Seconds());
        
    }

    /**Waits for 3 seconds then reloads the level
     * 
     */
    private IEnumerator reloadIn3Seconds()
    {
        yield return new WaitForSeconds(3f);
        gameState = gameStateEnum.START;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    /**
     * Plays ready message at the start of the level
     * Then sets the game state to playing
     */
    private void PlayStartMessage()
    {
        messageTimer -= Time.deltaTime;
        if (messageTimer > 2)
        {
            gameStatusText.text = "Ready?";
        }
        else if (messageTimer > 1)
        {
            gameStatusText.text = "Set";
        }
        else if (messageTimer > 0)
        {
            gameStatusText.text = "Race!";
        }
        else
        {
            gameStatusText.text = "";
            gameState = gameStateEnum.PLAYING;
            messageTimer = 3f;
        }

    }

    /**Updates the UI element to indicate the goal has been reached
     * Waits two seconds then loads the next level
     */
    private IEnumerator PlayWinMessage(int playerNumber)
    {
        gameStatusText.text = "Player "  + playerNumber + " reached the goal";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Congratulations");
    }

    
    /*Initialises the game, called in awake so objects are created before 
     * Start() method is called
     */
    void Awake()
    {
        InitialiseGame();
    }



    /**
     * Gets the UI text objects from the scene
     * Needs to be called on each loaded level as scene objects are destroyed
     * each level
     */
    private void setUIElements()
    {
        GameObject temp = GameObject.Find("GameStatusText");
        gameStatusText = temp.GetComponent<Text>();
        GameObject[] scoreTexts = GameObject.FindGameObjectsWithTag("ScoreText");
        if (scoreTexts[0].name.Equals("P1ScoreText"))
        {
            scoreP1Text = scoreTexts[0].GetComponent<Text>();
            if (isMultiplayer)
            {
                scoreP2Text = scoreTexts[1].GetComponent<Text>();
            }
        }else
        {
            scoreP1Text = scoreTexts[1].GetComponent<Text>();
            if (isMultiplayer)
            {
                scoreP2Text = scoreTexts[0].GetComponent<Text>();
            }
        }
        scoreP1Text.text = "Score: " + player1Score;
        if (isMultiplayer)
        {
            scoreP2Text.text = "Score: " + player2Score;
        }
    }

    /**Called when object is initialised on first level opened
     * Sets the UI elements
     */
    void Start()
    {
        gameState = gameStateEnum.START;
        setUIElements();
    }

    // If game is is start mode then update the 
    // Start message
    void Update () {
	    if(gameState == gameStateEnum.START)
        {

            PlayStartMessage();
        }
	}
}
