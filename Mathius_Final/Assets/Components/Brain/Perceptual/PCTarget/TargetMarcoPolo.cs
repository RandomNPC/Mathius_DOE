using UnityEngine;
using System.Collections;

public class TargetMarcoPolo : MonoBehaviour {
	
	public float scale = 1.0f;
	
	void Start () {
		
	}
	
	void Update () {
		Vector3 marco = gameObject.transform.position;
		Vector3 polo = Target.TARGET;
		
		Vector3 delta = marco - polo;
		
		if(delta.x.CompareTo(0.0f)>0.0f) transform.Translate(-1*scale,0.0f,0.0f);
		else if(delta.x.CompareTo(0.0f)<0.0f) transform.Translate(1*scale,0.0f,0.0f);
		else transform.Translate(0.0f,0.0f,0.0f);
		if(delta.y.CompareTo(0.0f)<0.0f) transform.Translate(0.0f,1*scale,0.0f);
		else if(delta.y.CompareTo(0.0f)>0.0f) transform.Translate(0.0f,-1*scale,0.0f);
		else transform.Translate(0.0f,0.0f,0.0f);
	}
}
