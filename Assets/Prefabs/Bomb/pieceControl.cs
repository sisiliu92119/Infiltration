using UnityEngine;
using System.Collections;

public class pieceControl : MonoBehaviour {
	private float timeBorn;

	// Use this for initialization
	void Start () 
	{
		timeBorn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - timeBorn) >= 0.8) 
		{
			Destroy(gameObject);
		}
	
	}
}
