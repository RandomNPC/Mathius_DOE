using UnityEngine;
using System.Collections;

public class SpawnAlien : MonoBehaviour {
	
	private bool start_spawn;
	private Alien ai;
	
	void Start () {
		start_spawn = true;
		ai = MasterController.BRAIN.al();
	}
	
	void Update () {
		if(start_spawn) ai.spawnAlien(gameObject);
	}
	
	public void setSpawn(bool spawn){start_spawn = spawn;}
}
