using UnityEngine;
using System.Collections;

public class GoldPowerUP : MonoBehaviour {

void Start () {
		SoundManager.SOUNDS.playSound(SoundManager.PU_GOLD,CameraCollider.MATHIUS_EARTH_CAM);
		ScoreManager points = MasterController.BRAIN.sm();	
		points.addBonusPoints(1000);
	}
}
