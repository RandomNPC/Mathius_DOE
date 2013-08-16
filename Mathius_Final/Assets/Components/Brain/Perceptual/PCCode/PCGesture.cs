using System;
using UnityEngine;
using System.Collections;

public class PCGesture : EventArgs{
	
	public Gesture gesture{get;private set;}
	
	public PCGesture(Gesture action){
		gesture = action;
	}
}
