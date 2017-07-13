using UnityEngine;
using System.Collections;

/**This is for triggers at the edges of
 * platforms that contain walking enemies
 * To cause them to change direction rather
 * than walking off the edge
 *
 * Stewart Collins - Last Edit 28/08/16
 */
public class WalkingEnemyTrigger : MonoBehaviour {

    //If trigger is reached then change direction
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WalkingEnemy"))
        {
            SlugController enemyController = other.gameObject.GetComponent < SlugController>();
            enemyController.changeDirection();
        }
    }
	
}
