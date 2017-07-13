using UnityEngine;
using System.Collections;
using System;

/**Controller for the bee enemy
 * That moves backwards and forwards horizontally
 * within a given range, at a certain speed
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class BeeController : EnemyController {

    //The amount of damage the enemy does to the player
    public int DAMAGE = 60;
    //The amount of points the player gets for destroying the enemy
    public int POINTS = 300;

    //Indicates the direction the enemy is moving
    private bool facingLeft = true;
    //The distance the enemy moves left or right
    public float beeRange= 5.0f;
    //The speed the enemy moves at
    public float speed = 5.0f;
    //The initial x position of the enemy
    private float startingX;
    //The rigidbody 2D component
    private Rigidbody2D rb2d;
    
    /**Moves the enemy
     * Inherited from AIController base class
     */
    protected override void Move()
    {
        if (facingLeft)
        {
            Vector3 currentPositon = this.transform.position;
            //If the enemy has reached the end of its range then change position
            if(currentPositon.x < (startingX -beeRange))
            {
                facingLeft = false;
                Vector3 currentScale = this.transform.localScale;

                currentScale.x *= -1;
                this.transform.localScale = currentScale;
                this.rb2d.velocity = Vector2.zero;
            }//Otherwise move the enemy in that direction
            else
            {
                this.rb2d.velocity += Vector2.left * speed;
            }
        }//Do the same if facingRight
        else
        {
            Vector3 currentPositon = this.transform.position;
            if (currentPositon.x > (startingX + beeRange))
            {
                facingLeft = true;
                Vector3 currentScale = this.transform.localScale;
                currentScale.x *= -1;
                this.transform.localScale = currentScale;
                this.rb2d.velocity = Vector2.zero;
            }
            else
            {
                this.rb2d.velocity += Vector2.right * speed;
            }
        }
    }
	
    //Initialise starting variables and set component variables
	void Start () {
        damage = DAMAGE;
        points = POINTS;
        startingX = this.transform.position.x;
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
	}
}
