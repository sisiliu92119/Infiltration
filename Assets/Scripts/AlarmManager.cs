using UnityEngine;
using System.Collections;

public class AlarmManager : MonoBehaviour {
	private AudioSource siren;										// Reference to the AudioSources of the siren.
	public float alarmTimer = 0;
	void Awake(){
		siren = this.audio;
	}

	public void reset(){
		alarmTimer = 0;
	}

	public void soundAlarm(){
		alarmTimer = 15;
	}

	// Update is called once per frame
	void FixedUpdate () {
//		Debug.Log (alarmTimer);
		if (alarmTimer > 0 && !siren.isPlaying) 
		{//if not playing siren and his position is not safe
			siren.Play ();
//			Debug.Log("Alarm!!");
		}
		else if (alarmTimer > 0 && siren.isPlaying){
			alarmTimer -= Time.deltaTime;
		}
		else
		{//player's position is safe
			siren.Stop();
			GameObject[] enemies;
			enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach(GameObject e in enemies){
				e.GetComponent<EnemySight>().resumePatrol();
			}
		}
	}
}
