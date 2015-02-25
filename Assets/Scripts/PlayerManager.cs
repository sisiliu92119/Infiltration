using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	public float health = 100f;                         // How much health the player has left.
	private bool playerDead;                            // A bool to show if the player is dead or not.

	public float turnSmoothing = 15f;   // A smoothing value for turning the player.
	public float speedDampTime = 0.1f;  // The damping for the speed parameter

	public float cloneTimeMax = 3.0f;
	public float cloneTimer;
	private bool isClone = false;

	private Animator anim;              // Reference to the animator component.
	private HashIDs hash;               // Reference to the HashIDs.
	private GameManager manager;

	private List<UserInput> inputs;
	private int inputIndex;
	private Vector3 startPos;
	private Quaternion startRot;
	private Transform spawn;

	[Serializable]
	public class UserInput{
		public float horizontalFloat;
		public float verticalFloat;
		public bool sneaking;

		public UserInput(float h, float v, bool s){
			horizontalFloat = h;
			verticalFloat = v;
			sneaking = s;
		}
	}
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		inputs  = new List<UserInput>();
		spawn = GameObject.FindGameObjectWithTag ("Spawn").transform;
		startPos = spawn.position;
		startRot = spawn.rotation;
		resetPlayer ();
	}
	
	
	void FixedUpdate ()
	{
		if(health > 0f){
			if(isClone){
				if(inputIndex < inputs.Count){ //progresing through clone life
					MovementManagement(inputs[inputIndex]);
					inputIndex++;
				}
				else{ //end of clone life
					//resetPlayer();
					health = 0f;
				}
			}	
			else{
				// Cache the inputs.
				if (Input.GetKey (KeyCode.F)) {
					if(cloneTimer > 0){
						cloneTimer -= Time.fixedDeltaTime; //decrease counter
					}
					else{
						isClone = true;
						resetPlayer();
						manager.clonePlayer();
					}
				}
				else{
					cloneTimer = cloneTimeMax;
				}
				
				UserInput input = new UserInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetButton("Sneak"));
				inputs.Add (input);
				MovementManagement(input);


			}
		}
		else{
			if(!playerDead){
				playerDead = true;
				anim.SetBool(hash.deadBool, playerDead);
			}
			else{
				if(anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState){
					anim.SetBool(hash.deadBool, false);
				}
			}
		}


	}
	
	public void resetPlayer(){
		health = 100f;
		playerDead = false;
		anim.SetBool(hash.deadBool, false);
		anim.Play (hash.idleState, 0, 0);
		inputIndex = 0;
		transform.position = startPos;
		transform.rotation = startRot;
		anim.SetFloat(hash.speedFloat, 0);
		cloneTimer = cloneTimeMax;
	}

	void MovementManagement (UserInput input)
	{
		// Set the sneaking parameter to the sneak input.
		anim.SetBool(hash.sneakingBool, input.sneaking);
		
		// If there is some axis input...
		if(input.horizontalFloat != 0f || input.verticalFloat != 0f)
		{
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotating(input.horizontalFloat, input.verticalFloat);
			anim.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
		}
		else
			// Otherwise set the speed parameter to 0.
			anim.SetFloat(hash.speedFloat, 0);
	}
	
	
	void Rotating (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);
	}
	
	
	void AudioManagement (bool shout)
	{
		// If the player is currently in the run state...
		if(anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.locomotionState)
		{
			// ... and if the footsteps are not playing...
			if(!audio.isPlaying)
				// ... play them.
				audio.Play();
		}
		else
			// Otherwise stop the footsteps.
			audio.Stop();
	}


	void PlayerDead(){

	}

	public void TakeDamage (float amount)
	{
		// Decrement the player's health by amount.
		health -= amount;
	}
}
