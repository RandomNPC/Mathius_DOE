using UnityEngine;
using System.Collections;

public class AlianUI : MonoBehaviour {
	
	public float AlianX, AlianY, G1X, G1Y, G2X, G2Y, XPix, YPix;
	private GameObject G1,G2;
	
	
	void Start(){
		G1 = GameObject.FindGameObjectWithTag("ReferenceOne") as GameObject;
		G2 = GameObject.FindGameObjectWithTag("ReferenceTwo") as GameObject;		
	}
	// Update is called once per frame
	void OnGUI() {
		string EQ = gameObject.GetComponent<Alian>().equation;
		AlianX = transform.position.x;
		AlianY = transform.position.y;
		//G1X = G1.transform.position.x;
		//G1Y = G1.transform.position.y;
		//G2X = G2.transform.position.x;
		//G2Y = G2.transform.position.y;
		//XPix = Screen.width * ( (G2X-G1X) / (G2X - AlianX) );
		//YPix = Screen.height * ( (G2Y-G1Y) / (G2Y - AlianY) );
		//print("("+YPix+","+XPix+")");
		
		GUI.Box(new Rect(AlianX,AlianY,100,20),""+EQ);
	}
}
