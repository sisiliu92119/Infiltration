using UnityEngine;
using System.Collections;

public class shooting : MonoBehaviour {
	public GameObject piece;
	public GameObject explodeEffect;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump")) 
		{
			explode();
			Instantiate(explodeEffect, gameObject.transform.position, gameObject.transform.rotation);
		}

	}

	void explode()
	{
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
	}
}
