using UnityEngine;
using System.Collections;

public class CloneBar : MonoBehaviour {
	public Texture2D CloneBarTexture;
	public Camera cam;
	private PlayerManager player;
	private Vector3 screenPoint;
	
	void OnGUI () {
		if (player && player.cloneTimer < player.cloneTimeMax) {
			GUI.DrawTexture(new Rect(0, Screen.height - 50, Screen.width * player.cloneTimer/player.cloneTimeMax, 50), CloneBarTexture, ScaleMode.StretchToFill, true, 0);
		}
	}

	public void setPlayer(GameObject p){
		player = p.GetComponent<PlayerManager>();
	}
}
