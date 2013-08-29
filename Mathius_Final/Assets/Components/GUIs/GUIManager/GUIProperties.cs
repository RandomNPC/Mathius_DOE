using UnityEngine;
using System.Collections;

public class GUIProperties{
	
	public Rect rect{get;private set;}
	public GUIType type{get;private set;}
	public string style{get;private set;}
	public string name{get; set;}
	public bool check{get; set;}
	public float min{get; private set;}
	public float max{get; private set;}
	public float defaultVal{get; set;}
	

	public GUIProperties(string name,Rect rect,GUIType type,string style,bool check,float min,float max, float defaultVal){
		this.name = name;
		this.rect = rect;
		this.type = type;
		this.style = style;
		this.check = check;
		this.max = max;
		this.min = min;
		this.defaultVal = defaultVal;
	}
}
