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
	
	
	public TileManager(PreferencesManager pfm){
		_prev = null;
		_next = null;
		_pos = 0.0f;
		_tile = 0;
		_pfm = pfm;
		_map = null;
		_maps = null;
	}
	
	public void setTerrains(GameObject[] maplist){
		_maps = maplist;
	}
	
	public void generateNextTerrain(){
		_map = (_map==null) ? _maps[(int)Random.Range(0,_maps.Length-1)] : _map;
	
		GameObject _land = (GameObject) Instantiate(_map,new Vector3(_pos,0.0f,0.0f),Quaternion.identity);
		_land.name = "Surface " + _tile++;
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
		_next.GetComponent<MapPropertiesHelper>().onTransitionTrigger();
	}
	
	public void set_pos(float pos){_pos = pos;}
	public float get_pos(){return _pos;}
}
