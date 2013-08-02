using UnityEngine;
using System.Collections;

public class SpawnAlien : MonoBehaviour {
	
	private bool start_spawn;
	private float timer;
	private Alien ai;
	
	void Start () {
		start_spawn = true;
		timer = 0;
		ai = MasterController.BRAIN.al();
	}
	
	void Update () {

		if(start_spawn){
			timer += Time.deltaTime;
			if (timer > ai.get_spawn_time())
			{
				ai.spawnAlien(gameObject);
				timer = 0;
			}
		}
	}
	
	public void setSpawn(bool spawn){start_spawn = spawn;}
}
