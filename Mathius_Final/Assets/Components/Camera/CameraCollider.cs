using UnityEngine;
using System.Collections;

public class CameraCollider : MonoBehaviour {
	
	private bool allocator_triggered;
	public static Vector3 MATHIUS_EARTH_CAM;
	private PreferencesManager _pref;
	private AudioSource[] _audio;
	
	void Start () {
		allocator_triggered = true;
		MasterController.BRAIN.onGameStart();
		MATHIUS_EARTH_CAM= gameObject.GetComponent<Transform>().position;
		_pref = MasterController.BRAIN.pm();
		_audio = gameObject.GetComponents<AudioSource>();
	}
	
	void Update () {
		MATHIUS_EARTH_CAM= gameObject.GetComponent<Transform>().position;
		if(allocator_triggered){
			MasterController.BRAIN.onTriggerNewTerrain();
			allocator_triggered = false;
		}
		_audio[0].volume = _pref.get_musicVolume()/100.0f;
		_audio[1].volume = _pref.get_SFXVolume()/100.0f;
	}
	
	void OnTriggerEnter(Collider obj){
		if(obj.name.Contains("Allocator")){
			allocator_triggered = true;
		}
		else if(obj.name.Contains("Transition")){
			MasterController.BRAIN.onTriggerTransition();
		}
	}
}
