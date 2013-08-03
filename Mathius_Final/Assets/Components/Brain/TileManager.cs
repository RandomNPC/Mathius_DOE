using UnityEngine;
using System.Collections;

public class TileManager : Object {
	
	private GameObject _prev;
	private GameObject _next;
	private float _pos;
	public int _tile;
	private GameObject[] _maps;
	private GameObject _map;
	
	
	public TileManager(GameObject[] maplist){
		_prev = null;
		_next = null;
		_pos = 0.0f;
		_tile = 0;
		_map = null;
		_maps = maplist;
	}
	
	public void generateNextTerrain(){
		_map = (_map==null) ? _maps[Random.Range(0,_maps.Length-1)] : _map;
	
		GameObject _land = (GameObject) Instantiate(_map,new Vector3(_pos,0.0f,0.0f),Quaternion.identity);
		_land.name = "Surface " + _tile++;
		_land.AddComponent<LandManager>();
		_land.AddComponent<SpawnAlien>();
		_land.AddComponent<MapPropertiesHelper>();
		_pos += _land.GetComponent<Terrain>().terrainData.size.x/2;
		_map = _maps[Random.Range(0,_maps.Length-1)];
		_pos +=_map.GetComponent<Terrain>().terrainData.size.x/2;
			
		Destroy(_prev);
		_prev = _next;
		if(_prev!=null) _prev.GetComponent<SpawnAlien>().setSpawn(false);
		_next = _land;
	}
	
	public void triggerTransition(){
		_next.GetComponent<MapPropertiesHelper>().onTransitionTrigger();
	}
	
	public void set_pos(float pos){_pos = pos;}
	public float get_pos(){return _pos;}
}
