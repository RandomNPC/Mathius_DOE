using UnityEngine;
using System.Collections;

public class LandManager : MonoBehaviour{

	private GameObject structure;
	
	
	void Start(){
		structure = Resources.Load("wrak_budova") as GameObject;
		
		Vector3 dim_land = gameObject.GetComponent<Terrain>().terrainData.size;
		Vector3 dim_building = structure.GetComponent<BoxCollider>().size;
		
		float center = gameObject.transform.position.x+dim_land.x/2;
		float bounds = dim_land.x/2-100; //-100 cause of water area(dont want buildings in water)
	
		place_building(center-bounds+(dim_building.x/2),center+bounds,Random.Range(1,200),dim_building.x,dim_land.z);
		add_allocator(dim_land);
	}
	
	private void place_building(float lower,float upper,float delta,float length,float width){
		
		float rand = Random.Range (lower,lower+delta);

		GameObject building = Instantiate(structure,new Vector3(rand,0.0f,width/2),Quaternion.identity) as GameObject;
		
		building.transform.parent = gameObject.transform;
		building.name = "Building";	
		delta = Random.Range(1,200);
		
		if((rand+length+delta)<upper)place_building(rand+length,upper,delta,length,width); 
	}


	
	private void add_allocator(Vector3 dim_land){
		GameObject allocator = new GameObject("Allocator");
		allocator.transform.position = new Vector3(gameObject.transform.position.x+dim_land.x/2,0.0f,dim_land.z/2);
		
		BoxCollider bc = allocator.AddComponent("BoxCollider") as BoxCollider;
		bc.size = new Vector3(10.0f,300.0f,dim_land.z);
		bc.isTrigger = true;
		
		bc.transform.parent = gameObject.transform;
	}

}