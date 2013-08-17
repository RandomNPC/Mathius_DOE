using System;
using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {
	
	enum State{INPUT,DISPLAY};
	
	public GUISkin thisMetalGUISkin;
	private int _score;
	private HighScoreManager _highScore;
	private HighScoreInitials _hsi;
	private State _state;
		
		
	void Start(){
		_score = MasterController.BRAIN.sm().get_score();
		_highScore = MasterController.BRAIN.hsm();
		if(_highScore.get_isHighScore()){//if it is a highscore!
			_hsi = MasterController.BRAIN.hsi();
			_hsi.onSucess_SwipedLeftToRight += new EventHandler(onSuccess_SwipedLeftToRight);
			_hsi.onSucess_SwipedRightToLeft += new EventHandler(onSuccess_SwipedRightToLeft);
			_state = State.INPUT;
		}else{
			_state = State.DISPLAY;
			_highScore.saveScores();
		}
		_highScore.set_isHighScore(false);
	}
	
	void OnGUI(){
		GUI.skin = thisMetalGUISkin;
		switch(_state){
			case State.INPUT:
				float intDivider = Screen.height/100;
				float widthDivider = Screen.width/100;
				//GUI.Label(new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(10*intDivider)), ("HIGHSCORE"),GUI.skin.GetStyle("label"));
				GUI.Label(new Rect((Screen.width/5),(15*intDivider),(3*(Screen.width/5)),(10*intDivider)), ("HIGHSCORE"),GUI.skin.GetStyle("box"));
				GUI.Label(new Rect((Screen.width/5),(27*intDivider),(3*(Screen.width/5)),(10*intDivider)), ("SCORE: " + _score),GUI.skin.GetStyle("label"));
				GUI.Label(new Rect((Screen.width/5),(45*intDivider),(3*(Screen.width/5)),(10*intDivider)), (_hsi.initials()),GUI.skin.GetStyle("box"));
				if(GUI.Button (new Rect((Screen.width/20) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("ENTER") ,GUI.skin.GetStyle("box") ) ){
					_highScore.hS().set_name(_hsi.initials());
					_highScore.saveScores();
					_state = State.DISPLAY;
				}
				if(GUI.Button(new Rect(widthDivider*30, intDivider*50,widthDivider*15, intDivider*5),("LEFT"),GUI.skin.GetStyle("button"))){
					_hsi.onSwipeRightToLeft();
				}
				if(GUI.Button(new Rect(widthDivider*60, intDivider*50,widthDivider*15, intDivider*5),("RIGHT"),GUI.skin.GetStyle("button"))){
					_hsi.onSwipeLeftToRight();
				}
				if(GUI.Button(new Rect(widthDivider*45, intDivider*45,widthDivider*15, intDivider*5),("UP"),GUI.skin.GetStyle("button"))){
					_hsi.onSwipeUp();
				}
				if(GUI.Button(new Rect(widthDivider*45, intDivider*55,widthDivider*15, intDivider*5),("DOWN"),GUI.skin.GetStyle("button"))){
					_hsi.onSwipeDown();
				}
			
				break;
			case State.DISPLAY:
				intDivider = Screen.height/100;
				GUI.Label(new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(10*intDivider)), ("GAMEOVER"),GUI.skin.GetStyle("label"));
				//GUI.Label(new Rect((Screen.width/5),(25*intDivider),(3*(Screen.width/5)),(18*intDivider)), ("GAMEOVER"),GUI.skin.GetStyle("box"));
				GUI.Label(new Rect((Screen.width/5),(47*intDivider),(3*(Screen.width/5)),(18*intDivider)), ("SCORE: " + _score),GUI.skin.GetStyle("label"));
				if(GUI.Button (new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
						Debug.Log("Mathius Clicked");
						Application.LoadLevel("MainMenu");}
				if(GUI.Button (new Rect((Screen.width/20) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Replay") ,GUI.skin.GetStyle("box") ) ){
						Debug.Log("Replay Clicked");
						Application.LoadLevel("Earth Scene");}
				break;
		}
		
	}

	private void onSuccess_SwipedLeftToRight(object sender, EventArgs e){
		
	}
	
	private void onSuccess_SwipedRightToLeft(object sender, EventArgs e){
		
	}
	
	
}