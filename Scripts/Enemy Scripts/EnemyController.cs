using UnityEngine;
using System.Collections;

/**
 * Base controller for all enemy units
 * Moves the enemy and handles the doing damage
 * or being destroyed by the player
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public abstract class EnemyController : MonoBehaviour
{
    //The central game manager interface
    protected GameManager gameManager;
    //The amount of damage the unit does
    protected int damage;
    //The amount of points a player would recieve for defeating the enemy
    protected int points;
    //Indicates whether the unit is in the process of dying
    private bool dying = false;
    

    //Inherited method to move all the enemy unity when game is playing
    abstract protected void Move();

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    // Checks if game is playing, if so move characters, if not ensure characters are not moving
    void Update()
    {
        if (gameManager.gameState == GameManager.gameStateEnum.PLAYING && !dying)
        {
            Move(); 
        }else
        {
            Rigidbody2D rb2d = this.gameObject.GetComponent<Rigidbody2D>();
            rb2d.velocity = Vector2.zero;
        }
    }

    //Method that determines the actions when the player touches an enemy
    //
    protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            //Get player and enemy transforms
            Transform enemyTransform = this.gameObject.GetComponent<Transform>();
            Transform playerTransform = coll.gameObject.GetComponent<Transform>();
            //This relies on groundcheck remaining the first child of player
            Transform groundCheckTransform = playerTransform.GetChild(0);
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();

            //Check if player is on top of an enemy
            if (enemyTransform.position.y < groundCheckTransform.position.y)
            {
                //If player is on top then jump player and kill enemy
                Rigidbody2D playerRb2d = coll.gameObject.GetComponent<Rigidbody2D>();
                playerRb2d.AddForce(new Vector2(0, 400));
                BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
                collider.enabled = false;
                gameManager.addToScore(points, player.getPlayerNumber());
                StartCoroutine(enemyDie());
                
            }else
            {
                //Otherwise damage player
                player.playerHit(damage);
            }
            
        }
    }

    /**
     * Stops the enemy from moving
     * Turns the sprite upside down and triggers die animation
     * Waits half a second then destroys gameobject
     */
    private IEnumerator enemyDie()
    {
        //If it is dying stop the enemy from moving
        dying = true;
        Rigidbody2D rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.zero;

        //Trigger the die animation
        Animator animator = this.gameObject.GetComponent<Animator>();
        animator.SetTrigger("EnemyDie");
        
        //Turn the enemy upside down
        Transform trans = this.gameObject.GetComponent<Transform>();
        Vector3 newScale = new Vector3();
        newScale = trans.localScale;
        newScale.y = -newScale.y;
        trans.localScale = newScale;

        //Wait then destroy the object
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
