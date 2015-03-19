using UnityEngine;
using System.Collections;

public class LaserManager : MonoBehaviour {
	public bool on = true;
	public bool blink = true;
	public float onTime = 1.5f;
	public float offTime = 1.5f;
	private float timer;
//	private General lastPlayerSighting;      // Reference to the global last sighting of the player.
//	int flag = 0;
//	public float alarmTimer =0;

	void Awake ()
	{
		// Setting up references.
//		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<General>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(blink && on){
			timer += Time.fixedDeltaTime;
			if (renderer.enabled && timer >= onTime) 
			{
				SwitchBeam();
			}
			if(!renderer.enabled && timer >= offTime)
			{
				SwitchBeam();
			}
		}

//		if (flag == 1) {
//			alarmTimer+=Time.deltaTime;
////			Debug.Log (alarmTimer);
//			if(alarmTimer>15){
//				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
//				flag=0;
//				alarmTimer=0;
//			}
//		}
	}
	void OnTriggerStay(Collider other)
	{
		// If the beam is on...
		if(renderer.enabled){
			// ... and if the colliding gameobject is the player...
			if(other.gameObject.tag == "Player"){
				// ... set the last global sighting of the player to the colliding object's position.
//				lastPlayerSighting.position = other.transform.position;
//				other.gameObject.GetComponent<PlayerManager>().health = 0;

			}
		}
		
	}
	void  SwitchBeam()
	{
		timer = 0f;
		renderer.enabled = !renderer.enabled;
		light.enabled = !light.enabled;
	}

	public void reset(){
		TurnOn ();
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
