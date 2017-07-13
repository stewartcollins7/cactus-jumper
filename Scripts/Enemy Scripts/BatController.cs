using UnityEngine;
using System.Collections;

/**
 * Class for a bat enemy that flies in the air. Enemy moves in a diagonal
 * pattern oscilating between a set range on the y axis.
 * 
 * Stewart Collins - Last Edit 18/09/16
 */

public class BatController : EnemyController {

    //The speed the enemy moves at
    public float speed = 2f;
    //Indicates the y direction currently moving at
    private bool goingDown = true;
    //The range the y values may differ by before changing direction
    public float yRange = 2;
    //The initial y value of the enemy
    public float yStart;

    //Set initial values
    public void Start()
    {
        //Set initial y value based on initial position
        yStart = this.gameObject.transform.position.y;
        damage = 50;
        points = 300;
    }

    //Method to move the enemy
    protected override void Move()
    {
        //Set the enemy to moving left
        Rigidbody2D rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.left * speed;
        //If y max or min reached then change direction
        if (goingDown)
        {
            if (this.gameObject.transform.position.y < (yStart - yRange))
            {
                goingDown = false;
            }
        }
        else
        {
            if (this.gameObject.transform.position.y > (yStart + yRange))
            {
                goingDown = true;
            }
        }//Set the y direction of the movement
        if (goingDown)
        {
            rb2d.velocity += Vector2.down * speed;
        }
        else
        {
            rb2d.velocity += Vector2.up * speed;
        }
    }

    //If the bat hits something then change direction
    new void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (goingDown)
        {
           goingDown = false;
        }else
        {
           goingDown = true;
        }
    }

}
