using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour {

	void Start () {
		SoundManager.SOUNDS.playSound(SoundManager.PU_LIFE,CameraCollider.MATHIUS_EARTH_CAM);
		Mathius life = MasterController.BRAIN.m();
		life.set_lives(life.get_lives()+1);
	}

}
