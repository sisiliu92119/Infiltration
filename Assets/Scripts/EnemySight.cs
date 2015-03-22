using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
	
	public float fieldOfViewAngle = 110f;				// Number of degrees, centred on forward, for the enemy see.
	public bool playerInSight;							// Whether or not the player is currently sighted.
	public Vector3 personalLastSighting;				// Last place this enemy spotted the player. 
	public Vector3 previousSighting;					// Where the player was sighted last frame.//
	
	
	private NavMeshAgent nav;							// Reference to the NavMeshAgent component.
	private SphereCollider col;							// Reference to the sphere collider trigger component.
	private Animator anim;								// Reference to the Animator.
	private General gameController;	// Reference to last global sighting of the player.
	private AlarmManager alarm;	// Reference to last global sighting of the player.
	private GameObject targetPlayer;							// Reference to the player.

	private Animator playerAnim;						// Reference to the player's animator component.
	private DonePlayerHealth playerHealth;				// Reference to the player's health script.
	private HashIDs hash;							// Reference to the HashIDs.
	
	
	void Awake ()
	{
		// Setting up the references.
		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		anim = GetComponent<Animator>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<General>();
		alarm = GameObject.FindGameObjectWithTag("GameController").GetComponent<AlarmManager>();
		targetPlayer = GameObject.FindGameObjectWithTag("Player");
		hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
		
		// Set the personal sighting and the previous sighting to the reset position.
		personalLastSighting = Vector3.zero;
		previousSighting = Vector3.zero;
	}
	
	public void reset(){
		playerInSight = false;
		personalLastSighting = Vector3.zero;
		previousSighting = Vector3.zero;
	}
	void FixedUpdate ()
	{
		//Sets target if guard sees player
		// If the last global sighting of the player has changed...
//		if(personalLastSighting != previousSighting)
			// ... then update the personal sighting to be the same as the global sighting.
//			personalLastSighting = previousSighting;
		//set personalLastSighting and previousSighting to alarm player position
		
		// Set the previous sighting to the be the sighting from this frame.
//		previousSighting = gameController.position;
//		previousSighting = personalLastSighting;
		
//		 If the player is alive...
		if(targetPlayer && targetPlayer.GetComponent<PlayerManager>().health > 0f)
//			 ... set the animator parameter to whether the player is in sight or not.
			anim.SetBool(hash.playerInSightBool, playerInSight);
		else
//			 ... set the animator parameter to false.
			anim.SetBool(hash.playerInSightBool, false);
	}
	
	public void setTarget(Vector3 targetPos){
//		Debug.Log ("Setting target to: (" + targetPos.x + "," + targetPos.y + "," + targetPos.z + ")");
		previousSighting = targetPos;
		personalLastSighting = targetPos;
	}

	public void resumePatrol(){
		previousSighting = Vector3.zero;
		personalLastSighting = Vector3.zero;
	}

	void OnTriggerStay (Collider other)
	{
		// If the player has entered the trigger sphere...
		if(other.gameObject.tag == "Player")
		{
			// By default the player is not in sight.
			playerInSight = false;
			targetPlayer = other.gameObject;
			EnemyAnimation localAnim = gameObject.GetComponent<EnemyAnimation>();
			localAnim.targetPlayer = other.transform;
			EnemyShooting localShoot = gameObject.GetComponent<EnemyShooting>();
			localShoot.targetPlayer = other.gameObject;
			EnemyAI localAI = gameObject.GetComponent<EnemyAI>();
			localAI.targetPlayer = other.transform;
			
			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			// If the angle between forward and where the player is, is less than half the angle of view...
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				
				// ... and if a raycast towards the player hits something...
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
				{
					// ... and if the raycast hits the player...
					if(hit.collider.gameObject.tag == "Player" && hit.collider.gameObject.GetComponent<PlayerManager>().health > 0)
					{
						
						// ... the player is in sight.
						playerInSight = true;
						
						// Set the last global sighting is the players current position.
//						gameController.position = other.gameObject.transform.position;
						alarm.soundAlarm();
						setTarget(other.gameObject.transform.position);
					}
				}
			}

		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if(other.gameObject.tag == "Player")
			// ... the player is not in sight.
			playerInSight = false;
	}
	
	
	float CalculatePathLength (Vector3 targetPosition)
	{
		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath();
		if(nav.enabled)
			nav.CalculatePath(targetPosition, path);
		
		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3 [] allWayPoints = new Vector3[path.corners.Length + 2];
		
		// The first point is the enemy's position.
		allWayPoints[0] = transform.position;
		
		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		
		// The points inbetween are the corners of the path.
		for(int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}
		
		// Create a float to store the path length that is by default 0.
		float pathLength = 0;
		
		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for(int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		
		return pathLength;
	}
}
