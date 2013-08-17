using UnityEngine;
using System.Collections;

public class GoldPowerUP : MonoBehaviour {

void Start () {
		ScoreManager points = MasterController.BRAIN.sm();	
		points.addBonusPoints(1000);
	}
}
