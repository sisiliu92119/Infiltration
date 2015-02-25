using UnityEngine;
using System.Collections;


public class plantBomb : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	private GameObject bomb;
	int flag=0;
	bool win=false;
	public Texture2D textureToDisplay;
	//float timer =0;
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		bomb = GameObject.FindGameObjectWithTag ("bomb");
		bomb.renderer.enabled = false;
	}
	void Update(){
		if (flag == 1) {
			setupBomb();	
		}
	}
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player) 
		{

			flag=1;
			//setupBomb();
		}
	}
	void OnTriggerExit(Collider other){
		if (other.gameObject == player) {
			//lastPlayerSighting.position = lastPlayerSighting.resetPosition;		
			flag=0;
		}
	}

	void setupBomb(){

		float timer=0;
		if(Input.GetButtonDown("Switch"))
		{
			timer = Time.time;
		}
		if(Input.GetButton("Switch"))
		{
			if (Time.time - timer >= 3)
			{
				bomb.renderer.enabled=true;
				win=true;
				timer = 0;

			}
		}
		if(Input.GetButtonUp("Switch"))
		{
			timer = 0;
		}
	}

	void OnGUI()
	{
		if(win)
		{
			GUI.Label(new Rect(180, -10, textureToDisplay.width, textureToDisplay.height), textureToDisplay);
		}
	}
}
