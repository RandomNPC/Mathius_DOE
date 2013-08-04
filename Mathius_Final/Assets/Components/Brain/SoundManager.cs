using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
	
	public AudioClip[] sounds;
	private Dictionary<string,AudioClip> audioMapping;
	private float audioVolume;
	
	public static Vector3 PLAY_ON_GAMEOBJECT;
	public static SoundManager SOUNDS;
	
	void Start(){
		PLAY_ON_GAMEOBJECT = gameObject.transform.position;
		SOUNDS = gameObject.GetComponent<SoundManager>();
		audioVolume = 1.0f;
		audioMapping = new Dictionary<string, AudioClip>();
		audioMapping.Clear();
		foreach(AudioClip ac in sounds){
			audioMapping.Add(ac.name,ac);
		}
	}
	
	void Update(){
		 PLAY_ON_GAMEOBJECT = gameObject.transform.position;
	}
	
	public void setVolume(float volume){ audioVolume = volume;}
	
	public void playSound(string name, Vector3 position){
		AudioClip playclip = null;
		try{
	 		playclip = audioMapping[name];
		}
		catch{
			print ("AudioClip " + name + " does not exist");
			return;
		}
		AudioSource.PlayClipAtPoint(playclip,position,audioVolume);
	}
}
