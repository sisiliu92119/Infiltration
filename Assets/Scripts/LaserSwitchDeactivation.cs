using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour 
{
	public LaserManager[] lasers;
//	private GameObject player;
	void Awake()
	{
//		player = GameObject.FindGameObjectWithTag("Player");
	}
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			if(Input.GetButton("Switch"))
			{
				LaserDeactivation();
			}
		}
	}
	void LaserDeactivation()
	{
		//Destroy (laser);
		foreach(LaserManager l in lasers){
			l.TurnOff();
		}
	}
}
