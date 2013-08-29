using UnityEngine;
using System.Collections;

public class HighScoreUI : MonoBehaviour {
	
	public GUISkin thisMetalGUISkin;
	private GUIManager gui;
	
	private const string HIGH_SCORES = "High Scores";
	private const string PLAYER1 = "1";
	private const string PLAYER2 = "2";
	private const string PLAYER3 = "3";
	private const string PLAYER4 = "4";
	private const string PLAYER5 = "5";
	private const string PLAYER6 = "6";
	private const string PLAYER7 = "7";
	private const string PLAYER8 = "8";
	private const string PLAYER9 = "9";
	private const string PLAYER10 = "10";
	private const string MAINMENU = "Main Menu";
	
	void Start(){
		SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,MasterController.UI_CAMERA_ALT);
		gui = new GUIManager(thisMetalGUISkin);
		gui.OnClick += HandleGuiOnClick;
		
		float intDivider = Screen.height/100;
		
		gui.CreateGUIObject(HIGH_SCORES,
							"High Scores",
							new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider)),
							GUIType.Label,
							"label");
		gui.CreateGUIObject(PLAYER1,
							("1:\t\t\t" + PlayerPrefs.GetInt("Player H0") +" "+ PlayerPrefs.GetString("Player 0","A")),
							new Rect((Screen.width/55),(35*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER2,
							("2:\t\t\t" + PlayerPrefs.GetInt("Player H1",1)+" "+ PlayerPrefs.GetString("Player 1","B")),
							new Rect((Screen.width/55),(45*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER3,
							("3:\t\t\t" + PlayerPrefs.GetInt("Player H2",2) +" "+ PlayerPrefs.GetString("Player 2","C")),
							new Rect((Screen.width/55),(55*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER4,
							("4:\t\t\t" + PlayerPrefs.GetInt("Player H3",3)+" "+ PlayerPrefs.GetString("Player 3","D")),
							new Rect((Screen.width/55),(65*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER5,
							("5:\t\t\t" + PlayerPrefs.GetInt("Player H4",4) +" "+ PlayerPrefs.GetString("Player 4","E")),
							new Rect((Screen.width/55),(75*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER6,
							("6:\t\t\t" + PlayerPrefs.GetInt("Player H5",5)+" "+ PlayerPrefs.GetString("Player 5","F")),
							new Rect((Screen.width/10)*5,(35*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER7,
							("7:\t\t\t" + PlayerPrefs.GetInt("Player H6",6) +" "+ PlayerPrefs.GetString("Player 6","G")),
							new Rect((Screen.width/10)*5,(45*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER8,
							("8:\t\t\t" + PlayerPrefs.GetInt("Player H7",7)+" "+ PlayerPrefs.GetString("Player 7","H")),
							new Rect((Screen.width/10)*5,(55*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER9,
							("9:\t\t\t" + PlayerPrefs.GetInt("Player H8",8) +" "+ PlayerPrefs.GetString("Player 8","I")),
							new Rect((Screen.width/10)*5,(65*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(PLAYER10,
							("10:\t\t\t" + PlayerPrefs.GetInt("Player H9",9)+" "+ PlayerPrefs.GetString("Player 9","J")),
							new Rect((Screen.width/10)*5,(75*intDivider),(50*(Screen.width/100)),(9*intDivider)),
							GUIType.Label,
							"box");
		gui.CreateGUIObject(MAINMENU,
							"Main Menu",
							new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ),
							GUIType.Button,
							"box");
		
		gui.connect(MAINMENU,MAINMENU,MAINMENU,MAINMENU,MAINMENU);
		gui.pointer = MAINMENU;
	}
	
	void OnGUI(){
		gui.RenderGUIObjects(gui);
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.W)){
			gui.swipe(Direction.Up);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			gui.swipe(Direction.Left);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			gui.swipe(Direction.Down);
		}
		if(Input.GetKeyDown(KeyCode.D)){
			gui.swipe(Direction.Right);			
		}
		if(Input.GetKeyDown(KeyCode.Return)){
			gui.selectOption(gui.pointer);
		}
	}
	
	void HandleGuiOnClick (object sender, ButtonName e)
	{
		switch(e.name){
			case MAINMENU:
				Application.LoadLevel("MainMenu");
				break;
			default:
				break;
		}
	}
	
	
}

