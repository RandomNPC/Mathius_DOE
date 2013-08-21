using UnityEngine;
using System.Collections;

public class DirectionScroller{
	
	public string left{get; private set;}
	public string right{get; private set;}
	public string up{get; private set;}
	public string down{get; private set;}
	
	public DirectionScroller(string left, string right, string up, string down){
		this.left = left;
		this.right = right;
		this.up = up;
		this.down = down;
	} 	
}
