using UnityEngine;
using System.Collections;

/**
 * This class was written to prevent the player from sticking to the 
 * sides of the platforms instead of falling when side velocity was
 * applied. Probably won't be needed as a better solution was found
 * with the platform effector component
 * 
 * Stewart Collins - Last Edited 28/08/16
 * 
 */
public class PlatformSideTrigger : MonoBehaviour {


    public Collider2D parentCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           Physics2D.IgnoreCollision(parentCollider, other);
        }
    }
}
