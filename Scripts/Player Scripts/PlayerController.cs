using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * This class handles the player object that is controlled by the user
 * It handles user input, updates the health of the player and 
 * identifies when they are out of health.
 *  
 * Stewart Collins - Last Edit 28/08/16
 */

public class PlayerController : MonoBehaviour {

    //The maximum speed of the player
    public float maxSpeed = 2;
    //The amount of force applied when moving horizontally
    public float moveSpeed = 365;
    //The amount of force applied when jumping
    public float jumpSize = 500;
    //The starting about of health
    public int health = 100;
    //Whether the player is on the ground
    protected bool onTheGround = true;
    //The rigidbody of the player which interacts with the physics engine
    protected Rigidbody2D rigid;
    //The player animator
    protected Animator animator;
    //Whether the player is jumping
    protected bool jumping = false;
    //Sets the initial touch recieved to -1
    protected Vector2 touchOrigin = -Vector2.one;
    //The UI component that displays the user's health
    protected Text healthText;
    //Indicates whether it is player 1 or player 2
    protected int playerNumber;
    //Indicates the direction the player is facing
    protected bool facingLeft;
    SpriteRenderer playerSprite;
    protected GameManager gameManager;



    /**
     * Used to set the number of the player (P1 or P2) when instantiating player
     */
    public void setPlayerNumber(int number)
    {
        playerNumber = number;
    }

    /*Used to get the number of the player
     * returns player number
     */
    public int getPlayerNumber()
    {
        return playerNumber;
    }

    /**If the player is injured change the color
     * of the sprite to provide visual feedback
     * 
     */
    private IEnumerator playerHurtAnimation()
    {
        Color hurtColor = Color.red;
        playerSprite.color = hurtColor;
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = hurtColor;
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = Color.white;

    }

    /*Do damage to the player
     * If damage means the player has run out of health
     * Then signal game over
     */
    public void playerHit(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            health = 0;
            gameManager.GameOver();
        }else
        {
            StartCoroutine(playerHurtAnimation());
        }
        healthText.text = "Health: " + health;
    }

    /**Sets the grounded value to true
     * Used by the ground check class
     */
    public void SetGrounded()
    {
        onTheGround = true;
    }

    // Sets the player components needed by the controller on initialisation
    void Start () {
        rigid = GetComponent<Rigidbody2D>();

        GameObject healthGameObject;
        if (this.playerNumber == 1)
        {
            healthGameObject = GameObject.FindWithTag("P1HealthText");
        }else
        {
            healthGameObject = GameObject.FindWithTag("P2HealthText");
        }
        healthText = healthGameObject.GetComponent<Text>();
        GroundCheck groundCheck = GetComponentInChildren<GroundCheck>();
        groundCheck.setPlayer(this);
        animator = GetComponent<Animator>();
        playerSprite = this.gameObject.GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    //Handles the movement of the player based on user input
    //Virtual is only there for testing of multiplayer interactions between players with the 
    //other player controller script, should be removed for final build
    public virtual void FixedUpdate()
    {
        //Only move if game is in playing state
        if (gameManager.gameState == GameManager.gameStateEnum.PLAYING)
        {
            //The horizontal direction to move in
            float moveDirection = 0;

            //Touch controls for the android platform
#if UNITY_ANDROID
            //If there has been a touch
            if (Input.touchCount > 0)
            {
                //Only use the first touch recieved, not using multiple touches
                //at present
                Touch firstTouch = Input.touches[0];
                //If it is the start of the touch then save the initial position
                if (firstTouch.phase == TouchPhase.Began)
                {
                    touchOrigin = firstTouch.position;
                }//If it is not the end of the touch then set the move direction
                if(firstTouch.phase != TouchPhase.Ended)
                {

                    //If touch is on the left side of the screen then move left
                    if (firstTouch.position.x > Screen.width / 2)
                    {
                        moveDirection = 1;
                    }//Otherwise move right
                    else
                    {
                        moveDirection = -1;
                    }
                }
                //If it is the end of the touch and the touch swiped upwards then set to jump
                if(firstTouch.phase == TouchPhase.Ended)
                {
                    if(firstTouch.position.y > (touchOrigin.y + 20) && onTheGround)
                    {
                        rigid.AddForce(jumpSize * Vector2.up);
                        jumping = true;
                        onTheGround = false;
                    }
                }
            }

#else
            //If using keyboard then set move direction based on arrow keys
            moveDirection = Input.GetAxis("Horizontal");
            //And jump based on space bar
            if (Input.GetButtonDown("Jump") && onTheGround)
            {
                rigid.AddForce(jumpSize * Vector2.up);
                jumping = true;
                onTheGround = false;
            }

#endif

            /**Note this animator code is not working at the moment.
             * Not quite sure why, need to look back in to how the animator
             * component works
             */
            //If no move has been made then set animator to stand
            if (moveDirection.Equals(0) && !jumping)
            {
                animator.SetTrigger("playerIdle");
            }
            else
            {
                if(moveDirection < 0)
                {
                    if (!facingLeft)
                    {
                        Vector3 playerScale = this.gameObject.transform.localScale;
                        playerScale.x *= -1;
                        this.gameObject.transform.localScale = playerScale;
                        facingLeft = true;
                    }
                    
                }else
                {
                    if (facingLeft)
                    {
                        Vector3 playerScale = this.gameObject.transform.localScale;
                        playerScale.x *= -1;
                        this.gameObject.transform.localScale = playerScale;
                        facingLeft = false;
                    }
                }
                //If player is not already moving at max speed then increase speed in given direction
                if (Mathf.Abs(rigid.velocity.x) < maxSpeed)
                {
                    rigid.AddForce(moveDirection * moveSpeed * Vector2.right);
                }//If it is now moving faster than max speed then update to max speed
                if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
                {
                    rigid.velocity = new Vector2(maxSpeed * Mathf.Sign(rigid.velocity.x), rigid.velocity.y);
                }//If on the ground then set animation to walk
                if (onTheGround)
                {
                    //anim.SetTrigger("PlayerWalk");
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk"))
                    {
                        animator.SetTrigger("playerWalk");
                    }

                }else if (jumping)
                {
                    animator.SetTrigger("playerJump");
                    jumping = false;
                }
            }
        }
    }

}
