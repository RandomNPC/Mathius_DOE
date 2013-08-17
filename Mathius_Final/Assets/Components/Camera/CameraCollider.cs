using UnityEngine;
using System.Collections;

public class CameraCollider : MonoBehaviour {
	
	private bool allocator_triggered;
	public static Vector3 MATHIUS_EARTH_CAM;

	void Start () {
		allocator_triggered = true;
		MasterController.BRAIN.onGameStart();
		MATHIUS_EARTH_CAM= gameObject.GetComponent<Transform>().position;
	}
	
	void Update () {
		MATHIUS_EARTH_CAM= gameObject.GetComponent<Transform>().position;
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
