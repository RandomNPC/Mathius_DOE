using System;
using UnityEngine;
using System.Collections;

public class ButtonName : EventArgs {
	
	public string name{get; private set;}
	public ButtonName(string name){this.name = name;}
	
}
