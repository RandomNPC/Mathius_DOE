using System;
using UnityEngine;
using System.Collections;

public class ButtonName : EventArgs {
	
	public string name{get; private set;}
	public bool state{get; private set;}
	public float amount{get;private set;}
	
	public ButtonName(string name){
		this.name = name;
		this.state = false;
		this.amount = 0.0f;
	}
	
	public ButtonName(string name,bool state){
		this.name = name;
		this.state = state;
		this.amount = 0.0f;
	}
	
	public ButtonName(string name, float amount){
		this.name = name;
		this.state = false;
		this.amount = amount;
	}
	
}
