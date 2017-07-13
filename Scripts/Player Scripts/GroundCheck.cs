using UnityEngine;
using System.Collections;

/**
 * Checks whether the player is touching the ground
 * This is used to determine whether the player is able to jump
 * and also whether the player is over an enemy in order to damage it
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class GroundCheck : MonoBehaviour {

    //The player controller of the player the ground check is attatched to
    private PlayerController player;
	
    //Set the player the ground check is attatched to (called by player)
    public void setPlayer(PlayerController player)
    {
        this.player = player;
    }

    //If ground is touched then set grounded on player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground")){
            player.SetGrounded();
        }
    }
}
