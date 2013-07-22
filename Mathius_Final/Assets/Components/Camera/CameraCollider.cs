using UnityEngine;
using System.Collections;

public class CameraCollider : MonoBehaviour {
	
	private bool allocator_triggered;
	private MasterController mc;
	
	void Start () {
		allocator_triggered = true;
		mc = GameObject.Find("Brain").GetComponent("MasterController") as MasterController;
		mc.onGameStart();
	}
	
	void Update () {
		if(allocator_triggered){
			mc.onTriggerNewTerrain();
			allocator_triggered = false;
		}
	}
	
	void OnTriggerEnter(Collider obj){
		if(obj.name.Contains("Allocator")){
			allocator_triggered = true;
		}
	}
}
