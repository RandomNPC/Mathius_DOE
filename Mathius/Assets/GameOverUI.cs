using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {
	public GUISkin thisMetalGUISkin;
	//private PaulScore gameData;
	void Start(){
		//gameData = GameObject.Find("MathiusEarthCam").GetComponent("PaulScore") as PaulScore;
		//print(gameData);
	}
	void OnGUI(){
		int score = 0;
		//(gameData.num_correct*100)-(gameData.num_wrong*10);
		float intDivider = Screen.height/100;
		GUI.skin = thisMetalGUISkin;
		GUI.Label(new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider)), ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/5),(25*intDivider),(3*(Screen.width/5)),(18*intDivider)), ("GAMEOVER"),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/5),(47*intDivider),(3*(Screen.width/5)),(18*intDivider)), ("SCORE: " + score),GUI.skin.GetStyle("label"));
		if(GUI.Button (new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Mathius Clicked");
				Application.LoadLevel("MainMenu");}
		if(GUI.Button (new Rect((Screen.width/20) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Replay") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Replay Clicked");
				Application.LoadLevel("Earth Scene");}
	}
}