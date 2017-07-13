using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("y:" + other.relativeVelocity.y + "  x:" + other.relativeVelocity.x);
        if (other.gameObject.CompareTag("Player")){
            Rigidbody2D rb2d = other.gameObject.GetComponent<Rigidbody2D>();
            rb2d.velocity = -other.relativeVelocity;
            //Debug.Log(rb2d.velocity.y);
            if(rb2d.velocity.y > 0)
            {
                //Debug.Break();
                BoxCollider2D platformCollider = this.gameObject.GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(other.collider, platformCollider);
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BoxCollider2D platformCollider = this.gameObject.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(other.collider, platformCollider, false);   
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
