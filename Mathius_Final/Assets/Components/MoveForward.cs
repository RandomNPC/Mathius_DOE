using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(0.4f,0.0f,0.0f);
	}
}
