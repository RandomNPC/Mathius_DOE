using UnityEngine;
using System.Collections;

public class MapPropertiesHelper : MonoBehaviour {
	
	private bool spawnBuildings;
	private GameObject[] sounds;
	private AudioSource[] sources;
	
	public static MapPropertiesHelper SET;
	
	void Start () {
		SET = gameObject.GetComponent<MapPropertiesHelper>();
		draw_moveTrigger();
		spawnBuildings = gameObject.GetComponent<MapProperties>().use_buildings;
		sounds = gameObject.GetComponent<MapProperties>().sounds;
		sources = GameObject.Find("MathiusEarthCam").GetComponents<AudioSource>(); //0 = music, 1 = sound
	}
	
	private void draw_moveTrigger(){
		GameObject go = new GameObject("Transition");
		go.transform.position = new Vector3(gameObject.transform.position.x,0.0f,gameObject.GetComponent<Terrain>().terrainData.size.z/2);
		
		BoxCollider bc = go.AddComponent<BoxCollider>();
		bc.size = new Vector3(10.0f,300.0f,gameObject.GetComponent<Terrain>().terrainData.size.z);
		bc.isTrigger = true;
		
		go.transform.parent = gameObject.transform;
	}
	
	public bool spawn_buildings(){return spawnBuildings;}
	
	private void play_sounds(){
		sources[0].loop = false;
		sources[1].loop = false;
		sources[0].mute = false;
		sources[1].mute = false;
		sources[0].Stop();
		sources[1].Stop();
		
		foreach(GameObject source in sounds){
			SoundMode sMode = source.GetComponent<SoundProfile>().mode;
			SoundType sType = source.GetComponent<SoundProfile>().type;
			AudioClip clip = source.GetComponent<SoundProfile>().sound;
			
			switch(sType){
				case SoundType.MUSIC:
					sources[0].clip = clip;
					switch(sMode){
						case SoundMode.LOOP:
							sources[0].loop = true;
							break;
						case SoundMode.MUTE:
							sources[0].mute = true;
							break;
						default:
							break;
					}
					sources[0].Play();
					break;
				case SoundType.SFX:
					sources[1].clip = clip;
					switch(sMode){
						case SoundMode.LOOP:
							sources[1].mute = true;
							break;
						case SoundMode.MUTE:
							sources[1].mute = true;
							break;
						default:
							break;
					}
					sources[1].Play();
					break;
			}
			
		}
	}
	
	private void render_skybox(){
		switch(gameObject.GetComponent<MapProperties>().mode){
			case SkyboxMode.DAYNIGHT:
				MasterController.BRAIN.sbm().mapSkyBox(MasterController.BRAIN.tm().get_next_terrain());
				break;
			case SkyboxMode.CONSTANT:
				GameObject.Find("MathiusEarthCam").GetComponent<Skybox>().material = gameObject.GetComponent<MapProperties>().constant_skybox;
				break;
		}
	}
	
	public void onTransitionTrigger(){
		TileManager tm = MasterController.BRAIN.tm();
		if(!tm.get_next_terrain().Equals(tm.get_prev_terrain())) play_sounds();
		render_skybox();
	}
}
