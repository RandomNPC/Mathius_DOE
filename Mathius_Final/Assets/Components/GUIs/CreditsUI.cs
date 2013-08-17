using UnityEngine;
using System.Collections;

public class CreditsUI : MonoBehaviour {
	
	
	
	
	private int gs;
	private ScoreManager stats;
	public GUISkin thisMetalGUISkin;
	public static Mathius_UI MUI;
	private string derp;
	
	void Start(){
		stats = MasterController.BRAIN.sm();
		gs = 0;
		MUI = gameObject.GetComponent<Mathius_UI>();
		derp = "";
	}
	
	void OnGUI(){
		float intDivider = Screen.height/100;
		float widthDivider = Screen.width/100;
		Rect titleRect = new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider));
		GUI.skin = thisMetalGUISkin;
		if(GUI.Button (new Rect(8*(Screen.width/10) ,(95*intDivider) ,(2*(Screen.width/10)) ,(10*intDivider)) ,("NEXT") ,GUI.skin.GetStyle("box") ) ){
					gs++;
					derp += "noob";}
		switch(gs){
			case 0://Main Team
				print(gs + derp);
				GUI.Label(titleRect, ("Credits"),GUI.skin.GetStyle("label"));	
				GUI.Label(new Rect(widthDivider*15, intDivider* 22, widthDivider * 70, intDivider *20),"Senior Programmer/ Lead Tools Programmer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 25, widthDivider * 70, intDivider *10),"Paul Matias",GUI.skin.GetStyle("box"));
				
				GUI.Label(new Rect(widthDivider*15, intDivider* 36, widthDivider * 70, intDivider *20),"Lead Designer/ Lead UI Programmer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 39, widthDivider * 70, intDivider *10),"Maximilian Bolling",GUI.skin.GetStyle("box"));
				
				GUI.Label(new Rect(widthDivider*15, intDivider* 50, widthDivider * 70, intDivider *20),"Senior World Builder/ Designer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 53, widthDivider * 70, intDivider *10),"Thomas Bolling",GUI.skin.GetStyle("box"));
				
				GUI.Label(new Rect(widthDivider*15, intDivider* 64, widthDivider * 70, intDivider *20),"Lead Peripherial Programmer/ Designer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 67, widthDivider * 70, intDivider *10),"Michial Green II",GUI.skin.GetStyle("box"));
				
				GUI.Label(new Rect(widthDivider*15, intDivider* 78, widthDivider * 70, intDivider *20),"Art Lead/ Sound/ Design",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 81, widthDivider * 70, intDivider *10),"Hasani Groce",GUI.skin.GetStyle("box"));
				
				break;
			case 1://Hackathon Teams
				print(gs + derp);
				GUI.Label(titleRect, ("Credits"),GUI.skin.GetStyle("label"));	
				GUI.Label(new Rect(widthDivider*15, intDivider* 22, widthDivider * 70, intDivider *10),"Hackathon Team 1",GUI.skin.GetStyle("box"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 32, widthDivider * 70, intDivider *10),"Anthony Jones	Programmer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 35, widthDivider * 70, intDivider *10),"Hasani Groce	Programmer/Design",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 38, widthDivider * 70, intDivider *10),"Alejandro Ramierez	Programmer/Design",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 41, widthDivider * 70, intDivider *10),"Mike Smith	Design",GUI.skin.GetStyle("button"));
				
				GUI.Label(new Rect(widthDivider*15, intDivider* 45, widthDivider * 70, intDivider *10),"Super Computing",GUI.skin.GetStyle("box"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 55, widthDivider * 70, intDivider *10),"Michial Green II	Programmer/Design",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 58, widthDivider * 70, intDivider *10),"Alexis Liu	Programmer/Design",GUI.skin.GetStyle("button"));
				
				GUI.Label(new Rect(widthDivider*15, intDivider* 62, widthDivider * 70, intDivider *10),"Hackathon Team 2",GUI.skin.GetStyle("box"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 72, widthDivider * 70, intDivider *10),"Paul Matias		Programmer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 75, widthDivider * 70, intDivider *10),"Michial Green II	Programmer/Design",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 78, widthDivider * 70, intDivider *10),"Hasani Groce	Art/Design",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 81, widthDivider * 70, intDivider *10),"Thomas Bolling	Sound Design ",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 84, widthDivider * 70, intDivider *10),"Olabiyi Oyewumi 	Programmer",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 87, widthDivider * 70, intDivider *10),"Maximilian Bolling	UI Programmer/Design",GUI.skin.GetStyle("button"));	
				
				
				break;
				
			case 2://Resources
				GUI.Label(titleRect, ("Credits"),GUI.skin.GetStyle("label"));
				
				break;
			case 3://
				GUI.Label(titleRect, ("Credits"),GUI.skin.GetStyle("label"));
				
				break;
			case 4: //Tom Murphy
				GUI.Label(titleRect, ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));	
				
				GUI.Label(new Rect(widthDivider*15, intDivider* 50, widthDivider * 70, intDivider *20),"Special Thanks to our mentor and friend:",GUI.skin.GetStyle("button"));
				GUI.Label(new Rect(widthDivider*20, intDivider* 53, widthDivider * 70, intDivider *10),"Tom 'Wolf' Murphy",GUI.skin.GetStyle("box"));
				
				break;
			case 5:
				Application.LoadLevel("MainMenu");
				
				break;
		}
	}
	
	public void changeMenuState(int state){
		gs = state;
	}
}