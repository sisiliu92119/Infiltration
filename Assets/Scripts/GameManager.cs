using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public GameObject playerPrefab;
	public CloneBar clonebar;
	public int level;
	public List<PlayerManager> playerInstances;
	public CameraManager cam;
	private GameObject currentPlayer;

	// Use this for initialization
	void Start () {
//		clonebar = GameObject.FindGameObjectWithTag ("CloneBar").GetComponent<CloneBar> ();

		resetLevel();
//		currentPlayer = (GameObject)Instantiate (playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity);
//		cam.setPlayer (currentPlayer.transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clonePlayer(){
		playerInstances.Add (currentPlayer.GetComponent<PlayerManager>());
		resetLevel ();
	}

	public void resetLevel(){
		foreach(PlayerManager p in playerInstances){
			p.resetPlayer();
		}
		currentPlayer = (GameObject)Instantiate (playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity);
		clonebar.setPlayer (currentPlayer);
		cam.setPlayer (currentPlayer.transform);

		//Reset lasers
		GameObject[] lasers;
		lasers = GameObject.FindGameObjectsWithTag("Lazer");
		for (int i=0; i<lasers.Length; i++) 
		{
			lasers[i].GetComponent<LaserManager>().TurnOn();
        }
	}
}
