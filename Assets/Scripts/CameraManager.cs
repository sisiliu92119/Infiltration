﻿using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float smooth = 1.5f;         // The relative speed at which the camera will catch up.
	public Transform player = null;           // Reference to the player's transform.
	public FloorManager floor;

	private Vector3 startPos;
	private Quaternion startRot;

	private Vector3 relCameraPos;       // The relative position of the camera from the player.
	private float relCameraPosMag = 10f;      // The distance of the camera from the player.
	private Vector3 newPos;             // The position the camera is trying to reach.
	
	
	void Awake ()
	{
		startPos = transform.position;
		startRot = transform.rotation;
	}
	
	
	void FixedUpdate ()
	{
		if(player){
			// The standard position of the camera is the relative position of the camera from the player.
//			Vector3 standardPos = player.position + relCameraPos;
//			Vector3 direction = Vector3.Normalize(player.position - GameObject.Find("Plane").transform.position);
			Vector3 direction = Vector3.Normalize(player.position - floor.currentFloorPos());
			Vector3 standardPos = (player.position + direction * relCameraPosMag)+ (Vector3.up * relCameraPosMag/1.5f);
//			Debug.Log ("STANDARDPOS: " + standardPos.x + "," + standardPos.y + "," + standardPos.z);
			// The abovePos is directly above the player at the same distance as the standard position.
			Vector3 abovePos = (2*standardPos + (player.position + Vector3.up * relCameraPosMag))/3;
			
			// An array of 5 points to check if the camera can see the player.
			Vector3[] checkPoints = new Vector3[5];
			
			// The first is the standard position of the camera.
			checkPoints[0] = standardPos;
			
			// The next three are 25%, 50% and 75% of the distance between the standard position and abovePos.
			checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
			checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
			checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
			
			// The last is the abovePos.
			checkPoints[4] = abovePos;
			
			// Run through the check points...
			for(int i = 0; i < checkPoints.Length; i++)
			{
				// ... if the camera can see the player...
				if(ViewingPosCheck(checkPoints[i]))
					// ... break from the loop.
					break;
			}
//			Debug.Log ("NEWPOS: " + newPos.x + "," + newPos.y + "," + newPos.z);
			// Lerp the camera's position between it's current position and it's new position.
			transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
			
			// Make sure the camera is looking at the player.
			SmoothLookAt();
		}
	}
	
	
	bool ViewingPosCheck (Vector3 checkPos)
	{
		RaycastHit hit;
		int playerLayer = 11;
		int playerMask = 1 << playerLayer;
		// If a raycast from the check position to the player hits something...
		if(Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag, playerMask))
			// ... if it is not the player...
			if(hit.transform != player)
				// This position isn't appropriate.
				return false;
		
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		newPos = checkPos;
		return true;
	}
	
	public void setPlayer(Transform p){
		transform.position = startPos;
		newPos = startPos;
		transform.rotation = startRot;
		player = p;
		// Setting the relative position as the initial relative position of the camera in the scene.
//		relCameraPos = transform.position - player.position;
//		relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}

	void SmoothLookAt ()
	{
		// Create a vector from the camera towards the player.
		Vector3 relPlayerPosition = player.position - transform.position;
		
		// Create a rotation based on the relative position of the player being the forward vector.
		Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
		
		// Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}

	public void setWinView(){
		Destroy (player.gameObject);
		transform.position = new Vector3 (-50, 30, -7);
		transform.rotation = Quaternion.LookRotation((floor.floorPositions[1] - transform.position), Vector3.up);
	}
}
