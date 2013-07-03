using UnityEngine;
using System.Collections;

public class SelfDestructTimer : MonoBehaviour {
	private float MAX_TIME;
	private float elapsed;
	
	// Use this for initialization
	void Start () {
		MAX_TIME = 75.0f;
		elapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
	elapsed += Time.deltaTime;
		if(elapsed >= MAX_TIME){Destroy(gameObject);}
	}
}
