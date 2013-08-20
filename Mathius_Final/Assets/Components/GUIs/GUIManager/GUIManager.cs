using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Rect postion, string Text, GUIStyle Style

public enum GUIType{
	Button,
	Label,
	Toggle
}

public class GUIManager{
	
	public event EventHandler<ButtonName> OnClick;
	public event EventHandler<ButtonName> OnToggle;
	private GUISkin skin;
	private Dictionary<string,GUIProperties> GUIObjects;
	
	public GUIManager(GUISkin skin){
		this.skin = skin;
		GUIObjects = new Dictionary<string, GUIProperties>();
		GUIObjects.Clear();
	}
	
	public void CreateGUIObject(string tag,string name, Rect position, GUIType type, string style, bool check=false){
		GUIObjects.Add(tag,new GUIProperties(name,position,type,style,check));
	}
			
	public void RenderGUIObjects(GUIManager gui){
		if(gui.Equals(null))return;
		foreach(KeyValuePair<string,GUIProperties> entry in GUIObjects){
			
			switch(entry.Value.type){
				case GUIType.Button:
					if(GUI.Button(new Rect(entry.Value.rect),entry.Value.name,skin.GetStyle(entry.Value.style))){ 
						OnClick(this,new ButtonName(entry.Key,entry.Value.check));
					}
					break;
				case GUIType.Label:
					GUI.Label(new Rect(entry.Value.rect),entry.Value.name,skin.GetStyle(entry.Value.style));
					break;
				case GUIType.Toggle:
				 	if(!GUI.Toggle(new Rect(entry.Value.rect),entry.Value.check,entry.Value.name,skin.GetStyle(entry.Value.style)).Equals(entry.Value.check)){
						entry.Value.check = !entry.Value.check;
						OnToggle(this,new ButtonName(entry.Key,entry.Value.check));
					}
					break;
				default:
					break;
			}	
		}
	}
}
