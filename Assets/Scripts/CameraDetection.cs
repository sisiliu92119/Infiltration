using UnityEngine;
using System.Collections;

public class CameraDetection : MonoBehaviour {

	private GameObject player;								// The player gameObject.
	private General lastPlayerSighting;		// The general type object.
	int flag = 0;
	public float timer =0;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<General>();
	}

	void Update(){
		if (flag == 1) {
			timer+=Time.deltaTime;
			if(timer>11){
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				flag=0;
				timer=0;
			}
		
		}

	}
	void OnTriggerStay (Collider other)
	{

		// If collides with player object
		if(other.gameObject == player)
		{
			// shoot a ray to see if the first intection object is player or not
			Vector3 direction = player.transform.position - transform.position;
			RaycastHit hit;
			if(Physics.Raycast(transform.position, direction, out hit))
				// If the raycast hits the player.
				if(hit.collider.gameObject == player){
					// ... set the last global sighting of the player to the player's position.
					//this triggers the alarm
					lastPlayerSighting.position = player.transform.position;

				}
		}
	}



	void OnTriggerExit(Collider other){
		if (other.gameObject == player) {
			//lastPlayerSighting.position = lastPlayerSighting.resetPosition;		
			flag=1;
		}


	}
	

}
