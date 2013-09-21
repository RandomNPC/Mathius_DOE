using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	 GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alian");
		foreach(GameObject alien in aliens){Destroy(alien);}
	 GameObject[] spikes = GameObject.FindGameObjectsWithTag("spike");
		foreach(GameObject spike in spikes){Destroy(spike);}
		
	}

}
