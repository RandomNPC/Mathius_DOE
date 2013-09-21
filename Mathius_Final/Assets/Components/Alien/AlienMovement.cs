using UnityEngine;
using System.Collections;

public class AlienMovement : MonoBehaviour {
	
	private int state;
	private int oscilate;
	private int oscPlace;
	private int tracker;
	private float scale;
	
	// Use this for initialization
	void Start (){
		state = Random.Range(1,3);
		oscilate = 10;
		oscPlace = 0;
		tracker = 0;
		scale = 0;
	}

	// Update is called once per frame
	void Update () {
		scale = MasterController.BRAIN.pm ().get_alienSpeed();
		switch(state){
			case 1: //sine wave
				transform.Translate(-scale, 0, 0);
				if (oscPlace < oscilate)
				{
					transform.Translate(0, 1, 0);
					oscPlace++;
					if (oscPlace > oscilate) oscilate *= -1; 
				}
				else
				{
					transform.Translate(0, -1, 0);
					oscPlace--;
					if (oscPlace < oscilate) oscilate *= -1; 
				}
				break;
			case 2: // straight line
				transform.Translate(-scale,0,0);
				break;
			case 3://random stepping
				{ // step up
					transform.Translate(-scale,0,0);
					switch(Random.Range(1,3)){
						case 1: //up
							if(tracker<1){
								transform.Translate(0,1,0);
								tracker++;
							}
							break;
						case 2: //down
						case 3:
							if(tracker>-1){
								transform.Translate(0,-1,0);
								tracker--;
							}								
							break;
						default:
							break;
					}
				break;
				}
			default:
				break;
		}	
	}
	
	void OnCollisionEnter (Collision obj){
		state = Random.Range(1,3);
	}
}
