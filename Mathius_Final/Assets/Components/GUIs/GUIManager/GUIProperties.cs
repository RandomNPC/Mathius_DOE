using UnityEngine;
using System.Collections;

public class GUIProperties{
	
	public Rect rect{get;private set;}
	public GUIType type{get;private set;}
	public string style{get;private set;}
	
	public GUIProperties(Rect rect, GUIType type,string style){
		this.rect = rect;
		this.type = type;
		this.style = style;
	}
	
	
}
