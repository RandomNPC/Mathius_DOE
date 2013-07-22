using UnityEngine;
using System.Collections;

public class Mathius_UI : MonoBehaviour {
	
	private MasterController.Scoreboard stats;
	public GUISkin thisMetalGUISkin;

	void Start(){
		stats = GameObject.Find("Brain").GetComponent<MasterController>().mc_scoreboard();
	}
	
	void OnGUI(){
		string equation = ""; 
		float intDivider = Screen.height/100;
		GUI.skin = thisMetalGUISkin;
		GUI.Label(new Rect((Screen.width/5)/3,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Lives: "+stats.mathius_lives()),GUI.skin.GetStyle("button"));
		GUI.Label(new Rect((Screen.width/5)/3,(6*intDivider),((Screen.width/5)),(18*intDivider)), ("Score: "+((stats.correct()*100)-(stats.wrong()*10))),GUI.skin.GetStyle("button"));
		GUI.Label(new Rect((Screen.width/5)/3,(9*intDivider),((Screen.width/5)),(18*intDivider)), ("Streak: "+stats.streak()),GUI.skin.GetStyle("button"));
		GUI.Label (new Rect((Screen.width/25) ,(70*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Mathius Number: "+ stats.mathius_answer()) ,GUI.skin.GetStyle("window"));
		GUI.Label (new Rect((Screen.width/25) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Next: "+ equation) ,GUI.skin.GetStyle("window"));
		if(GUI.Button (new Rect(6*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Mathius Clicked");
				Application.LoadLevel("MainMenu");}
	}
}

