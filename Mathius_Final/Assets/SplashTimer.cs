using UnityEngine;
using System.Collections;

public class SplashTimer : MonoBehaviour {
	float myTimer;
	// Use this for initialization
	void Start () {
		myTimer = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(myTimer > 0){
			myTimer -= Time.deltaTime;
		}
		if(myTimer<= 0){
			Application.LoadLevel("MainMenu");
		}
		
		
	}
}
