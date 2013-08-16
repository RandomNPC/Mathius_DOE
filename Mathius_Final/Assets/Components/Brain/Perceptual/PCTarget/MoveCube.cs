using UnityEngine;
using System.Collections;

public class MoveCube : MonoBehaviour {

	private float scale = 0.05f;
	private Vector3 delta;
	
	void Start(){
		delta = new Vector3(0.0f,0.0f,0.0f);
	}
	
	void Update(){
		
		Vector3 cpos = Camera.main.transform.position;
		Vector3 mpos = gameObject.transform.position;

		delta.z = mpos.z-cpos.z;
		delta.y = delta.z*Mathf.Tan(Camera.main.fov*Mathf.PI/360);
		delta.x = delta.y*Camera.main.aspect;

		if(mpos.y>cpos.y+delta.y){transform.Translate(0,-1*scale,0);}
		else if(mpos.y < cpos.y-delta.y){transform.Translate(0,scale,0);}

		if(mpos.x>cpos.x+delta.x){transform.Translate(-1*scale,0,0);}
		else if(mpos.x < cpos.x-delta.x){transform.Translate(scale,0,0);}
		
		if(Input.GetKey(KeyCode.W))transform.Translate(0,scale,0);
		if(Input.GetKey(KeyCode.A))transform.Translate(-1*scale,0,0);
		if(Input.GetKey(KeyCode.S))transform.Translate(0,-1*scale,0);
		if(Input.GetKey(KeyCode.D))transform.Translate(scale,0,0);
	}
	
}
