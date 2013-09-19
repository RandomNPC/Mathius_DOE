using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {
	
	void Start () {
		
		Mathius mHelper = MasterController.BRAIN.m();
		SoundManager.SOUNDS.playSound(SoundManager.PU_SPIKE,CameraCollider.MATHIUS_EARTH_CAM);
		gameObject.GetComponent<Player>().destroy_mathius();
		mHelper.set_lives(mHelper.get_lives()-1);
		Vector3 cam = GameObject.Find("MathiusEarthCam").transform.position;
		mHelper.spawn_mathius(cam.x,cam.y+15.0f,cam.z+100.0f);
	}

}
