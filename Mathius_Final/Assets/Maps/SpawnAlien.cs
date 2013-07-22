using UnityEngine;
using System.Collections;

public class SpawnAlien : MonoBehaviour {

	private GameObject alien;
	private Vector3 dim_alien;
	private Vector3 landspace;
	private bool start_spawn;
	private float timer;
	private MasterController mc;
	
	void Start () {
		landspace = gameObject.GetComponent<Terrain>().terrainData.size;
		alien = (GameObject)Resources.Load ("Alian");
		start_spawn = true;
		timer = 0;
		
		mc = GameObject.Find("Brain").GetComponent<MasterController>();
		dim_alien = alien.transform.Find("AlianBody").GetComponent<MeshFilter>().sharedMesh.bounds.size;
		
		
	}
	
	// Update is called once per frame
	void Update () {

		float spawnTime = 3;
		if(start_spawn){
			timer += Time.deltaTime;
			if (timer > spawnTime)
			{
				GameObject alienObj = Instantiate(alien,new Vector3(
								Camera.main.transform.position.x+ landspace.x/2,
								Random.Range (Camera.main.transform.position.y+dim_alien.y/2,
											  Camera.main.transform.position.y+mc.getBounds().y-dim_alien.y/2),
											  344.0f), Quaternion.identity) as GameObject;
				alienObj.transform.parent = gameObject.transform;
				alienObj.name = "Alian";
				timer = 0;
			}
		}
	}
	
	public void setSpawn(bool spawn){start_spawn = spawn;}
}
