using System;
using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {
	
	enum State{
		INPUT,
		DISPLAY
	};
	
	public GUISkin thisMetalGUISkin;
	private int _score;
	private HighScoreManager _highScore;
	private HighScoreInitials _hsi;
	private State _state;
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
	private const string UP = "Up";
	private const string DOWN = "Down";
	
	//Display state tags
	private const string MAINMENU = "Main Menu";
	private const string REPLAY = "Replay";
	private const string GAMEOVER = "Game Over";
	private const string FINAL_SCORE = "Final Score";
	
	
	void Start(){
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
		guiInput.CreateGUIObject(ENTER,
								 "ENTER",
								 new Rect((Screen.width/20) ,(90*(Screen.height/100)) ,(4*(Screen.width/10)) ,(15*(Screen.height/100))),
								 GUIType.Button,
								 "box");
		guiInput.CreateGUIObject(LEFT,
								 "Left",
								 new Rect((Screen.width/100)*30, (Screen.height/100)*50,(Screen.width/100)*15, (Screen.height/100)*5),
								 GUIType.Button,
								 "button");
		guiInput.CreateGUIObject(RIGHT,
								 "Right",
								 new Rect((Screen.width/100)*60, (Screen.height/100)*50,(Screen.width/100)*15, (Screen.height/100)*5),
								 GUIType.Button,
								 "button");
		guiInput.CreateGUIObject(UP,
								 "Up",
								 new Rect((Screen.width/100)*45, (Screen.height/100)*45,(Screen.width/100)*15, (Screen.height/100)*5),
								 GUIType.Button,
								 "button");
		guiInput.CreateGUIObject(DOWN,
								 "Down",
								 new Rect((Screen.width/100)*45, (Screen.height/100)*55,(Screen.width/100)*15, (Screen.height/100)*5),
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
		
		guiInput.connect(LEFT,LEFT,RIGHT,UP,DOWN);
		guiInput.connect(UP,LEFT,RIGHT,UP,DOWN);
		guiInput.connect(RIGHT,LEFT,RIGHT,UP,DOWN);
		guiInput.connect(DOWN,LEFT,RIGHT,UP,ENTER);
		guiInput.connect(ENTER,"","",DOWN,UP);
		guiInput.pointer = LEFT;
		
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
			case UP:
				_hsi.swipe(SwipeDirection.Up);
				guiInput.SetGUINameProperty(INITIALS,_hsi.initials());
				break;
			case DOWN:
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