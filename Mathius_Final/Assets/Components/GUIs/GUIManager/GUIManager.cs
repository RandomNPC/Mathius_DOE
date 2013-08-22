using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Rect postion, string Text, GUIStyle Style
//Slider is not implemented... yet. This describes the available GUI's renderable to this class.
public enum GUIType{
	Button,
	Label,
	Toggle
}

//Direction in which the perceptual/keyboard points to. Used for selecting guis. See line 110 for more info (swipe function)
public enum Direction{
	Left,
	Right,
	Up,
	Down
}

public class GUIManager{
	//Event handlers to handle the event functions. Important to implement!
	public event EventHandler<ButtonName> OnClick;
	public event EventHandler<ButtonName> OnToggle;
	private GUISkin skin;
	private Dictionary<string,GUIProperties> GUIObjects;
	
	private Dictionary<string,DirectionScroller> scrollmap;
	public string pointer{get; set;} //This pointer will initialize the first object selected (string) and gets a GUI box colored red.

	private Texture2D texture;
	
	//Skin is provided in the inspector of the script with this instance
	public GUIManager(GUISkin skin){
		pointer = "";
		this.skin = skin;
		GUIObjects = new Dictionary<string, GUIProperties>();
		GUIObjects.Clear();
		scrollmap = new Dictionary<string, DirectionScroller>();
		scrollmap.Clear ();
		
		//Texture for red color, to replace color with a new color use Color.XXXXX where XXXXX is the color.
		texture = new Texture2D(1,1);
		texture.SetPixel(0,0,Color.red);
		texture.Apply();
	}
	
	//Add the GUIObject to the list to render
	public void CreateGUIObject(string tag,string name, Rect position, GUIType type, string style, bool check=false){
		GUIObjects.Add(tag,new GUIProperties(name,position,type,style,check));
	}
	
	
	//!WARNING! This function can only be called in the OnGUI function of the MonoBehavior script		
	public void RenderGUIObjects(GUIManager gui){
		if(gui.Equals(null))return;// Check to see if the instance exists before proceeding to iterate.
		
		GUI.skin.box.normal.background = texture; //Setting texture of selecting to red
		showSelection(pointer,6.0f); //will render the red selection based on the tag passed in and the thickness of the line. Tested to be best at a minimum of 6.0f.
		
		foreach(KeyValuePair<string,GUIProperties> entry in GUIObjects){
			//Renders the GUI based on the enum GUIType
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
	
	
	//Selects the option that will call the event as if it was triggered by mouse. See MyGUI for implementation. 
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
	
	//Connect each gui object by tag to scroll around
	public void connect(string parent, string left, string right, string up, string down){
		scrollmap.Add(parent,new DirectionScroller(left,right,up,down));
	}
	
	//Swiping via keyboard or perceptual, See the demo MyGUI for implementation.
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
	
	
	//Renders the GUI selection. This method is not to be exposed to outside classes.
	private void showSelection(string tag, float delta){
		Rect pos = GUIObjects[tag].rect;
		GUI.Box(new Rect(pos.xMin-delta,pos.yMin-delta,pos.width+2*delta,delta),GUIContent.none); //1
		GUI.Box(new Rect(pos.xMin-delta,pos.yMax,pos.width+2*delta,delta),GUIContent.none);//2
		GUI.Box(new Rect(pos.xMin-delta,pos.yMin-delta,delta,pos.height+2*delta),GUIContent.none); //3	
		GUI.Box(new Rect(pos.xMax,pos.yMin-delta,delta,pos.height+2*delta),GUIContent.none);//4
	}
}
