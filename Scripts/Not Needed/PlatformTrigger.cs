using UnityEngine;
using System.Collections;


/**
 * This class was written to handle one way jumps through platforms
 * Probably won't be needed as a better solution was found using the 
 * Platform Effector component built in to Unity
 * 
 * Stewart Collins - Last Edited 28/08/16
 */
public class PlatformTrigger : MonoBehaviour {

    public Collider2D parentCollider;

    public void Start()
    {
        //Not working for some reason but works when assigned through Unity GUI
        //parentCollider = this.gameObject.GetComponentInParent<Collider2D>();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb2d = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb2d.velocity.y > 0)
            {
                Physics2D.IgnoreCollision(parentCollider, other);
            }
        }
    }
	
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb2d = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb2d.velocity.y > 0)
            {
                Physics2D.IgnoreCollision(parentCollider, other, false);
            }
        }
    }
}
