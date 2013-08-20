using System;
using UnityEngine;
using System.Collections;

public class ButtonName : EventArgs {
	
	public string name{get; private set;}
	public bool state{get; private set;}
	public ButtonName(string name, bool state){
		this.name = name;
		this.state = state;
	}
	
}
