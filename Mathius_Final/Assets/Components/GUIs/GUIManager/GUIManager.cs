using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Rect postion, string Text, GUIStyle Style

public enum GUIType{
	Button,
	Label
}

public class GUIManager{
	
	public event EventHandler<ButtonName> OnClick;
	private GUISkin skin;
	private Dictionary<string,GUIProperties> GUIObjects;
	
	public GUIManager(GUISkin skin){
		this.skin = skin;
		GUIObjects = new Dictionary<string, GUIProperties>();
		GUIObjects.Clear();
	}
	
	public void CreateGUIObject(string name, Rect position, GUIType type, string style){
		GUIObjects.Add(name,new GUIProperties(position,type,style));
	}
			
	public void RenderGUIObjects(GUIManager gui){
		if(gui.Equals(null))return;
		foreach(KeyValuePair<string,GUIProperties> entry in GUIObjects){
			
			switch(entry.Value.type){
				case GUIType.Button:
					if(GUI.Button(new Rect(entry.Value.rect),entry.Key,skin.GetStyle(entry.Value.style))){ 
						OnClick(this,new ButtonName(entry.Key));
					}
					break;
				case GUIType.Label:
					GUI.Label(new Rect(entry.Value.rect),entry.Key,skin.GetStyle(entry.Value.style));
					break;
				default:
					break;
			}	
		}
	}
}
