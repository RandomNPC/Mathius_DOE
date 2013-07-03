using UnityEngine;
using System.Collections;

public class meteor : MonoBehaviour {

	public float speed = 4;
	public float spin = 2;	
	
	// Use this for initialization
	void Start () {
		//speed = Random.Range((1*Time.deltaTime), (8*Time.deltaTime));
		speed = 15;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(-speed, 0, 0);
		if(transform.position.x < -400){Destroy(gameObject);}
	}
	

}
