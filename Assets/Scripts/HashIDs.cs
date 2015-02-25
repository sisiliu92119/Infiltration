using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class HashIDs : MonoBehaviour {

	public int idleState;
	public int dyingState;
	public int locomotionState;
	public int deadBool;
	public int speedFloat;
	public int sneakingBool;
=======
public class HashIDs : MonoBehaviour
{
	// Here we store the hash tags for various strings used in our animators.
	public int dyingState;
	public int locomotionState;
	public int shoutState;
	public int deadBool;
	public int speedFloat;
	public int sneakingBool;
	public int shoutingBool;
>>>>>>> Weijian_temp
	public int playerInSightBool;
	public int shotFloat;
	public int aimWeightFloat;
	public int angularSpeedFloat;
	public int openBool;
	
	
	void Awake ()
	{
<<<<<<< HEAD
		idleState = Animator.StringToHash("Base Layer.Idle");
		dyingState = Animator.StringToHash("Base Layer.Dying");
		locomotionState = Animator.StringToHash("Base Layer.Locomotion");
		deadBool = Animator.StringToHash("Dead");
		speedFloat = Animator.StringToHash("Speed");
		sneakingBool = Animator.StringToHash("Sneaking");
=======
		dyingState = Animator.StringToHash("Base Layer.Dying");
		locomotionState = Animator.StringToHash("Base Layer.Locomotion");
		shoutState = Animator.StringToHash("Shouting.Shout");
		deadBool = Animator.StringToHash("Dead");
		speedFloat = Animator.StringToHash("Speed");
		sneakingBool = Animator.StringToHash("Sneaking");
		shoutingBool = Animator.StringToHash("Shouting");
>>>>>>> Weijian_temp
		playerInSightBool = Animator.StringToHash("PlayerInSight");
		shotFloat = Animator.StringToHash("Shot");
		aimWeightFloat = Animator.StringToHash("AimWeight");
		angularSpeedFloat = Animator.StringToHash("AngularSpeed");
		openBool = Animator.StringToHash("Open");
	}
}
