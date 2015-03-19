using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour 
{
//	public GameObject laser;
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
		GameObject[] lasers;
		lasers = GameObject.FindGameObjectsWithTag("Lazer");
		for (int i=0; i<lasers.Length; i++) 
		{
			lasers[i].GetComponent<LaserManager>().TurnOff();
		}
	}
}
