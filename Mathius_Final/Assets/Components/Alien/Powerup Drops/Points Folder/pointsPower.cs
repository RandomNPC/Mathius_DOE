using UnityEngine;
using System.Collections;

public class pointsPower : MonoBehaviour {

	void Start () {
		MasterController.BRAIN.sm().addBonusPoints(1000);
			
	}

}
