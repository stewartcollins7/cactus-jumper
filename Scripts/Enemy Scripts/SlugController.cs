using UnityEngine;
using System.Collections;
using System;

/**
 * This class determines the actions of the walking enemy
 * which walks back and forth on flat ground, and changes direction
 * when it hits a trigger that signifies the end of the platform
 * 
 * Stewart Collins - Last Edit 18/09/16
 */

public class SlugController : EnemyController {
    //The speed the enemy moves at
    public float speed = 2f;
    //The direction the enemy is moving
    private bool goingLeft = true;
    //The object transform
    private Transform trans;

    //Set the transform object on initialisation
    public void Start()
    {
        trans = this.gameObject.GetComponent<Transform>();
        damage = 20;
        points = 150;
    }

    //The overrided move method from the superclass that moves the enemy
    protected override void Move()
    {
        Rigidbody2D rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        if (goingLeft)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }else
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        
    }

    //Method to change direction, called when a trigger is reached
    //Also flips the sprite image by multiplying the scale of the object by -1
    public void changeDirection()
    {
        if (goingLeft)
        {
            goingLeft = false;
        } else
        {
            goingLeft = true;
        }Vector3 previousScale = new Vector3();
        previousScale = trans.localScale;
        previousScale.x = -previousScale.x;
        this.trans.localScale = previousScale;
    }

    //If it hits something then change direction
    new void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        changeDirection();
    }
    
}
