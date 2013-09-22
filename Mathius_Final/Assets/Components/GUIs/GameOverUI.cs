using System;
using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {
	
	enum State{
		INPUT,
		DISPLAY
	};
	
	enum InputInitial{
		ONE,
		TWO,
		THREE,
		NONE
	};
	
	public GUISkin thisMetalGUISkin;
	private int _score;
	private HighScoreManager _highScore;
	private HighScoreInitials _hsi;
	private State _state;
	private InputInitial _initial;
	private GUIManager guiInput;
	private GUIManager guiDisplay;
	private PCInterface pc;
	
	//Input state tags
	private const string HIGHSCORE = "Highscore";
	private const string SCORE = "Score";
	private const string INITIALS = "Initials";
	private const string ENTER = "Enter";
	private const string LEFT = "Left";
	private const string RIGHT = "Right";
	private const string UP1 = "Up1";
	private const string DOWN1 = "Down1";
	private const string UP2 = "Up2";
	private const string DOWN2 = "Down2";
	private const string UP3 = "Up3";
	private const string DOWN3 = "Down3";
	
	//Display state tags
	private const string MAINMENU = "Main Menu";
	private const string REPLAY = "Replay";
	private const string GAMEOVER = "Game Over";
	private const string FINAL_SCORE = "Final Score";
	
	
	void Start(){
		_initial = InputInitial.ONE;
		MasterController.BRAIN.onEnterMenu();
		pc = MasterController.BRAIN.pci();
		pc.onGesturePerformed += HandlePconGesturePerformed;
		_score = MasterController.BRAIN.sm().get_score();
		_highScore = MasterController.BRAIN.hsm();
		_hsi = MasterController.BRAIN.hsi();
		if(_highScore.get_isHighScore()){//if it is a highscore!
			_hsi.onSuccessSwipe += Handle_hsionSuccessSwipe;
			_state = State.INPUT;
		}else{
			_state = State.DISPLAY;
			_highScore.saveScores();
		}
		_highScore.set_isHighScore(false);
		
		//Now to the actual gui making!
		guiInput = new GUIManager(thisMetalGUISkin);
		guiDisplay = new GUIManager(thisMetalGUISkin);
		
		//Input
		guiInput.OnClick += HandleGuiInputOnClick;
		
		guiInput.CreateGUIObject(HIGHSCORE,
								 "HighScore",
								 new Rect((Screen.width/5),(15*(Screen.height/100)),(3*(Screen.width/5)),(10*(Screen.height/100))),
								 GUIType.Label,
								 "box");
		guiInput.CreateGUIObject(SCORE,
								 ("SCORE: " + _score),
								 new Rect((Screen.width/5),(27*(Screen.height/100)),(3*(Screen.width/5)),(10*(Screen.height/100))),
								 GUIType.Label,
								 "box");
		guiInput.CreateGUIObject(INITIALS,
								 (_hsi.initials()),
								 new Rect((Screen.width/5),(45*(Screen.height/100)),(3*(Screen.width/5)),(10*(Screen.height/100))),
								 GUIType.Label,
								 "box");
		
		//left most
		
		guiInput.CreateGUIObject(UP1,
								" /\\",
								new Rect((Screen.width/100)*45, (Screen.height/100)*45,(Screen.width/100)*2, (Screen.height/100)*5),
								GUIType.Button,
							 	"button",
								false,
								0.0f,
								0.0f,
								0.0f,
								false);
		guiInput.CreateGUIObject(DOWN1,
								 " \\/",
								 new Rect((Screen.width/100)*45, (Screen.height/100)*55,(Screen.width/100)*2, (Screen.height/100)*5),
								 GUIType.Button,
								 "button",
								 false,
								 0.0f,
								 0.0f,
								 0.0f,
								 false);
		//---end left most
		
		//center
		guiInput.CreateGUIObject(UP2,
								 " /\\",
								 new Rect((Screen.width/100)*50, (Screen.height/100)*45,(Screen.width/100)*2, (Screen.height/100)*5),
								 GUIType.Button,
								 "button",
								 false,
								 0.0f,
								 0.0f,
								 0.0f,
								 false);
		guiInput.CreateGUIObject(DOWN2,
								 " \\/",
								 new Rect((Screen.width/100)*50, (Screen.height/100)*55,(Screen.width/100)*2, (Screen.height/100)*5),
								 GUIType.Button,
								 "button",
								 false,
								 0.0f,
								 0.0f,
								 0.0f,
								 false);
		//---end center
		
		//far right
		guiInput.CreateGUIObject(UP3,//right up button
								 " /\\",
								 new Rect((Screen.width/100)*54, (Screen.height/100)*45,(Screen.width/100)*2, (Screen.height/100)*5),
								 GUIType.Button,
								 "button",
								 false,
								 0.0f,
								 0.0f,
								 0.0f,
								 false);
		guiInput.CreateGUIObject(DOWN3, //right down button
								 " \\/",
								 new Rect((Screen.width/100)*54, (Screen.height/100)*55,(Screen.width/100)*2, (Screen.height/100)*5),
								 GUIType.Button,
								 "button",
								 false,
								 0.0f,
								 0.0f,
								 0.0f,
								 false);
		
		//---end far right
		
		guiInput.CreateGUIObject(ENTER,
								 "ENTER",
								 new Rect((Screen.width/20) ,(90*(Screen.height/100)) ,(4*(Screen.width/10)) ,(15*(Screen.height/100))),
								 GUIType.Button,
								 "box");
		guiInput.CreateGUIObject(LEFT,
								 "<",
								 new Rect((Screen.width/100)*42, (Screen.height/100)*50,(Screen.width/100)*2, (Screen.height/100)*5),
								 GUIType.Button,
								 "button");
		guiInput.CreateGUIObject(RIGHT,
								 ">",
								 new Rect((Screen.width/100)*58, (Screen.height/100)*50,(Screen.width/100)*2, (Screen.height/100)*5),
								 GUIType.Button,
								 "button");

		//Display
		guiDisplay.OnClick += HandleGuiDisplayOnClick;
		
		guiDisplay.CreateGUIObject(MAINMENU,
								   "Main Menu",
								   new Rect(5*(Screen.width/10) ,(90*(Screen.height/100)) ,(4*(Screen.width/10)) ,(15*(Screen.height/100))),
								   GUIType.Button,
								   "box");
		guiDisplay.CreateGUIObject(REPLAY,
								   "Replay",
								   new Rect((Screen.width/20) ,(90*(Screen.height/100)) ,(4*(Screen.width/10)) ,(15*(Screen.height/100))),
								   GUIType.Button,
								   "box");
		guiDisplay.CreateGUIObject(GAMEOVER,
								   "GameOver",
								   new Rect((Screen.width/5)/2,(3*(Screen.height/100)),(4*(Screen.width/5)),(10*(Screen.height/100))),
								   GUIType.Label,
								   "label");
		guiDisplay.CreateGUIObject(FINAL_SCORE,
								   ("SCORE: " + _score),
								   new Rect((Screen.width/5),(47*(Screen.height/100)),(3*(Screen.width/5)),(18*(Screen.height/100))),
								   GUIType.Label,
								   "label");
		
		guiInput.connect(UP1,ENTER,UP2,DOWN1,DOWN1);
		guiInput.connect(UP2,UP1,UP3,DOWN2,DOWN2);
		guiInput.connect(UP3,UP2,ENTER,DOWN3,DOWN3);
		guiInput.connect(DOWN1,ENTER,DOWN2,UP1,UP1);
		guiInput.connect(DOWN2,DOWN1,DOWN3,UP2,UP2);
		guiInput.connect(DOWN3,DOWN2,ENTER,UP3,UP3);
		guiInput.connect(ENTER,UP3,UP1,UP1,UP1);
		
		guiInput.pointer = UP1;
		
		guiDisplay.connect(REPLAY,MAINMENU,MAINMENU,"","");
		guiDisplay.connect(MAINMENU,REPLAY,REPLAY,"","");
		guiDisplay.pointer = REPLAY;
		
	}


	void HandleGuiDisplayOnClick (object sender, ButtonName e)
	{
		switch(e.name){
			case MAINMENU:
				Application.LoadLevel("MainMenu");
				SoundManager.SOUNDS.playSound(SoundManager.UI_MUSIC_MAIN,MasterController.UI_CAMERA_ALT);
				SoundManager.SOUNDS.playSound(SoundManager.UI_MAIN_LOOP,MasterController.UI_CAMERA_ALT);
				break;
			case REPLAY:
				Application.LoadLevel("Earth Scene");
				break;
		}
	}

	void HandleGuiInputOnClick (object sender, ButtonName e)
	{
		switch(e.name){
			case ENTER:
				SoundManager.SOUNDS.playSound(SoundManager.UI_CLICK,MasterController.UI_CAMERA_ALT);
				_highScore.hS().set_name(_hsi.initials());
				_highScore.saveScores();
				_state = State.DISPLAY;
				break;
			case LEFT:
				_hsi.swipe(SwipeDirection.Left);
				break;
			case RIGHT:
				_hsi.swipe(SwipeDirection.Right);
				break;
			case UP1:
			case UP2:
			case UP3:
				_hsi.swipe(SwipeDirection.Up);
				guiInput.SetGUINameProperty(INITIALS,_hsi.initials());
				break;
			case DOWN1:
			case DOWN2:
			case DOWN3:
				_hsi.swipe(SwipeDirection.Down);
				guiInput.SetGUINameProperty(INITIALS,_hsi.initials());
				break;
			default:
				break;
		}
	}

	void Handle_hsionSuccessSwipe (object sender, Swipe e)
	{
		switch(e.swipe){
			case SwipeDirection.Left:
				break;
			case SwipeDirection.Right:
				break;
			case SwipeDirection.Up:
				break;
			case SwipeDirection.Down:
				break;
			default:
				break;
		}	
	}
	
	void OnGUI(){
		switch(_state){
			case State.DISPLAY:
				guiDisplay.RenderGUIObjects(guiDisplay);
				break;
			case State.INPUT:
				guiInput.RenderGUIObjects(guiInput);
				switch(_initial){
					case InputInitial.ONE:
						guiInput.SetGUIVisibleProperty(UP1,true);
						guiInput.SetGUIVisibleProperty(UP2,false);
						guiInput.SetGUIVisibleProperty(UP3,false);
						guiInput.SetGUIVisibleProperty(DOWN1,true);
						guiInput.SetGUIVisibleProperty(DOWN2,false);
						guiInput.SetGUIVisibleProperty(DOWN3,false);
						break;
					case InputInitial.TWO:
						guiInput.SetGUIVisibleProperty(UP1,false);
						guiInput.SetGUIVisibleProperty(UP2,true);
						guiInput.SetGUIVisibleProperty(UP3,false);
						guiInput.SetGUIVisibleProperty(DOWN1,false);
						guiInput.SetGUIVisibleProperty(DOWN2,true);
						guiInput.SetGUIVisibleProperty(DOWN3,false);
						break;
					case InputInitial.THREE:
						guiInput.SetGUIVisibleProperty(UP1,false);
						guiInput.SetGUIVisibleProperty(UP2,false);
						guiInput.SetGUIVisibleProperty(UP3,true);
						guiInput.SetGUIVisibleProperty(DOWN1,false);
						guiInput.SetGUIVisibleProperty(DOWN2,false);
						guiInput.SetGUIVisibleProperty(DOWN3,true);
						break;
					case InputInitial.NONE:
						guiInput.SetGUIVisibleProperty(UP1,false);
						guiInput.SetGUIVisibleProperty(UP2,false);
						guiInput.SetGUIVisibleProperty(UP3,false);
						guiInput.SetGUIVisibleProperty(DOWN1,false);
						guiInput.SetGUIVisibleProperty(DOWN2,false);
						guiInput.SetGUIVisibleProperty(DOWN3,false);
						break;
					default:
						break;
				}
				break;
			default:
				break;
		}
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.W)){//up
			switch(_state){
				case State.INPUT:
					guiInput.swipe(Direction.Up);
					break;
				case State.DISPLAY:
					guiDisplay.swipe(Direction.Up);
					break;
				default:
					break;
			}
		}
		if(Input.GetKeyDown(KeyCode.A)){//left
			switch(_state){
				case State.INPUT:
					guiInput.swipe(Direction.Left);
					if(_initial != InputInitial.NONE) _hsi.swipe(SwipeDirection.Left);
					break;
				case State.DISPLAY:
					guiDisplay.swipe(Direction.Left);
					break;
				default:
					break;
			}
		}
		if(Input.GetKeyDown(KeyCode.S)){//down
			switch(_state){
				case State.INPUT:
					guiInput.swipe(Direction.Down);
					break;
				case State.DISPLAY:
					guiDisplay.swipe(Direction.Down);
					break;
				default:
					break;
			}
		}
		if(Input.GetKeyDown(KeyCode.D)){//right
			switch(_state){
				case State.INPUT:
					guiInput.swipe(Direction.Right);
					if(_initial != InputInitial.NONE) _hsi.swipe(SwipeDirection.Right);
					break;
				case State.DISPLAY:
					guiDisplay.swipe(Direction.Right);
					break;
				default:
					break;
			}
		}
		if(Input.GetKeyDown(KeyCode.Return)){//select
			switch(_state){
				case State.INPUT:
					guiInput.selectOption(guiInput.pointer);
					break;
				case State.DISPLAY:
					guiDisplay.selectOption(guiDisplay.pointer);
					break;
				default:
					break;
			}
		}
		switch(guiInput.pointer){
			case UP1:
			case DOWN1:
				_initial = InputInitial.ONE;
				break;
			case UP2:
			case DOWN2:
				_initial = InputInitial.TWO;
				break;
			case UP3:
			case DOWN3:
				_initial = InputInitial.THREE;
				break;
			default:
				_initial = InputInitial.NONE;
				break;
		}
}

	void HandlePconGesturePerformed (object sender, PCGesture e)
	{
		switch(e.gesture){
			case Gesture.LEFT:
				switch(_state){
					case State.INPUT:
						guiInput.swipe(Direction.Left);
						break;
					case State.DISPLAY:
						guiDisplay.swipe(Direction.Left);
						break;
					default:
						break;
				}
				break;
			case Gesture.RIGHT:
				switch(_state){
					case State.INPUT:
						guiInput.swipe(Direction.Right);
						break;
					case State.DISPLAY:
						guiDisplay.swipe(Direction.Right);
						break;
					default:
						break;
				}
				break;
			case Gesture.UP:
				switch(_state){
					case State.INPUT:
						guiInput.swipe(Direction.Up);
						break;
					case State.DISPLAY:
						guiDisplay.swipe(Direction.Up);
						break;
					default:
						break;
				}
				break;
			case Gesture.DOWN:
				switch(_state){
					case State.INPUT:
						guiInput.swipe(Direction.Down);
						break;
					case State.DISPLAY:
						guiDisplay.swipe(Direction.Down);
						break;
					default:
						break;
				}
				break;
			case Gesture.SELECT:
				switch(_state){
					case State.INPUT:
						guiInput.selectOption(guiInput.pointer);
						break;
					case State.DISPLAY:
						guiDisplay.selectOption(guiDisplay.pointer);
						break;
					default:
						break;
				}
				break;
			default:
				break;
		}
	}
}