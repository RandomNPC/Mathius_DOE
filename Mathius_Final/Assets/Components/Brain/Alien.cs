using UnityEngine;
using System.Collections;

public class Alien : Object {
	
	private GameObject _alien;
	private float _spawn_time;
	private float _z_axis;
	private bool _on_transition;
	
	public Alien(GameObject alien){
		_alien = alien;
		_spawn_time = 3.0f;
		_z_axis = 150.0f;
		_on_transition = false;
	}
	
	public void spawnAlien(GameObject _land){
		if(_on_transition) return;
		Vector3 cam = GameObject.Find("MathiusEarthCam").transform.position;
		Vector3 _terrain = _land.GetComponent<Terrain>().terrainData.size;
		Vector3 _alien_dim = _alien.transform.Find("AlianBody").GetComponent<MeshFilter>().sharedMesh.bounds.size;
		float _delta = MasterController.BRAIN.m().get_bounds().y- _alien_dim.y/2;
		GameObject alien = (GameObject)Instantiate(_alien,
												   new Vector3(cam.x+_terrain.x/2,
															   Random.Range(cam.y+_alien_dim.y/2,cam.y+_delta), //cam.y -_delta clips thru the floor
															   _z_axis),
												   Quaternion.identity);
		alien.name = "Alian";
		alien.transform.parent = _land.transform;
	}
	
	public void set_spawn_time(float time){_spawn_time = time;}
	public float get_spawn_time(){return _spawn_time;}
	public void set_z_axis(float z){_z_axis = z;}
	
}
