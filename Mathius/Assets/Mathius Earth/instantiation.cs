using UnityEngine;
using System.Collections;
  
public class instantiation : MonoBehaviour {
	
	public GameObject earth;
	private bool activated;
	
void Start ()
{
	activated = false;
		
}
	void Update() 
	{
	
    }
	
	void OnTriggerEnter(Collider data){
		if(!activated){
			activated = true;
			if(data.tag == "Player"){
			
				float a,b,c;
				a = transform.position.x + 300.0f;
				b = -5.0f;
				c = 0.0f;
				Vector3 myVec = new Vector3(a,b,c);
				Instantiate(earth, myVec , transform.rotation);
			}
		}
	}
}