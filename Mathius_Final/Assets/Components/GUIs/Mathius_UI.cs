using UnityEngine;
using System.Collections;

public class Mathius_UI : MonoBehaviour {
	
	private ScoreManager stats;
	public GUISkin thisMetalGUISkin;

	void Start(){
		stats = MasterController.BRAIN.sm();
	}
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		GUI.skin = thisMetalGUISkin;
		GUI.Label(new Rect((Screen.width/5)/3,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Lives: "+stats.get_lives()),GUI.skin.GetStyle("button"));
		GUI.Label(new Rect((Screen.width/5)/3,(6*intDivider),((Screen.width/5)),(18*intDivider)), ("Score: "+stats.get_score()),GUI.skin.GetStyle("button"));
		GUI.Label(new Rect((Screen.width/5)/3,(9*intDivider),((Screen.width/5)),(18*intDivider)), ("Streak: "+stats.get_streak()),GUI.skin.GetStyle("button"));
		GUI.Label (new Rect((Screen.width/25) ,(70*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Mathius Number: "+ stats.get_answer()) ,GUI.skin.GetStyle("window"));
		GUI.Label (new Rect((Screen.width/25) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Next: "+ stats.get_equation()) ,GUI.skin.GetStyle("window"));
		if(GUI.Button (new Rect(6*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Mathius Clicked");
				Application.LoadLevel("MainMenu");}
	}
}

