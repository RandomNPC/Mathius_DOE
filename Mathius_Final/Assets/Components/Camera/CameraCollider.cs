using UnityEngine;
using System.Collections;

public class CameraCollider : MonoBehaviour {
	
	private bool allocator_triggered;

	void Start () {
		allocator_triggered = true;
		MasterController.BRAIN.onGameStart();
	}
	
	void Update () {
		if(allocator_triggered){
			MasterController.BRAIN.onTriggerNewTerrain();
			allocator_triggered = false;
		}
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
