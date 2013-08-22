using System; //Don't forget to add System!!!!!1
using UnityEngine;
using System.Collections;

//This is a sample GUI script found in the GUISample package

public class MyGUI : MonoBehaviour {

	private GUIManager gui;
	public GUISkin skin;
	
	void Start () {
		//Step 0: Create an instance of GUIManager and pass in a GUI skin to use
		gui = new GUIManager(skin);
		//Register the events this way. You are doing it correctly if Intellisense suggests an EventFunction for you. These are called subscribers
		gui.OnClick += HandleGuiOnClick;
		gui.OnToggle += HandleGuiOnToggle;
		gui.OnScroll += HandleGuiOnScroll;
		
		//Step 1. Create your guiobjects here. 
		gui.CreateGUIObject("MyToggle","Text",new Rect(20.0f,20.0f,100.0f,100.0f),GUIType.Toggle,"button",false);
		gui.CreateGUIObject("Hello","Hi",new Rect(170.0f,50.0f,200.0f,300.0f),GUIType.Label,"box");
		gui.CreateGUIObject("lolcat","",new Rect(300.0f,200.0f,100.0f,200.0f),GUIType.Button,"");
		gui.CreateGUIObject("Slider","",new Rect(480.0f,20.0f,100.0f,20.0f),GUIType.Slider,"button",false,0.0f,100.0f,5.0f);
		
		//Step 2.
		//Connect tags together based on the origin the tag up, left, right, and down from the origin. 
		//I would suggest mapping out the scroll controls to get an idea of how you should connect them using this function
		gui.connect("MyToggle","","","Slider","Hello");
		gui.connect("Hello","","","MyToggle","lolcat");
		gui.connect("lolcat","","","Hello","Slider");
		gui.connect("Slider","","","lolcat","MyToggle");
		
		//Step 3. Assign the default tag. This tag will be the first to have a gui box selection box and the selection.
		gui.pointer = "Slider";
	}

	//If you created a subscriber (event handler  on line 16 and 17) these events should pop up. 
	//name = tag of the GUIObject
	//state = Only for toggles when they change state (on/off)
	void HandleGuiOnScroll (object sender, ButtonName e)
	{
		print (e.name + " has scrolled! New value: " + e.amount);
	}
	
	void HandleGuiOnToggle (object sender, ButtonName e)
	{
		print (e.name +" got toggled! " + e.state);
	}

	void HandleGuiOnClick (object sender, ButtonName e)
	{
		print (e.name);
	}
	
	//Step 4: hook up connections to the keys/perceptual here
	void Update () {//These update keypresses are to simulate gesture movement. 
					//Replace  Input.GetKeyDown with perceptual Gesture functions here.
		if(Input.GetKeyDown(KeyCode.W)){
			gui.swipe(Direction.Up);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			gui.swipe(Direction.Left);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			gui.swipe(Direction.Down);
		}
		if(Input.GetKeyDown(KeyCode.D)){
			gui.swipe(Direction.Right);			
		}
		if(Input.GetKeyDown(KeyCode.X)){
			gui.selectOption(gui.pointer);
		}
	}
	//Step 5. Render the gui objects here with RenderGUIObjects
	void OnGUI(){//To render the gui objects, you must call RenderGUIObjects, 
				 //a method in the GUIManager and pass the current instance of GUIManager (check for null)
		gui.RenderGUIObjects(gui);
	}
}
