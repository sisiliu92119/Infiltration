using UnityEngine;
using System.Collections;

public class LaserManager : MonoBehaviour {
	public bool on = true;
	public float onTime = 1.5f;
	public float offTime = 1.5f;
	private float timer;
	private General lastPlayerSighting;      // Reference to the global last sighting of the player.
	int flag = 0;
	public float alarmTimer =0;

	void Awake ()
	{
		// Setting up references.
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<General>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(on){
			timer += Time.deltaTime;
			if (renderer.enabled && timer >= onTime) 
			{
				Debug.Log("SWITCH1");
				SwitchBeam();
			}
			if(!renderer.enabled && timer >= offTime)
			{
				Debug.Log("SWITCH2");
				
				SwitchBeam();
			}
		}

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
	void  SwitchBeam()
	{
		timer = 0f;
		renderer.enabled = !renderer.enabled;
		light.enabled = !light.enabled;
	}
	public void TurnOff(){
		on = false;
		renderer.enabled = false;
	}
	
	public void TurnOn(){
		on = true;
		renderer.enabled = true;
	}
}
