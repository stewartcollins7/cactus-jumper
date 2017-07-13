using UnityEngine;
using System.Collections;

/**This trigger script is used to catch the player if
 * they fall off the bottom of the level
 * 
 * Stewart Collins - Last Edit 28/08/16
 */
public class DeathScript : MonoBehaviour {

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

	/**If player hits the trigger then signal game over
     * Otherwise destroy the object that hit the trigger
     * 
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GameOver();
        }else
        {
            Destroy(other.gameObject);
        }
        
        
    }

}
