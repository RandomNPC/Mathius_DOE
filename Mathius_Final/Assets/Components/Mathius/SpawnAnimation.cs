using UnityEngine;
using System.Collections;

public class SpawnAnimation : MonoBehaviour {
	
	private float animate;
	
	// Use this for initialization
	void Start () {
		animate = 60.0f;
		gameObject.GetComponent<BoxCollider>().enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(animate >= 0.0f){
			if(gameObject.transform.Find("MathiusModel").GetComponent<MeshRenderer>().isVisible){
				gameObject.transform.Find("MathiusModel").GetComponent<MeshRenderer>().enabled = false;
			} else gameObject.transform.Find("MathiusModel").GetComponent<MeshRenderer>().enabled = true;
			
			animate--;
		}else{
			gameObject.transform.Find("MathiusModel").GetComponent<MeshRenderer>().enabled = true;
			gameObject.GetComponent<BoxCollider>().enabled = true;
			Destroy(gameObject.GetComponent<SpawnAnimation>());
		}
		
		
		
	}
}
