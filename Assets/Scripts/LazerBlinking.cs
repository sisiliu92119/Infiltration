using UnityEngine;
using System.Collections;

public class LazerBlinking : MonoBehaviour 
{
	public bool on;
	public float onTime;
	public float offTime;
	private float timer;
	void Update()
	{
		if(on){
			timer += Time.deltaTime;
			if (renderer.enabled && timer >= onTime) 
			{
				SwitchBeam();
			}
			if(!renderer.enabled && timer >= offTime)
			{
				SwitchBeam();
			}
		}
	}

	void  SwitchBeam()
	{
		timer = 0f;
		renderer.enabled = !renderer.enabled;
		light.enabled = !light.enabled;
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
