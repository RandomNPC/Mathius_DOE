using UnityEngine;
using System.Collections;

public class GUIProperties{
	
	public Rect rect{get;private set;}
	public GUIType type{get;private set;}
	public string style{get;private set;}
	public string name{get;private set;}
	public bool check{get; set;}
	
	public GUIProperties(string name,Rect rect, GUIType type,string style, bool check){
		this.name = name;
		this.rect = rect;
		this.type = type;
		this.style = style;
		this.check = check;
	}
	
	
}
