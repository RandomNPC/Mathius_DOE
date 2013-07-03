using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	public int score = 0;
	
	void Update () {
		if (score < 0) score = 0;	
	}
	
	void OnGUI () {
		GUI.Box (new Rect(10, 275, 70, 20), ("" + score));
	}
}
