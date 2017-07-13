using UnityEngine;
using System.Collections;

/**
 * This is for an point item pickups such as
 * goal and diamonds that add points to the players score
 * 
 * Stewart Collins - Last Edit 28/08/16
 */
public class Pickup : MonoBehaviour {
    //The amount of points the object is worth
    public int points = 200;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    //If the object is touched by a player then destroy
    //the object and add their points the player's score
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            gameManager.addToScore(points, player.getPlayerNumber());
            Destroy(gameObject);
        }
    }
}
