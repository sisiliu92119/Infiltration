using UnityEngine;
using System.Collections;

public class BombBar : MonoBehaviour {
	public Texture2D BombBarTexture;
	public Camera cam;
	private PlayerManager player;
	private Vector3 screenPoint;
	
	void OnGUI () {
		if (player && player.dropTimer > 0) {
			GUI.DrawTexture(new Rect(0, Screen.height - 50, Screen.width * (player.dropTime - player.dropTimer)/player.dropTime, 50), BombBarTexture, ScaleMode.StretchToFill, true, 0);
		}
	}
	
	public void setPlayer(GameObject p){
		player = p.GetComponent<PlayerManager>();
	}
}
