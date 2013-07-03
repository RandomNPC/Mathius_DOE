using UnityEngine;
using System.Collections;

public class CannonManager : MonoBehaviour {
	
	private GameObject [] cannons;
	private float timer;
	
	public GameObject FireBall;
	public int shotsPerSecond;
	
	// Use this for initialization
	void Start () {
		cannons = GameObject.FindGameObjectsWithTag("cannon");
		timer = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(Mathf.Floor(timer)>= (1/shotsPerSecond)){
			for(int i = 0; i<cannons.Length; i++){ 
				Instantiate(FireBall,cannons[i].transform.position,cannons[i].transform.rotation);
			}
		}
	}
}
