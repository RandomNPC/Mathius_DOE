using UnityEngine;
using System.Collections;

public class BronzePowerUP : MonoBehaviour {

	void Start () {
		ScoreManager points = MasterController.BRAIN.sm();	
		points.addBonusPoints(100);
	}
}
