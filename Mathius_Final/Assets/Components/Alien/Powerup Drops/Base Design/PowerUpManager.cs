using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

	public string powerUpScript;
	public float dropChance;
	
	void OnTriggerEnter(Collider obj){
		if(!obj.name.Contains("Mathius")) return;	
		if(powerUpScript!="")obj.gameObject.AddComponent(powerUpScript);
		Destroy(gameObject);
	}
}
