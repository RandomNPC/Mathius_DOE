using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	void Start () {
		MasterController.BRAIN.onGameEnd();
	}
}
