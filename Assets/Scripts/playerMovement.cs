﻿using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

	public float speed = 6f;            // The speed that the player will move at.
	
	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.



	void Awake ()
	{

		playerRigidbody = GetComponent <Rigidbody> ();
		renderer.material.color = Color.green;
	}

	void FixedUpdate ()
	{
		// Store the input axes.
		transform.position += Vector3.zero;
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		// Move the player around the scene.
		Move (h, v);


	}
	
	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (v, 0f, h);
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}

}
