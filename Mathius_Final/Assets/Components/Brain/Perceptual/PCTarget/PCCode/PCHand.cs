using System;
using UnityEngine;
using System.Collections;

public class PCHand : EventArgs{
	
	public float x{get; private set;}
	public float y{get; private set;}
	
	public PCHand(float x,float y){
		this.x = x;
		this.y = y;
	}
	
}
