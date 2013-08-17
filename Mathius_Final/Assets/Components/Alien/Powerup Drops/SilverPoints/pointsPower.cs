using UnityEngine;
using System.Collections;

public class pointsPower : MonoBehaviour {

	void Start () {
		SoundManager.SOUNDS.playSound(SoundManager.PU_SILVER,CameraCollider.MATHIUS_EARTH_CAM);
		ScoreManager points = MasterController.BRAIN.sm();	
		points.addBonusPoints(250);
	}

}
