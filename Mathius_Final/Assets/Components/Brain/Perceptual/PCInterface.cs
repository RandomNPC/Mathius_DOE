using System;
using UnityEngine;
using System.Collections;

public enum GameState{
	MAINMENU,
	GAMEMENU,
	GAMEOVER,
	HIGHSCORE,
	LEVELEDITOR,
	UNDEFINED
}

public class PCInterface : MonoBehaviour{
		
	private bool _using_PCI;
	public event EventHandler<PCVoice> onVoiceCommand;
	public event EventHandler<PCGesture> onGesturePerformed;
	private GameState state;
	
	public bool[] stuff;
	
	public static PCInterface PC;
	
	void Start(){
		PC = gameObject.GetComponent<PCInterface>();
		state = GameState.GAMEMENU;
		_using_PCI = false;
	}
	
	void Update(){
		
	}

	public void set_using_PCI(bool state){_using_PCI = state;}
	public bool get_using_PCI(){return _using_PCI;}
	public void set_gameState(GameState current){state = current;}
}
