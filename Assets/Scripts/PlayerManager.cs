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

	private Renderer p_renderer;
	public Material opaque;
	public Material clear;

	private List<UserInput> inputs;
	private int inputIndex;
	private Vector3 startPos;
	private Quaternion startRot;
	private Transform spawn;

	private GameObject collidingBomb;
	private GameObject holdingBomb;
	private bool droppingBomb;
	public float dropTimer;
	public float dropTime = 3.0f;

	[Serializable]
	public class UserInput{
		public float horizontalFloat;
		public float verticalFloat;
		public bool usingBool;

		public UserInput(float h, float v, bool s){
			horizontalFloat = h;
			verticalFloat = v;
			usingBool = s;
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
		p_renderer = transform.GetChild (0).renderer;
		p_renderer.material = opaque;
		startPos = spawn.position;
		startRot = spawn.rotation;
		resetPlayer ();
	}
	
	
	void FixedUpdate ()
	{
		if(health > 0f){
			if(isClone){
				if(inputIndex == 10){
					transform.position = startPos;
					transform.rotation = startRot;
				}
//				Debug.Log ("CLONE POSITION INDEX " + inputIndex + ": (" + (int)transform.position.x + "," + (int)transform.position.y + "," + (int)transform.position.z + ")");

				if(inputIndex < inputs.Count){ //progresing through clone life
					MovementManagement(inputs[inputIndex]);
					inputIndex++;
				}
				else{ //end of clone life
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
						manager.clonePlayer();
//						resetPlayer();
					}
				}
				else{
					cloneTimer = cloneTimeMax;
				}

				Vector3 forward = transform.position - GameObject.Find("Main Camera").transform.position;
				forward.y = 0;
				forward = Vector3.Normalize(forward);
				Vector3 right = Vector3.Normalize(Vector3.Cross(forward, Vector3.up));
				forward = forward * Input.GetAxis("Vertical");
				right = right * -1 * Input.GetAxis("Horizontal");
				float verticalInput = forward.x + right.x;
				float horizontalInput = forward.z + right.z;
				UserInput input = new UserInput(verticalInput, horizontalInput, Input.GetButton("Use"));
				inputs.Add (input);
				MovementManagement(input);


			}
		}
		else{
			if(!playerDead){
				playerDead = true;
				dropBomb();
				anim.SetBool(hash.deadBool, playerDead);
			}
			else{
				if(anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState){
					anim.SetBool(hash.deadBool, false);
				}
				else{
					if(!isClone){
						isClone = true;
                        manager.clonePlayer();
//						resetPlayer();
					}
				}
			}
		}


	}

	public void resetPlayer(){
		health = 100f;
		playerDead = false;
		anim.SetBool(hash.deadBool, false);
		anim.SetFloat(hash.speedFloat, 0);
		anim.Play (hash.idleState, 0, 0);
		p_renderer.enabled = true;
		if(isClone){
			Material[] matArray = new Material[5]{clear, clear, clear, clear, clear};
			p_renderer.materials = matArray;
		}
		else{
			p_renderer.material = opaque;
		}
		inputIndex = 0;
		transform.position = startPos;
		transform.rotation = startRot;
		
		cloneTimer = cloneTimeMax;

		dropBomb ();

		collidingBomb = null;
		holdingBomb = null;
		droppingBomb = false;
		dropTimer = 0;
//		Debug.Log ("RESET POSITION: (" + transform.position.x + "," + transform.position.y + "," + transform.position.z + ")");
		
	}

	void MovementManagement (UserInput input)
	{
		if(input.usingBool){
			if(collidingBomb != null && holdingBomb == null) { //pick up bomb
				holdingBomb = collidingBomb;
				collidingBomb = null;
				holdingBomb.collider.enabled = false;
				holdingBomb.transform.parent = transform;
				holdingBomb.transform.localPosition = new Vector3(0, 1.2f, -0.4f);
				holdingBomb.transform.localRotation = Quaternion.identity;
			}
			else if(holdingBomb != null){
				if(!droppingBomb){
					droppingBomb = true;
					dropTimer = dropTime;

				}
				else{
					dropTimer -= Time.fixedDeltaTime;
					if(dropTimer <= 0){ //Plant bomb
						droppingBomb = false;
						dropTimer = 0;
						holdingBomb.renderer.material.color = Color.red;
						holdingBomb.transform.localPosition = new Vector3(0, 0.25f, 0);
						holdingBomb.transform.parent = null;
						holdingBomb = null;			
						manager.bombsPlanted++;
						manager.checkWin();
					}
				}
			}
		}
		else{
			droppingBomb = false;
			dropTimer = 0;
		}

		// If there is some axis input...
		if(input.horizontalFloat != 0f || input.verticalFloat != 0f)
		{
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotating(input.horizontalFloat, input.verticalFloat);
			anim.SetFloat(hash.speedFloat, 5.5f, 0, Time.deltaTime);
		}
		else
			// Otherwise set the speed parameter to 0.
			anim.SetFloat(hash.speedFloat, 0);


	}

	public void dropBomb(){
		if(holdingBomb != null){
			holdingBomb.transform.localPosition = new Vector3(0, 0.25f, 0);
			holdingBomb.collider.enabled = true;
			holdingBomb.transform.parent = null;
		}
	}

	void Rotating (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
//		Vector3 cameraDirection = transform.position - 
//		Vector3 targetDirection
		
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


	void OnTriggerEnter(Collider other){
		if(holdingBomb == null && other.gameObject.tag == "Bomb"){
			collidingBomb = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Bomb"){
			collidingBomb = null;
		}
	}

	public void TakeDamage (float amount)
	{
		// Decrement the player's health by amount.
		health -= amount;
	}
}
