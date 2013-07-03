using UnityEngine;
using System.Collections;

public class SpawnAlien : MonoBehaviour {

	private GameObject alien;
	
	private Terrain master;
	private Vector3 dimensions;
	public bool start_spawn;
	private float timer;
	private Camera cam;
	
	// Use this for initialization
	void Start () {
		master = gameObject.GetComponent(typeof(Terrain)) as Terrain;
		dimensions = master.terrainData.size;
		alien = Resources.Load ("Alian") as GameObject;
		cam = Camera.main;
	/*	
		GameObject ts = new GameObject("Spawn_Trigger");
		BoxCollider spawner = ts.AddComponent("BoxCollider") as BoxCollider;
		ts.transform.position = new Vector3(gameObject.transform.position.x+dimensions.x/4,0.0f,100.0f+dimensions.z/2);
		spawner.size = new Vector3(10.0f,300.0f,dimensions.z);
		spawner.isTrigger = true;
		ts.transform.parent = gameObject.transform;
		
		GameObject ds = new GameObject("Spawn_DeTrigger");
		BoxCollider stop_spawning = ds.AddComponent("BoxCollider") as BoxCollider;
		ds.transform.position = new Vector3(gameObject.transform.position.x+dimensions.x*3/4,0.0f,100.0f+dimensions.z/2);
		stop_spawning.size = new Vector3(10.0f,300.0f,dimensions.z);
		stop_spawning.isTrigger = true;
		ds.transform.parent = gameObject.transform;
		*/
		start_spawn = true;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {

		float spawnTime = 3;
		if(start_spawn){
			timer += Time.deltaTime;
			if (timer > spawnTime)
			{
					
				GameObject alienObj = Instantiate(alien,new Vector3(
								 cam.transform.position.x+ dimensions.x/2,
								Random.Range (17.0f,50.0f),
								344.0f), Quaternion.identity) as GameObject;
				alienObj.transform.parent = gameObject.transform;
				timer = 0;
			}
		}
	}
}
