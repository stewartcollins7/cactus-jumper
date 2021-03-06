﻿using UnityEngine;
using System.Collections;

/**
 * This class most likely not needed, was meant to seperate 
 * user input control from server control but apparently that
 * is not needed with the way the unity sever handles things
 * 
 */
public class UserInputPlayerController : PlayerController {
    /*
    //Handles the movement of the player based on user input
    public override void FixedUpdate()
    {
        //Only move if game is in playing state
        if (GameManager.instance.gameState == GameManager.gameStateEnum.PLAYING)
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
             *
            //If no move has been made then set animator to stand
            if (moveDirection.Equals(float.Epsilon) && !jumping)
            {
                this.anim.SetTrigger("Stand");
            }
            else
            {
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
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk"))
                    {
                        anim.SetTrigger("PlayerWalk");
                    }

                }
            }
        }
    }*/
}
