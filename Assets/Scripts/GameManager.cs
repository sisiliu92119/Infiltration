using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public GameObject playerPrefab;
	public CloneBar clonebar;
	public BombBar bombbar;
	public int level;
	public List<PlayerManager> playerInstances;
	public CameraManager cam;
	public GameObject currentPlayer;
	public int bombsPlanted = 0;
	public int totalNumBombs;
	public bool win = false;
	public Texture2D textureToDisplay;
	public FloorManager floors;
	public GameObject[] bombs;
	public GameObject[] walls;
	float explodeTime = 3.0f;
	float explodeTimer = 3.0f;
	int numExploded = 0;
	bool explodingBombs = false;
    // Use this for initialization
	void Start () {
//		clonebar = GameObject.FindGameObjectWithTag ("CloneBar").GetComponent<CloneBar> ();
		totalNumBombs = GameObject.FindGameObjectsWithTag("Bomb").Length;
		bombs = GameObject.FindGameObjectsWithTag("Bomb");
		resetLevel();
//		currentPlayer = (GameObject)Instantiate (playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity);
//		cam.setPlayer (currentPlayer.transform);
	}

	void Update(){
		if(explodingBombs){
			Debug.Log ("EXPLODE TIMER: " + explodeTimer);
			if(numExploded < bombs.Length){
				if(explodeTimer <= 0){

					bombs[numExploded].GetComponent<BombController>().explode();
					numExploded++;
					explodeTimer = explodeTime;
				}
                else{
                    explodeTimer -= Time.deltaTime;
                }
            }
		}
	}

	public void checkWin () {
		if(bombsPlanted == totalNumBombs){
			win = true;
			GameObject.Find ("GameController").GetComponent<AlarmManager> ().reset ();
			floors.showAll();
			cam.setWinView ();
			explodeBombs();
		}
	}
	
	public void clonePlayer(){
		playerInstances.Add (currentPlayer.GetComponent<PlayerManager>());
		resetLevel ();
	}

	public void resetLevel(){
		GameObject.Find ("GameController").GetComponent<General> ().reset ();
		GameObject.Find ("GameController").GetComponent<AlarmManager> ().reset ();
		
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject e in enemies){
			e.GetComponent<EnemyAI>().reset();
		}

		foreach(PlayerManager p in playerInstances){
			p.resetPlayer();
		}
		currentPlayer = (GameObject)Instantiate (playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
		clonebar.setPlayer (currentPlayer);
		bombbar.setPlayer (currentPlayer);
		cam.setPlayer (currentPlayer.transform);

		//Reset lasers
		GameObject[] lasers;
		lasers = GameObject.FindGameObjectsWithTag("Lazer");
		for (int i=0; i<lasers.Length; i++) 
		{
			lasers[i].GetComponent<LaserManager>().reset();
        }

		//Reset lasers
		GameObject[] buttons;
		buttons = GameObject.FindGameObjectsWithTag("Button");
		for (int i=0; i<buttons.Length; i++) 
		{
			buttons[i].GetComponent<ButtonController>().reset();
		}

		//Reset bombs
		for (int i=0; i<bombs.Length; i++) 
		{
			bombs[i].GetComponent<BombController>().reset();
		}

		bombsPlanted = 0;
	}

	void explodeBombs(){
		float explodeTimer = 3;
		explodingBombs = true;
	}

//	void OnGUI()
//	{
//		if(win)
//		{
//			GUI.Label(new Rect(180, -10, textureToDisplay.width, textureToDisplay.height), textureToDisplay);
//		}
//	}
}
