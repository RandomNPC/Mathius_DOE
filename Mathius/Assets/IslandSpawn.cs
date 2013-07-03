using UnityEngine;
using System.Collections;

public class IslandSpawn : MonoBehaviour {
	
	private GameObject master;
	private GameObject parent;
	private static float length;
	private static float upper;
	private static float delta;
	
	private Vector3 dimensions;
	private Terrain terrain;
	
	
	void Start(){
		master = Resources.Load("wrak_budova") as GameObject;
		parent = gameObject;
		terrain = parent.GetComponent(typeof(Terrain)) as Terrain;
	
		dimensions = terrain.terrainData.size;
		
		float center = parent.transform.position.x+dimensions.x/2;
		float bounds = dimensions.x/2-100; //-100 cause of water area(dont want buildings in water)
		
		upper = center + bounds;
		float lower = center - bounds;
	 
		BoxCollider bc = master.GetComponent(typeof(BoxCollider)) as BoxCollider;
		length = bc.size.x;
		
		delta = Random.Range(1,200);	
		
		place_building(lower+(length/2));
	}
	
	void place_building(float lower){
		
		float rand = Random.Range (lower,lower+delta);

		GameObject building = Instantiate(master,new Vector3(rand,0.0f,dimensions.z/2),Quaternion.identity) as GameObject;
		
		building.transform.parent = parent.transform;
		building.name = "Building";
		delta = Random.Range(1,200);		

		if((rand+length+delta)<upper)place_building(rand+length); 
	}
	
}
