using UnityEngine;
using System.Collections;

public class EquationDisplay : MonoBehaviour {
	
	//public Alian target;
	
	string displayEq = "Offline";
	string displayAn = "Offline";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject target = GameObject.FindWithTag("TargetedShip");
		if (target != null)
		{
			displayEq = target.GetComponent<Alian>().equation;
			displayAn = target.GetComponent<Alian>().variable;
		}
			
	}
	
	void OnGUI () {
		GUI.Box (new Rect(400, 10, 100, 90), displayEq);
		GUI.Box (new Rect(500, 10, 100, 90), displayAn);
	}
}
