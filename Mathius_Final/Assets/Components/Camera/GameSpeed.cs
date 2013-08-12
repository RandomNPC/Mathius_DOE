using UnityEngine;
using System.Collections;

public class GameSpeed : MonoBehaviour {
	
	float _gameSpeed;
	const float MAX_SPEED = 1.0f;
	public static GameSpeed SPEED;
	
	void Start () {
		_gameSpeed = PlayerPrefs.GetFloat("_gameSpeed",0.5f);
		SPEED = gameObject.GetComponent<GameSpeed>();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Equals)){
			if(_gameSpeed<MAX_SPEED){
				_gameSpeed += 0.1f;
				PlayerPrefs.SetFloat("_gameSpeed",_gameSpeed);
			}
		}
		if(Input.GetKeyDown(KeyCode.Minus)){
			if(_gameSpeed>0){
				_gameSpeed -= 0.1f;
				PlayerPrefs.SetFloat("_gameSpeed",_gameSpeed);
			}
		}
	}
	
	public float get_gameSpeed(){return _gameSpeed;}
}
