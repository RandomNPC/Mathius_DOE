using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {
	
	enum State{INPUT,DISPLAY};
	
	public GUISkin thisMetalGUISkin;
	private int _score;
	private HighScoreManager _highScore;
	private State _state;
	
		
		
	void Start(){

		_score = MasterController.BRAIN.sm().get_score();
		_highScore = MasterController.BRAIN.hsm();
		if(_highScore.get_isHighScore()){//if it is a highscore!
			_state = State.INPUT;
		}else{
			_state = State.DISPLAY;
		}
		_highScore.set_isHighScore(false);
		
	}
	
	void OnGUI(){
		
		switch(_state){
			case State.INPUT:
				print ("I GOT HIGHSCORE");
				_highScore.hS().set_name("BOB");
				_state = State.DISPLAY;
				break;
			case State.DISPLAY:
				print("DONE");
				break;
		}
		
		
		float intDivider = Screen.height/100;
		GUI.skin = thisMetalGUISkin;
		GUI.Label(new Rect((Screen.width/5)/2,(3*intDivider),(4*(Screen.width/5)),(18*intDivider)), ("Mathius: Defender of Earth!"),GUI.skin.GetStyle("label"));
		GUI.Label(new Rect((Screen.width/5),(25*intDivider),(3*(Screen.width/5)),(18*intDivider)), ("GAMEOVER"),GUI.skin.GetStyle("box"));
		GUI.Label(new Rect((Screen.width/5),(47*intDivider),(3*(Screen.width/5)),(18*intDivider)), ("SCORE: " + _score),GUI.skin.GetStyle("label"));
		if(GUI.Button (new Rect(5*(Screen.width/10) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Main Menu") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Mathius Clicked");
				Application.LoadLevel("MainMenu");}
		if(GUI.Button (new Rect((Screen.width/20) ,(90*intDivider) ,(4*(Screen.width/10)) ,(15*intDivider) ) ,("Replay") ,GUI.skin.GetStyle("box") ) ){
				Debug.Log("Replay Clicked");
				Application.LoadLevel("Earth Scene");}
	}
	
	
	
}