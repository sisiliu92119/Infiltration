using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {
	public LaserManager[] lasers;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reset(){
		renderer.material.color = new Color(1,1,0,1);
		foreach(LaserManager laser in lasers){
			laser.TurnOn();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player"){
			renderer.material.color = new Color(0,1,0,1);
			foreach(LaserManager laser in lasers){
				if(laser.on){
					laser.TurnOff();
				}
			}
		}
		
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			renderer.material.color = new Color(1,1,0,1);
			foreach(LaserManager laser in lasers){
				laser.TurnOn();
			}
		}
	}
}
