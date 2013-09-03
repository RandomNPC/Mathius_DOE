using UnityEngine;
using System.Collections;

public class TargetMarcoPolo : MonoBehaviour {
	
	public float scale = 0.0001f;
	
	void Start () {
		
	}
	
	void Update () {
		Vector3 marco = gameObject.transform.position;
		GameObject near = gameObject.GetComponent<NearestEnemy>().get_nearest_enemy();
		if(!near)return;
		Vector3 polo = near.transform.position;
	
		Vector3 delta = marco - polo;
		if(delta.magnitude > 250.0f) return; //Do action within 250 units of the nearest enemy
		
		if(delta.y > -1.0f && delta.y < 1.0f){
			//do nothing
		}
		else{ //addjust transformation
			if(delta.y.CompareTo(0.0f)<=-1.0f) transform.Translate(0.0f,1*scale,0.0f);
			else if(delta.y.CompareTo(0.0f)>=1.0f) transform.Translate(0.0f,-1*scale,0.0f);
			else transform.position.Set(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
		}
		
		if(delta.magnitude < 100.0f){ //Fire within 100 units of the nearest enemy
			if(GameObject.Find("Bullet")) return;
			GameObject.Find("Gun").GetComponent<Gun>().fireNum(near.GetComponent<AlienManager>().answer);
		}
	}
}
