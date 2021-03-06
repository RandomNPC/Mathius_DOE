using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour {
	
	private bool gameDisabled;
	private bool gameEnd;
	
	public static GamePause PAUSE;
	
	void Awake(){
		gameDisabled = false;
		gameEnd = false;
		PAUSE = gameObject.GetComponent<GamePause>();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!gameDisabled){
				PauseGame();
				gameDisabled = true;
			}
			else{ 
				ResumeGame();
				gameDisabled = false;
			}
		}
	}
	
	
	
	public void PauseGame(){
		SoundManager.SOUNDS.setVolume(0.1f);
		SoundManager.SOUNDS.playSound(SoundManager.PAUSE,CameraCollider.MATHIUS_EARTH_CAM);
		GameObject[] obj = GameObject.FindGameObjectsWithTag("Pause");
		foreach(GameObject g in obj){
			GameObject active = g.transform.parent.gameObject;
			
			active.GetComponent<MoveForward>().enabled = false;
			if(active.name.Contains("Alian")){
				active.GetComponent<AlienMovement>().enabled = false;
			}
			if(active.name.Contains("MathiusEarthCam")){
				active.GetComponentInChildren<Player>().enabled = false;
				active.GetComponentInChildren<Gun>().enabled = false;
			}
		}
		
		obj = GameObject.FindGameObjectsWithTag("World");
		foreach(GameObject k in obj){
			k.GetComponent<SpawnAlien>().enabled = false;
		}
		Mathius_UI.MUI.changeMenuState(Mathius_UI.GAMESTATE.PAUSE);
	}
	
	public void ResumeGame(){
		//logic here before I unfreeze everything
		SoundManager.SOUNDS.setVolume(1.0f);
		SoundManager.SOUNDS.playSound(SoundManager.PAUSE,CameraCollider.MATHIUS_EARTH_CAM);
		
		GameObject[] obj = GameObject.FindGameObjectsWithTag("Pause");
		foreach(GameObject g in obj){
			GameObject inactive = g.transform.parent.gameObject;
			if(!gameEnd)inactive.GetComponent<MoveForward>().enabled = true;
			if(inactive.name.Contains("Alian")){
				inactive.GetComponent<AlienMovement>().enabled = true;
			}
			if(inactive.name.Contains("MathiusEarthCam")){
				inactive.GetComponentInChildren<Player>().enabled = true;
				if(!gameEnd)inactive.GetComponentInChildren<Gun>().enabled = true;
			}
			
		}
		Mathius_UI.MUI.changeMenuState(Mathius_UI.GAMESTATE.RESUME);
		obj = GameObject.FindGameObjectsWithTag("World");
		foreach(GameObject k in obj){
			if(!gameEnd)k.GetComponent<SpawnAlien>().enabled = true;
			if(gameEnd)k.GetComponentInChildren<BoxCollider>().enabled = false;
		}
		
	}
	
	public void set_gameEnd(bool status){gameEnd = status;}
}
