using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {
	public int currentFloor;
	public GameObject[] floors;
	public GameObject[] floorEntities;
	public Vector3[] floorPositions;
	public Vector3 ramp0_1;
	private float floorDist;
	public Material floor0mat;
	public Material floor1mat;
	public Material floor2mat;
	public Material ramp0mat;
	public Material ramp1mat;
	public GameManager game;
	public bool showAllBool;
	void Start () {
		currentFloor = 0;
		floors = new GameObject[3];
		floorPositions = new Vector3[3];
		ramp0_1 = GameObject.Find ("Ramp0_1").transform.position;
		floor0mat.color = new Color (1, 1, 1, 1);
		floor1mat.color = new Color (1, 1, 1, 0);
		floor2mat.color = new Color (1, 1, 1, 0);
		ramp0mat.color = new Color (1, 1, 1, 1);
		ramp1mat.color = new Color (1, 1, 1, 0);
		floors [0] = transform.GetChild (0).gameObject;
		floors [1] = transform.GetChild (2).gameObject;
		floors [2] = transform.GetChild (1).gameObject;
		floorPositions [0] = transform.GetChild (0).position;
		floorPositions [1] = transform.GetChild (2).position;
		floorPositions [2] = transform.GetChild (1).position;
		showAllBool = false;
		floorDist = floorPositions[1].y - floorPositions [0].y;

		floorEntities [0].SetActive (true);
		floorEntities [1].SetActive (false);
		floorEntities [2].SetActive (false);
	}

	void FixedUpdate () {
		if(!showAllBool){
			if(game.currentPlayer.transform.position.y < floorPositions[1].y){
				float distToFloor1 = (floorPositions[1].y - game.currentPlayer.transform.position.y);
				floor0mat.color = new Color(1,1,1, (distToFloor1 / floorDist));
				floor1mat.color = new Color(1,1,1, 1 - (distToFloor1 / floorDist));
				ramp1mat.color = new Color(1,1,1, 1 - (distToFloor1 / floorDist));
				if(distToFloor1 > (floorDist / 2.0f)){
					currentFloor = 0;
				}
				else{
					currentFloor = 1;		
				}
			}
			else if (game.currentPlayer.transform.position.y < floorPositions[2].y){
				float distToFloor2 = (floorPositions[2].y - game.currentPlayer.transform.position.y);
				floor1mat.color = new Color(1,1,1, (distToFloor2 / floorDist));
				floor2mat.color = new Color(1,1,1, 1 - (distToFloor2 / floorDist));
                ramp0mat.color = new Color(1,1,1, (distToFloor2 / floorDist));
				if(distToFloor2 > (floorDist / 2.0f)){
					currentFloor = 1;
				}
				else{
                    currentFloor = 2;		
                }
            }
            else{
                currentFloor = 2;
			}
			checkFloor (currentFloor);
		}
	}
	public void checkFloor(int current){
		for(int i = 0; i < 3; i++){
			bool active = false;
			if(i == current) active = true;
			if(floorEntities[i].activeSelf != active){
				floorEntities[i].SetActive(active);
			}
		}
	}
	public Vector3 currentFloorPos(){
		return floorPositions[currentFloor];
	}

	public void showAll(){
		showAllBool = true;
		floor0mat.color = new Color (1, 1, 1, 1);
		floor1mat.color = new Color (1, 1, 1, 1);
		floor2mat.color = new Color (1, 1, 1, 1);
		foreach(GameObject e in floorEntities){
			Destroy (e);
		}
	}
}
