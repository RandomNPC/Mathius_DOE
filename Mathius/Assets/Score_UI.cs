using UnityEngine;
using System.Collections;



public class Score_ui : MonoBehaviour {
	public Texture2D icon;
	public int Score = 0;
	public int Lives = 5;
	public GUISkin thisMetalGUISkin;

	void OnGUI () {
		GUI.skin = thisMetalGUISkin;
		GUI.Box (new Rect(10,55, 200, 50), ("Score: " + Mathf.Round(Score)),GUI.skin.GetStyle("box"));
	
		GUI.Box (new Rect(10,115, 200, 50), ("Lives: " + Mathf.Round(Lives)),GUI.skin.GetStyle("box"));
	}
	
	void Update () {
		if(Input.GetButtonUp("Fire1")){
			Score += 100;
		}
		if(Input.GetButtonUp("Fire2")){
			Score -= 100;
		}
		
		
	}
}