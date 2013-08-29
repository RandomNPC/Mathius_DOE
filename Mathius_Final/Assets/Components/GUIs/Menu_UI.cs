using UnityEngine;
using System.Collections;

public class Menu_UI : MonoBehaviour {
	
	public GUISkin thisMetalGUISkin;
	private GUIManager gui;
	private PCInterface pc;
	
	private const string TITLE = "Mathius: Defender of Earth!";
	private const string START_GAME = "START GAME";
	private const string LEVEL_EDITOR = "LEVEL EDITOR";
	private const string OPTIONS = "OPTIONS";
	private const string HIGH_SCORE = "HIGH SCORE";
	private const string CREDITS = "CREDITS";
	private const string EXIT = "EXIT";
	
	void Start(){
		
		SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,MasterController.UI_MAIN_MENU);
		pc = MasterController.BRAIN.pci();
		gui = new GUIManager(thisMetalGUISkin);
		gui.OnClick += HandleGuiOnClick;
		pc.onGesturePerformed += HandlePconGesturePerformed;
		gui.CreateGUIObject(TITLE,
							"Mathius: Defender of Earth!",
							new Rect((Screen.width/5)/2,(3*(Screen.height/100)),(4*(Screen.width/5)),(18*(Screen.height/100))),
							GUIType.Label,
							"label");
		gui.CreateGUIObject(START_GAME,
							"START GAME",
							new Rect(Screen.width/3 ,(23*(Screen.height/100)) ,(2*(Screen.width/5)) ,(10*(Screen.height/100))),
							GUIType.Button,
							"box");
		gui.CreateGUIObject(LEVEL_EDITOR,
							"LEVEL EDITOR",
							new Rect((Screen.width/3),(35*(Screen.height/100)),(2*(Screen.width/5)),(10*(Screen.height/100))),
							GUIType.Button,
							"box");
		gui.CreateGUIObject(OPTIONS,
							"OPTIONS",
							new Rect((Screen.width/3),(46*(Screen.height/100)),(2*(Screen.width/5)),(10*(Screen.height/100))),
							GUIType.Button,
							"box");
		gui.CreateGUIObject(HIGH_SCORE,
							"HIGH SCORE",
							new Rect(Screen.width/3,(57*(Screen.height/100)), (2*(Screen.width/5)), (10*(Screen.height/100))),
							GUIType.Button,
							"box");
		gui.CreateGUIObject(CREDITS,
							"CREDITS",
							new Rect(Screen.width/3,(68*(Screen.height/100)),(2*(Screen.width/5)),(10*(Screen.height/100))),
							GUIType.Button,
							"box");
		gui.CreateGUIObject(EXIT,
							"EXIT",
							new Rect(Screen.width/3,(79*(Screen.height/100)), (2*(Screen.width/5)), (10*(Screen.height/100))),
							GUIType.Button,
							"box");
		
		gui.connect(START_GAME,null,null,EXIT,LEVEL_EDITOR);
		gui.connect(LEVEL_EDITOR,null,null,START_GAME,OPTIONS);
		gui.connect(OPTIONS,null,null,LEVEL_EDITOR,HIGH_SCORE);
		gui.connect(HIGH_SCORE,null,null,OPTIONS,CREDITS);
		gui.connect(CREDITS,null,null,HIGH_SCORE,EXIT);
		gui.connect(EXIT,null,null,CREDITS,START_GAME);
			
		gui.pointer = START_GAME;
	}

	void HandlePconGesturePerformed (object sender, PCGesture e)
	{
		switch(e.gesture){
			case Gesture.LEFT:
				gui.swipe(Direction.Left);
				break;
			case Gesture.RIGHT:
				gui.swipe(Direction.Right);
				break;
			case Gesture.UP:
				gui.swipe(Direction.Up);
				break;
			case Gesture.DOWN:
				gui.swipe(Direction.Down);
				break;
			case Gesture.SELECT:
				gui.selectOption(gui.pointer);
				break;
			default:
				break;
		}
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.W)){//up
			gui.swipe(Direction.Up);
		}
		if(Input.GetKeyDown(KeyCode.A)){//left
			gui.swipe(Direction.Left);
		}
		if(Input.GetKeyDown(KeyCode.S)){//down
			gui.swipe(Direction.Down);
		}
		if(Input.GetKeyDown(KeyCode.D)){//right
			gui.swipe(Direction.Right);			
		}
		if(Input.GetKeyDown(KeyCode.Return)){//select
			gui.selectOption(gui.pointer);
		}
	}
	
	void OnGUI () {
		gui.RenderGUIObjects(gui);
	}
	
	void HandleGuiOnClick (object sender, ButtonName e)
	{
		switch(e.name){
			case START_GAME:
				Application.LoadLevel("Earth Scene");
				break;
			case LEVEL_EDITOR:
				Application.LoadLevel("Settings");
				break;
			case OPTIONS:
				Application.LoadLevel("Options");
				break;
			case HIGH_SCORE:
				Application.LoadLevel("HighScores");
				break;
			case CREDITS:
				Application.LoadLevel("Credits");
				break;
			case EXIT:
				Application.Quit();
				break;
		}
		
	}
}