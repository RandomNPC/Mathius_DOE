using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

	// Use this for initialization
	void Start () {
	SoundManager.SOUNDS.playSound(SoundManager.PU_SPIKE,CameraCollider.MATHIUS_EARTH_CAM);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
