using UnityEngine;
using System.Collections;

public class pointsPower : MonoBehaviour {

	void Start () {
		ScoreManager points = MasterController.BRAIN.sm();	
		points.addBonusPoints(250);
	}

}
