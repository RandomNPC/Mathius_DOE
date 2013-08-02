using UnityEngine;
using System.Collections;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 

public class MapPropertiesHelper : MonoBehaviour {
	
	private bool spawnBuildings;
	private float z_axis;

	private bool start_transition;
	
	void Start () {
		draw_moveTrigger();
		spawnBuildings = gameObject.GetComponent<MapProperties>().use_buildings;
		z_axis = gameObject.GetComponent<MapProperties>().z_axis;
		start_transition = false;
	}
	
	void Update(){
		spawnBuildings = gameObject.GetComponent<MapProperties>().use_buildings;
		z_axis = gameObject.GetComponent<MapProperties>().z_axis;
		if(start_transition){
			float mpos_z = GameObject.Find ("Mathius").transform.position.z;
			
			if((int)mpos_z>(int)z_axis){
				GameObject.Find("MathiusEarthCam").transform.Translate(0.0f,0.0f,-1.0f);
			}else if((int)mpos_z<(int)z_axis){
				GameObject.Find("MathiusEarthCam").transform.Translate(0.0f,0.0f,1.0f);
			}else{
				start_transition = false;
				MasterController.BRAIN.al().set_on_transition(start_transition);
				Destroy(gameObject.GetComponent<MapPropertiesHelper>());
			}
		}
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
		start_transition = true;
		MasterController.BRAIN.al().set_on_transition(start_transition);
	}
	
	
}
