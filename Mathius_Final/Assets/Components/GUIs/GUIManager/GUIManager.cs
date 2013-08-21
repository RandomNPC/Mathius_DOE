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

public enum Direction{
	Left,
	Right,
	Up,
	Down
}

public class GUIManager{
	
	public event EventHandler<ButtonName> OnClick;
	public event EventHandler<ButtonName> OnToggle;
	private GUISkin skin;
	private Dictionary<string,GUIProperties> GUIObjects;
	
	private Dictionary<string,DirectionScroller> scrollmap;
	public string pointer{get; set;}

	private Texture2D texture;
	
	public GUIManager(GUISkin skin){
		pointer = "";
		this.skin = skin;
		GUIObjects = new Dictionary<string, GUIProperties>();
		GUIObjects.Clear();
		scrollmap = new Dictionary<string, DirectionScroller>();
		scrollmap.Clear ();
		
		texture = new Texture2D(1,1);
		texture.SetPixel(0,0,Color.red);
		texture.Apply();
	}
	
	public void CreateGUIObject(string tag,string name, Rect position, GUIType type, string style, bool check=false){
		GUIObjects.Add(tag,new GUIProperties(name,position,type,style,check));
	}
			
	public void RenderGUIObjects(GUIManager gui){
		if(gui.Equals(null))return;
		
		GUI.skin.box.normal.background = texture;
		showSelection(pointer,6.0f);
		
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
	
	public void selectOption(string tag){
		switch(GUIObjects[tag].type){
			case GUIType.Button:
				OnClick(this,new ButtonName(tag,false));
				break;
			case GUIType.Toggle:
				GUIObjects[tag].check = !GUIObjects[tag].check;
				OnToggle(this,new ButtonName(tag,GUIObjects[tag].check));
				break;
			case GUIType.Label:
				//its a label... it doesnt do anything	
				break;
			default:
				break;
		}
	}
	
	public void connect(string parent, string left, string right, string up, string down){
		scrollmap.Add(parent,new DirectionScroller(left,right,up,down));
	}
	
	public void swipe(Direction position){
		switch(position){
			case Direction.Left:
				if(!scrollmap[pointer].left.Equals("")) pointer = scrollmap[pointer].left; 
				break;
			case Direction.Right:
				if(!scrollmap[pointer].right.Equals("")) pointer = scrollmap[pointer].right;
				break;
			case Direction.Up:
				if(!scrollmap[pointer].up.Equals("")) pointer = scrollmap[pointer].up;
				break;
			case Direction.Down:
				if(!scrollmap[pointer].down.Equals("")) pointer = scrollmap[pointer].down;
				break;
			default:
				break;
		}
	}
	
	private void showSelection(string tag, float delta){
		Rect pos = GUIObjects[tag].rect;
		GUI.Box(new Rect(pos.xMin-delta,pos.yMin-delta,pos.width+2*delta,delta),GUIContent.none); //1
		GUI.Box(new Rect(pos.xMin-delta,pos.yMax,pos.width+2*delta,delta),GUIContent.none);//2
		GUI.Box(new Rect(pos.xMin-delta,pos.yMin-delta,delta,pos.height+2*delta),GUIContent.none); //3	
		GUI.Box(new Rect(pos.xMax,pos.yMin-delta,delta,pos.height+2*delta),GUIContent.none);//4
	}
}
