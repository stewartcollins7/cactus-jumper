using UnityEngine;
using System.Collections;

/**
 * The camera follows the player around the game level moving
 * along the x axis
 * 
 * Stewart Collins - Last Edit 18/09/16
 */
public class CameraController : MonoBehaviour {

    private GameObject player1;
    //Sets the start of the level
    public float xMin = 0;
    //Sets the maximum length of the level
    public float xMax = 157;

    //Set the camera to follow player 1
	void Start () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PlayerController controller = players[0].GetComponent<PlayerController>();
        if(controller.getPlayerNumber() == 1)
        {
            player1 = players[0];
        }else
        {
            player1 = players[1];
        }
	}
	
    //Updates the camera position based on the position of the player
	void LateUpdate () {
        
        if (player1.transform.position.x > xMin && player1.transform.position.x < xMax)
        {
            Camera.main.transform.position = new Vector3(player1.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
	}
}
