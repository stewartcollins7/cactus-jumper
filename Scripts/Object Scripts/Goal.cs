using UnityEngine;
using System.Collections;

/**
 * This class is for the goal gameobject
 * that the player must reach to complete a level
 * Changes color when the change time limit is reached
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class Goal : MonoBehaviour {

    //The game manager
    private GameManager gameManager;
    
    //The timer for the level to be added to score when goal is reached
    private GameTimer timer;

    //The sprite renderer that renders the cactus
    private SpriteRenderer sprite;

    //Indicate whether it is time for the cactus to change color
    private bool readyForColorChange = true;

    //The different colors the cactus can turn
    public Color cactusColor1 = Color.green;
    public Color cactusColor2 = Color.red;
    public Color cactusColor3 = Color.blue;
    public Color cactusColor4 = Color.yellow;

    //The speed the cactus changes color to
    public float cactusChangeSpeed = 10;

    //Set initial values
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        timer = FindObjectOfType<GameTimer>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    //Changes the color of the goal if the timer has been reached
    void Update()
    {
        if (readyForColorChange)
        {
            readyForColorChange = false;
            StartCoroutine(changeColor());
        }
    }

    //Changes to goal to a random color
    public IEnumerator changeColor()
    {
        int randomColor = Random.Range(1, 4);
        if(randomColor < 2)
        {
            sprite.color = cactusColor1;
        }
        else if(randomColor < 3)
        {
            sprite.color = cactusColor2;
        }
        else if (randomColor < 4)
        {
            sprite.color = cactusColor3;
        }
        else
        {
            sprite.color = cactusColor4;
        }yield return new WaitForSeconds(5 / cactusChangeSpeed);
        readyForColorChange = true;

    }

    /*If a player touches the goal trigger
     * then stop the player from moving
     * and notify game Manager that goal has been reached
     */
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb2d = other.gameObject.GetComponent<Rigidbody2D>();
            rb2d.velocity = Vector3.zero;
            gameManager.gameState = GameManager.gameStateEnum.GAMEOVER;
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            timer.TimerCountdown(player.getPlayerNumber());
            
        }
    }
}
