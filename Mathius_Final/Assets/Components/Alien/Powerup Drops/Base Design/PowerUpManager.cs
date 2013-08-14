using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

	public string powerUpScript;
	public Texture powerUpTexture;
	public float dropChance;
	
	void OnTriggerEnter(Collider obj){
		if(!obj.name.Contains("Mathius")) return;	
		//attach script to Mathius
		if(powerUpScript!=""){
			GameObject ship = GameObject.Find("Mathius") as GameObject;
			ship.AddComponent(powerUpScript);
		}
		Destroy(gameObject);
	}
	
	void Update(){
		gameObject.renderer.material.mainTexture = powerUpTexture;
	}
}
