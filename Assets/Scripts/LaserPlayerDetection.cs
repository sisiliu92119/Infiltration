using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour
{
	private GameObject player;                          // Reference to the player.
	private General lastPlayerSighting;      // Reference to the global last sighting of the player.
	int flag = 0;
	public float timer =0;
	
	void Awake ()
	{
		// Setting up references.
		player = GameObject.FindGameObjectWithTag("Player");
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<General>();
	}
	
	void Update(){
		if (flag == 1) {
			timer+=Time.deltaTime;
			if(timer>8){
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				flag=0;
				timer=0;
			}
			
		}
		
	}

	void OnTriggerStay(Collider other)
	{
		// If the beam is on...
		if(renderer.enabled)
			// ... and if the colliding gameobject is the player...
			if(other.gameObject == player)
				// ... set the last global sighting of the player to the colliding object's position.
				lastPlayerSighting.position = other.transform.position;
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == player) {
			//lastPlayerSighting.position = lastPlayerSighting.resetPosition;		
			flag=1;
		}
		
		
	}
}