using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CameraDetection : MonoBehaviour {

	private AlarmManager alarm;		// The general type object.

	void Awake ()
	{
		alarm = GameObject.FindGameObjectWithTag("GameController").GetComponent<AlarmManager>();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerManager>().health > 0)
		{
			// shoot a ray to see if the first intection object is player or not
			Vector3 direction = other.transform.position - transform.position;
			RaycastHit hit;
			if(Physics.Raycast(transform.position, direction, out hit))
				// If the raycast hits the player.
			if(hit.collider.gameObject == other.gameObject){
				// ... set the last global sighting of the player to the player's position.
				//this triggers the alarm
				alarm.soundAlarm();
				//and calls the guards
				signalGuards(other.gameObject.transform.position);
			}
		}
	}
	void OnTriggerStay (Collider other)
	{
		// If collides with player object
		if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerManager>().health > 0)
		{
			// shoot a ray to see if the first intection object is player or not
			Vector3 direction = other.transform.position - transform.position;
			RaycastHit hit;
			if(Physics.Raycast(transform.position, direction, out hit))
				// If the raycast hits the player.
    			if(hit.collider.gameObject == other.gameObject){
					// ... set the last global sighting of the player to the player's position.
					//this triggers the alarm
					alarm.soundAlarm();
				}
		}
	}

	void signalGuards(Vector3 targetPos){
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
//		foreach(GameObject e in enemies){
//			if((e.transform.position - transform.position).magnitude );
//		}
		for(int i = 1; i < enemies.Length; i++){
			int j = i;
			float jLen = (enemies[j].transform.position - transform.position).magnitude;
			float jprevLen = (enemies[j-1].transform.position - transform.position).magnitude;
			while(j > 0 && jprevLen > jLen){
				GameObject temp = enemies[j];
				enemies[j] = enemies[j - 1];
				enemies[j-1] = temp;

				j--;
				jLen = (enemies[j].transform.position - transform.position).magnitude;
				jprevLen = (enemies[j-1].transform.position - transform.position).magnitude;
			}
		}

        int count = 0;
		for(int i = 0; i < enemies.Length; i++){
			if(count < 2 && !enemies[i].GetComponent<EnemySight>().playerInSight){
				enemies[i].GetComponent<EnemySight>().setTarget(targetPos);
				count++;
			}
		}
	}
}
