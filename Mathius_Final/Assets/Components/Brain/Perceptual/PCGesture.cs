using System;
using UnityEngine;
using System.Collections;

public enum Gesture{
	UP,
	DOWN,
	LEFT,
	RIGHT,
	OK
}

public class PCGesture : EventArgs{
	
	public Gesture gesture{get;private set;}
	
	public PCGesture(Gesture action){
		gesture = action;
	}
}
