using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {
	Vector3 startPos;
	Quaternion startRot;
	public GameObject piece;
	public GameObject explodeEffect;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		startRot = transform.rotation;
		renderer.material.color = Color.green;
	}

	public void reset(){
		collider.enabled = true;
		transform.position = startPos;
		transform.rotation = startRot;
		renderer.material.color = Color.green;
	}

//	void Update(){
//		if (Input.GetButtonDown ("Jump")) 
//		{
//			explode();
//			Instantiate(explodeEffect, gameObject.transform.position, gameObject.transform.rotation);
//		}
//	}

	public void explode()
	{
		Instantiate(explodeEffect, gameObject.transform.position, gameObject.transform.rotation);
		for (int i=0; i<500; i++) 
		{
			float x = Random.Range(-300,300)/200;
			float y = Random.Range(-300,300)/200;
			float z = Random.Range(-300,300)/200;
			GameObject ppy = Instantiate(piece, gameObject.transform.position + new Vector3(x,y,z), Quaternion.identity) as GameObject;
			//pieces[i] = Instantiate(piece);
			//ppy.transform.position = gameObject.transform.position + new Vector3(x,y,z);
			//ppy.rigidbody.AddForce(x1,x2,x3);
			//piece.transform.rotation = Vector3(0,0,0);
			float x1 = Random.Range(-100,100);
			float x2 = Random.Range(-100,100);
			float x3 = Random.Range(-100,100);
			//string what = x + ", " + y + ", " + z;			
			//Debug.Log (what);
			Vector3 whh = new Vector3(x1,x2,x3);
			whh.Normalize();
			ppy.rigidbody.AddForce(whh*2000);
		}
		Destroy (gameObject);
	}
}
