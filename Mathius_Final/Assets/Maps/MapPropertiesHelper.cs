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
	
	public void onTransitionTrigger(){
		sources[0].loop = false;
		sources[1].loop = false;
		sources[0].mute = false;
		sources[1].mute = false;
		
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
					break;
			}
			
		}
	}
}
