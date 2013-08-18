using UnityEngine;
using System.Collections;

public class BronzePowerUP : MonoBehaviour {

	void Start () {
		SoundManager.SOUNDS.playSound(SoundManager.PU_BRONZE,CameraCollider.MATHIUS_EARTH_CAM);
		ScoreManager points = MasterController.BRAIN.sm();	
		points.addBonusPoints(100);
	}
}
