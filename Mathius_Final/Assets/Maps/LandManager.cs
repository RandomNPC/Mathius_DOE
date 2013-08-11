using UnityEngine;
using System.Collections;

public class LandManager : MonoBehaviour{

	private BuildingPlacer bp;
	
	void Start(){
		Vector3 dim = gameObject.GetComponent<Terrain>().terrainData.size;
		Object[] structure = Resources.LoadAll("wrak_budova",typeof(Object));
		if(MapPropertiesHelper.SET.spawn_buildings()){
			bp = new BuildingPlacer(structure,gameObject);
			bp.place_buildings_recursively();
		}
		add_allocator(dim);
	}
	
	private void add_allocator(Vector3 dim_land){
		GameObject allocator = new GameObject("Allocator");
		allocator.transform.position = new Vector3(gameObject.transform.position.x+dim_land.x/2,0.0f,dim_land.z/2);
		
		BoxCollider bc = allocator.AddComponent("BoxCollider") as BoxCollider;
		bc.size = new Vector3(10.0f,300.0f,dim_land.z);
		bc.isTrigger = true;
		
		bc.transform.parent = gameObject.transform;
	}
	
	private class BuildingPlacer : Object{
		
		private Object[] _buildings;
		private float _pos;
		private Vector3 _dim;
		private GameObject _land;
		
		public BuildingPlacer(Object[] structure,GameObject land){
			_buildings = structure;
			_pos = 100.0f;
			_dim = land.GetComponent<Terrain>().terrainData.size;
			_land = land;
		}
		
		public void place_buildings_recursively(){
			GameObject structure = (GameObject)_buildings[Random.Range(0,(_buildings.Length-1))];
			float length = structure.GetComponent<BoxCollider>().size.x;
			float delta = Random.Range(1,200);
			_pos += length/2;
			if(_pos >= (_dim.x-length-100))return;
			_pos = Random.Range(_pos,_pos+delta);
			GameObject s = (GameObject)Instantiate(structure,new Vector3(_pos+_land.transform.position.x,0.0f,_dim.z/2),Quaternion.identity);
			s.name = "Building";
			s.transform.parent = _land.transform;
			_pos += length/2;
			place_buildings_recursively();
		}
		
		
	}

}