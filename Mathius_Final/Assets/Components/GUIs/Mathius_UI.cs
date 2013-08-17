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
				GUI.Label(new Rect((Screen.width/100)*48,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Lives: "+stats.get_lives()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/100)*25,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Score: "+stats.get_score()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/100)*2,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Streak: "+stats.get_streak()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/100)*71,(3*intDivider),((Screen.width/4)),(18*intDivider)), ("Answers Left: "+ stats.get_problems_remaining()),GUI.skin.GetStyle("button"));
				GUI.Label (new Rect((Screen.width/3) ,(75*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Mathius Number: "+ stats.get_answer()) ,GUI.skin.GetStyle("button"));
				GUI.Label (new Rect((Screen.width/3) ,(80*intDivider) ,(4*(Screen.width/10)) ,(14*intDivider) ) ,("Next: "+ stats.get_equation()) ,GUI.skin.GetStyle("window"));
				if(GUI.Button (new Rect((Screen.width/3) ,(94*intDivider) ,(4*(Screen.width/10)) ,(10*intDivider) ) ,("Pause") ,GUI.skin.GetStyle("box") ) ){
					GameObject.Find("MathiusEarthCam").GetComponent<GamePause>().PauseGame();
					//changeMenuState(GAMESTATE.PAUSE);
			}
				break;
			case GAMESTATE.PAUSE:
				GUI.Label(new Rect((Screen.width/100)*70,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Lives: "+stats.get_lives()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/100)*40,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Score: "+stats.get_score()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/100)*10,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Streak: "+stats.get_streak()),GUI.skin.GetStyle("button"));
				GUI.Label (new Rect((Screen.width/3) ,(75*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Mathius Number: "+ stats.get_answer()) ,GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(Screen.width/4,Screen.height/2,500,100),"Pause");
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

