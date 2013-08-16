using System;
using UnityEngine;
using System.Collections;

public class PCVoice : EventArgs{
	
	public int number{get; private set;}
	
	public PCVoice(int number){
		this.number = number;
	}
	
	
}
