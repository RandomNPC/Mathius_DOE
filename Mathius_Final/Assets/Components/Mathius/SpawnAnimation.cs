using UnityEngine;
using System.Collections;

public class SpawnAnimation : MonoBehaviour {
	
	private float animate;
	private Mathius m;
	// Use this for initialization
	void Start () {
		animate = 60.0f;
		m = MasterController.BRAIN.m();
		//gameObject.GetComponent<BoxCollider>().enabled = false;
		m.set_invisible(true);
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
			//gameObject.GetComponent<BoxCollider>().enabled = true;
			m.set_invisible(false);
			//invisible = false;
			Destroy(gameObject.GetComponent<SpawnAnimation>());
		}
	}
}
