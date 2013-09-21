using UnityEngine;
using System.Collections;

public class Alien : Object {
	
	private GameObject _alien;
	private float _z_axis;
	private float _spawn_distance;
	private bool _on_transition;
	private Vector3 _spawn_pos;
	private Vector3 _prev_spawn_pos;
	
	public Alien(GameObject alien){
		_alien = alien;
		_z_axis = 150.0f;
		_on_transition = false;
		_spawn_distance = 100.0f;
		_spawn_pos = new Vector3(0.0f,0.0f,0.0f);
		_prev_spawn_pos = new Vector3(0.0f,0.0f,0.0f);
	}
	
	public void spawnAlien(GameObject _land){
		if(_on_transition) return;
		Vector3 cam = GameObject.Find("MathiusEarthCam").transform.position;
		Vector3 _terrain = _land.GetComponent<Terrain>().terrainData.size;
		Vector3 _alien_dim = _alien.transform.Find("AlianBody").GetComponent<MeshFilter>().sharedMesh.bounds.size;
		float _delta = MasterController.BRAIN.m().get_bounds().y- _alien_dim.y/2;
		
		_spawn_pos.Set(cam.x+_terrain.x/2,Random.Range(cam.y+_alien_dim.y/2,cam.y+_delta), /*cam.y -_delta clips thru the floor*/_z_axis);
		
		if(Mathf.Abs((_spawn_pos-_prev_spawn_pos).x) >= _spawn_distance){
			_prev_spawn_pos = _spawn_pos;
			
			GameObject alien = (GameObject)Instantiate(_alien,new Vector3(_spawn_pos.x,_spawn_pos.y,_spawn_pos.z),Quaternion.identity);
			alien.name = "Alian";
			alien.transform.parent = _land.transform;
		}
	}
	
	public void set_z_axis(float z){_z_axis = z;}
	public void reset_postions(){
		_spawn_pos.Set(0.0f,0.0f,0.0f);
		_prev_spawn_pos.Set(0.0f,0.0f,0.0f);
	}
	public void set_spawn_distance(float distance){_spawn_distance = distance;}
	public float get_spawn_distance(){return _spawn_distance;}
	
}
