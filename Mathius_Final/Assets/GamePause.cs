using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour {
	
	private bool gameDisabled;
	
	void Start(){
		gameDisabled = false;
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
	}
	
	public void ResumeGame(){
		GameObject[] obj = GameObject.FindGameObjectsWithTag("Pause");
		foreach(GameObject g in obj){
			GameObject inactive = g.transform.parent.gameObject;
			inactive.GetComponent<MoveForward>().enabled = true;
			if(inactive.name.Contains("Alian")){
				inactive.GetComponent<AlienMovement>().enabled = true;
			}
			if(inactive.name.Contains("MathiusEarthCam")){
				inactive.GetComponentInChildren<Player>().enabled = true;
				inactive.GetComponentInChildren<Gun>().enabled = true;
			}
			
		}
		
		obj = GameObject.FindGameObjectsWithTag("World");
		foreach(GameObject k in obj){
			k.GetComponent<SpawnAlien>().enabled = true;
		}
	}
}
