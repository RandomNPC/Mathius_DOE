using UnityEngine;
using System.Collections;

public class MapPropertiesHelper : MonoBehaviour {
	
	private bool spawnBuildings;
	
	public static MapPropertiesHelper SET;
	
	void Start () {
		SET = gameObject.GetComponent<MapPropertiesHelper>();
		draw_moveTrigger();
		spawnBuildings = gameObject.GetComponent<MapProperties>().use_buildings;
	}
	
	private void draw_moveTrigger(){
		GameObject go = new GameObject("Transition");
		go.transform.position = new Vector3(gameObject.transform.position.x,0.0f,gameObject.GetComponent<Terrain>().terrainData.size.z/2);
		
		BoxCollider bc = go.AddComponent<BoxCollider>();
		bc.size = new Vector3(10.0f,300.0f,gameObject.GetComponent<Terrain>().terrainData.size.z);
		bc.isTrigger = true;
		
		go.transform.parent = gameObject.transform;
	}
	
	public void onTransitionTrigger(){

	}
	
	public bool spawn_buildings(){return spawnBuildings;}
	
}
