using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
	
	public AudioClip[] sounds;
	private Dictionary<string,AudioClip> audioMapping;
	private float audioVolume;
	
	public static Vector3 PLAY_ON_GAMEOBJECT;
	public static SoundManager SOUNDS;
	public const string MATHIUS_EXPLOSION ="NearExplosionB";
	public const string SFX_RIGHT_NUM_HIT ="sfx right num hit";
	public const string UI_CLICK ="UI click";
	public const string UI_MAIN_LOOP ="UI music  loop";
	public const string UI_GAME_OVER_LOOP ="UI game over music loop";
	public const string SFX_WRONG_NUM_HIT ="sfx wrong num hit";
	public const string SFX_LIFTOFF_FOREST ="sfx liftoff forest and others maybe";
	public const string SFX_LEVEL_COMPLETE ="sfx level completed";
	public const string SFX_JFK_MOON ="sfx jfk moon";
	public const string SFX_CREDITS ="sfx high score speech";
	public const string SFX_HIGH_SCORE_SPEECH ="sfx high score niel armstrong";
	public const string SFX_GAME_OVER ="sfx gameover huston we have a problem";
	public const string SFX_EARTH_ROGER ="sfx earth roger lift off";
	public const string SFX_CAPSULE ="sfx capsule";
	public const string PU_SPIKE ="PU spike";
	public const string PU_SILVER ="PU silver";
	public const string PU_SHIELD ="PU shield";
	public const string PU_MISSILE ="PU Missile";
	public const string PU_LIFE ="PU life";
	public const string PU_GOLD ="PU gold";
	public const string PU_BRONZE ="PU Bronze";
	public const string PAUSE ="pause";
	public const string MATH_CRASH ="MathCrash";
	public const string LEVEL_VOLCANO ="level volcano";
	public const string LEVEL_SNOW ="level snow";
	public const string LEVEL_MOON ="level moon";
	public const string LEVEL_FOREST ="level forest";
	public const string LEVEL_EARTH ="level earth";
	public const string LEVEL_DESERT ="level desert";
	public const string UI_MUSIC_MAIN ="UI music main";
	public const string UI_LR_buttons ="UI left right buttons";
	public const string SFX_SHOOT_NUM ="shoot num";
	public const string LEVEL_ZACK ="level zack";
	
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
