using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	// Use this for initialization
	void Start () {
	SoundManager.SOUNDS.playSound(SoundManager.PU_SHIELD,CameraCollider.MATHIUS_EARTH_CAM);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
