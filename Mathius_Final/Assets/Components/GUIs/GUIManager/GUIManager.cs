using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Rect postion, string Text, GUIStyle Style

public enum GUIType{
	Button,
	Label,
	Toggle,
	Slider
}

//Direction in which the perceptual/keyboard points to. Used for selecting guis. See line 110 for more info (swipe function)
public enum Direction{
	Left,
	Right,
	Up,
	Down
}

public class GUIManager{
	//Event handlers to handle the event functions. Important to implement! You will get a null ref exception if you dont
	public event EventHandler<ButtonName> OnClick; //Implement if there is a button
	public event EventHandler<ButtonName> OnToggle; //Implement if there is a toggle
	public event EventHandler<ButtonName> OnScroll; //Implement if there is a slider
	private GUISkin skin;
	private Dictionary<string,GUIProperties> GUIObjects; //Mapping each GUI Object via key value pair system
	private Dictionary<string,DirectionScroller> scrollmap; //Mapping each scroll with gestures via key/value pair
	public string pointer{get; set;} //pointer to the current tag. This is used for the red focus box. Required to set after adding all of the GUIObjects.

	private Texture2D texture;
	
	public GUIManager(GUISkin skin){
		pointer = "";
		this.skin = skin;
		GUIObjects = new Dictionary<string, GUIProperties>();
		GUIObjects.Clear();
		scrollmap = new Dictionary<string, DirectionScroller>();
		scrollmap.Clear ();
		
		texture = new Texture2D(1,1); //texture set to red here.
		texture.SetPixel(0,0,Color.red);
		texture.Apply();
	}
	
	
	//Parameters:
	//tag: Identify each GUI object individually. Any GUIObject data that share the same tag will be overidden by the second
	//name: GUI Object that has a name will be labeled
	//position: Rectangular coordinates in order to draw out the GUI Object
	//type: The type of GUI to render see lines 9-12 of this code of the available options
	//style: using GUI.skin.getStyle(style). Provide the string of the style and if exists, will skin the GUI Object
	//check: default setting of the checkbox when the UI interface gets opened. This parameter only applies to toggles.
	//min: minimum value for slider
	//max: maximum value for slider
	//defaultVal: default value for the slider at the start
	public void CreateGUIObject(string tag,string name, Rect position, GUIType type, string style, bool check=false, float min=0.0f, float max=0.0f, float defaultVal=0.0f){
		GUIObjects.Add(tag,new GUIProperties(name,position,type,style,check,min,max,defaultVal));
	}
			
	public void RenderGUIObjects(GUIManager gui){
		if(gui.Equals(null))return;//I require checking the instance to avoid null ref exception errors
		
		GUI.skin.box.normal.background = texture; //applying red texture here for box
		showSelection(pointer,6.0f);//Displays a red box over the current selection
		
		
		//Go thru each GUIObject to render and see if they have been pressed, where appropriate
		foreach(KeyValuePair<string,GUIProperties> entry in GUIObjects){
			
			switch(entry.Value.type){
				case GUIType.Button:
					if(GUI.Button(new Rect(entry.Value.rect),entry.Value.name,skin.GetStyle(entry.Value.style))){ 
						OnClick(this,new ButtonName(entry.Key));
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
				case GUIType.Slider:
					entry.Value.defaultVal = GUI.HorizontalSlider(new Rect(entry.Value.rect),entry.Value.defaultVal,entry.Value.min,entry.Value.max);
					if(GUI.changed) OnScroll(this,new ButtonName(entry.Key,entry.Value.defaultVal));
					break;
				default:
					break;
			}	
		}
	}
	
	//Selects the option, simulating a button click. This is required for perceptual coding
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
			case GUIType.Slider:
				break;
			default:
				break;
		}
	}
	
	
	// Map out each of the options
	//------------up-------
	//----left<-parent->right
	//-----------down------
	//If there is no mapping to any direction, the focus will stay at the parent
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
	
	//This renders the red GUI box around selected GUI Objects
	private void showSelection(string tag, float delta){
		Rect pos = GUIObjects[tag].rect;
		GUI.Box(new Rect(pos.xMin-delta,pos.yMin-delta,pos.width+2*delta,delta),GUIContent.none); //1
		GUI.Box(new Rect(pos.xMin-delta,pos.yMax,pos.width+2*delta,delta),GUIContent.none);//2
		GUI.Box(new Rect(pos.xMin-delta,pos.yMin-delta,delta,pos.height+2*delta),GUIContent.none); //3	
		GUI.Box(new Rect(pos.xMax,pos.yMin-delta,delta,pos.height+2*delta),GUIContent.none);//4
	}
	
	public void SetGUISliderProperty(string tag,float val){
		GUIProperties prop = GUIObjects[tag];	
		prop.defaultVal += val;
		if(prop.defaultVal> prop.max || prop.defaultVal < prop.min){ 
			if(prop.defaultVal > prop.max) prop.defaultVal = prop.max;
			else if(prop.defaultVal < prop.min) prop.defaultVal = prop.min;
		}
		OnScroll(this,new ButtonName(tag,prop.defaultVal));
	}
	
	public void SetGUINameProperty(string tag, string val){
		GUIProperties prop = GUIObjects[tag];
		prop.name = val;
	}
}
