using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {
	
	public Vector3 position = new Vector3(1000f, 1000f, 1000f);			// The last global sighting of the player.
	public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);	// The default position if the player is not in sight.
	
	
	private AudioSource siren;										// Reference to the AudioSources of the siren.
	
	void Awake(){
		siren = this.audio;
	}
	
	public void reset(){
		position = resetPosition;
	}
	
	void Update ()
	{
		// Switch the alarms 
		SwitchAlarms();
	}
	
	
	void SwitchAlarms ()
	{
		if (position != resetPosition && !siren.isPlaying) 
		{//if not playing siren and his position is not safe
			siren.Play ();
			Debug.Log("Alarm!!");
		}
		else if(position == resetPosition)
		{//player's position is safe
			siren.Stop();
		}
	}
	
	
	
}
