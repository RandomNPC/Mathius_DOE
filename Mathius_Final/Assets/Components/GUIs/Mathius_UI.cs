using UnityEngine;
using System.Collections;

public class Mathius_UI : MonoBehaviour {
	
	public enum GAMESTATE{
		RESUME,
		PAUSE
	}
	
	
	private GAMESTATE gs;
	private ScoreManager stats;
	public GUISkin thisMetalGUISkin;
	public static Mathius_UI MUI;
	
	void Start(){
		stats = MasterController.BRAIN.sm();
		gs = GAMESTATE.RESUME;
		MUI = gameObject.GetComponent<Mathius_UI>();
	}
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		GUI.skin = thisMetalGUISkin;
		switch(gs){
			case GAMESTATE.RESUME:
				GUI.Label(new Rect((Screen.width/5)/3,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Lives: "+stats.get_lives()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/5)/3,(6*intDivider),((Screen.width/5)),(18*intDivider)), ("Score: "+stats.get_score()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/5)/3,(9*intDivider),((Screen.width/5)),(18*intDivider)), ("Streak: "+stats.get_streak()),GUI.skin.GetStyle("button"));
				GUI.Label (new Rect((Screen.width/25) ,(70*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Mathius Number: "+ stats.get_answer()) ,GUI.skin.GetStyle("window"));
				GUI.Label (new Rect((Screen.width/25) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Next: "+ stats.get_equation()) ,GUI.skin.GetStyle("window"));
				break;
			case GAMESTATE.PAUSE:
				if(GUI.Button (new Rect(6*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
					Debug.Log("Mathius Clicked");
					Application.LoadLevel("MainMenu");}
				break;
		}
	}
	
	public void changeMenuState(GAMESTATE state){
		gs = state;
	}
}

