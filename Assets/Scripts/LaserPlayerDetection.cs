using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour
{
//	private GameObject player;                          // Reference to the player.
	public bool on;
	private General lastPlayerSighting;      // Reference to the global last sighting of the player.
	int flag = 0;
	public float alarmTimer =0;
	
	void Awake ()
	{
		// Setting up references.
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<General>();
	}
	
	void Update(){
		if (flag == 1) {
			alarmTimer+=Time.deltaTime;
			if(alarmTimer>8){
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				flag=0;
				alarmTimer=0;
			}
		}
		
	}

	void OnTriggerStay(Collider other)
	{
		// If the beam is on...
		if(renderer.enabled){
			// ... and if the colliding gameobject is the player...
			if(other.gameObject.tag == "Player"){
				// ... set the last global sighting of the player to the colliding object's position.
				lastPlayerSighting.position = other.transform.position;
			}
		}
			
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			//lastPlayerSighting.position = lastPlayerSighting.resetPosition;		
			flag=1;
		}
	}
	void ShutOff(){
		on = false;
		renderer.enabled = false;
	}
	
	void TurnOn(){
		on = true;
		renderer.enabled = true;
	}
}