using UnityEngine;
using System.Collections;

public class CreditsUI : MonoBehaviour {
	
	public enum GAMESTATE{
		START,
		ONE,
		TWO,
		LAST
	}
	
	private GAMESTATE gs;
	private ScoreManager stats;
	public GUISkin thisMetalGUISkin;
	public static Mathius_UI MUI;
	
	void Start(){
		stats = MasterController.BRAIN.sm();
		gs = GAMESTATE.START;
		MUI = gameObject.GetComponent<Mathius_UI>();
	}
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		float widthDivider = Screen.width/100;
		Rect titleRect = new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider));
		GUI.skin = thisMetalGUISkin;
		switch(gs){
			case GAMESTATE.START:
				GUI.Label(titleRect, ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));	
				GUI.Label(new Rect(widthDivider*10, intDivider* 22, widthDivider * 70, intDivider *20),"Senior Programmer/ Lead Tools Programmer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*10, intDivider* 26, widthDivider * 60, intDivider *10),"Paul Matias",GUI.skin.GetStyle("box"));
				
				GUI.Label(new Rect(widthDivider*10, intDivider* 38, widthDivider * 70, intDivider *20),"Lead Designer/ Lead UI Programmer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*10, intDivider* 42, widthDivider * 60, intDivider *10),"Max Bolling",GUI.skin.GetStyle("box"));
				
				GUI.Label(new Rect(widthDivider*10, intDivider* 54, widthDivider * 70, intDivider *20),"Senior World Builder/ Designer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*10, intDivider* 58, widthDivider * 60, intDivider *10),"Tommy Bolling",GUI.skin.GetStyle("box"));
				
				GUI.Label(new Rect(widthDivider*10, intDivider* 70, widthDivider * 70, intDivider *20),"Lead Peripherial Programmer/ Designer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*10, intDivider* 74, widthDivider * 60, intDivider *10),"Michial Green III",GUI.skin.GetStyle("box"));
				
				GUI.Label(new Rect(widthDivider*10, intDivider* 86, widthDivider * 70, intDivider *20),"Art Lead/ Sound/ Design",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*10, intDivider* 90, widthDivider * 60, intDivider *10),"Hasani Groce",GUI.skin.GetStyle("box"));
				
				if(GUI.Button (new Rect(8*(Screen.width/10) ,(92*intDivider) ,(2*(Screen.width/10)) ,(10*intDivider)) ,("NEXT") ,GUI.skin.GetStyle("box") ) ){
					gs = GAMESTATE.LAST;}
				break;
			case GAMESTATE.LAST:
				GUI.Label(titleRect, ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));
				GUI.Label(new Rect((Screen.width/100)*70,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Lives: "+stats.get_lives()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/100)*40,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Score: "+stats.get_score()),GUI.skin.GetStyle("button"));
				GUI.Label(new Rect((Screen.width/100)*10,(3*intDivider),((Screen.width/5)),(18*intDivider)), ("Streak: "+stats.get_streak()),GUI.skin.GetStyle("button"));
				GUI.Label (new Rect((Screen.width/3) ,(75*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Mathius Number: "+ stats.get_answer()) ,GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(Screen.width/4,Screen.height/2,500,100),"Pause");
				if(GUI.Button (new Rect(5*(Screen.width/10) ,(95*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
					Debug.Log("Mathius Clicked");
					Application.LoadLevel("MainMenu");}
				break;
		}
	}
	
	public void changeMenuState(GAMESTATE state){
		gs = state;
	}
}