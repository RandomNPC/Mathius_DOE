using UnityEngine;
using System.Collections;

public class TileManager : Object {
	
	private GameObject _prev;
	private GameObject _next;
	private float _pos;
	public int _tile;
	private GameObject[] _maps;
	private GameObject _map;
	private PreferencesManager _pfm;
	private string _prev_terrain;
	private string _next_terrain;
	private string _current_terrain;
	
	
	public TileManager(PreferencesManager pfm){
		_prev = null;
		_next = null;
		_pos = 0.0f;
		_tile = 0;
		_pfm = pfm;
		_map = null;
		_maps = null;
		_prev_terrain = "";
		_next_terrain = "";
		_current_terrain = "";
	}
	
	public void setTerrains(GameObject[] maplist){
		_maps = maplist;
	}
	
	public void generateNextTerrain(){
		_map = (_map==null) ? _maps[(int)Random.Range(0,_maps.Length-1)] : _map;
	
		GameObject _land = (GameObject) Instantiate(_map,new Vector3(_pos,0.0f,0.0f),Quaternion.identity);
		//insert name here
		_prev_terrain = _next_terrain;
		_next_terrain = _land.name;
		_land.name = "Surface " + _tile++;
		_land.GetComponent<TerrainCollider>().isTrigger = true;
		if(_tile.Equals(_pfm.get_tileNum())){//We reached the last set of land tiles, now setup a trigger
			MasterController.BRAIN.onReachedTargetTile();	
		}
		_land.AddComponent<MapPropertiesHelper>();
		_land.AddComponent<LandManager>();
		_land.AddComponent<SpawnAlien>();
		_pos += _land.GetComponent<Terrain>().terrainData.size.x/2;
		_map = _maps[Random.Range(0,_maps.Length-1)];
		_pos +=_map.GetComponent<Terrain>().terrainData.size.x/2;
			
		Destroy(_prev);
		_prev = _next;
		if(_prev!=null) _prev.GetComponent<SpawnAlien>().setSpawn(false);
		_next = _land;
	}
	
	public void triggerTransition(){
		_current_terrain = "Surface " + (_tile-1);
		_next.GetComponent<MapPropertiesHelper>().onTransitionTrigger();
	}
	
	public void set_pos(float pos){_pos = pos;}
	public float get_pos(){return _pos;}
	
	public string get_prev_terrain(){return _prev_terrain;}
	public string get_next_terrain(){return _next_terrain;}
	public string get_current_terrain(){return _current_terrain;}
	public void reset_terrain(){
		_prev_terrain = "";
		_next_terrain = "";
		_current_terrain = "";
	}
}
