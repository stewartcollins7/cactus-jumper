using UnityEngine;
using System.Collections;
using System;

/**Controller for the frog enemy that waits
 * and then jumps
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class FrogController : EnemyController {

    //The amount of points a player recieves for destroying the enemy
    public int POINTS = 200;
    //The amount of damage the enemy does
    public int DAMAGE = 40;
    //The speed at which the enemy jumps
    public float JUMPSPEED=20;
    //The horizontal distance of the jump
    public float JUMPLENGTH=4;
    //The vertical distance of the jump
    public float JUMPHEIGHT=4;

    //The timer for the jump
    private float jumpTimer;
    //The rigidbody element of the enemy
    private Rigidbody2D rb2d;
    //The sprite render for the enemy
    private SpriteRenderer rnderer;


    /**Moves the enemy
     * Inherited from AIController base class
     */
    protected override void Move()
    {
        //If it is on screen then move, if not do nothing
        if (rnderer.isVisible)
        {
            jumpTimer -= Time.deltaTime;
            //When timer is reached then jump
            if (jumpTimer < 0)
            {
                Vector2 jumpVector = new Vector2(-JUMPLENGTH, JUMPHEIGHT);
                rb2d.AddForce(jumpVector);
                jumpTimer = 10 / JUMPSPEED;
            }
        }
        
    }

    // Get components and set timer to zero
    void Start () {
        jumpTimer = 0;
        rnderer = this.gameObject.GetComponent<SpriteRenderer>();
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
	}
}
